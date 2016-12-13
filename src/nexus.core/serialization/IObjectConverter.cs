// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;

namespace nexus.core.serialization
{
   /// <summary>
   /// Transform one object into another. See <see cref="ObjectConverter.Create{TFrom,TTo}" />
   /// </summary>
   public interface IObjectConverter<in TFrom, out TTo>
   {
      TTo Convert( TFrom source );
   }

   public static class ObjectConverter
   {
      /// <summary>
      /// Create a new <see cref="IObjectConverter{TFrom,TTo}" /> from the provided conversion function.
      /// </summary>
      public static IObjectConverter<TFrom, TTo> Create<TFrom, TTo>( Func<TFrom, TTo> convert )
      {
         Contract.Requires<ArgumentNullException>( convert != null );
         return new TypedObjectConverter<TFrom, TTo>( convert );
      }

      private sealed class TypedObjectConverter<TFrom, TTo> : IObjectConverter<TFrom, TTo>
      {
         private readonly Func<TFrom, TTo> m_convert;

         public TypedObjectConverter( Func<TFrom, TTo> convert )
         {
            m_convert = convert;
         }

         public TTo Convert( TFrom source )
         {
            return m_convert( source );
         }
      }
   }
}