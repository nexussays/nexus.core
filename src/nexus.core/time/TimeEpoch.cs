// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.time
{
   /// <summary>
   /// A named <see cref="DateTime" /> representing the start of an epoch.
   /// </summary>
   /// <see cref="ISynchronizedTimeProvider" />
   public sealed class TimeEpoch
   {
      public TimeEpoch( DateTime epochStart, String epochName )
      {
         EpochStart = epochStart;
         EpochName = epochName;
      }

      public String EpochName { get; }

      public DateTime EpochStart { get; }

      /// <summary>
      /// Epoch representing no value, starts at <see cref="DateTime.MinValue" />
      /// </summary>
      public static TimeEpoch None { get; } = new TimeEpoch( DateTime.MinValue, "n/a" );

      /// <summary>
      /// Epoch which starts at January 1, 1900 UTC
      /// </summary>
      public static TimeEpoch NtpEpoch { get; } = new TimeEpoch(
         new DateTime( 1900, 1, 1, 0, 0, 0, DateTimeKind.Utc ),
         "NTP" );

      /// <summary>
      /// Epoch which starts at January 1, 1970 UTC
      /// </summary>
      public static TimeEpoch UnixEpoch { get; } =
         new TimeEpoch( new DateTime( 1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc ), "Unix" );
   }
}