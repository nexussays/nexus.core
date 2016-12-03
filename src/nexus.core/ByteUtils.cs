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

      public static Byte[] DecodeBase16String( this String value )
      {
         try
         {
            return DecodeBase16String( value, true );
         }
         catch(FormatException)
         {
            return DecodeBase16String( value, false );
         }
      }

      public static Byte[] DecodeBase16String( this String value, Boolean lowercase )
      {
         return lowercase ? Base16Encoder.Lowercase.Deserialize( value ) : Base16Encoder.Uppercase.Deserialize( value );
      }

      public static Byte[] DecodeBase64String( this String value )
      {
         return value.IsNullOrEmpty() ? new Byte[0] : Base64Encoder.Instance.Deserialize( value );
      }

      public static String EncodeToBase16String( this Byte[] value, Boolean lowercase = true )
      {
         return lowercase ? Base16Encoder.Lowercase.Serialize( value ) : Base16Encoder.Uppercase.Serialize( value );
      }

      public static String EncodeToBase16String( this Byte value, Boolean lowercase = true )
      {
         return lowercase ? Base16Encoder.Lowercase.Serialize( value ) : Base16Encoder.Uppercase.Serialize( value );
      }

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

      public static Boolean IsNullOrEmpty( this Byte[] bytes )
      {
         return bytes == null || bytes.Length <= 0;
      }

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

      public static Byte[] ToBytes( this Int16 value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return endian == ByteOrder.BigEndian
            ? new[] {(Byte)(value >> 8), (Byte)value}
            : new[] {(Byte)value, (Byte)(value >> 8)};
      }

      public static Byte[] ToBytes( this Int32 value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return endian == ByteOrder.BigEndian
            ? new[] {(Byte)(value >> 24), (Byte)(value >> 16), (Byte)(value >> 8), (Byte)value}
            : new[] {(Byte)value, (Byte)(value >> 8), (Byte)(value >> 16), (Byte)(value >> 24)};
      }

      public static Byte[] ToBytes( this Int64 value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         throw new NotImplementedException();
      }

      public static Byte[] ToBytes( this UInt16 value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return endian == ByteOrder.BigEndian
            ? new[] {(Byte)(value >> 8), (Byte)value}
            : new[] {(Byte)value, (Byte)(value >> 8)};
      }

      public static Byte[] ToBytes( this UInt32 value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return endian == ByteOrder.BigEndian
            ? new[] {(Byte)(value >> 24), (Byte)(value >> 16), (Byte)(value >> 8), (Byte)value}
            : new[] {(Byte)value, (Byte)(value >> 8), (Byte)(value >> 16), (Byte)(value >> 24)};
      }

      public static Byte[] ToBytes( this UInt64 value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         throw new NotImplementedException();
      }

      public static Byte[] ToBytes( this Single value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         throw new NotImplementedException();
      }

      public static Byte[] ToBytes( this Double value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         throw new NotImplementedException();
      }

      public static Double ToDouble( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         throw new NotImplementedException();
      }

      public static Int16 ToInt16( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return endian == ByteOrder.BigEndian
            ? (Int16)(bytes[startIndex] << 8 | bytes[startIndex + 1])
            : (Int16)(bytes[startIndex + 1] << 8 | bytes[startIndex]);
      }

      public static Int32 ToInt32( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return endian == ByteOrder.BigEndian
            ? bytes[startIndex] << 24 | (bytes[startIndex + 1] << 16) | (bytes[startIndex + 2] << 8) |
              bytes[startIndex + 3]
            : bytes[startIndex + 3] << 24 | (bytes[startIndex + 2] << 16) | (bytes[startIndex + 1] << 8) |
              bytes[startIndex];
      }

      public static Int64 ToInt64( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return endian == ByteOrder.BigEndian
            ? (bytes[startIndex] << 56) | (bytes[startIndex + 1] << 48) | (bytes[startIndex + 2] << 40) |
              (bytes[startIndex + 3] << 32) | (bytes[startIndex + 4] << 24) | (bytes[startIndex + 5] << 16) |
              (bytes[startIndex + 6] << 8) | bytes[startIndex + 7]
            : (bytes[startIndex + 7] << 56) | (bytes[startIndex + 6] << 48) | (bytes[startIndex + 5] << 40) |
              (bytes[startIndex + 4] << 32) | (bytes[startIndex + 3] << 24) | (bytes[startIndex + 2] << 16) |
              (bytes[startIndex + 1] << 8) | bytes[startIndex];
      }

      public static Double ToSingle( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         throw new NotImplementedException();
      }

      public static UInt16 ToUInt16( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return
            (UInt16)
            (endian == ByteOrder.BigEndian
               ? bytes[startIndex] << 8 | bytes[startIndex + 1]
               : bytes[startIndex + 1] << 8 | bytes[startIndex]);
      }

      public static UInt32 ToUInt32( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return
            (UInt32)
            (endian == ByteOrder.BigEndian
               ? bytes[startIndex] << 24 | (bytes[startIndex + 1] << 16) | (bytes[startIndex + 2] << 8) |
                 bytes[startIndex + 3]
               : bytes[startIndex + 3] << 24 | (bytes[startIndex + 2] << 16) | (bytes[startIndex + 1] << 8) |
                 bytes[startIndex]);
      }

      public static UInt64 ToUInt64( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return
            (UInt64)
            (endian == ByteOrder.BigEndian
               ? (bytes[startIndex] << 56) | (bytes[startIndex + 1] << 48) | (bytes[startIndex + 2] << 40) |
                 (bytes[startIndex + 3] << 32) | (bytes[startIndex + 4] << 24) | (bytes[startIndex + 5] << 16) |
                 (bytes[startIndex + 6] << 8) | bytes[startIndex + 7]
               : (bytes[startIndex + 7] << 56) | (bytes[startIndex + 6] << 48) | (bytes[startIndex + 5] << 40) |
                 (bytes[startIndex + 4] << 32) | (bytes[startIndex + 3] << 24) | (bytes[startIndex + 2] << 16) |
                 (bytes[startIndex + 1] << 8) | bytes[startIndex]);
      }
   }
}