// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace nexus.core.serialization.binary
{
   /// <summary>
   /// Use <see cref="Instance" /> instead.
   /// </summary>
   public sealed class Base16Encoder : IBinaryEncoder
   {
      public static readonly Base16Encoder Instance = new Base16Encoder();

      private readonly IList<Char> m_chars;

      private Base16Encoder()
      {
         m_chars = SymbolTable;
      }

      public IList<Char> SymbolTable
      {
         get { return "0123456789abcdef".ToCharArray(); }
      }

      /// <summary>
      /// Decodes a hexadecimal string into a byte array.
      /// </summary>
      /// <param name="source">The string value to convert</param>
      /// <returns>A byte array.</returns>
      /// <exception cref="FormatException">
      /// If any characters of the provided encoded string are not valid hex characters in the
      /// range [0-9A-Z].
      /// </exception>
      public Byte[] Deserialize( String source )
      {
         if(source == null)
         {
            return null;
         }

         // short-circuit when evaluating short strings
         switch(source.Length)
         {
            case 1:
               return new[] { Deserialize(source[0]) };
            case 2:
               return new[] { Deserialize(source[0], source[1]) };
         }
         return Deserialize(source.ToCharArray());
      }

      /// <exception cref="FormatException">If the provided characters are not valid base16 characters</exception>
      public Byte[] Deserialize( Char[] chars )
      {
         Contract.Requires(chars != null);
         Contract.Requires(chars.Length > 1);
         Contract.Ensures(Contract.Result<Byte[]>() != null);
         Contract.Ensures(Contract.Result<Byte[]>().Length == chars.Length / 2);

         var newBytes = new Byte[chars.Length / 2];
         var odd = chars.Length % 2 == 1;
         for(var x = 0; x < chars.Length; x += 2)
         {
            try
            {
               if(x == 0 && odd)
               {
                  // if there are an odd number of characters then read the first one in solo
                  newBytes[x] = (Byte)(0xf & m_chars.IndexOf(chars[x]));
               }
               else
               {
                  var index = (odd ? x + 1 : x) / 2;
                  Contract.Assume(index < newBytes.Length);
                  newBytes[index] = Deserialize(chars[x], chars[x + 1]);
               }
            }
            catch(Exception ex)
            {
               throw new FormatException(
                  "Character '{0}' at position {1} or '{2}' at position {3} is not a valid a base16 value.".F(
                     chars[x],
                     x,
                     chars[x + 1],
                     x + 1),
                  ex);
            }
         }
         return newBytes;
      }

      /// <param name="digit1">Should represent a character in the range [0-9A-Z] and therefore represent 4 bits.</param>
      /// <param name="digit2">Should represent a character in the range [0-9A-Z] and therefore represent 4 bits.</param>
      public Byte Deserialize( Char digit1, Char? digit2 = null )
      {
         return digit2 == null
            ? (Byte)(0xf & m_chars.IndexOf(digit1))
            : (Byte)(((0xf & m_chars.IndexOf(digit1)) << 4) | (0xf & m_chars.IndexOf(digit2.Value)));
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

      public String Serialize( Byte[] data )
      {
         if(data == null)
         {
            return null;
         }

         var result = new Char[data.Length * 2];
         var charIndex = 0;
         for(var x = 0; x < data.Length; x++)
         {
            result[charIndex++] = m_chars[((data[x] & 0xf0) >> 4)];
            result[charIndex++] = m_chars[(data[x] & 0x0f)];
         }
         return new String(result);
      }

      public String Serialize( Byte source )
      {
         return new String(new[] { m_chars[((source & 0xf0) >> 4)], m_chars[(source & 0x0f)] });
      }
   }
}