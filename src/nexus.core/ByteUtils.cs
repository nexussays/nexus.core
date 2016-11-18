// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;

namespace nexus.core
{
   public static class ByteUtils
   {
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

      public static Boolean IsNullOrNullByte( this Byte[] bytes )
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

      public static Byte[] ToBytes( this Int16 value )
      {
         return BitConverter.GetBytes( value );
      }

      public static Byte[] ToBytes( this Int32 value )
      {
         return BitConverter.GetBytes( value );
      }

      public static Byte[] ToBytes( this Int64 value )
      {
         return BitConverter.GetBytes( value );
      }

      public static Byte[] ToBytes( this UInt16 value )
      {
         return BitConverter.GetBytes( value );
      }

      public static Byte[] ToBytes( this UInt32 value )
      {
         return BitConverter.GetBytes( value );
      }

      public static Byte[] ToBytes( this UInt64 value )
      {
         return BitConverter.GetBytes( value );
      }

      public static Byte[] ToBytes( this Single value )
      {
         return BitConverter.GetBytes( value );
      }

      public static Byte[] ToBytes( this Double value )
      {
         return BitConverter.GetBytes( value );
      }

      public static Double ToDouble( this Byte[] bytes, Int32 startIndex = 0 )
      {
         return BitConverter.ToDouble( bytes, startIndex );
      }

      public static Int16 ToInt16( this Byte[] bytes, Int32 startIndex = 0 )
      {
         return BitConverter.ToInt16( bytes, startIndex );
      }

      public static Int32 ToInt32( this Byte[] bytes, Int32 startIndex = 0 )
      {
         return BitConverter.ToInt32( bytes, startIndex );
      }

      public static Int64 ToInt64( this Byte[] bytes, Int32 startIndex = 0 )
      {
         return BitConverter.ToInt64( bytes, startIndex );
      }

      public static UInt16 ToIUnt16( this Byte[] bytes, Int32 startIndex = 0 )
      {
         return BitConverter.ToUInt16( bytes, startIndex );
      }

      public static UInt32 ToUInt32( this Byte[] bytes, Int32 startIndex = 0 )
      {
         return BitConverter.ToUInt32( bytes, startIndex );
      }

      public static UInt64 ToUInt64( this Byte[] bytes, Int32 startIndex = 0 )
      {
         return BitConverter.ToUInt64( bytes, startIndex );
      }
   }
}