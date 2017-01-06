// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

#pragma warning disable 1591

namespace nexus.core
{
   internal static partial class AssemblyInfo
   {
      public const String VERSION = "0.25.8";
      public const String VERSION_SHORT = VERSION;
      public const String PROJECT_ID = "nexus.core";
      public const String DESCRIPTION = "Cross-platform library of core utility methods and data structures.";

      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif
   }
}