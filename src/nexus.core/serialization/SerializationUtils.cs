// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.IO;
using System.Threading.Tasks;

namespace nexus.core.serialization
{
   public static class SerializationUtils
   {
      public static T ReadObject<T>( this Stream stream, IDeserializer<Stream> deserializer )
      {
         return deserializer.Deserialize<T>( stream );
      }

      public static T ReadObject<T>( this TextReader source, IDeserializer<TextReader> deserializer )
      {
         return deserializer.Deserialize<T>( source );
      }

      public static Task<T> ReadObjectAsync<T>( this Stream stream, IStreamDeserializer deserializer )
      {
         return deserializer.DeserializeAsync<T>( stream );
      }

      public static Task<T> ReadObjectAsync<T>( this TextReader source, ITextReaderDeserializer deserializer )
      {
         return deserializer.DeserializeAsync<T>( source );
      }

      public static void WriteObject<T>( this Stream stream, T source, IOutputSerializer<Stream> serializer )
      {
         serializer.Serialize( source, stream );
      }

      public static void WriteObject<T>( this TextWriter destination, T source, IOutputSerializer<TextWriter> serializer )
      {
         serializer.Serialize( source, destination );
      }

      public static Task WriteObjectAsync<T>( this Stream stream, T source, IStreamSerializer serializer )
      {
         return serializer.SerializeAsync( source, stream );
      }

      public static Task WriteObjectAsync<T>( this TextWriter destination, T source, ITextWriterSerializer serializer )
      {
         return serializer.SerializeAsync( source, destination );
      }
   }
}