// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using nexus.core.resharper;

namespace nexus.core.text
{
   /// <summary>
   /// Use <see cref="Lowercase" /> or <see cref="Uppercase" />
   /// </summary>
   public sealed class Base16Encoder : IBinaryEncoder
   {
      /// <summary>
      /// Singleton instance of <see cref="Base16Encoder" />
      /// </summary>
      public static readonly Base16Encoder Lowercase = new Base16Encoder( SymbolsLowercase );
      /// <summary>
      /// Singleton instance of <see cref="Base16Encoder" />
      /// </summary>
      public static readonly Base16Encoder Uppercase = new Base16Encoder( SymbolsUppercase );

      private readonly IList<Char> m_symbols;

      /// <summary>
      /// Create a new base16 encoder with a custom symbol table
      /// </summary>
      public Base16Encoder( [NotNull] IList<Char> symbolTable )
      {
         if(symbolTable == null)
         {
            throw new ArgumentNullException(
               nameof(symbolTable),
               "Symbol table provided to " + nameof(Base16Encoder) + " cannot be null" );
         }
         if(symbolTable.Count != 16)
         {
            throw new ArgumentException(
               "Symbol table provided to  " + nameof(Base16Encoder) + " must contain exactly 16 characters",
               nameof(symbolTable) );
         }
         m_symbols = symbolTable ?? throw new ArgumentNullException( nameof(symbolTable) );
      }

      /// <inheritdoc />
      public Int32 Base { get; } = 16;

      /// <summary>
      /// Hex symbols using lower case a-f
      /// </summary>
      [NotNull]
      public static Char[] SymbolsLowercase => "0123456789abcdef".ToCharArray();

      /// <summary>
      /// Hex symbols using uppercase A-F
      /// </summary>
      [NotNull]
      public static Char[] SymbolsUppercase => "0123456789ABCDEF".ToCharArray();

      /// <inheritdoc />
      public IEnumerable<Char> SymbolTable => new List<Char>( m_symbols );

      /// <summary>
      /// Decodes a hexadecimal string into a byte array.
      /// </summary>
      /// <param name="source">The string value to convert</param>
      /// <returns>A byte array.</returns>
      /// <exception cref="FormatException">
      /// If any characters of the provided encoded string are not valid hex characters in the
      /// range [0-9A-Z].
      /// </exception>
      public Byte[] Decode( String source )
      {
         if(source.IsNullOrEmpty())
         {
            return null;
         }

         // short-circuit when evaluating short strings
         switch(source.Length)
         {
            case 1: return new[] {Decode( source[0], null )};
            case 2: return new[] {Decode( source[0], source[1] )};
            default: return Decode( source.ToCharArray() );
         }
      }

      /// <exception cref="FormatException">If any characters provided cannot be found in the symbol table</exception>
      [NotNull]
      public Byte[] Decode( [NotNull] Char[] chars )
      {
         Contract.Requires( chars != null );
         Contract.Requires( chars.Length > 1 );
         Contract.Ensures( Contract.Result<Byte[]>() != null );
         Contract.Ensures( Contract.Result<Byte[]>().Length == chars.Length / 2 );

         var newBytes = new Byte[chars.Length / 2];
         var odd = chars.Length % 2 == 1;
         for(var x = 0; x < chars.Length; x += 2)
         {
            try
            {
               if(x == 0 && odd)
               {
                  // if there are an odd number of characters then read the first one in solo
                  newBytes[x] = (Byte)(0xf & m_symbols.IndexOf( chars[x] ));
               }
               else
               {
                  var index = (odd ? x + 1 : x) / 2;
                  Contract.Assume( index < newBytes.Length );
                  newBytes[index] = Decode( chars[x], chars[x + 1] );
               }
            }
            catch(Exception ex)
            {
               throw new FormatException(
                  "One of char '{0}' or char '{1}' is not a valid encoded value and cannot be deserialized.".F(
                     chars.Length >= x ? chars[x] : default(Char),
                     chars.Length >= x + 1 ? chars[x + 1] : default(Char) ),
                  ex );
            }
         }
         return newBytes;
      }

      /// <summary>
      /// Decode one or two hex characters to a single byte
      /// </summary>
      public Byte Decode( Char digit1, Char? digit2 = null )
      {
         return digit2 == null
            ? (Byte)(0xf & m_symbols.IndexOf( digit1 ))
            : (Byte)(((0xf & m_symbols.IndexOf( digit1 )) << 4) | (0xf & m_symbols.IndexOf( digit2.Value )));
      }

      /*
      private static Char GetHexDigit( Int32 num, Boolean uppercase = false )
      {
         // return the value plus a character encoding offset:
         // if num < 10, return ASCII for 0-9 (48 is "0")
         // if num > 10 return ASCII for A-F or a-f depending on range value
         // 87 + num as an offset == 97 == 'a'
         // 55 + num as an offset == 65 == 'A'
         var asciiStart = uppercase ? 55 : 87;
         return ((num < 10) ? ((Char)((UInt16)(num + 48))) : ((Char)((UInt16)(asciiStart + num))));
      }
      //*/

      /// <inheritdoc />
      public String Encode( Byte[] data )
      {
         if(data == null)
         {
            return null;
         }

         var result = new Char[data.Length * 2];
         var charIndex = 0;
         foreach(var d in data)
         {
            result[charIndex++] = m_symbols[(d & 0xf0) >> 4];
            result[charIndex++] = m_symbols[d & 0x0f];
         }
         return new String( result );
      }

      /// <inheritdoc cref="Encode(byte[])" />
      public String Encode( Byte source )
      {
         return new String( new[] {m_symbols[(source & 0xf0) >> 4], m_symbols[source & 0x0f]} );
      }
   }
}
