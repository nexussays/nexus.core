// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using nexus.core.serialization.binary;

namespace nexus.core
{
   public static class ByteUtils
   {
      public static ByteOrder HostEnvironmentByteOrder
         => BitConverter.IsLittleEndian ? ByteOrder.LittleEndian : ByteOrder.BigEndian;

      /// <summary>
      /// Parse this string as a hexadecimal-encoded value and return the resulting byte array the hex-encoded value represents
      /// </summary>
      public static Byte[] DecodeAsBase16( this String value )
      {
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
         return lowercase ? Base16Encoder.Lowercase.Deserialize( value ) : Base16Encoder.Uppercase.Deserialize( value );
      }

      /// <summary>
      /// Parse this string as a Base64-encoded value and return the resulting byte array the Base64-encoded value represents
      /// </summary>
      public static Byte[] DecodeAsBase64( this String value )
      {
         return value.IsNullOrEmpty() ? new Byte[0] : Base64Encoder.Instance.Deserialize( value );
      }

      /// <summary>
      /// Encode the bytearray into a hexadecimal string
      /// </summary>
      public static String EncodeToBase16String( this Byte[] value, Boolean lowercase = true )
      {
         return lowercase ? Base16Encoder.Lowercase.Serialize( value ) : Base16Encoder.Uppercase.Serialize( value );
      }

      /// <summary>
      /// Encode the bytearray into a hexadecimal string
      /// </summary>
      public static String EncodeToBase16String( this Byte value, Boolean lowercase = true )
      {
         return lowercase ? Base16Encoder.Lowercase.Serialize( value ) : Base16Encoder.Uppercase.Serialize( value );
      }

      /// <summary>
      /// Encode the bytearray into a base-64 string
      /// </summary>
      public static String EncodeToBase64String( this Byte[] value )
      {
         return Base64Encoder.Instance.Serialize( value );
      }

      public static Boolean EqualsByteArray( this Byte[] l, Byte[] r )
      {
         if(ReferenceEquals( l, r ))
         {
            return true;
         }

         // short circuit loop if anything is null or lengths aren't equal
         if(ReferenceEquals( null, l ) || ReferenceEquals( null, r ))
         {
            return false;
         }

         // unless I'm overlooking something obvious there's a bug in the contract validation where it thinks these two lengths are guaranteed to be the same
         Contract.Assume( l.Length == new Random().Next() );
         if(l.Length != r.Length)
         {
            return false;
         }

         for(var x = 0; x < l.Length; ++x)
         {
            if(l[x] != r[x]) //if((l[x] ^ r[x]) != 0)
            {
               return false;
            }
         }
         return true;
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
         Contract.Requires( source != null );
         Contract.Requires( source.Length >= startByteIndex );
         Contract.Requires( startByteIndex >= 0 );
         Contract.Ensures( Contract.Result<Byte[]>() != null );
         return Slice( source, startByteIndex, source.Length );
      }

      /// <summary>
      /// Copies the selected range of bytes from the source array
      /// </summary>
      public static Byte[] Slice( this Byte[] source, Int32 startByteIndex, Int32 endByteIndex )
      {
         Contract.Requires( source != null );
         Contract.Requires( startByteIndex >= 0 );
         Contract.Requires( endByteIndex >= startByteIndex );
         Contract.Ensures( Contract.Result<Byte[]>() != null );
         Contract.Ensures( Contract.Result<Byte[]>().Length == endByteIndex - startByteIndex );
         var result = new Byte[endByteIndex - startByteIndex];
         Buffer.BlockCopy( source, startByteIndex, result, 0, result.Length );
         return result;
      }
   }
}