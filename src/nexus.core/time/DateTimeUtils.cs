// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.time
{
   public static class DateTimeUtils
   {
      public static String ToIso8601String( this DateTime time, Boolean includeDelimeters = false )
      {
         return time.ToString( includeDelimeters ? "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK" : "yyyyMMdd'T'HHmmss.fffK" );
      }

      public static Int64 ToUnixTimestamp( this DateTime dateTime )
      {
         return (Int64)Math.Floor( dateTime.ToUniversalTime().Subtract( TimeEpoch.UnixEpoch.EpochStart ).TotalSeconds );
      }

      public static Int64 ToUnixTimestamp( this DateTime? dateTime )
      {
         return dateTime != null ? ToUnixTimestamp( dateTime.Value ) : 0;
      }

      public static Int64 ToUnixTimestampInMilliseconds( this DateTime? dateTime )
      {
         return dateTime != null ? ToUnixTimestampInMilliseconds( dateTime.Value ) : 0;
      }

      public static Int64 ToUnixTimestampInMilliseconds( this DateTime dateTime )
      {
         return
            (Int64)Math.Floor( dateTime.ToUniversalTime().Subtract( TimeEpoch.UnixEpoch.EpochStart ).TotalMilliseconds );
      }

      public static DateTime UnixTimestampInMillisecondsToDateTime( this Int64 millisecondsSinceEpoch )
      {
         return TimeEpoch.UnixEpoch.EpochStart.AddMilliseconds( millisecondsSinceEpoch );
      }

      public static DateTime UnixTimestampInMillisecondsToDateTime( this Double millisecondsSinceEpoch )
      {
         return TimeEpoch.UnixEpoch.EpochStart.AddMilliseconds( millisecondsSinceEpoch );
      }

      public static DateTime UnixTimestampToDateTime( this Int64 secondsSinceEpoch )
      {
         return TimeEpoch.UnixEpoch.EpochStart.AddSeconds( secondsSinceEpoch );
      }

      public static DateTime UnixTimestampToDateTime( this Int32 secondsSinceEpoch )
      {
         return TimeEpoch.UnixEpoch.EpochStart.AddSeconds( secondsSinceEpoch );
      }

      public static DateTime UnixTimestampToDateTime( this Double secondsSinceEpoch )
      {
         return TimeEpoch.UnixEpoch.EpochStart.AddSeconds( secondsSinceEpoch );
      }
   }
}