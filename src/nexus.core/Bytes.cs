// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;

namespace nexus.core
{
   public static class Bytes
   {
      /// <summary>
      /// zero byte
      /// </summary>
      public const Byte Null = 0;
      /// <summary>
      /// 32
      /// </summary>
      public const Byte Space = 32;
      /// <summary>
      /// 10
      /// </summary>
      public const Byte Linefeed = 10;

      /// <summary>
      /// The byte order of the underlying host platform
      /// </summary>
      public static ByteOrder HostEnvironmentByteOrder => BitConverter.IsLittleEndian
         ? ByteOrder.LittleEndian
         : ByteOrder.BigEndian;

      /// <summary>
      /// Convert <see cref="short" /> to a byte array
      /// </summary>
      public static Byte[] ToBytes( this Int16 value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return endian == ByteOrder.BigEndian
            ? new[] {(Byte)(value >> 8), (Byte)value}
            : new[] {(Byte)value, (Byte)(value >> 8)};
      }

      /// <summary>
      /// Convert <see cref="int" /> to a byte array
      /// </summary>
      public static Byte[] ToBytes( this Int32 value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return endian == ByteOrder.BigEndian
            ? new[] {(Byte)(value >> 24), (Byte)(value >> 16), (Byte)(value >> 8), (Byte)value}
            : new[] {(Byte)value, (Byte)(value >> 8), (Byte)(value >> 16), (Byte)(value >> 24)};
      }

      /// <summary>
      /// Convert <see cref="long" /> to a byte array
      /// </summary>
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

      /// <summary>
      /// Convert <see cref="ushort" /> to a byte array
      /// </summary>
      public static Byte[] ToBytes( this UInt16 value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return endian == ByteOrder.BigEndian
            ? new[] {(Byte)(value >> 8), (Byte)value}
            : new[] {(Byte)value, (Byte)(value >> 8)};
      }

      /// <summary>
      /// Convert <see cref="uint" /> to a byte array
      /// </summary>
      public static Byte[] ToBytes( this UInt32 value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return endian == ByteOrder.BigEndian
            ? new[] {(Byte)(value >> 24), (Byte)(value >> 16), (Byte)(value >> 8), (Byte)value}
            : new[] {(Byte)value, (Byte)(value >> 8), (Byte)(value >> 16), (Byte)(value >> 24)};
      }

      /// <summary>
      /// Convert <see cref="ulong" /> to a byte array
      /// </summary>
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

      //public static Byte[] ToBytes( this Single value, ByteOrder endian = ByteOrder.LittleEndian )
      //{
      //   throw new NotImplementedException();
      //}

      //public static Byte[] ToBytes( this Double value, ByteOrder endian = ByteOrder.LittleEndian )
      //{
      //   throw new NotImplementedException();
      //}

      /// <summary>
      /// Convert byte array to <see cref="Single" />
      /// </summary>
      public static Single ToFloat32( this Byte[] bytes, Int32 startIndex = 0,
                                      ByteOrder endian = ByteOrder.LittleEndian )
      {
         Contract.Requires<ArgumentNullException>( bytes != null );
         Contract.Requires<ArgumentException>( startIndex >= 0 );
         return ToUInt32( bytes, startIndex, endian );
      }

      /// <summary>
      /// Convert byte array to <see cref="Double" />
      /// </summary>
      public static Double ToFloat64( this Byte[] bytes, Int32 startIndex = 0,
                                      ByteOrder endian = ByteOrder.LittleEndian )
      {
         Contract.Requires<ArgumentNullException>( bytes != null );
         Contract.Requires<ArgumentException>( startIndex >= 0 );
         return ToUInt64( bytes, startIndex, endian );
      }

      /// <summary>
      /// Convert byte array to <see cref="Int16" />
      /// </summary>
      public static Int16 ToInt16( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         Contract.Requires<ArgumentNullException>( bytes != null );
         Contract.Requires<ArgumentException>( startIndex >= 0 );
         if(bytes.Length <= startIndex)
         {
            return 0;
         }
         return bytes.Length > startIndex + 1
            ? (endian == ByteOrder.BigEndian
               ? (Int16)(bytes[startIndex] << 8 | bytes[startIndex + 1])
               : (Int16)(bytes[startIndex + 1] << 8 | bytes[startIndex]))
            : (endian == ByteOrder.BigEndian ? (Int16)(bytes[startIndex] << 8) : (Int16)(0 << 8 | bytes[startIndex]));
      }

