// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.core.serialization
{
   public interface IDeserializer<in TFrom, out TTo>
   {
      TTo Deserialize( TFrom data );
   }

   public delegate TTo Deserializer<in TFrom, out TTo>( TFrom data );

   public static class DeserializerExtensions
   {
      public static IDeserializer<TFrom, TTo> Wrap<TFrom, TTo>( this Deserializer<TFrom, TTo> function )
      {
         return new DeserializerWrapper<TFrom, TTo>( function );
      }

      private sealed class DeserializerWrapper<A, B> : IDeserializer<A, B>
      {
         private readonly Deserializer<A, B> m_func;

         public DeserializerWrapper( Deserializer<A, B> func )
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