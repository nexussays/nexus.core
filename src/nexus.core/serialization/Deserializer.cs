using System;
using System.Diagnostics.Contracts;
using nexus.core.resharper;

namespace nexus.core.serialization
{
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

         public GenericDeserializer( [NotNull] Func<TFrom, Type, Object> deserialize )
         {
            Contract.Requires( deserialize != null );
            m_deserialize = deserialize;
         }

         [NotNull]
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