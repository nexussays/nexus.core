// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using nexus.core.resharper;

namespace nexus.core
{
   /// <summary>
   /// Utility methods to create and convert <see cref="IDeserializer{TSource}" />
   /// </summary>
   public static class Deserializer
   {
      /// <summary>
      /// Convert a generic <see cref="IDeserializer{TSource}" /> to a strongly typed
      /// <see cref="IObjectConverter{TSource,TResult}" />
      /// that can only deserialize to one specific type (<typeparamref name="TResult" />).
      /// </summary>
      public static IObjectConverter<TSource, TResult> AsConverter
         <TSource, TResult>( [NotNull] this IDeserializer<TSource> deserializer )
      {
         return new DeserializerToObjectConverterWrapper<TSource, TResult>( deserializer );
      }

      /// <summary>
      /// Create a new <see cref="IDeserializer{TSource}" /> from the provided function. If you simple want to convert one
      /// object to another, see <see cref="ObjectConverter.Create{TSource,TResult}" />
      /// </summary>
      public static IDeserializer<TSource> Create<TSource>( [NotNull] this Func<TSource, Type, Object> deserialize )
      {
         if(deserialize == null)
         {
            throw new ArgumentNullException( nameof(deserialize) );
         }
         return new GenericDeserializer<TSource>( deserialize );
      }

      /// <inheritdoc />
      private sealed class DeserializerToObjectConverterWrapper<TSource, TResult> : IObjectConverter<TSource, TResult>
      {
         private readonly IDeserializer<TSource> m_deserializer;

         public DeserializerToObjectConverterWrapper( IDeserializer<TSource> deserializer )
         {
            m_deserializer = deserializer;
         }

         /// <inheritdoc />
         public TResult Convert( TSource source )
         {
            return m_deserializer.Deserialize<TResult>( source );
         }
      }

      /// <inheritdoc />
      private sealed class GenericDeserializer<TSource> : IDeserializer<TSource>
      {
         private readonly Func<TSource, Type, Object> m_deserialize;

         public GenericDeserializer( [NotNull] Func<TSource, Type, Object> deserialize )
         {
            Contract.Requires( deserialize != null );
            m_deserialize = deserialize;
         }

         /// <inheritdoc />
         public T Deserialize<T>( TSource source )
         {
            return (T)Deserialize( source, typeof(T) );
         }

         /// <inheritdoc />
         public Object Deserialize( TSource source, Type desiredReturnType )
         {
            return m_deserialize.Invoke( source, desiredReturnType );
         }
      }
   }
}
