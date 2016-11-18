// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Threading.Tasks;

namespace nexus.core
{
   public delegate Task ActionAsync<in T1>( T1 arg1 );

   public delegate Task ActionAsync<in T1, in T2>( T1 arg1, T2 arg2 );

   public delegate Task ActionAsync<in T1, in T2, in T3>( T1 arg1, T2 arg2, T3 arg3 );

   public delegate Task ActionAsync<in T1, in T2, in T3, in T4>( T1 arg1, T2 arg2, T3 arg3, T4 arg4 );

   public delegate Task ActionAsync<in T1, in T2, in T3, in T4, in T5>( T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5 );

   public delegate Task ActionAsync<in T1, in T2, in T3, in T4, in T5, in T6>(
      T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6 );
}