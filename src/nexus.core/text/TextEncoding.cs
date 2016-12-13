// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace nexus.core.text
{
   public class TextEncoding : ITextEncoding
   {
      public TextEncoding( Encoding encoding )
      {
         Contract.Requires( encoding != null );
         Encoding = encoding;
      }

      public Encoding Encoding { get; }

      public Char[] AsCharArray( Byte[] sourceBytes )
      {
         return Encoding.GetChars( sourceBytes, 0, sourceBytes.Length );
      }

      public String AsString( Byte[] input )
      {
         return Encoding.GetString( input, 0, input.Length );
      }

      public override Boolean Equals( Object obj )
      {
         var enc = obj as TextEncoding;
         return enc != null && enc.Encoding == Encoding;
      }

      public Byte[] GetBytes( String data )
      {
         return Encoding.GetBytes( data );
      }

      public override Int32 GetHashCode()
      {
         return Encoding.GetHashCode();
      }

      public static explicit operator Encoding( TextEncoding encoding )
      {
         return encoding?.Encoding;
      }

      public static explicit operator TextEncoding( Encoding encoding )
      {
         return encoding == null ? null : new TextEncoding( encoding );
      }
   }
}