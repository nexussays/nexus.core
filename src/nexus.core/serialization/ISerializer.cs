// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.serialization
{
   public delegate TTo Serializer<in TFrom, out TTo>( TFrom source );

   public interface ISerializer<in TFrom, out TTo>
   {
      TTo Serialize( TFrom source );
   }

   public interface IGenericSerializer
   {
      Type From { get; }

      Type To { get; }

      Object Serialize( Object source );
   }

   public static class SerializerExtensions
   {
      public static IGenericSerializer AsGeneric<TFrom, TTo>( this ISerializer<TFrom, TTo> serializer )
      {
         return new GenericSerializer<TFrom, TTo>( serializer );
      }

      public static ISerializer<TFrom, TTo> Wrap<TFrom, TTo>( this Serializer<TFrom, TTo> function )
      {
         if(function == null)
         {
            throw new ArgumentNullException( nameof( function ) );
         }
         return new SerializerWrapper<TFrom, TTo>( function );
      }

      private sealed class GenericSerializer<TFrom, TTo>
         : IGenericSerializer,
           ISerializer<TFrom, TTo>
      {
         private readonly ISerializer<TFrom, TTo> m_serializer;

         public GenericSerializer( ISerializer<TFrom, TTo> serializer )
         {
            m_serializer = serializer;
         }

         public Type From { get; } = typeof(TFrom);

         public Type To { get; } = typeof(TTo);

         public Object Serialize( Object source )
         {
            if(source == null)
            {
               return null;
            }
            if(source is TFrom)
            {
               return Serialize( (TFrom)source );
            }
            throw new ArgumentException(
               "Serializer {0} cannot serialize objects of type {1}".F(
                  m_serializer.GetType().FullName,
                  source.GetType().FullName ) );
         }

         public TTo Serialize( TFrom source )
         {
            return m_serializer.Serialize( source );
         }
      }

      private sealed class SerializerWrapper<A, B> : ISerializer<A, B>
      {
         private readonly Serializer<A, B> m_function;

         public SerializerWrapper( Serializer<A, B> function )
         {
            m_function = function;
         }

         public B Serialize( A source )
         {
            return m_function( source );
         }
      }
   }
}