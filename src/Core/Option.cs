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
   /// A non-nullable type representing an optional value. If C# didn't fuck up and confuse nullability and value types this wouldn't be necessary, sigh.
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public struct Option<T>
      : IEquatable<Option<T>>,
        IEquatable<T>
   {
      public static readonly Option<T> NoValue = default(Option<T>);

      private readonly T m_value;

      public Option( T value )
      {
         m_value = value;
         HasValue = true;
      }

      /// <summary>
      /// The value of this option, if available. Ensure you check <see cref="HasValue" /> before accessing this property.
      /// </summary>
      /// <exception cref="InvalidOperationException">If access is attempted when <see cref="HasValue" /> is false.</exception>
      public Boolean HasValue { get; }

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

      [Pure]
      public override Boolean Equals( Object obj )
      {
         if(obj is Option<T>)
         {
            return Equals( (Option<T>)obj );
         }
         return HasValue && m_value.Equals( obj );
      }

      [Pure]
      public Boolean Equals( T other )
      {
         return HasValue && m_value.Equals( other );
      }

      [Pure]
      public Boolean Equals( Option<T> other )
      {
         return (other.HasValue == HasValue) && (!HasValue || Equals( m_value, other.m_value ));
      }

      public override Int32 GetHashCode()
      {
         return HasValue ? (ReferenceEquals( m_value, null ) ? -1 : m_value.GetHashCode()) : 0;
      }

      public override String ToString()
      {
         return HasValue ? (ReferenceEquals( m_value, null ) ? "null" : m_value.ToString()) : "No Value";
      }

      public static implicit operator Option<T>( T value )
      {
         return new Option<T>( value );
      }
   }

   public static class Option
   {
      public static readonly Option<Object> NoValue = default(Option<Object>);

      public static Option<T> Of<T>( T value )
      {
         return new Option<T>( value );
      }

      public static T OrDefault<T>( this Option<T> option )
      {
         return option.HasValue ? option.Value : default(T);
      }

      public static T OrNull<T>( this Option<T> option ) where T : class
      {
         return option.HasValue ? option.Value : null;
      }

      public static T? OrNullable<T>( this Option<T> option ) where T : struct
      {
         return option.HasValue ? option.Value : (T?)null;
      }
   }
}