using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using nexus.core.resharper;

namespace nexus.core
{
   /// <summary>
   /// Create <see cref="IObjectConverter{TSource,TResult}" />
   /// </summary>
   public static class ObjectConverter
   {
      /// <summary>
      /// Convert a generic <see cref="IGenericObjectConverter{TResult}" /> to a strongly typed
      /// <see cref="IObjectConverter{TSource,TResult}" />
      /// that can only convert from one specific type (<typeparamref name="TSource" />).
      /// </summary>
      public static IObjectConverter<TSource, TResult> AsTyped
         <TSource, TResult>( [NotNull] this IGenericObjectConverter<TResult> converter )
      {
         Contract.Requires( converter != null );
         return new GenericToTypedObjectConverterWrapper<TSource, TResult>( converter );
      }

      /// <summary>
      /// Return an <see cref="IObjectConverter" /> is not a generic interface and can be used in collections or other places a
      /// generic interface causes problems.
      /// </summary>
      public static IObjectConverter AsUntyped
         <TSource, TResult>( [NotNull] this IObjectConverter<TSource, TResult> converter )
      {
         Contract.Requires( converter != null );
         Contract.Requires<ArgumentNullException>( converter != null );
         return new TypedToUntypedObjectConverterWrapper<TSource, TResult>( converter );
      }

      /// <summary>
      /// Create a new <see cref="IObjectConverter{TSource,TResult}" /> from the provided conversion function.
      /// </summary>
      public static IObjectConverter<TSource, TResult> Create
         <TSource, TResult>( [NotNull] Func<TSource, TResult> converter )
      {
         Contract.Requires( converter != null );
         Contract.Requires<ArgumentNullException>( converter != null );
         return new TypedObjectConverter<TSource, TResult>( converter );
      }

      /// <summary>
      /// Create a new <see cref="IObjectConverter" /> from the provided conversion function.
      /// </summary>
      public static IObjectConverter Create( [NotNull] Func<Object, Object> converter,
                                             [NotNull] Func<Type, Boolean> canConvert )
      {
         Contract.Requires( converter != null );
         Contract.Requires( canConvert != null );
         Contract.Requires<ArgumentNullException>( converter != null );
         Contract.Requires<ArgumentNullException>( canConvert != null );
         return new UntypedObjectConverter( converter, canConvert );
      }

      /// <inheritdoc />
      private sealed class GenericToTypedObjectConverterWrapper<TSource, TResult> : IObjectConverter<TSource, TResult>
      {
         private readonly IGenericObjectConverter<TResult> m_generic;

         public GenericToTypedObjectConverterWrapper( [NotNull] IGenericObjectConverter<TResult> generic )
         {
            m_generic = generic;
         }

         /// <inheritdoc />
         public TResult Convert( TSource source )
         {
            return m_generic.Convert( source );
         }
      }

      private sealed class TypedObjectConverter<TSource, TResult> : IObjectConverter<TSource, TResult>
      {
         private readonly Func<TSource, TResult> m_converter;

         public TypedObjectConverter( [NotNull] Func<TSource, TResult> converter )
         {
            Contract.Requires( converter != null );
            m_converter = converter;
         }

         public TResult Convert( TSource source )
         {
            return m_converter( source );
         }
      }

      /// <inheritdoc />
      private sealed class TypedToUntypedObjectConverterWrapper<TSource, TResult> : IObjectConverter
      {
         private readonly IObjectConverter<TSource, TResult> m_converter;

         public TypedToUntypedObjectConverterWrapper( [NotNull] IObjectConverter<TSource, TResult> converter )
         {
            Contract.Requires( converter != null );
            m_converter = converter;
         }

         /// <inheritdoc />
         public Boolean CanConvertObjectOfType( Type source )
         {
            return source != null && typeof(TSource).GetTypeInfo().IsAssignableFrom( source.GetTypeInfo() );
         }

         /// <inheritdoc />
         public Object Convert( Object source )
         {
            if(CanConvertObjectOfType( source?.GetType() ))
            {
               return m_converter.Convert( (TSource)source );
            }
            throw new ArgumentException(
               "{0} cannot convert objects of type {1}".F(
                  m_converter.GetType().FullName,
                  source?.GetType().FullName ?? "null" ) );
         }
      }

      /// <inheritdoc />
      private sealed class UntypedObjectConverter : IObjectConverter
      {
         private readonly Func<Type, Boolean> m_canConvert;
         private readonly Func<Object, Object> m_converter;

         public UntypedObjectConverter( [NotNull] Func<Object, Object> converter,
                                        [NotNull] Func<Type, Boolean> canConvert )
         {
            Contract.Requires( converter != null );
            Contract.Requires( canConvert != null );
            m_converter = converter;
            m_canConvert = canConvert;
         }

         /// <inheritdoc />
         public Boolean CanConvertObjectOfType( Type source )
         {
            return m_canConvert( source );
         }

         /// <inheritdoc />
         public Object Convert( Object source )
         {
            return m_converter( source );
         }
      }
   }
}