      /// <summary>
      /// Convert byte array to <see cref="Int32" />. This method does not throw if <paramref name="bytes" /> is not long enough
      /// to fill <see cref="Int32" />, instead the missing bytes are presumed to be 0.
      /// </summary>
      public static unsafe Int32 ToInt32( this Byte[] bytes, Int32 startIndex = 0,
                                          ByteOrder endian = ByteOrder.LittleEndian )
      {
         Contract.Requires<ArgumentNullException>( bytes != null );
         Contract.Requires<ArgumentException>( startIndex >= 0 );
         var length = bytes.Length;
         if(length == 0 || length <= startIndex)
         {
            return 0;
         }
         length -= startIndex;
         fixed(Byte* b = &bytes[startIndex])
         {
            if(endian == ByteOrder.BigEndian)
            {
               if(length > 3)
               {
                  return (*b << 24) | (*(b + 1) << 16) | (*(b + 2) << 8) | *(b + 3);
               }
               if(length > 2)
               {
                  return (*b << 24) | (*(b + 1) << 16) | (*(b + 2) << 8);
               }
               if(length > 1)
               {
                  return (*b << 24) | (*(b + 1) << 16);
               }
               return *b << 24;
            }
            if(length > 3)
            {
               return *b | (*(b + 1) << 8) | (*(b + 2) << 16) | (*(b + 3) << 24);
            }
            if(length > 2)
            {
               return *b | (*(b + 1) << 8) | (*(b + 2) << 16);
            }
            if(length > 1)
            {
               return *b | (*(b + 1) << 8);
            }
            return *b;
         }
      }

      /// <summary>
      /// Convert byte array to <see cref="Int64" />
      /// </summary>
      public static Int64 ToInt64( this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         Contract.Requires<ArgumentNullException>( bytes != null );
         Contract.Requires<ArgumentException>( startIndex >= 0 );
         var length = bytes.Length;
         if(length == 0 || length <= startIndex)
         {
            return 0;
         }
         var i1 = ToInt32( bytes, startIndex, endian );
         var i2 = ToInt32( bytes, startIndex + 4, endian );
         return endian == ByteOrder.BigEndian ? ((Int64)i1 << 32) | (UInt32)i2 : (UInt32)i1 | ((Int64)i2 << 32);
      }

      /// <summary>
      /// Convert byte array to <see cref="UInt16" />
      /// </summary>
      public static UInt16 ToUInt16( this Byte[] bytes, Int32 startIndex = 0,
                                     ByteOrder endian = ByteOrder.LittleEndian )
      {
         Contract.Requires<ArgumentNullException>( bytes != null );
         Contract.Requires<ArgumentException>( startIndex >= 0 );
         return (UInt16)ToInt16( bytes, startIndex, endian );
      }

      /// <summary>
      /// Convert byte array to <see cref="UInt32" />
      /// </summary>
      public static UInt32 ToUInt32( this Byte[] bytes, Int32 startIndex = 0,
                                     ByteOrder endian = ByteOrder.LittleEndian )
      {
         Contract.Requires<ArgumentNullException>( bytes != null );
         Contract.Requires<ArgumentException>( startIndex >= 0 );
         return (UInt32)ToInt32( bytes, startIndex, endian );
      }

      /// <summary>
      /// Convert byte array to <see cref="UInt64" />
      /// </summary>
      public static UInt64 ToUInt64( this Byte[] bytes, Int32 startIndex = 0,
                                     ByteOrder endian = ByteOrder.LittleEndian )
      {
         Contract.Requires<ArgumentNullException>( bytes != null );
         Contract.Requires<ArgumentException>( startIndex >= 0 );
         return (UInt64)ToInt64( bytes, startIndex, endian );
      }
   }
}