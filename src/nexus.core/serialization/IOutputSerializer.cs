// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.core.serialization
{
   /// <summary>
   /// Serialize an object to a given output (e.g. <see cref="IOutputSerializer{Stream}" />)
   /// </summary>
   public interface IOutputSerializer<in TTo>
   {
      void Serialize<TFrom>( TFrom source, TTo output );
   }
}