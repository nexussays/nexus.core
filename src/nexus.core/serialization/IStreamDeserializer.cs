// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.IO;

namespace nexus.core.serialization
{
   public interface IStreamDeserializer
   {
      // TODO: Implement IInputStream
      //T Deserialize<T>( IInputStream source );

      T Deserialize<T>( Stream source );
   }

   public interface IStreamDeserializer<out T>
   {
      // TODO: Implement IInputStream
      //T Deserialize( IInputStream source );

      T Deserialize( Stream source );
   }
}