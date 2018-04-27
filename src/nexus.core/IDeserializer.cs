// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core
{
   /// <summary>
   /// Deserialize from some <typeparamref name="TSource" /> object to a call-time-specified type. See
   /// <see cref="Deserializer.Create{TFrom}" />
   /// </summary>
   public interface IDeserializer<in TSource>
   {
      /// <summary>
      /// Deserialize from some source object to a call-time-specified type
      /// </summary>
      TResult Deserialize<TResult>( TSource source );

      /// <summary>
      /// Non-generic version of <see cref="Deserialize{TResult}" />
      /// </summary>
      Object Deserialize( TSource source, Type desiredReturnType );
   }
}
