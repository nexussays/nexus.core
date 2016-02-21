// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.IO;
using System.Threading.Tasks;

namespace nexus.core.serialization
{
   public interface IStreamDeserializer: IGenericDeserializer<Stream>
   {
      // TODO: Implement IInputStream
      //T Deserialize<T>( IInputStream source );

      Task<T> DeserializeAsync<T>( Stream source );
   }

   public interface IStreamDeserializer<T>
   {
      // TODO: Implement IInputStream
      //T Deserialize( IInputStream source );

      T Deserialize( Stream source );

      Task<T> DeserializeAsync( Stream source );
   }

   public delegate TTo StreamDeserializer<out TTo>( Stream source );

   public delegate Task<TTo> StreamDeserializerAsync<TTo>( Stream source );
}