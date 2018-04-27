// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Threading.Tasks;

namespace nexus.core
{
   /// <summary>
   /// Provides completed task constants.
   /// </summary>
   public static class TaskConstants
   {
      /// <summary>
      /// A Task where <see cref="Task.IsCanceled" /> is true
      /// </summary>
      public static Task Canceled { get; } = CreateCanceledTask<Boolean>();

      /// <summary>
      /// A <see cref="Task" /> that will never complete.
      /// </summary>
      public static Task PerpetuallyIncomplete { get; } = new TaskCompletionSource<Boolean>().Task;

      /// <summary>
      /// Task.FromResult( false )
      /// </summary>
      public static Task<Boolean> ResultFalse { get; } = Task.FromResult( default(Boolean) );

      /// <summary>
      /// Task.FromResult( -1 )
      /// </summary>
      public static Task<Int32> ResultNegativeOne { get; } = Task.FromResult( -1 );

      /// <summary>
      /// Task.FromResult( true )
      /// </summary>
      public static Task<Boolean> ResultTrue { get; } = Task.FromResult( true );

      /// <summary>
      /// Task.FromResult( default(Int32) )
      /// </summary>
      public static Task<Int32> ResultZero { get; } = Task.FromResult( default(Int32) );

      internal static Task<T> CreateCanceledTask<T>()
      {
         var tcs = new TaskCompletionSource<T>();
         tcs.SetCanceled();
         return tcs.Task;
      }
   }
}
