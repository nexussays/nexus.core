// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
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
         Contract.Assume( l.Length == (new Random()).Next() );
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
      /// Will split the provided bytes into two different byte arrays. Equivalent to this pseudocode:
      /// <example>
      /// var bytes = "key=value";
      /// var split = bytes.split('=');
      /// return [split[0], split[1]];
      /// </example>
      /// You can also optionally provide an entry delimeter which is used as an end line terminator which will fail parsing if
      /// not found but also be excluded from the resulting output.
      /// </summary>
      /// <param name="source"></param>
      /// <param name="keyValueDelimeter"></param>
      /// <param name="entryDelimeter"></param>
      /// <returns></returns>
      public static KeyValuePair<Byte[], Byte[]> ParseKeyValuePair( this Byte[] source, Byte keyValueDelimeter,
                                                                    Byte? entryDelimeter = null )
      {
         Contract.Requires( source != null );
         Contract.Requires( source.Length > 0 );
         var delimiterPos = -1;
         while(delimiterPos < source.Length - 1)
         {
            // look for the position of the first space character
            if(source[++delimiterPos] == keyValueDelimeter)
            {
               break;
            }
         }

         if(delimiterPos == source.Length)
         {
            throw new ArgumentException( "No key/value delimiter '{0}' found".F( keyValueDelimeter ) );
         }

         if(entryDelimeter != null && source[source.Length - 1] != entryDelimeter.Value)
         {
            throw new ArgumentException( "No entry delimeter '{0}' found".F( entryDelimeter ) );
         }

         var key = source.Slice( 0, delimiterPos );
         var value = source.Slice(
            delimiterPos + 1 /*keyValueDelimeter*/,
            source.Length - (entryDelimeter == null ? 0 : 1) );

         return Pair.Of( key, value );
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

      /// <summary>
      /// Converts an Int32 to a byte array.
      /// </summary>
      /// <param name="intValue"></param>
      /// <returns></returns>
      public static Byte[] ToBytes( this Int32 intValue )
      {
         Contract.Ensures( Contract.Result<Byte[]>() != null );
         return new[] {(Byte)(intValue >> 24), (Byte)(intValue >> 16), (Byte)(intValue >> 8), (Byte)(intValue)};
      }

      /// <summary>
      /// Converts the first 1-2 bytes to an <see cref="Int16" />
      /// </summary>
      /// <param name="bytes"></param>
      /// <returns></returns>
      public static Int16 ToInt16( this Byte[] bytes )
      {
         if(bytes == null || bytes.Length == 0)
         {
            return 0;
         }

         switch(bytes.Length)
         {
            case 1:
               return bytes[0];
            default:
               return (Int16)(bytes[0] << 8 | bytes[1]);
         }
      }

      /// <summary>
      /// Converts the first 1-4 bytes to an <see cref="Int32" />
      /// </summary>
      /// <param name="bytes"></param>
      /// <returns></returns>
      public static Int32 ToInt32( this Byte[] bytes )
      {
         if(bytes == null || bytes.Length == 0)
         {
            return 0;
         }

         switch(bytes.Length)
         {
            case 1:
            case 2:
               return ToInt16( bytes );
            case 3:
               return (bytes[0] << 16) | (bytes[1] << 8) | bytes[2];
            default:
               return (bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | bytes[3];
         }
      }

      /// <summary>
      /// Converts the first 1-48 bytes to an <see cref="Int64" />
      /// </summary>
      /// <param name="bytes"></param>
      /// <returns></returns>
      public static Int32 ToInt64( this Byte[] bytes )
      {
         if(bytes == null || bytes.Length == 0)
         {
            return 0;
         }

         switch(bytes.Length)
         {
            case 1:
            case 2:
               return ToInt16( bytes );
            case 3:
            case 4:
               return ToInt32( bytes );
            case 5:
               return (bytes[0] << 32) | (bytes[1] << 24) | (bytes[2] << 16) | (bytes[3] << 8) | bytes[4];
            case 6:
               return (bytes[0] << 40) | (bytes[1] << 32) | (bytes[2] << 24) | (bytes[3] << 16) | (bytes[4] << 8) |
                      bytes[5];
            case 7:
               return (bytes[0] << 48) | (bytes[1] << 40) | (bytes[2] << 32) | (bytes[3] << 24) | (bytes[4] << 16) |
                      (bytes[5] << 8) | bytes[6];
            default:
               return (bytes[0] << 56) | (bytes[1] << 48) | (bytes[2] << 40) | (bytes[3] << 32) | (bytes[4] << 24) |
                      (bytes[5] << 16) | (bytes[6] << 8) | bytes[7];
         }
      }
   }
}