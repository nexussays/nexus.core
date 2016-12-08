// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.IO;
using System.Threading.Tasks;

namespace nexus.core.serialization
{
   public delegate TTo StreamDeserializer<out TTo>( Stream source );

   public delegate Task<TTo> StreamDeserializerAsync<TTo>( Stream source );

   public interface IStreamDeserializer<T>
   {
      T Deserialize( Stream source );

      Task<T> DeserializeAsync( Stream source );
   }

   public static class StreamDeserializerExtensions
   {
      public static void ReadObject<T>( this Stream stream, T source, IStreamDeserializer<T> deserializer )
      {
         deserializer.Deserialize( stream );
      }

      public static Task ReadObjectAsync<T>( this Stream stream, T source, IStreamDeserializer<T> deserializer )
      {
         return deserializer.DeserializeAsync( stream );
      }
   }
}