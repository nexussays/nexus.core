﻿// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace nexus.core.time
{
   public interface ITimeSource
   {
      EpochTime Epoch { get; }

      /// <summary>
      /// True if this time source has been synchronized with a time offset
      /// </summary>
      Boolean IsSynchronized { get; }

      /// <summary>
      /// The time difference between <see cref="DateTime.Now" /> and <see cref="ITimeSource.Now" />.
      /// </summary>
      TimeSpan? OffsetFromLocalEnvironment { get; }

      /// <summary>
      /// The time at which this <see cref="ITimeSource" /> was synchronized, if it indeed has been synchronized.
      /// </summary>
      DateTime? SynchronizedAt { get; }

      DateTime UtcNow { get; }
   }
}