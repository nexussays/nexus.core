// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core
{
   public static class Bytes
   {
      public const Byte Null = 0;
      public const Byte Space = 32;
      public const Byte Linefeed = 10;

      public static ByteOrder HostEnvironmentByteOrder
         => BitConverter.IsLittleEndian ? ByteOrder.LittleEndian : ByteOrder.BigEndian;

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
         return endian == ByteOrder.BigEndian
            ? new[]
            {
               (Byte)((value >> 56) & 0xff),
               (Byte)((value >> 48) & 0xff),
               (Byte)((value >> 40) & 0xff),
               (Byte)((value >> 32) & 0xff),
               (Byte)((value >> 24) & 0xff),
               (Byte)((value >> 16) & 0xff),
               (Byte)((value >> 8) & 0xff),
               (Byte)(value & 0xff)
            }
            : new[]
            {
               (Byte)(value & 0xff),
               (Byte)((value >> 8) & 0xff),
               (Byte)((value >> 16) & 0xff),
               (Byte)((value >> 24) & 0xff),
               (Byte)((value >> 32) & 0xff),
               (Byte)((value >> 40) & 0xff),
               (Byte)((value >> 48) & 0xff),
               (Byte)((value >> 56) & 0xff)
            };
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
         return endian == ByteOrder.BigEndian
            ? new[]
            {
               (Byte)((value >> 56) & 0xff),
               (Byte)((value >> 48) & 0xff),
               (Byte)((value >> 40) & 0xff),
               (Byte)((value >> 32) & 0xff),
               (Byte)((value >> 24) & 0xff),
               (Byte)((value >> 16) & 0xff),
               (Byte)((value >> 8) & 0xff),
               (Byte)(value & 0xff)
            }
            : new[]
            {
               (Byte)(value & 0xff),
               (Byte)((value >> 8) & 0xff),
               (Byte)((value >> 16) & 0xff),
               (Byte)((value >> 24) & 0xff),
               (Byte)((value >> 32) & 0xff),
               (Byte)((value >> 40) & 0xff),
               (Byte)((value >> 48) & 0xff),
               (Byte)((value >> 56) & 0xff)
            };
      }

      public static Byte[] ToBytes( this Single value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         throw new NotImplementedException();
      }

      public static Byte[] ToBytes( this Double value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         throw new NotImplementedException();
      }

      /// <summary>
      /// To <c>System.Single</c>
      /// </summary>
      public static Single ToFloat32( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return ToUInt32( bytes, startIndex, endian );
      }

      /// <summary>
      /// To <c>System.Double</c>
      /// </summary>
      public static Double ToFloat64( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return ToUInt64( bytes, startIndex, endian );
      }

      public static Int16 ToInt16( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return (Int16)ToUInt16( bytes, startIndex, endian );
      }

      public static Int32 ToInt32( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return (Int32)ToUInt32( bytes, startIndex, endian );
      }

      public static Int64 ToInt64( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return (Int64)ToUInt64( bytes, startIndex, endian );
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
         return endian == ByteOrder.BigEndian
            ? ((UInt64)bytes[startIndex] << 56) | ((UInt64)bytes[startIndex + 1] << 48) |
              ((UInt64)bytes[startIndex + 2] << 40) | ((UInt64)bytes[startIndex + 3] << 32) |
              (UInt32)(bytes[startIndex + 4] << 24) | (UInt32)(bytes[startIndex + 5] << 16) |
              (UInt32)(bytes[startIndex + 6] << 8) | bytes[startIndex + 7]
            : ((UInt64)bytes[startIndex + 7] << 56) | ((UInt64)bytes[startIndex + 6] << 48) |
              ((UInt64)bytes[startIndex + 5] << 40) | ((UInt64)bytes[startIndex + 4] << 32) |
              (UInt32)(bytes[startIndex + 3] << 24) | (UInt32)(bytes[startIndex + 2] << 16) |
              (UInt32)(bytes[startIndex + 1] << 8) | bytes[startIndex];
      }
   }
}