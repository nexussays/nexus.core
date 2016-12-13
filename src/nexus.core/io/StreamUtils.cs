// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
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

      public static void WriteObject<T>( this Stream stream, T source, IStreamSerializer serializer )
      {
         if(serializer.CanSerializeObjectOfType( typeof(T) ))
         {
            serializer.Serialize( stream, source );
         }
         else
         {
            throw new ArgumentException(
               "Serializer {0} cannot serialize objects of type {1}".F(
                  serializer.GetType().FullName,
                  source?.GetType().FullName ?? "null" ) );
         }
      }

      public static Task WriteObjectAsync<T>( this Stream stream, T source, IStreamSerializer serializer )
      {
         if(serializer.CanSerializeObjectOfType( typeof(T) ))
         {
            return serializer.SerializeAsync( stream, source );
         }
         throw new ArgumentException(
            "Serializer {0} cannot serialize objects of type {1}".F(
               serializer.GetType().FullName,
               source?.GetType().FullName ?? "null" ) );
      }
   }
}