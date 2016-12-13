// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace nexus.core
{
   /// <summary>
   /// Transform one object into another.
   /// </summary>
   public interface IObjectConverter<in TFrom, out TTo>
   {
      TTo Convert( TFrom source );
   }

   /// <summary>
   /// Converter without type information for use in collections or other places a generic interface can cause problems
   /// <see cref="ObjectConverter.AsUntyped{TFrom,TTo}" />
   /// </summary>
   public interface IObjectConverter : IObjectConverter<Object, Object>
   {
      Boolean CanConvertObjectOfType( Type source );
   }

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
      public static IObjectConverter<TFrom, TTo> Create<TFrom, TTo>( Func<TFrom, TTo> convert )
      {
         Contract.Requires<ArgumentNullException>( convert != null );
         return new TypedObjectConverter<TFrom, TTo>( convert );
      }

      /// <summary>
      /// Create a new <see cref="IObjectConverter" /> from the provided conversion function.
      /// </summary>
      public static IObjectConverter Create( Func<Object, Object> convert, Func<Type, Boolean> canConvert )
      {
         Contract.Requires<ArgumentNullException>( convert != null );
         Contract.Requires<ArgumentNullException>( canConvert != null );
         return new UntypedObjectConverter( convert, canConvert );
      }

      private sealed class TypedObjectConverter<TFrom, TTo> : IObjectConverter<TFrom, TTo>
      {
         private readonly Func<TFrom, TTo> m_converter;

         public TypedObjectConverter( Func<TFrom, TTo> converter )
         {
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