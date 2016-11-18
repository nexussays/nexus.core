// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace nexus.core.serialization.text
{
   public class TextEncoder : ITextEncoder
   {
      public TextEncoder( Encoding encoding )
      {
         Contract.Requires( encoding != null );
         Encoding = encoding;
      }

      public Encoding Encoding { get; }

      public String Deserialize( Byte[] input )
      {
         return Encoding.GetString( input, 0, input.Length );
      }

      public Char[] DeserializeChars( Byte[] input )
      {
         return Encoding.GetChars( input, 0, input.Length );
      }

      public override Boolean Equals( Object obj )
      {
         var enc = obj as TextEncoder;
         return enc != null && enc.Encoding == Encoding;
      }

      public override Int32 GetHashCode()
      {
         return Encoding.GetHashCode();
      }

      public Byte[] Serialize( String data )
      {
         return Encoding.GetBytes( data );
      }

      public static explicit operator Encoding( TextEncoder encoder )
      {
         return encoder?.Encoding;
      }

      public static explicit operator TextEncoder( Encoding encoding )
      {
         return encoding == null ? null : new TextEncoder( encoding );
      }
   }
}