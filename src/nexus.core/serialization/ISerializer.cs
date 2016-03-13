// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.core.serialization
{
   public interface ISerializer<in TFrom, out TTo>
   {
       TTo Serialize( TFrom source );
   }

   public delegate TTo Serializer<in TFrom, out TTo>( TFrom source );

   public static class SerializerExtensions
   {
      public static ISerializer<TFrom, TTo> Wrap<TFrom, TTo>( this Serializer<TFrom, TTo> function )
      {
         return new SerializerWrapper<TFrom, TTo>( function );
      }

      private sealed class SerializerWrapper<A, B> : ISerializer<A, B>
      {
         private readonly Serializer<A, B> m_func;

         public SerializerWrapper( Serializer<A, B> func )
         {
            m_func = func;
         }

         public B Serialize( A source )
         {
            return m_func( source );
         }
      }
   }
}