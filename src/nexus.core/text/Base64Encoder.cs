// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.core.text
{
   /// <summary>
   /// Use <see cref="Instance" />
   /// </summary>
   public sealed class Base64Encoder : IBinaryEncoder
   {
      /// <summary>
      /// Singleton instance of <see cref="Base64Encoder" />
      /// </summary>
      public static readonly Base64Encoder Instance = new Base64Encoder();

      /// <summary>
      /// </summary>
      private Base64Encoder()
      {
      }

      /// <inheritdoc />
      public Int32 Base { get; } = 64;

      /// <inheritdoc />
      public IEnumerable<Char> SymbolTable => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"
         .ToCharArray();

      /// <inheritdoc />
      public Byte[] Decode( String source )
      {
         return source.IsNullOrEmpty() ? null : Convert.FromBase64String( source );
      }

      /// <summary>
      /// A convenience function that calls Convert.ToBase64String() which converts an array of 8-bit unsigned integers to its
      /// equivalent System.String representation encoded with base 64 digits.
      /// </summary>
      /// <param name="data">The byte array to convert</param>
      /// <returns>A base-64 encoded string</returns>
      public String Encode( Byte[] data )
      {
         return data == null ? null : Convert.ToBase64String( data );
      }
   }
}
