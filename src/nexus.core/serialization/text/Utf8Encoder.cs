// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Text;

namespace nexus.core.serialization.text
{
   public class Utf8Encoder : TextEncoder
   {
      public static readonly ITextEncoder WithBOM = new Utf8Encoder( true );
      public static readonly ITextEncoder WithoutBOM = new Utf8Encoder( false );

      private Utf8Encoder( Boolean useBom )
         : base( new UTF8Encoding( useBom ) )
      {
      }
   }

   public static class Utf8TextEncoderUtils
   {
      public static String DecodeUtf8( this Byte[] value, Boolean includeBom = false )
      {
         return value == null ? null : (includeBom ? Utf8Encoder.WithBOM : Utf8Encoder.WithoutBOM).Deserialize( value );
      }

      public static Byte[] EncodeToUtf8( this String value, Boolean includeBom = false )
      {
         return value == null ? null : (includeBom ? Utf8Encoder.WithBOM : Utf8Encoder.WithoutBOM).Serialize( value );
      }

      public static Byte[] EncodeToUtf8( this Option<String> value, Boolean includeBom = false )
      {
         return !value.HasValue ? null : EncodeToUtf8( value.Value );
      }
   }
}