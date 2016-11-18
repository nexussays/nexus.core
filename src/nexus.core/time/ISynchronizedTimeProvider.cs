// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.time
{
   /// <summary>
   /// A <see cref="ITimeProvider" /> which can be synchronized to some standard time (eg, an NTP server). Allows you
   /// to provide a consistent time regardless of variances in the time reported by the local device, or to create a
   /// <see cref="ITimeProvider" /> set in a different period.
   /// </summary>
   public interface ISynchronizedTimeProvider : ITimeProvider
   {
      TimeEpoch Epoch { get; }

      /// <summary>
      /// True if this time source has been synchronized with a time offset
      /// </summary>
      Boolean IsSynchronized { get; }

      /// <summary>
      /// The time difference between <see cref="DateTime.UtcNow" /> and <see cref="ITimeProvider.UtcNow" />.
      /// </summary>
      TimeSpan? OffsetFromLocalEnvironment { get; }

      /// <summary>
      /// The time at which this <see cref="ITimeProvider" /> was synchronized, if it indeed has been synchronized.
      /// </summary>
      DateTime? SynchronizedAt { get; }
   }
}