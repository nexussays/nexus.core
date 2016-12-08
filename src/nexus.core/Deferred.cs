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
   /// Defer the act of obtaining a given value until it is requested by a getter. Basically a small wrapper which gives state
   /// to <see cref="Func{TResult}" /> by containing its return value upon first execution.
   /// </summary>
   /// <typeparam name="T"></typeparam>
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
      public Deferred( Func<T> retrieve )
      {
         Contract.Requires<ArgumentNullException>( retrieve != null );
         m_retrieve = retrieve;
         m_value = default(T);
      }

      /// <summary>
      /// True if the deferred value has already been evaluated
      /// </summary>
      public Boolean IsResolved => m_retrieve == null;

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
      /// Static operator to create a <see cref="Deferred{T}" /> from a <see cref="Func{T}" />
      /// </summary>
      /// <param name="retrievalFunc"></param>
      public static implicit operator Deferred<T>( Func<T> retrievalFunc )
      {
         return new Deferred<T>( retrievalFunc );
      }
   }

   public static class Deferred
   {
      /// <summary>
      /// Create an already-resolved deferred value
      /// </summary>
      public static Deferred<T> FromResult<T>( T source )
      {
         return new Deferred<T>( source );
      }

      /// <summary>
      /// Create a new deferred from this provided function. The function will be called once upon the deferred being resolved
      /// </summary>
      public static Deferred<T> Of<T>( Func<T> source )
      {
         return new Deferred<T>( source );
      }
   }
}