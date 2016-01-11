// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace nexus.core.time
{
   /// <summary>
   /// Creates a <see cref="ITimeSource" /> which can be synchronized to some standard time (eg, an NTP server). Allows you to
   /// provide a consistent time regardless of variances in the time reported by the local device, or to create a
   /// <see cref="ITimeSource" /> set in a different period.
   /// </summary>
   public class SynchronizedTimeSource : ITimeSource
   {
      public SynchronizedTimeSource()
      {
         Reset();
      }

      public EpochTime Epoch { get; private set; }

      public Boolean IsSynchronized => SynchronizedAt.HasValue;

      public TimeSpan? OffsetFromLocalEnvironment { get; private set; }

      public DateTime? SynchronizedAt { get; private set; }

      public DateTime UtcNow
         =>
            OffsetFromLocalEnvironment.HasValue
               ? DateTime.UtcNow.AddMilliseconds( OffsetFromLocalEnvironment.Value.TotalMilliseconds )
               : DateTime.UtcNow;

      /// <summary>
      /// Synchronize to the provided millisecond offset from the provided epoch.
      /// </summary>
      /// <param name="milliseconds">The number of milliseconds that have elapsed since the start of the given epoch.</param>
      /// <param name="epoch">The epoch being referenced in this synchronization.</param>
      public void Synchronize( Int64 milliseconds, EpochTime epoch )
      {
         var localTime = DateTime.UtcNow;
         try
         {
            SynchronizedAt = epoch.EpochStart().AddMilliseconds( milliseconds );
         }
         catch(Exception) // ArgumentException from EpochStart() or ArgumentOutOfRangeException from AddMilliseconds()
         {
            Reset();
            throw;
         }
         OffsetFromLocalEnvironment = SynchronizedAt.Value - localTime;
         Epoch = epoch;
      }

      private void Reset()
      {
         SynchronizedAt = null;
         OffsetFromLocalEnvironment = null;
         Epoch = EpochTime.None;
      }
   }
}