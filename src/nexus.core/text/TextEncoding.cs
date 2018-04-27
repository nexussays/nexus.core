// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Text;
using nexus.core.resharper;

namespace nexus.core.text
{
   /// <inheritdoc />
   public class TextEncoding : ITextEncoding
   {
      /// <summary>
      /// </summary>
      public TextEncoding( [NotNull] Encoding encoding )
      {
         Encoding = encoding ?? throw new ArgumentNullException( nameof(encoding) );
      }

      /// <inheritdoc />
      [NotNull]
      public Encoding Encoding { get; }

      /// <inheritdoc />
      public Char[] AsCharArray( Byte[] sourceBytes )
      {
         return Encoding.GetChars( sourceBytes, 0, sourceBytes.Length );
      }

      /// <inheritdoc />
      public String AsString( Byte[] input )
      {
         return Encoding.GetString( input, 0, input.Length );
      }

      /// <inheritdoc />
      public override Boolean Equals( Object obj )
      {
         var enc = obj as TextEncoding;
         return enc != null && enc.Encoding == Encoding;
      }

      /// <inheritdoc />
      public Byte[] GetBytes( String data )
      {
         return Encoding.GetBytes( data );
      }

      /// <inheritdoc cref="object.GetHashCode" />
      public override Int32 GetHashCode()
      {
         return Encoding.GetHashCode();
      }

      /// <summary>
      /// Explicit type conversion for <see cref="Encoding" /> and <see cref="TextEncoding" />
      /// </summary>
      public static explicit operator Encoding( TextEncoding encoding )
      {
         return encoding?.Encoding;
      }

      /// <summary>
      /// Explicit type conversion for <see cref="Encoding" /> and <see cref="TextEncoding" />
      /// </summary>
      public static explicit operator TextEncoding( Encoding encoding )
      {
         return encoding == null ? null : new TextEncoding( encoding );
      }
   }
}
