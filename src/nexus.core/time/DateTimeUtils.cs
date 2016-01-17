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
      public static Int64 ToUnixTimestamp( this DateTime dateTime )
      {
         return (Int64)Math.Floor( dateTime.ToUniversalTime().Subtract( EpochTime.Unix.EpochStart() ).TotalSeconds );
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
            (Int64)Math.Floor( dateTime.ToUniversalTime().Subtract( EpochTime.Unix.EpochStart() ).TotalMilliseconds );
      }

      public static DateTime UnixTimestampInMillisecondsToDateTime( this Int64 millisecondsSinceEpoch )
      {
         return EpochTime.Unix.EpochStart().AddMilliseconds( millisecondsSinceEpoch );
      }

      public static DateTime UnixTimestampInMillisecondsToDateTime( this Double millisecondsSinceEpoch )
      {
         return EpochTime.Unix.EpochStart().AddMilliseconds( millisecondsSinceEpoch );
      }

      public static DateTime UnixTimestampToDateTime( this Int64 secondsSinceEpoch )
      {
         return EpochTime.Unix.EpochStart().AddSeconds( secondsSinceEpoch );
      }

      public static DateTime UnixTimestampToDateTime( this Int32 secondsSinceEpoch )
      {
         return EpochTime.Unix.EpochStart().AddSeconds( secondsSinceEpoch );
      }

      public static DateTime UnixTimestampToDateTime( this Double secondsSinceEpoch )
      {
         return EpochTime.Unix.EpochStart().AddSeconds( secondsSinceEpoch );
      }
   }
}