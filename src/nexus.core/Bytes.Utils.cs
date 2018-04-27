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
   public static partial class Bytes
   {
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

      /// <summary>
      ///    <code>bytes == null || bytes.Length &lt;= 0</code>
      /// </summary>
      public static Boolean IsNullOrEmpty( this Byte[] bytes )
      {
         return bytes == null || bytes.Length <= 0;
      }

      /// <summary>
      ///    <code>bytes == null || bytes.Length &lt;= 0 || (bytes.Length == 1 &amp;&amp; bytes[0] == 0)</code>
      /// </summary>
      public static Boolean IsNullOrEmptyOrNullByte( this Byte[] bytes )
      {
         return bytes == null || bytes.Length <= 0 || bytes.Length == 1 && bytes[0] == 0;
      }

      /// <summary>
      /// Copies the byte array starting at the provided index through the last element of the array.
      /// </summary>
      [NotNull]
      public static Byte[] Slice( [NotNull] this Byte[] source, Int32 startByteIndex = 0 )
      {
         if(source == null)
         {
            throw new ArgumentNullException( nameof(source) );
         }
         if(!(source.Length >= startByteIndex))
         {
            throw new ArgumentException(
               $"{nameof(source)}.Length must be >= {nameof(startByteIndex)}",
               nameof(source) );
         }
         if(!(startByteIndex >= 0))
         {
            throw new ArgumentException( $"{nameof(startByteIndex)} must be >= 0", nameof(startByteIndex) );
         }
         Contract.Ensures( Contract.Result<Byte[]>() != null );
         return Slice( source, startByteIndex, source.Length );
      }

      /// <summary>
      /// Copies the selected range of bytes from the source array
      /// </summary>
      [NotNull]
      public static Byte[] Slice( [NotNull] this Byte[] source, Int32 startByteIndex, Int32 endByteIndex )
      {
         if(source == null)
         {
            throw new ArgumentNullException( nameof(source) );
         }
         if(!(endByteIndex >= startByteIndex))
         {
            throw new ArgumentException(
               $"{nameof(endByteIndex)} must be >= {nameof(startByteIndex)}",
               nameof(endByteIndex) );
         }
         if(!(startByteIndex >= 0))
         {
            throw new ArgumentException( $"{nameof(startByteIndex)} must be >= 0", nameof(startByteIndex) );
         }
         Contract.Ensures( Contract.Result<Byte[]>() != null );
         Contract.Ensures( Contract.Result<Byte[]>().Length == endByteIndex - startByteIndex );
         var result = new Byte[endByteIndex - startByteIndex];
         Buffer.BlockCopy( source, startByteIndex, result, 0, result.Length );
         return result;
      }
   }
}
