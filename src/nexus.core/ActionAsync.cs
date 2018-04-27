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
   /// <see cref="Action{T}" /> but returns Task. Equivalent to <see cref="Func{T1, Task}" />
   /// </summary>
   public delegate Task ActionAsync<in T1>( T1 arg1 );

   /// <summary>
   /// <see cref="Action{T1, T2}" /> but returns Task. Equivalent to <see cref="Func{T1, T2, Task}" />
   /// </summary>
   public delegate Task ActionAsync<in T1, in T2>( T1 arg1, T2 arg2 );

   /// <summary>
   /// <see cref="Action{T1, T2, T3}" /> but returns Task. Equivalent to <see cref="Func{T1, T2, T3, Task}" />
   /// </summary>
   public delegate Task ActionAsync<in T1, in T2, in T3>( T1 arg1, T2 arg2, T3 arg3 );

   /// <summary>
   /// <see cref="Action{T1, T2, T3, T4}" /> but returns Task. Equivalent to <see cref="Func{T1, T2, T3, T4, Task}" />
   /// </summary>
   public delegate Task ActionAsync<in T1, in T2, in T3, in T4>( T1 arg1, T2 arg2, T3 arg3, T4 arg4 );

   /// <summary>
   /// <see cref="Action{T1, T2, T3, T4, T5}" /> but returns Task. Equivalent to <see cref="Func{T1, T2, T3, T4, T5, Task}" />
   /// </summary>
   public delegate Task ActionAsync<in T1, in T2, in T3, in T4, in T5>( T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5 );

   /// <summary>
   /// <see cref="Action{T1, T2, T3, T4, T5, T6}" /> but returns Task. Equivalent to
   /// <see cref="Func{T1, T2, T3, T4, T5, T6, Task}" />
   /// </summary>
   public delegate Task ActionAsync
      <in T1, in T2, in T3, in T4, in T5, in T6>( T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6 );
}
