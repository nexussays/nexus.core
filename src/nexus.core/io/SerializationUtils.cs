// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.IO;
using System.Threading.Tasks;

namespace nexus.core.io
{
   /// <summary>
   /// Extension methods for <see cref="Stream" /> and <see cref="TextWriter" />
   /// </summary>
   public static class SerializationUtils
   {
      /// <summary>
      /// Read an object from <paramref name="source" /> using the given <paramref name="deserializer" />
      /// </summary>
      public static T ReadObject<T>( this Stream source, IDeserializer<Stream> deserializer )
      {
         return deserializer.Deserialize<T>( source );
      }

      /// <summary>
      /// Read an object from <paramref name="source" /> using the given <paramref name="deserializer" />
      /// </summary>
      public static T ReadObject<T>( this TextReader source, IDeserializer<TextReader> deserializer )
      {
         return deserializer.Deserialize<T>( source );
      }

      /// <summary>
      /// Read an object from <paramref name="source" /> using the given <paramref name="deserializer" />
      /// </summary>
      public static Task<T> ReadObjectAsync<T>( this Stream source, IStreamDeserializer deserializer )
      {
         return deserializer.DeserializeAsync<T>( source );
      }

      /// <summary>
      /// Read an object from <paramref name="source" /> using the given <paramref name="deserializer" />
      /// </summary>
      public static Task<T> ReadObjectAsync<T>( this TextReader source, ITextReaderDeserializer deserializer )
      {
         return deserializer.DeserializeAsync<T>( source );
      }

      /// <summary>
      /// Wrte an object to <paramref name="destination" /> using the given <paramref name="serializer" />
      /// </summary>
      public static void WriteObject<T>( this Stream destination, T source, IStreamSerializer serializer )
      {
         serializer.Serialize( source, destination );
      }

      /// <summary>
      /// Wrte an object to <paramref name="destination" /> using the given <paramref name="serializer" />
      /// </summary>
      public static void WriteObject<T>( this TextWriter destination, T source, ITextWriterSerializer serializer )
      {
         serializer.Serialize( source, destination );
      }

      /// <summary>
      /// Wrte an object to <paramref name="destination" /> using the given <paramref name="serializer" />
      /// </summary>
      public static Task WriteObjectAsync<T>( this Stream destination, T source, IStreamSerializer serializer )
      {
         return serializer.SerializeAsync( source, destination );
      }

      /// <summary>
      /// Wrte an object to <paramref name="destination" /> using the given <paramref name="serializer" />
      /// </summary>
      public static Task WriteObjectAsync<T>( this TextWriter destination, T source, ITextWriterSerializer serializer )
      {
         return serializer.SerializeAsync( source, destination );
      }
   }
}
