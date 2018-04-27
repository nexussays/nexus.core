// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using nexus.core.resharper;

namespace nexus.core
{
   /// <summary>
   /// Utility methods and constants related to <see cref="byte" /> manipulation
   /// </summary>
   public static partial class Bytes
   {
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

      /*
      /// <summary>
      /// Convert <see cref="Single" /> to a byte array
      /// </summary>
      public static Byte[] ToBytes( this Single value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return ToBytes(((UInt32)value ));
      }

      /// <summary>
      /// Convert <see cref="Double" /> to a byte array
      /// </summary>
      public static Byte[] ToBytes( this Double value, ByteOrder endian = ByteOrder.LittleEndian )
      {
         return ToBytes(  ((UInt64)value ));
      }
      */
      /// <summary>
      /// Convert byte array to <see cref="Single" />
      /// </summary>
      public static Single ToFloat32( [NotNull] this Byte[] bytes, Int32 startIndex = 0,
                                      ByteOrder endian = ByteOrder.LittleEndian )
      {
         if(bytes == null)
         {
            throw new ArgumentNullException( nameof(bytes) );
         }
         //Contract.Requires<ArgumentNullException>( bytes != null );
         //Contract.Requires<ArgumentException>( startIndex >= 0 );
         return ToUInt32( bytes, startIndex, endian );
      }

      /// <summary>
      /// Convert byte array to <see cref="Double" />
      /// </summary>
      public static Double ToFloat64( [NotNull] this Byte[] bytes, Int32 startIndex = 0,
                                      ByteOrder endian = ByteOrder.LittleEndian )
      {
         if(bytes == null)
         {
            throw new ArgumentNullException( nameof(bytes) );
         }
         //Contract.Requires<ArgumentNullException>( bytes != null );
         //Contract.Requires<ArgumentException>( startIndex >= 0 );
         return ToUInt64( bytes, startIndex, endian );
      }

      /// <summary>
      /// Convert bytes array to <see cref="Guid" /> by parsing the bytes according to the endianness of
      /// <paramref name="order" />. So if <see cref="ByteOrder.BigEndian" /> then <paramref name="bytes" /><c>[0]</c> will be
      /// the left-most bytes of the GUID.
      /// </summary>
      public static unsafe Guid ToGuid( [NotNull] this Byte[] bytes, Int32 startIndex = 0,
                                        ByteOrder order = ByteOrder.LittleEndian )
      {
         if(bytes == null)
         {
            throw new ArgumentNullException( nameof(bytes) );
         }
         //Contract.Requires<ArgumentNullException>( bytes != null );
         //Contract.Requires<ArgumentException>( startIndex >= 0 );
         //Contract.Requires<ArgumentException>( bytes.Length == 0 || bytes.Length - startIndex >= 16 );
         // ReSharper disable once PossibleNullReferenceException
         var length = bytes.Length;
         if(length == 0 || length <= startIndex)
         {
            return Guid.Empty;
         }
         fixed(Byte* b = &bytes[startIndex])
         {
            return order == ByteOrder.BigEndian
               ? new Guid(
                  (*b << 24) | (*(b + 1) << 16) | (*(b + 2) << 8) | *(b + 3),
                  (Int16)((*(b + 4) << 8) | *(b + 5)),
                  (Int16)((*(b + 6) << 8) | *(b + 7)),
                  *(b + 8),
                  *(b + 9),
                  *(b + 10),
                  *(b + 11),
                  *(b + 12),
                  *(b + 13),
                  *(b + 14),
                  *(b + 15) )
               : new Guid(
                  *(b + 15) << 24 | *(b + 14) << 16 | *(b + 13) << 8 | *(b + 12),
                  (Int16)(*(b + 11) << 8 | *(b + 10)),
                  (Int16)(*(b + 9) << 8 | *(b + 8)),
                  *(b + 7),
                  *(b + 6),
                  *(b + 5),
                  *(b + 4),
                  *(b + 3),
                  *(b + 2),
                  *(b + 1),
                  *(b + 0) );
         }
      }

      /// <summary>
      /// Convert byte array to <see cref="Int16" />
      /// </summary>
      public static Int16 ToInt16( [NotNull] this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         if(bytes == null)
         {
            throw new ArgumentNullException( nameof(bytes) );
         }
         //Contract.Requires<ArgumentNullException>( bytes != null );
         //Contract.Requires<ArgumentException>( startIndex >= 0 );
         // ReSharper disable once PossibleNullReferenceException
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
      public static unsafe Int32 ToInt32( [NotNull] this Byte[] bytes, Int32 startIndex = 0,
                                          ByteOrder endian = ByteOrder.LittleEndian )
      {
         if(bytes == null)
         {
            throw new ArgumentNullException( nameof(bytes) );
         }
         //Contract.Requires<ArgumentNullException>( bytes != null );
         //Contract.Requires<ArgumentException>( startIndex >= 0 );
         // ReSharper disable once PossibleNullReferenceException
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
      public static Int64 ToInt64( [NotNull] this Byte[] bytes, Int32 startIndex = 0, ByteOrder endian = ByteOrder.LittleEndian )
      {
         if(bytes == null)
         {
            throw new ArgumentNullException( nameof(bytes) );
         }
         //Contract.Requires<ArgumentNullException>( bytes != null );
         //Contract.Requires<ArgumentException>( startIndex >= 0 );
         // ReSharper disable once PossibleNullReferenceException
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
      public static UInt16 ToUInt16( [NotNull] this Byte[] bytes, Int32 startIndex = 0,
                                     ByteOrder endian = ByteOrder.LittleEndian )
      {
         if(bytes == null)
         {
            throw new ArgumentNullException( nameof(bytes) );
         }
         //Contract.Requires<ArgumentNullException>( bytes != null );
         //Contract.Requires<ArgumentException>( startIndex >= 0 );
         return (UInt16)ToInt16( bytes, startIndex, endian );
      }

      /// <summary>
      /// Convert byte array to <see cref="UInt32" />
      /// </summary>
      public static UInt32 ToUInt32( [NotNull] this Byte[] bytes, Int32 startIndex = 0,
                                     ByteOrder endian = ByteOrder.LittleEndian )
      {
         if(bytes == null)
         {
            throw new ArgumentNullException( nameof(bytes) );
         }
         //Contract.Requires<ArgumentNullException>( bytes != null );
         //Contract.Requires<ArgumentException>( startIndex >= 0 );
         return (UInt32)ToInt32( bytes, startIndex, endian );
      }

      /// <summary>
      /// Convert byte array to <see cref="UInt64" />
      /// </summary>
      public static UInt64 ToUInt64( [NotNull] this Byte[] bytes, Int32 startIndex = 0,
                                     ByteOrder endian = ByteOrder.LittleEndian )
      {
         if(bytes == null)
         {
            throw new ArgumentNullException( nameof(bytes) );
         }
         //Contract.Requires<ArgumentNullException>( bytes != null );
         //Contract.Requires<ArgumentException>( startIndex >= 0 );
         return (UInt64)ToInt64( bytes, startIndex, endian );
      }
   }
}
