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
   /// A <see cref="ITextEncoding"/> for UTF-16
   /// </summary>
   public sealed class Utf16Encoding : TextEncoding
   {
      /// <summary>
      /// </summary>
      public Utf16Encoding( Boolean useByteOrderMark, ByteOrder endianness, Boolean throwOnInvalidBytes )
         : base( new UnicodeEncoding( endianness == ByteOrder.BigEndian, useByteOrderMark, throwOnInvalidBytes ) )
      {
      }

      /// <summary>
      /// <see cref="Utf16Encoding" /> with byte-order-mark, endianness set to <see cref="Bytes.HostEnvironmentByteOrder" />, and
      /// exceptions thrown on invalid bytes
      /// </summary>
      public static ITextEncoding WithBOM { get; } = new Utf16Encoding( true, Bytes.HostEnvironmentByteOrder, true );

      /// <summary>
      /// <see cref="Utf16Encoding" /> with **no** byte-order-mark, endianness set to
      /// <see cref="Bytes.HostEnvironmentByteOrder" />, and exceptions thrown on invalid bytes
      /// </summary>
      public static ITextEncoding WithoutBOM { get; } =
         new Utf16Encoding( false, Bytes.HostEnvironmentByteOrder, true );
   }
}
