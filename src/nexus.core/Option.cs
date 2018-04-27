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
   /// A non-nullable type representing an optional value. If C# didn't fuck up and confuse nullability and value types this
   /// wouldn't be necessary, sigh.
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public struct Option<T>
      : IEquatable<Option<T>>,
        IEquatable<T>
   {
      /// <summary>
      ///    <c>default(Option&lt;T&gt;)</c>
      /// </summary>
      public static readonly Option<T> NoValue = default(Option<T>);

      private readonly T m_value;

      /// <summary>
      /// Construct an option that will have the provided value
      /// </summary>
      public Option( T value )
      {
         m_value = value;
         HasValue = true;
      }

      /// <summary>
      /// <c>true</c> if this option type contains a value.
      /// </summary>
      public Boolean HasValue { get; }

      /// <summary>
      /// The resolved value of this option type, or throws <see cref="InvalidOperationException" /> if <see cref="HasValue" />
      /// is
      /// <c>false</c>
      /// </summary>
      /// <exception cref="InvalidOperationException">If access is attempted when <see cref="HasValue" /> is false.</exception>
      public T Value
      {
         get
         {
            if(!HasValue)
            {
               throw new InvalidOperationException(
                  "Cannot retrieve value of Option<{0}> with no value.".F( typeof(T).FullName ) );
            }
            return m_value;
         }
      }

      /// <inheritdoc />
      [System.Diagnostics.Contracts.Pure]
      public override Boolean Equals( Object obj )
      {
         if(obj is Option<T>)
         {
            return Equals( (Option<T>)obj );
         }
         return HasValue && m_value.Equals( obj );
      }

      /// <inheritdoc />
      [System.Diagnostics.Contracts.Pure]
      public Boolean Equals( T other )
      {
         return HasValue && m_value.Equals( other );
      }

      /// <inheritdoc />
      [System.Diagnostics.Contracts.Pure]
      public Boolean Equals( Option<T> other )
      {
         return other.HasValue == HasValue && (!HasValue || Equals( m_value, other.m_value ));
      }

      /// <inheritdoc />
      public override Int32 GetHashCode()
      {
         return HasValue ? (ReferenceEquals( m_value, null ) ? -1 : m_value.GetHashCode()) : 0;
      }

      /// <inheritdoc />
      public override String ToString()
      {
         return HasValue ? (ReferenceEquals( m_value, null ) ? "null" : m_value.ToString()) : "[undefined]";
      }

      /// <summary>
      /// Implicitly wrap any <typeparamref name="T" /> to an <see cref="Option{T}" />
      /// </summary>
      public static implicit operator Option<T>( T value )
      {
         return new Option<T>( value );
      }
   }

   /// <summary>
   /// Create new <see cref="Option{T}"/> values
   /// </summary>
   public static class Option
   {
      /// <summary>
      ///    <c>default(Option&lt;Object&gt;)</c>
      /// </summary>
      public static readonly Option<Object> NoValue = default(Option<Object>);

      /// <summary>
      /// Create a new <see cref="Option{T}" /> from the provided <paramref name="value" />
      /// </summary>
      /// <typeparam name="T">The type pf the resultign <see cref="Option{T}" /></typeparam>
      /// <param name="value">The value to wrap in an option type</param>
      public static Option<T> Of<T>( T value )
      {
         return new Option<T>( value );
      }

      /// <summary>
      /// If <see cref="Option{T}.HasValue" /> is <c>true</c>, returns <see cref="Option{T}.Value" />, else returns
      /// <paramref name="alternateValue" />
      /// </summary>
      public static T Or<T>( this Option<T> option, T alternateValue )
      {
         return option.HasValue ? option.Value : alternateValue;
      }

      /// <summary>
      /// If <see cref="Option{T}.HasValue" /> is <c>true</c>, returns <see cref="Option{T}.Value" />, else resolves deferred
      /// <paramref name="alternateValue" />
      /// </summary>
      public static T Or<T>( this Option<T> option, Deferred<T> alternateValue )
      {
         return option.HasValue ? option.Value : alternateValue.Value;
      }

      /// <summary>
      /// If <see cref="Option{T}.HasValue" /> is <c>true</c>, returns <see cref="Option{T}.Value" />, else returns the result of
      /// executing <paramref name="alternateValue" />
      /// </summary>
      public static T Or<T>( this Option<T> option, [NotNull] Func<T> alternateValue )
      {
         if(alternateValue == null)
         {
            throw new ArgumentNullException( nameof(alternateValue) );
         }
         return option.HasValue ? option.Value : alternateValue();
      }

      /// <summary>
      /// If <see cref="Option{T}.HasValue" /> is <c>true</c>, returns <see cref="Option{T}.Value" />, else returns
      /// <paramref name="alternateValue" />
      /// </summary>
      public static Option<T> Or<T>( this Option<T> option, Option<T> alternateValue )
      {
         return option.HasValue ? option.Value : alternateValue;
      }

      /// <summary>
      /// If <see cref="Option{T}.HasValue" /> is <c>true</c>, returns <see cref="Option{T}.Value" />, else returns
      /// <c>default(T)</c>
      /// </summary>
      public static T OrDefault<T>( this Option<T> option )
      {
         return option.HasValue ? option.Value : default(T);
      }

      /// <summary>
      /// If <see cref="Option{T}.HasValue" /> is <c>true</c>, returns <see cref="Option{T}.Value" />, else returns
      /// <c>null</c>
      /// </summary>
      public static T OrNull<T>( this Option<T> option )
         where T : class
      {
         return option.HasValue ? option.Value : null;
      }

      /// <summary>
      /// If <see cref="Option{T}.HasValue" /> is <c>true</c>, returns <see cref="Option{T}.Value" />, else returns
      /// <c>T?</c>
      /// </summary>
      public static T? OrNullable<T>( this Option<T> option )
         where T : struct
      {
         return option.HasValue ? option.Value : (T?)null;
      }
   }
}
