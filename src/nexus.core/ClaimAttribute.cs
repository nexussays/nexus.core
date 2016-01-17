// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace nexus.core
{
   /// <summary>
   /// Apply to constructor and (less often method) arguments to inform the caller that ownership, disposal, etc, is being
   /// transferred
   /// </summary>
   [AttributeUsage( AttributeTargets.Parameter )]
   public class ClaimAttribute : Attribute
   {
   }
}