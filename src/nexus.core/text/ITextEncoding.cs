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
   /// Use <see cref="ITextEncoding" /> when your source data is a string and you want to convert that information
   /// losslessly to a byte array.
   /// <see cref="ITextEncoding" /> can
   /// 1. Encode an arbitrary string to formatted bytes
   /// 2. Decode formatted bytes to the original string
   /// </summary>
   public interface ITextEncoding
   {
      /// <summary>
      /// The underling <see cref="Encoding" /> in use
      /// </summary>
      Encoding Encoding { get; }

      /// <summary>
      /// Decode <paramref name="sourceBytes" /> into a character array according to this encoding
      /// </summary>
      Char[] AsCharArray( Byte[] sourceBytes );

      /// <summary>
      /// Decode <paramref name="sourceBytes" /> into a string according to this encoding
      /// </summary>
      String AsString( Byte[] sourceBytes );

      /// <summary>
      /// Get the string bytes according to this encoding
      /// </summary>
      Byte[] GetBytes( String stringValue );
   }
}
