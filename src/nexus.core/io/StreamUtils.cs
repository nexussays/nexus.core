// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.IO;
using System.Threading.Tasks;

namespace nexus.core.io
{
   public static class StreamUtils
   {
      public static void ReadObject<T>( this Stream stream, T source, IStreamDeserializer<T> deserializer )
      {
         deserializer.Deserialize( stream );
      }

      public static Task ReadObjectAsync<T>( this Stream stream, T source, IStreamDeserializer<T> deserializer )
      {
         return deserializer.DeserializeAsync( stream );
      }

      public static void WriteObject<T>( this Stream stream, T source, IStreamSerializer<T> serializer )
      {
         serializer.Serialize( stream, source );
      }

      public static Task WriteObjectAsync<T>( this Stream stream, T source, IStreamSerializer<T> serializer )
      {
         return serializer.SerializeAsync( stream, source );
      }
   }
}