// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using nexus.core.serialization.binary;
using nexus.core.text;

namespace nexus.core
{
   public static class ByteUtils
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
      /// Compare two byte arrays and return true if they are the same length and have the same values at each index
      /// </summary>
      public static Boolean EqualsByteArray( this Byte[] l, Byte[] r )
      {
         if(ReferenceEquals( l, r ))
         {
            return true;
         }

         // short circuit loop if anything is null or lengths aren't equal
         if(ReferenceEquals( null, l ) || ReferenceEquals( null, r ) || l.Length != r.Length)
         {
            return false;
         }

         for(var x = 0; x < l.Length; ++x)
         {
            // TODO: is the bitwise operation faster?
            if(l[x] != r[x]) //if((l[x] ^ r[x]) != 0)
            {
               return false;
            }
         }
         return true;
      }

      public static Byte[] GetUtf16Bytes( this String value, Boolean includeByteOrderMark = false )
      {
         return value == null
            ? null
            : (includeByteOrderMark ? Utf16Encoding.WithBOM : Utf16Encoding.WithoutBOM).GetBytes( value );
      }

      public static Option<Byte[]> GetUtf16Bytes( this Option<String> value, Boolean includeByteOrderMark = false )
      {
         return value.HasValue ? GetUtf16Bytes( value.Value ) : Option<Byte[]>.NoValue;
      }

      public static Byte[] GetUtf8Bytes( this String value, Boolean includeByteOrderMark = false )
      {
         return value == null
            ? null
            : (includeByteOrderMark ? Utf8Encoding.WithBOM : Utf8Encoding.WithoutBOM).GetBytes( value );
      }

      public static Option<Byte[]> GetUtf8Bytes( this Option<String> value, Boolean includeByteOrderMark = false )
      {
         return value.HasValue ? GetUtf8Bytes( value.Value ) : Option<Byte[]>.NoValue;
      }

      /// <summary>
      ///    <code>bytes == null || bytes.Length &lt;= 0</code>
      /// </summary>
      public static Boolean IsNullOrEmpty( this Byte[] bytes )
      {
         return bytes == null || bytes.Length <= 0;
      }

      /// <summary>
      ///    <code>bytes == null || bytes.Length &;t;= 0 || (bytes.Length == 1 && bytes[0] == 0)</code>
      /// </summary>
      public static Boolean IsNullOrEmptyOrNullByte( this Byte[] bytes )
      {
         return bytes == null || bytes.Length <= 0 || (bytes.Length == 1 && bytes[0] == 0);
      }

      /// <summary>
      /// Copies the byte array starting at the provided index through the last element of the array.
      /// </summary>
      public static Byte[] Slice( this Byte[] source, Int32 startByteIndex = 0 )
      {
         Contract.Requires<ArgumentNullException>( source != null );
         // ReSharper disable once PossibleNullReferenceException
         Contract.Requires<ArgumentException>( source.Length >= startByteIndex );
         Contract.Requires<ArgumentException>( startByteIndex >= 0 );
         Contract.Ensures( Contract.Result<Byte[]>() != null );
         return Slice( source, startByteIndex, source.Length );
      }

      /// <summary>
      /// Copies the selected range of bytes from the source array
      /// </summary>
      public static Byte[] Slice( this Byte[] source, Int32 startByteIndex, Int32 endByteIndex )
      {
         Contract.Requires<ArgumentNullException>( source != null );
         Contract.Requires<ArgumentException>( startByteIndex >= 0 );
         Contract.Requires<ArgumentException>( endByteIndex >= startByteIndex );
         Contract.Ensures( Contract.Result<Byte[]>() != null );
         Contract.Ensures( Contract.Result<Byte[]>().Length == endByteIndex - startByteIndex );
         var result = new Byte[endByteIndex - startByteIndex];
         Buffer.BlockCopy( source, startByteIndex, result, 0, result.Length );
         return result;
      }
   }
}