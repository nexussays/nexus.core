// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading;
using nexus.core.resharper;

namespace nexus.core
{
   /// <summary>
   /// Defer the act of obtaining a given value until it is requested by a getter. Basically a small wrapper which gives state
   /// to <see cref="Func{TResult}" /> by containing its return value upon first execution.
   /// </summary>
   public struct Deferred<T>
   {
      private Func<T> m_retrieve;
      private T m_value;

      /// <summary>
      /// Create an already-resolved deferred value
      /// </summary>
      public Deferred( T value )
      {
         m_retrieve = null;
         m_value = value;
      }

      /// <summary>
      /// Create a new deferred from this provided function. The function will be called once upon the deferred being resolved
      /// </summary>
      public Deferred( [NotNull] Func<T> retrieve )
      {
         m_retrieve = retrieve ?? throw new ArgumentNullException( nameof(retrieve) );
         m_value = default(T);
      }

      /// <summary>
      /// True if the deferred value has already been evaluated
      /// </summary>
      public Boolean IsResolved => m_retrieve == null;

      /// <summary>
      /// The type of value contained in this deferred
      /// </summary>
      public Type Type => typeof(T);

      /// <summary>
      /// Resolve the value of this deferred. If this is the first time the value has been resolved, any underlying code will be
      /// executed now.
      /// </summary>
      public T Value
      {
         get
         {
            if(!IsResolved)
            {
               m_value = m_retrieve();
               m_retrieve = null;
            }
            return m_value;
         }
      }

      /// <summary>
      /// Create a <see cref="Deferred{T}" /> from a function <paramref name="retrievalFunc" />
      /// </summary>
      public static implicit operator Deferred<T>( Func<T> retrievalFunc )
      {
         return new Deferred<T>( retrievalFunc );
      }

      /// <summary>
      /// Create a <see cref="Deferred{T}" /> from a <paramref name="resolvedValue" />
      /// </summary>
      public static implicit operator Deferred<T>( T resolvedValue )
      {
         return new Deferred<T>( resolvedValue );
      }
   }

   /// <summary>
   /// Static utility methods for <see cref="Deferred{T}" />
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Always )]
   public static class Deferred
   {
      /// <summary>
      /// Create an already-resolved <see cref="Deferred{T}" /> value
      /// </summary>
      public static Deferred<T> FromResult<T>( T resolvedValue )
      {
         return new Deferred<T>( resolvedValue );
      }

      /// <summary>
      /// Create a new <see cref="Deferred{T}" /> from this provided <paramref name="retrievalFunc" />. The function will be
      /// called once upon the deferred being resolved.
      /// </summary>
      public static Deferred<T> Of<T>( Func<T> retrievalFunc )
      {
         return new Deferred<T>( retrievalFunc );
      }
   }

   /// <summary>
   /// Extension methods for <see cref="Deferred{T}" />
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class DeferredExtensions
   {
      /// <summary>
      /// Create a <see cref="Deferred{T}" /> from a  <see cref="Lazy{T}" />
      /// </summary>
      public static Deferred<T> ToDeferred<T>( [NotNull] this Lazy<T> lazy )
      {
         return lazy.IsValueCreated ? new Deferred<T>( lazy.Value ) : new Deferred<T>( () => lazy.Value );
      }

      /// <summary>
      /// Convert a <see cref="Deferred{T}" /> to a <see cref="Lazy{T}" />
      /// </summary>
      public static Lazy<T> ToLazy<T>( this Deferred<T> deferred, LazyThreadSafetyMode mode )
      {
         return new Lazy<T>( () => deferred.Value, mode );
      }
   }
}
