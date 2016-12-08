// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;

namespace nexus.core.serialization
{
   public interface ISerializer<in TFrom, out TTo>
   {
      TTo Serialize( TFrom source );
   }

   public static class Serializer
   {
      /// <summary>
      /// Return an <see cref="IUntypedSerializer" /> which retains the type information but is not a generic interface and can
      /// be used in collections or other places a generic interface causes problems
      /// </summary>
      public static IUntypedSerializer AsUntyped<TFrom, TTo>( this ISerializer<TFrom, TTo> serializer )
      {
         return new UntypedSerializer<TFrom, TTo>( serializer );
      }

      /// <summary>
      /// Create a new <see cref="ISerializer{TFrom,TTo}" /> from the provided function
      /// </summary>
      public static ISerializer<TFrom, TTo> Create<TFrom, TTo>( Func<TFrom, TTo> function )
      {
         Contract.Requires<ArgumentNullException>( function != null );
         return new SerializerWrapper<TFrom, TTo>( function );
      }

      private sealed class SerializerWrapper<A, B> : ISerializer<A, B>
      {
         private readonly Func<A, B> m_function;

         public SerializerWrapper( Func<A, B> function )
         {
            m_function = function;
         }

         public B Serialize( A source )
         {
            return m_function( source );
         }
      }

      private sealed class UntypedSerializer<TFrom, TTo>
         : IUntypedSerializer,
           ISerializer<TFrom, TTo>
      {
         private readonly ISerializer<TFrom, TTo> m_serializer;

         public UntypedSerializer( ISerializer<TFrom, TTo> serializer )
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
   }
}