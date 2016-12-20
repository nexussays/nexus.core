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
   /// Deserialize from some source object to a call-time-specified type. See <see cref="Deserializer.Create{TFrom}" />
   /// </summary>
   public interface IDeserializer<in TFrom>
   {
      T Deserialize<T>( TFrom source );

      /// <summary>
      /// Non-generic version of <see cref="Deserialize{T}" />
      /// </summary>
      Object Deserialize( TFrom source, Type desiredReturnType );
   }

   public static class Deserializer
   {
      /// <summary>
      /// Create a new <see cref="IDeserializer{TFrom}" /> from the provided function. If you simple want to convert one
      /// object to another, see <see cref="ObjectConverter.Create{TFrom,TTo}" />
      /// </summary>
      public static IDeserializer<TFrom> Create<TFrom>( this Func<TFrom, Type, Object> deserialize )
      {
         Contract.Requires<ArgumentNullException>( deserialize != null );
         return new GenericDeserializer<TFrom>( deserialize );
      }

      private sealed class GenericDeserializer<TFrom> : IDeserializer<TFrom>
      {
         private readonly Func<TFrom, Type, Object> m_deserialize;

         public GenericDeserializer( Func<TFrom, Type, Object> deserialize )
         {
            Contract.Requires( deserialize != null );
            m_deserialize = deserialize;
         }

         public T Deserialize<T>( TFrom source )
         {
            Contract.Ensures( Contract.Result<Object>() != null );
            return (T)Deserialize( source, typeof(T) );
         }

         public Object Deserialize( TFrom source, Type desiredReturnType )
         {
            return m_deserialize.Invoke( source, desiredReturnType );
         }
      }
   }
}