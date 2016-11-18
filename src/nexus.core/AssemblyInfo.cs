// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core
{
   public static class AssemblyInfo
   {
      public const String VERSION = "0.17.0";
      public const String VERSION_SHORT = VERSION;
      public const String ID = "nexus.core";

      public const String DESCRIPTION = "Core utilities and data structures";

      // ReSharper disable once ConditionIsAlwaysTrueOrFalse
      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif
   }
}