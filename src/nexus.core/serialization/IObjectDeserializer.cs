// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace nexus.core.serialization
{
   /// <summary>
   /// Deserialize from some source object to any type. See <see cref="ObjectDeserializer.Create{TFrom}" />
   /// </summary>
   public interface IObjectDeserializer<in TFrom>
   {
      T Deserialize<T>( TFrom source );

      /// <summary>
      /// Non-generic version of <see cref="Deserialize{T}" />
      /// </summary>
      Object Deserialize( TFrom source, Type desiredReturnType );
   }

   /// <summary>
   /// Deserialize from some source object to any type.
   /// </summary>
   public interface IObjectDeserializer
   {
      Boolean CanDeserializeObjectOfType( Type source );

      Object Deserialize( Object source, Type desiredReturnType );
   }

   public static class ObjectDeserializer
   {
      /// <summary>
      /// Return an <see cref="IObjectDeserializer" /> which is not a generic interface and can be used in collections or other
      /// places a generic interface causes problems.
      /// </summary>
      public static IObjectDeserializer AsUntyped<T>( this IObjectDeserializer<T> deserializer )
      {
         Contract.Requires<ArgumentNullException>( deserializer != null );
         return new UntypedObjectDeserializer<T>( deserializer );
      }

      /// <summary>
      /// Create a new <see cref="IObjectDeserializer{TFrom}" /> from the provided function.
      /// </summary>
      public static IObjectDeserializer<TFrom> Create<TFrom>( this Func<TFrom, Type, Object> deserialize )
      {
         Contract.Requires<ArgumentNullException>( deserialize != null );
         return new TypedObjectDeserializer<TFrom>( deserialize );
      }

      private sealed class TypedObjectDeserializer<TFrom> : IObjectDeserializer<TFrom>
      {
         private readonly Func<TFrom, Type, Object> m_func;

         public TypedObjectDeserializer( Func<TFrom, Type, Object> func )
         {
            m_func = func;
         }

         public T Deserialize<T>( TFrom source )
         {
            return (T)Deserialize( source, typeof(T) );
         }

         public Object Deserialize( TFrom source, Type desiredReturnType )
         {
            return m_func.Invoke( source, desiredReturnType );
         }
      }

      private sealed class UntypedObjectDeserializer<T> : IObjectDeserializer
      {
         private readonly IObjectDeserializer<T> m_serializer;

         public UntypedObjectDeserializer( IObjectDeserializer<T> serializer )
         {
            m_serializer = serializer;
         }

         public Boolean CanDeserializeObjectOfType( Type source )
         {
            return source == typeof(T) || source.GetTypeInfo().IsSubclassOf( typeof(T) );
         }

         public Object Deserialize( Object source, Type desiredReturnType )
         {
            return m_serializer.Deserialize( (T)source, desiredReturnType );
         }
      }
   }
}