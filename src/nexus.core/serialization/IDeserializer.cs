// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.serialization
{
   /// <summary>
   /// Deserialize from some source object to a call-time-specified type. See <see cref="Deserializer.Create{TFrom}" />
   /// </summary>
   public interface IDeserializer<in TFrom>
   {
      TTo Deserialize<TTo>( TFrom source );

      /// <summary>
      /// Non-generic version of <see cref="Deserialize{T}" />
      /// </summary>
      Object Deserialize( TFrom source, Type desiredReturnType );
   }
}