// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;

namespace nexus.core.serialization
{
   /// <summary>
   /// See <see cref="ObjectSerializer.Create{T}" />
   /// </summary>
   public interface IObjectSerializer<out TTo>
   {
      Boolean CanSerializeObjectOfType( Type source );

      TTo Serialize( Object source );
   }

   /// <summary>
   /// Serializer without type information for use in collections or other places a generic interface can cause problems
   /// <see cref="ObjectSerializer.AsUntyped{TTo}" />
   /// </summary>
   public interface IObjectSerializer
   {
      Boolean CanSerializeObjectOfType( Type source );

      Object Serialize( Object source );
   }

   public static class ObjectSerializer
   {
      /// <summary>
      /// Return an <see cref="IObjectSerializer" /> is not a generic interface and can be used in collections or other places a
      /// generic interface causes problems.
      /// </summary>
      public static IObjectSerializer AsUntyped<T>( this IObjectSerializer<T> serializer )
      {
         Contract.Requires<ArgumentNullException>( serializer != null );
         return new UntypedObjectSerializer<T>( serializer );
      }

      /// <summary>
      /// Create a new <see cref="IObjectSerializer{TTo}" /> from the provided serialization function.
      /// </summary>
      public static IObjectSerializer<TTo> Create<TTo>( Func<Object, TTo> serialize, Func<Type, Boolean> canSerialize )
      {
         Contract.Requires<ArgumentNullException>( serialize != null );
         Contract.Requires<ArgumentNullException>( canSerialize != null );
         return new TypedObjectSerializer<TTo>( serialize, canSerialize );
      }

      private sealed class TypedObjectSerializer<T> : IObjectSerializer<T>
      {
         private readonly Func<Type, Boolean> m_canSerialize;
         private readonly Func<Object, T> m_serialize;

         public TypedObjectSerializer( Func<Object, T> serialize, Func<Type, Boolean> canSerialize )
         {
            m_serialize = serialize;
            m_canSerialize = canSerialize;
         }

         public Boolean CanSerializeObjectOfType( Type source )
         {
            return m_canSerialize( source );
         }

         public T Serialize( Object source )
         {
            return m_serialize( source );
         }
      }

      private sealed class UntypedObjectSerializer<T> : IObjectSerializer
      {
         private readonly IObjectSerializer<T> m_serializer;

         public UntypedObjectSerializer( IObjectSerializer<T> serializer )
         {
            m_serializer = serializer;
         }

         public Boolean CanSerializeObjectOfType( Type source )
         {
            return m_serializer.CanSerializeObjectOfType( source );
         }

         public Object Serialize( Object source )
         {
            return m_serializer.Serialize( source );
         }
      }
   }
}