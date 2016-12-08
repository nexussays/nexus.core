// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Text;
using nexus.core.serialization.binary;

namespace nexus.core.serialization.text
{
   /// <summary>
   /// Use <see cref="ITextEncoder" /> when your source data is a string and you want to convert that information
   /// losslessly to a byte array.
   /// This is the string/char equivalent to to <see cref="IBinaryEncoder" />.
   /// <see cref="ITextEncoder" /> can
   /// 1. Encode an arbitrary string to formatted bytes
   /// 2. Decode formatted bytes to the original string
   /// </summary>
   public interface ITextEncoder
      : ISerializer<String, Byte[]>,
        IDeserializer<Byte[], String>
   {
      Encoding Encoding { get; }

      Char[] DeserializeChars( Byte[] input );
   }
}