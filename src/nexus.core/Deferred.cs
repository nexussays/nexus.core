// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core
{
   /// <summary>
   /// Defer the act of obtaining a given value until it is requested by a getter. Basically a small wrapper which gives state
   /// to <see cref="Func{TResult}" /> by containing its return value upon first execution.
   /// </summary>
   /// <typeparam name="TResult"></typeparam>
   public struct Deferred<TResult>
   {
      private Func<TResult> m_retrieve;
      private TResult m_value;

      public Deferred( TResult value )
      {
         m_value = value;
         m_retrieve = null;
      }

      public Deferred( Func<TResult> retrieve )
      {
         m_retrieve = retrieve;
         m_value = default(TResult);
      }

      /// <summary>
      /// True if the deferred value has already been evaluated
      /// </summary>
      public Boolean IsResolved => m_retrieve == null;

      public Type Type => typeof(TResult);

      public TResult Value
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

      public static explicit operator TResult( Deferred<TResult> deferred )
      {
         return deferred.Value;
      }

      public static implicit operator Deferred<TResult>( Func<TResult> retrievalFunc )
      {
         return new Deferred<TResult>( retrievalFunc );
      }
   }

   public static class Deferred
   {
      public static Deferred<T> FromResult<T>( T source )
      {
         return new Deferred<T>( source );
      }

      public static Deferred<T> Of<T>( Func<T> source )
      {
         return new Deferred<T>( source );
      }
   }
}