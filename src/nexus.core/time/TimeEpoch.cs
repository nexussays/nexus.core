// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.time
{
   /// <summary>
   /// A named <see cref="DateTimeOffset" /> representing the start of an epoch. See <see cref="ITimeProvider" />
   /// </summary>
   public sealed class TimeEpoch
   {
      /// <summary>
      /// </summary>
      public TimeEpoch( DateTimeOffset epochStart, String epochName )
      {
         EpochStart = epochStart;
         EpochName = epochName;
      }

      /// <summary>
      /// Friendly-name for this epoch
      /// </summary>
      public String EpochName { get; }

      /// <summary>
      /// The point in time when this epoch starts
      /// </summary>
      public DateTimeOffset EpochStart { get; }

      /// <summary>
      /// Epoch representing no value, starts at <see cref="DateTime.MinValue" />
      /// </summary>
      public static TimeEpoch None { get; } = new TimeEpoch( DateTimeOffset.MinValue, "n/a" );

      /// <summary>
      /// Epoch which starts at January 1, 1900 UTC
      /// </summary>
      public static TimeEpoch NtpEpoch { get; } = new TimeEpoch(
         new DateTimeOffset( 1900, 1, 1, 0, 0, 0, 0, TimeSpan.Zero ),
         "NTP" );

      /// <summary>
      /// Epoch which starts at January 1, 1970 UTC
      /// </summary>
      public static TimeEpoch UnixEpoch { get; } = new TimeEpoch(
         new DateTimeOffset( 1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero ),
         "Unix" );
   }
}
