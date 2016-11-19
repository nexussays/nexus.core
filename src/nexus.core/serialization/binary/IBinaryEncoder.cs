// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.core.serialization.binary
{
   /// <summary>
   /// Use <see cref="IBinaryEncoder" /> when your source data is a byte array and you want to convert that information
   /// losslessly to a human-readable string.
   /// Functionally, this means taking a byte array (base-2) and converting it into another base number system using
   /// characters as the symbols and resulting in a string as the encoded representation of the resulting value.
   /// <see cref="IBinaryEncoder" /> can
   /// 1. Serialize arbitrary bytes to a formatted string
   /// 2. Deserialize a formatted string to the original bytes
   /// </summary>
   public interface IBinaryEncoder
      : ISerializer<Byte[], String>,
        IDeserializer<String, Byte[]>
   {
      /// <summary>
      /// An indexed and ordered list of the characters used to represent each numeral in this base number system.
      /// </summary>
      IEnumerable<Char> SymbolTable { get; }
   }
}