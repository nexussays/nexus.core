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
   /// to
   /// a <see cref="Func{TResult}" /> by containing its return value upon first execution.
   /// </summary>
   /// <typeparam name="TResult"></typeparam>
   public class Deferred<TResult>
   {
      private Func<TResult> m_retrieve;
      private TResult m_value;

      public Deferred( TResult value )
      {
         m_value = value;
         IsResolved = true;
      }

      public Deferred( Func<TResult> retrieve )
      {
         m_retrieve = retrieve;
      }

      public Boolean IsResolved { get; private set; }

      public TResult Value
      {
         get
         {
            if(!IsResolved)
            {
               IsResolved = true;
               m_value = m_retrieve();
               m_retrieve = null;
            }
            return m_value;
         }
      }

      public static explicit operator TResult( Deferred<TResult> deferred )
      {
         return deferred == null ? default(TResult) : deferred.Value;
      }

      public static explicit operator Deferred<TResult>( TResult value )
      {
         return ReferenceEquals( value, null ) ? null : new Deferred<TResult>( value );
      }

      public static implicit operator Deferred<TResult>( Func<TResult> retrievalFunc )
      {
         return new Deferred<TResult>( retrievalFunc );
      }

      public static implicit operator Func<TResult>( Deferred<TResult> deferred )
      {
         return deferred == null ? null : deferred.m_retrieve;
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