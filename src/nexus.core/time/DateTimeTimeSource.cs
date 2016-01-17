// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace nexus.core.time
{
   public class DateTimeTimeSource : ITimeSource
   {
      public EpochTime Epoch => EpochTime.None;

      public Boolean IsSynchronized => false;

      public TimeSpan? OffsetFromLocalEnvironment => TimeSpan.Zero;

      public DateTime? SynchronizedAt => null;

      public DateTime UtcNow => DateTime.UtcNow;
   }
}