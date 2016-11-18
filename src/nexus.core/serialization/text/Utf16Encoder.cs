// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Text;

namespace nexus.core.serialization.text
{
   public class Utf16Encoder : TextEncoder
   {
      public static readonly ITextEncoder WithBOM = new Utf16Encoder( true, false );
      public static readonly ITextEncoder WithoutBOM = new Utf16Encoder( false, false );

      public Utf16Encoder( Boolean useBom, Boolean bigEndian )
         : base( new UnicodeEncoding( bigEndian, useBom ) )
      {
      }
   }
}