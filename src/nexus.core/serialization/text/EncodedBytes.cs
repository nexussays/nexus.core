// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;

namespace nexus.core.serialization.text
{
   /// <summary>
   /// A string encoded to a Byte[]
   /// </summary>
   public class EncodedBytes
      : IEquatable<EncodedBytes>,
        IEquatable<Byte[]>,
        IEquatable<Option<EncodedBytes>>
   {
      private readonly ITextEncoder m_decoder;
      private readonly Byte[] m_value;

      public EncodedBytes( Byte[] value, ITextEncoder decoder )
      {
         Contract.Requires( value != null );
         Contract.Requires( decoder != null );
         m_value = value;
         m_decoder = decoder;
      }

      public Int32 Length => m_value.Length;

      public Byte[] Value
      {
         get
         {
            Contract.Ensures( Contract.Result<Byte[]>() != null );
            return m_value;
         }
      }

      public EncodedBytes Clone()
      {
         Contract.Ensures( Contract.Result<EncodedBytes>() != null );
         return new EncodedBytes( m_value, m_decoder );
      }

      public String Decode()
      {
         return m_decoder.Deserialize( m_value );
      }

      [Pure]
      public override Boolean Equals( Object obj )
      {
         if(obj is Option<EncodedBytes>)
         {
            return Equals( (Option<EncodedBytes>)obj );
         }
         return obj is Byte[] ? Equals( (Byte[])obj ) : Equals( obj as EncodedBytes );
      }

      [Pure]
      public Boolean Equals( Option<EncodedBytes> option )
      {
         return option.HasValue && Equals( option.Value );
      }

      [Pure]
      public Boolean Equals( EncodedBytes other )
      {
         return Equals( other.m_value );
      }

      [Pure]
      public Boolean Equals( Byte[] other )
      {
         return m_value.EqualsByteArray( other );
      }

      public override Int32 GetHashCode()
      {
         return m_value.GetHashCode();
      }

      public static Boolean operator ==( EncodedBytes l, EncodedBytes r )
      {
         if(ReferenceEquals( l, r ))
         {
            return true;
         }
         return !ReferenceEquals( null, l ) && l.Equals( r );
      }

      public static implicit operator Byte[]( EncodedBytes enc )
      {
         return enc == null ? null : enc.m_value;
      }

      public static Boolean operator !=( EncodedBytes l, EncodedBytes r )
      {
         return !(l == r);
      }
   }

   public static class EncodedBytesUtils
   {
      public static EncodedBytes SerializeWithEncoding( this ITextEncoder encoder, String source )
      {
         Contract.Requires( encoder != null );
         var str = encoder.Serialize( source );
         return str == null ? null : new EncodedBytes( str, encoder );
      }
   }
}