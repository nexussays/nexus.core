// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.text
{
   /// <summary>
   /// Extension and utility methods to convert to/from bytes and encoded string values
   /// </summary>
   public static class EncodingUtils
   {
      /// <summary>
      /// Presume the given bytes are Unicode UTF-16 encoded and return its string value
      /// </summary>
      public static String AsUtf16String( this Byte[] value, Boolean includeByteOrderMark = false )
      {
         return value == null
            ? null
            : (includeByteOrderMark ? Utf16Encoding.WithBOM : Utf16Encoding.WithoutBOM).AsString( value );
      }

      /// <summary>
      /// Presume the given bytes are Unicode UTF-8 encoded and return its string value
      /// </summary>
      public static String AsUtf8String( this Byte[] value, Boolean includeByteOrderMark = false )
      {
         return value == null
            ? null
            : (includeByteOrderMark ? Utf8Encoding.WithBOM : Utf8Encoding.WithoutBOM).AsString( value );
      }

      /// <summary>
      /// Parse this string as a hexadecimal-encoded value and return the resulting byte array the hex-encoded value represents
      /// </summary>
      public static Byte[] DecodeAsBase16( this String value )
      {
         // TODO: This should really just handle all cases in the same string without this hacky method
         try
         {
            return DecodeAsBase16( value, true );
         }
         catch(FormatException)
         {
            return DecodeAsBase16( value, false );
         }
      }

      /// <summary>
      /// Parse this string as a hexadecimal-encoded value and return the resulting byte array the hex-encoded value represents
      /// </summary>
      public static Byte[] DecodeAsBase16( this String value, Boolean lowercase )
      {
         return lowercase ? Base16Encoder.Lowercase.Decode( value ) : Base16Encoder.Uppercase.Decode( value );
      }

      /// <summary>
      /// Parse this string as a Base64-encoded value and return the resulting byte array the Base64-encoded value represents
      /// </summary>
      public static Byte[] DecodeAsBase64( this String value )
      {
         return value.IsNullOrEmpty() ? new Byte[0] : Base64Encoder.Instance.Decode( value );
      }

      /// <summary>
      /// Encode the bytearray into a hexadecimal string
      /// </summary>
      public static String EncodeToBase16String( this Byte[] value, Boolean lowercase = true )
      {
         return lowercase ? Base16Encoder.Lowercase.Encode( value ) : Base16Encoder.Uppercase.Encode( value );
      }

      /// <summary>
      /// Encode the bytearray into a hexadecimal string
      /// </summary>
      public static String EncodeToBase16String( this Byte value, Boolean lowercase = true )
      {
         return lowercase ? Base16Encoder.Lowercase.Encode( value ) : Base16Encoder.Uppercase.Encode( value );
      }

      /// <summary>
      /// Encode the bytearray into a base-64 string
      /// </summary>
      public static String EncodeToBase64String( this Byte[] value )
      {
         return Base64Encoder.Instance.Encode( value );
      }

      /// <summary>
      /// Get the bytes representing this string in utf-16 encoding
      /// </summary>
      public static Byte[] GetUtf16Bytes( this String value, Boolean includeByteOrderMark = false )
      {
         return value == null
            ? null
            : (includeByteOrderMark ? Utf16Encoding.WithBOM : Utf16Encoding.WithoutBOM).GetBytes( value );
      }

      /// <summary>
      /// Get the bytes representing this string in utf-16 encoding
      /// </summary>
      public static Option<Byte[]> GetUtf16Bytes( this Option<String> value, Boolean includeByteOrderMark = false )
      {
         return value.HasValue ? GetUtf16Bytes( value.Value, includeByteOrderMark ) : Option<Byte[]>.NoValue;
      }

      /// <summary>
      /// Get the bytes representing this string in utf-8 encoding
      /// </summary>
      public static Byte[] GetUtf8Bytes( this String value, Boolean includeByteOrderMark = false )
      {
         return value == null
            ? null
            : (includeByteOrderMark ? Utf8Encoding.WithBOM : Utf8Encoding.WithoutBOM).GetBytes( value );
      }

      /// <summary>
      /// Get the bytes representing this string in utf-8 encoding
      /// </summary>
      public static Option<Byte[]> GetUtf8Bytes( this Option<String> value, Boolean includeByteOrderMark = false )
      {
         return value.HasValue ? GetUtf8Bytes( value.Value, includeByteOrderMark ) : Option<Byte[]>.NoValue;
      }
   }
}
