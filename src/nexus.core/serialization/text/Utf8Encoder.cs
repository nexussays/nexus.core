// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Text;

namespace nexus.core.serialization.text
{
   public sealed class Utf8Encoder : TextEncoder
   {
      public Utf8Encoder( Boolean useByteOrderMark, Boolean throwOnInvalidBytes )
         : base( new UTF8Encoding( useByteOrderMark, throwOnInvalidBytes ) )
      {

      }

      /// <summary>
      /// <see cref="Utf8Encoder" /> with byte-order-mark, and
      /// exceptions thrown on invalid bytes
      /// </summary>
      public static ITextEncoder WithBOM { get; } = new Utf8Encoder( true, true );

      /// <summary>
      /// <see cref="Utf8Encoder" /> with **no** byte-order-mark, and exceptions thrown on invalid bytes
      /// </summary>
      public static ITextEncoder WithoutBOM { get; } = new Utf8Encoder( false, true );
   }
}