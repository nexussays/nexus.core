// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.serialization.binary
{
   public static class Base16EncoderExtensions
   {
      public static Byte[] DecodeBase16( this String value )
      {
         return Base16Encoder.Instance.Deserialize(value);
      }

      public static String EncodeToBase16( this Byte[] bytes )
      {
         return Base16Encoder.Instance.Serialize(bytes);
      }

      public static String EncodeToBase16( this Byte byteValue )
      {
         return Base16Encoder.Instance.Serialize(byteValue);
      }
   }
}