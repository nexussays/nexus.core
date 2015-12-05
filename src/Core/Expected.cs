// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Diagnostics.Contracts;

namespace nexus.core
{
   //[StructLayout( LayoutKind.Explicit )]
   public struct Expected<T>
   {
      //[FieldOffset( 4 )]
      private readonly Exception m_error;
      //[FieldOffset( 0 )] // Boolean uses 4 bytes >.<
      //[FieldOffset( 4 )]
      private readonly T m_value;

      public Expected( T value )
      {
         HasValue = true;
         // required by compiler because value is readonly
         m_error = null;
         // don't set before assignment to m_error or it will be overridden
         m_value = value;
      }

      public Expected( Exception error )
      {
         Contract.Requires( error != null );
         HasValue = false;
         // required by compiler because value is readonly
         m_value = default(T);
         // don't set before assignment to m_value or it will be overridden
         m_error = error;
      }

      public Exception Error
      {
         // should this throw like Value does?
         get { return HasValue ? null : m_error; }
      }

      public Boolean HasValue { get; }

      public T Value
      {
         get
         {
            if(!HasValue)
            {
               throw new InvalidOperationException(
                  "Cannot retrieve value of Expected<{0}> with no value.".F( typeof(T).FullName ) );
            }
            return m_value;
         }
      }

      [ContractInvariantMethod]
      private void Invariant()
      {
         // we have either a value or an error
         Contract.Invariant( HasValue || Error != null );
         // but we never have both
         Contract.Invariant( !(HasValue && Error != null) );
      }

      public static Expected<T> No( Exception value )
      {
         return new Expected<T>( value );
      }
   }

   public static class Expected
   {
      public static Expected<T> Of<T>( T value )
      {
         return new Expected<T>( value );
      }
   }
}