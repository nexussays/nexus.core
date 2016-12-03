// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;

namespace nexus.core
{
   /// <summary>
   /// Utility methods for objects implementing <see cref="IComparable{T}" />
   /// </summary>
   public static class ComparableExtensions
   {
      /// <summary>
      /// Returns true if the value is in the given range (inclusive)
      /// </summary>
      public static Boolean Between<T>( this IComparable<T> value, T lower, T upper )
      {
         Contract.Requires( value != null );
         return value.CompareTo( lower ) >= 0 && value.CompareTo( upper ) <= 0;
      }

      /// <summary>
      /// Returns true if the value is in the given range (exclusive)
      /// </summary>
      public static Boolean Within<T>( this IComparable<T> value, T lower, T upper )
      {
         Contract.Requires( value != null );
         return value.CompareTo( lower ) > 0 && value.CompareTo( upper ) < 0;
      }
   }
}