using System;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace nexus.core
{
   public static class ObjectConverter
   {
      /// <summary>
      /// Return an <see cref="IObjectConverter" /> is not a generic interface and can be used in collections or other places a
      /// generic interface causes problems.
      /// </summary>
      public static IObjectConverter AsUntyped<TFrom, TTo>( this IObjectConverter<TFrom, TTo> converter )
      {
         Contract.Requires<ArgumentNullException>( converter != null );
         return new WrappedObjectConverter<TFrom, TTo>( converter );
      }

      /// <summary>
      /// Create a new <see cref="IObjectConverter{TFrom,TTo}" /> from the provided conversion function.
      /// </summary>
      public static IObjectConverter<TFrom, TTo> Create<TFrom, TTo>( Func<TFrom, TTo> converter )
      {
         Contract.Requires<ArgumentNullException>( converter != null );
         return new TypedObjectConverter<TFrom, TTo>( converter );
      }

      /// <summary>
      /// Create a new <see cref="IObjectConverter" /> from the provided conversion function.
      /// </summary>
      public static IObjectConverter Create( Func<Object, Object> converter, Func<Type, Boolean> canConvert )
      {
         Contract.Requires<ArgumentNullException>( converter != null );
         Contract.Requires<ArgumentNullException>( canConvert != null );
         return new UntypedObjectConverter( converter, canConvert );
      }

      private sealed class TypedObjectConverter<TFrom, TTo> : IObjectConverter<TFrom, TTo>
      {
         private readonly Func<TFrom, TTo> m_converter;

         public TypedObjectConverter( Func<TFrom, TTo> converter )
         {
            Contract.Requires( converter != null );
            m_converter = converter;
         }

         public TTo Convert( TFrom source )
         {
            return m_converter( source );
         }
      }

      private sealed class UntypedObjectConverter : IObjectConverter
      {
         private readonly Func<Type, Boolean> m_canConvert;
         private readonly Func<Object, Object> m_converter;

         public UntypedObjectConverter( Func<Object, Object> converter, Func<Type, Boolean> canConvert )
         {
            Contract.Requires( converter != null );
            Contract.Requires( canConvert != null );
            m_converter = converter;
            m_canConvert = canConvert;
         }

         public Boolean CanConvertObjectOfType( Type source )
         {
            return m_canConvert( source );
         }

         public Object Convert( Object source )
         {
            return m_converter( source );
         }
      }

      private sealed class WrappedObjectConverter<TFrom, TTo> : IObjectConverter
      {
         private readonly IObjectConverter<TFrom, TTo> m_converter;

         public WrappedObjectConverter( IObjectConverter<TFrom, TTo> converter )
         {
            Contract.Requires( converter != null );
            m_converter = converter;
         }

         public Boolean CanConvertObjectOfType( Type source )
         {
            return source != null && typeof(TFrom).GetTypeInfo().IsAssignableFrom( source.GetTypeInfo() );
         }

         public Object Convert( Object source )
         {
            if(CanConvertObjectOfType( source?.GetType() ))
            {
               return m_converter.Convert( (TFrom)source );
            }
            throw new ArgumentException(
               "{0} cannot convert objects of type {1}".F(
                  m_converter.GetType().FullName,
                  source?.GetType().FullName ?? "null" ) );
         }
      }
   }
}