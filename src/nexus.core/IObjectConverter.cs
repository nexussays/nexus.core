// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core
{
   /// <summary>
   /// A typed converter from one object into another.
   /// </summary>
   public interface IObjectConverter<in TFrom, out TTo>
   {
      /// <summary>
      /// Convert from a source object to a destination object
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      TTo Convert( TFrom source );
   }

   /// <summary>
   /// Converter without type information for use in collections or other places a generic interface can cause problems. See
   /// <see cref="ObjectConverter.AsUntyped{TFrom,TTo}" />
   /// </summary>
   public interface IObjectConverter : IObjectConverter<Object, Object>
   {
      /// <summary>
      /// <c>true</c> if this type can be converted
      /// </summary>
      Boolean CanConvertObjectOfType( Type source );
   }
}