// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.IO;
using System.Threading.Tasks;

namespace nexus.core.serialization
{
   public delegate TTo StreamSerializer<out TTo>( Stream serializeTo );

   public delegate Task<TTo> StreamSerializerAsync<TTo>( Stream source );

   public interface IStreamSerializer<in T>
   {
      void Serialize( Stream to, T source );

      Task SerializeAsync( Stream to, T source );
   }

   public static class StreamSerializerExtensions
   {
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