// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using nexus.core.resharper;

namespace nexus.core.text
{
   /// <summary>
   /// Use <see cref="IBinaryEncoder" /> when your source data is a byte array and you want to convert that information
   /// losslessly to a human-readable string.
   /// Functionally, this means taking a byte array (base-2) and converting it into another base number system using
   /// characters as the symbols and resulting in a string as the encoded representation of the resulting value.
   /// Therefore use <see cref="IBinaryEncoder" /> to:
   /// 1. Serialize arbitrary bytes to a formatted string
   /// 2. Deserialize a formatted string to the original bytes
   /// </summary>
   public interface IBinaryEncoder
   {
      /// <summary>
      /// The number of symbols in the symbol table. I.e., the base of the number system of the encoded value.
      /// </summary>
      Int32 Base { get; }

      /// <summary>
      /// An indexed and ordered list of the characters used to represent each numeral in this base number system.
      /// </summary>
      [NotNull]
      IEnumerable<Char> SymbolTable { get; }

      /// <summary>
      /// Decode a formatted string to the original bytes
      /// </summary>
      Byte[] Decode( String formattedValue );

      /// <summary>
      /// Encode arbitrary bytes to a formatted string
      /// </summary>
      String Encode( Byte[] bytes );
   }
}
