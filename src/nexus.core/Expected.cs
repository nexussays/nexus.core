// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using nexus.core.resharper;

namespace nexus.core
{
   /// <summary>
   /// Represents an expected value or an exception if the value is not present
   /// </summary>
   //[StructLayout( LayoutKind.Explicit )]
   public struct Expected<T>
   {
      //[FieldOffset( 4 )]
      private readonly Exception m_error;
      //[FieldOffset( 0 )] // Boolean uses 4 bytes >.<
      //[FieldOffset( 4 )]
      private readonly T m_value;

      /// <summary>
      /// Create an expected with a valid value
      /// </summary>
      public Expected( T value )
      {
         HasValue = true;
         // required by compiler because value is readonly
         m_error = null;
         // don't set before assignment to m_error or it will be overridden
         m_value = value;
      }

      /// <summary>
      /// Create an expected with an exception
      /// </summary>
      public Expected( [NotNull] Exception error )
      {
         Contract.Requires( error != null );
         HasValue = false;
         // required by compiler because value is readonly
         m_value = default(T);
         // don't set before assignment to m_value or it will be overridden
         m_error = error;
      }

      /// <summary>
      /// The exception if this expected does not have a value
      /// </summary>
      public Exception Error => HasValue ? null : m_error;

      /// <summary>
      /// <c>true</c> if this expected has a value instead of an error
      /// </summary>
      public Boolean HasValue { get; }

      /// <summary>
      /// The value of this expected, or throws <see cref="InvalidOperationException" /> if this expected does not have a value.
      /// See <see cref="HasValue" />
      /// </summary>
      /// <exception cref="InvalidOperationException">If <see cref="HasValue" /> is <c>false</c></exception>
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
      // ReSharper disable once UnusedMember.Local
      private void Invariant()
      {
         // we have either a value or an error
         Contract.Invariant( HasValue || Error != null );
         // but we never have both
         Contract.Invariant( !(HasValue && Error != null) );
      }

      /// <summary>
      /// Create a <see cref="Expected{T}" /> from an exception
      /// </summary>
      public static implicit operator Expected<T>( Exception exception )
      {
         return new Expected<T>( exception );
      }

      /// <summary>
      /// Create a <see cref="Expected{T}" /> from a <paramref name="expectedValue" />
      /// </summary>
      public static implicit operator Expected<T>( T expectedValue )
      {
         return new Expected<T>( expectedValue );
      }
   }

   /// <summary>
   /// Static utility methods for <see cref="Expected{T}" />
   /// </summary>
   public static class Expected
   {
      /// <summary>
      /// Create a new failed expected value with the given exception
      /// </summary>
      public static Expected<T> No<T>( Exception value )
      {
         return new Expected<T>( value );
      }

      /// <summary>
      /// Create a new valid expected of the given value
      /// </summary>
      public static Expected<T> Of<T>( T value )
      {
         return new Expected<T>( value );
      }

      /// <summary>
      /// Create a new expected value from the given option, or <paramref name="exception" /> if <paramref name="option" /> has
      /// no value
      /// </summary>
      public static Expected<T> Of<T>( Option<T> option, Deferred<Exception> exception )
      {
         return option.HasValue ? Of( option.Value ) : No<T>( exception.Value );
      }

      /// <summary>
      /// Create a new expected value from the given option, or <paramref name="exception" /> if <paramref name="option" /> has
      /// no value
      /// </summary>
      public static Expected<T> Of<T>( Option<T> option, Lazy<Exception> exception )
      {
         return option.HasValue ? Of( option.Value ) : No<T>( exception.Value );
      }

      /// <summary>
      /// Create a new expected value from the given option, or <paramref name="exception" /> if <paramref name="option" /> has
      /// no value
      /// </summary>
      public static Expected<T> Of<T>( Option<T> option, Exception exception )
      {
         return option.HasValue ? Of( option.Value ) : No<T>( exception );
      }
   }
}
