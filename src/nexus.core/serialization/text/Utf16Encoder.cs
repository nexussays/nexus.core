// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Text;

namespace nexus.core.serialization.text
{
   public sealed class Utf16Encoder : TextEncoder
   {
      public Utf16Encoder( Boolean useByteOrderMark, ByteOrder endianness, Boolean throwOnInvalidBytes )
         : base( new UnicodeEncoding( endianness == ByteOrder.BigEndian, useByteOrderMark, throwOnInvalidBytes ) )
      {
      }

      /// <summary>
      /// <see cref="Utf16Encoder" /> with byte-order-mark, endianness set to <see cref="Bytes.HostEnvironmentByteOrder" />, and
      /// exceptions thrown on invalid bytes
      /// </summary>
      public static ITextEncoder WithBOM { get; } = new Utf16Encoder( true, Bytes.HostEnvironmentByteOrder, true );

      /// <summary>
      /// <see cref="Utf16Encoder" /> with **no** byte-order-mark, endianness set to
      /// <see cref="Bytes.HostEnvironmentByteOrder" />, and exceptions thrown on invalid bytes
      /// </summary>
      public static ITextEncoder WithoutBOM { get; } = new Utf16Encoder( false, Bytes.HostEnvironmentByteOrder, true );
   }
}