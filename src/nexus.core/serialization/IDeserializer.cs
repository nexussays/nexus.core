// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;

namespace nexus.core.serialization
{
   public interface IDeserializer<in TFrom, out TTo>
   {
      TTo Deserialize( TFrom data );
   }

   public static class Deserializer
   {
      /// <summary>
      /// Create a new <see cref="IDeserializer{TFrom,TTo}" /> from the provided function
      /// </summary>
      public static IDeserializer<TFrom, TTo> Create<TFrom, TTo>( this Func<TFrom, TTo> function )
      {
         Contract.Requires<ArgumentNullException>( function != null );
         return new DeserializerWrapper<TFrom, TTo>( function );
      }

      private sealed class DeserializerWrapper<A, B> : IDeserializer<A, B>
      {
         private readonly Func<A, B> m_func;

         public DeserializerWrapper( Func<A, B> func )
         {
            m_func = func;
         }

         public B Deserialize( A data )
         {
            return m_func.Invoke( data );
         }
      }
   }
}