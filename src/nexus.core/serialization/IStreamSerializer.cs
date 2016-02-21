// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.IO;
using System.Threading.Tasks;

namespace nexus.core.serialization
{
   public interface IStreamSerializer<in TFrom>
   {
      void Serialize( Stream to, TFrom source );

      Task SerializeAsync( Stream to, TFrom data );

      //Task Serialize( IOutputStream serializeTo, T data );

      // TODO: Implement IOutputStream
   }

   public delegate TTo StreamSerializer<out TTo>( Stream serializeTo );

   public delegate Task<TTo> StreamSerializerAsync<TTo>( Stream source );
}