// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core_test
{
   public static class AssemblyInfo
   {
      public const String VERSION = core.AssemblyInfo.VERSION;
      public const String VERSION_SHORT = core.AssemblyInfo.VERSION_SHORT;
      public const String ID = core.AssemblyInfo.ID + "_test";

      public const String DESCRIPTION = "Unit tests for " + core.AssemblyInfo.ID;

      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif
   }
}