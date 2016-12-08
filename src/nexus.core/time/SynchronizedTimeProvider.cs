// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.time
{
   public class SynchronizedTimeProvider : ISynchronizedTimeProvider
   {
      public SynchronizedTimeProvider()
      {
         Reset();
      }

      public TimeEpoch Epoch { get; private set; }

      public Boolean IsSynchronized => SynchronizedAt.HasValue;

      public TimeSpan? OffsetFromHostEnvironment { get; private set; }

      public DateTime? SynchronizedAt { get; private set; }

      public DateTime UtcNow
         =>
         OffsetFromHostEnvironment.HasValue
            ? DateTime.UtcNow.AddMilliseconds( OffsetFromHostEnvironment.Value.TotalMilliseconds )
            : DateTime.UtcNow;

      /// <summary>
      /// Synchronize to the provided millisecond offset from the provided epoch.
      /// </summary>
      /// <param name="epoch">The epoch being referenced in this synchronization.</param>
      /// <param name="msOffset"></param>
      public void Synchronize( TimeEpoch epoch, Int64 msOffset )
      {
         var localTime = DateTime.UtcNow;
         try
         {
            SynchronizedAt = epoch.EpochStart.AddMilliseconds( msOffset );
         }
         catch(Exception) // ArgumentException from EpochStart() or ArgumentOutOfRangeException from AddMilliseconds()
         {
            Reset();
            throw;
         }
         OffsetFromHostEnvironment = SynchronizedAt.Value - localTime;
         Epoch = epoch;
      }

      private void Reset()
      {
         SynchronizedAt = null;
         OffsetFromHostEnvironment = null;
         Epoch = TimeEpoch.None;
      }
   }
}