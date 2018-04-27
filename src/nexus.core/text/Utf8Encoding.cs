// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Text;

namespace nexus.core.text
{
   /// <summary>
   /// A <see cref="ITextEncoding"/> for UTF-8
   /// </summary>
   public sealed class Utf8Encoding : TextEncoding
   {
      /// <summary>
      /// </summary>
      public Utf8Encoding( Boolean useByteOrderMark, Boolean throwOnInvalidBytes )
         : base( new UTF8Encoding( useByteOrderMark, throwOnInvalidBytes ) )
      {
      }

      /// <summary>
      /// <see cref="Utf8Encoding" /> with byte-order-mark, and
      /// exceptions thrown on invalid bytes
      /// </summary>
      public static ITextEncoding WithBOM { get; } = new Utf8Encoding( true, true );

      /// <summary>
      /// <see cref="Utf8Encoding" /> with **no** byte-order-mark, and exceptions thrown on invalid bytes
      /// </summary>
      public static ITextEncoding WithoutBOM { get; } = new Utf8Encoding( false, true );
   }
}
