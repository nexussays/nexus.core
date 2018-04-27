// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.time
{
   /// <summary>
   /// Extension and utility methods for <see cref="DateTime" /> and similar
   /// </summary>
   public static class DateTimeUtils
   {
      /// <summary>
      /// <code>yyyyMMddTHHmmssfffK</code> if <paramref name="includeDelimeters" /> is <c>false</c>, or
      /// <code>yyyy-MM-ddTHH:mm:ss.fffK</code> if <paramref name="includeDelimeters" /> is <c>true</c>
      /// </summary>
      /// <param name="time"></param>
      /// <param name="includeDelimeters"><c>true</c> to include '-', ':' and '.' in output</param>
      public static String ToIso8601String( this DateTime time, Boolean includeDelimeters = false )
      {
         return time.ToUniversalTime().ToString(
            includeDelimeters ? "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK" : "yyyyMMdd'T'HHmmssfffK" );
      }

      /// <summary>
      /// yyyy-MM-ddTHH:mm:ss.fffK
      /// </summary>
      /// <param name="time"></param>
      /// <param name="includeDelimeters"><c>true</c> to include '-', ':' and '.' in output</param>
      public static String ToIso8601String( this DateTimeOffset time, Boolean includeDelimeters = false )
      {
         return ToIso8601String( time.UtcDateTime );
      }

      /// <summary>
      /// Total seconds elapsed since January 1, 1970 UTC
      /// </summary>
      public static Double ToUnixTimestamp( this DateTime dateTime )
      {
         return Math.Floor(
            dateTime.ToUniversalTime().Subtract( TimeEpoch.UnixEpoch.EpochStart.UtcDateTime ).TotalSeconds );
      }

      /// <summary>
      /// Total seconds elapsed since January 1, 1970 UTC
      /// </summary>
      public static Double ToUnixTimestamp( this DateTimeOffset dateTime )
      {
         return Math.Floor( dateTime.Subtract( TimeEpoch.UnixEpoch.EpochStart ).TotalSeconds );
      }

      /// <summary>
      /// Total seconds elapsed since January 1, 1970 UTC
      /// </summary>
      public static Double ToUnixTimestamp( this DateTime? dateTime )
      {
         return dateTime != null ? ToUnixTimestamp( dateTime.Value ) : 0;
      }

      /// <summary>
      /// Total seconds elapsed since January 1, 1970 UTC
      /// </summary>
      public static Double ToUnixTimestamp( this DateTimeOffset? dateTime )
      {
         return dateTime != null ? ToUnixTimestamp( dateTime.Value ) : 0;
      }

      /// <summary>
      /// Total milliseconds elapsed since January 1, 1970 UTC
      /// </summary>
      public static Int64 ToUnixTimestampInMilliseconds( this DateTime? dateTime )
      {
         return dateTime != null ? ToUnixTimestampInMilliseconds( dateTime.Value ) : 0;
      }

      /// <summary>
      /// Total milliseconds elapsed since January 1, 1970 UTC
      /// </summary>
      public static Int64 ToUnixTimestampInMilliseconds( this DateTimeOffset? dateTime )
      {
         return dateTime != null ? ToUnixTimestampInMilliseconds( dateTime.Value ) : 0;
      }

      /// <summary>
      /// Total milliseconds elapsed since January 1, 1970 UTC
      /// </summary>
      public static Int64 ToUnixTimestampInMilliseconds( this DateTime dateTime )
      {
         return (Int64)Math.Floor(
            dateTime.ToUniversalTime().Subtract( TimeEpoch.UnixEpoch.EpochStart.UtcDateTime ).TotalMilliseconds );
      }

      /// <summary>
      /// Total milliseconds elapsed since January 1, 1970 UTC
      /// </summary>
      public static Int64 ToUnixTimestampInMilliseconds( this DateTimeOffset dateTime )
      {
         return (Int64)Math.Floor( dateTime.Subtract( TimeEpoch.UnixEpoch.EpochStart ).TotalMilliseconds );
      }

      /// <summary>
      /// Convert milliseconds elapsed since January 1, 1970 UTC into a <see cref="DateTime" />
      /// </summary>
      public static DateTime UnixTimestampInMillisecondsToDateTime( this Int64 millisecondsSinceEpoch )
      {
         return UnixTimestampInMillisecondsToDateTimeOffset( millisecondsSinceEpoch ).UtcDateTime;
      }

      /// <summary>
      /// Convert milliseconds elapsed since January 1, 1970 UTC into a <see cref="DateTime" />
      /// </summary>
      public static DateTime UnixTimestampInMillisecondsToDateTime( this Double millisecondsSinceEpoch )
      {
         return UnixTimestampInMillisecondsToDateTimeOffset( millisecondsSinceEpoch ).UtcDateTime;
      }

      /// <summary>
      /// Convert milliseconds elapsed since January 1, 1970 UTC into a <see cref="DateTime" />
      /// </summary>
      public static DateTimeOffset UnixTimestampInMillisecondsToDateTimeOffset( this Int64 millisecondsSinceEpoch )
      {
         return TimeEpoch.UnixEpoch.EpochStart.AddMilliseconds( millisecondsSinceEpoch );
      }

      /// <summary>
      /// Convert milliseconds elapsed since January 1, 1970 UTC into a <see cref="DateTime" />
      /// </summary>
      public static DateTimeOffset UnixTimestampInMillisecondsToDateTimeOffset( this Double millisecondsSinceEpoch )
      {
         return TimeEpoch.UnixEpoch.EpochStart.AddMilliseconds( millisecondsSinceEpoch );
      }

      /// <summary>
      /// Convert seconds elapsed since January 1, 1970 UTC into a <see cref="DateTime" />
      /// </summary>
      public static DateTime UnixTimestampToDateTime( this Int64 secondsSinceEpoch )
      {
         return UnixTimestampToDateTimeOffset( secondsSinceEpoch ).UtcDateTime;
      }

      /// <summary>
      /// Convert seconds elapsed since January 1, 1970 UTC into a <see cref="DateTime" />
      /// </summary>
      public static DateTime UnixTimestampToDateTime( this Int32 secondsSinceEpoch )
      {
         return UnixTimestampToDateTimeOffset( secondsSinceEpoch ).UtcDateTime;
      }

      /// <summary>
      /// Convert seconds elapsed since January 1, 1970 UTC into a <see cref="DateTime" />
      /// </summary>
      public static DateTime UnixTimestampToDateTime( this Double secondsSinceEpoch )
      {
         return UnixTimestampToDateTimeOffset( secondsSinceEpoch ).UtcDateTime;
      }

      /// <summary>
      /// Convert seconds elapsed since January 1, 1970 UTC into a <see cref="DateTime" />
      /// </summary>
      public static DateTimeOffset UnixTimestampToDateTimeOffset( this Int64 secondsSinceEpoch )
      {
         return TimeEpoch.UnixEpoch.EpochStart.AddSeconds( secondsSinceEpoch );
      }

      /// <summary>
      /// Convert seconds elapsed since January 1, 1970 UTC into a <see cref="DateTime" />
      /// </summary>
      public static DateTimeOffset UnixTimestampToDateTimeOffset( this Int32 secondsSinceEpoch )
      {
         return TimeEpoch.UnixEpoch.EpochStart.AddSeconds( secondsSinceEpoch );
      }

      /// <summary>
      /// Convert seconds elapsed since January 1, 1970 UTC into a <see cref="DateTime" />
      /// </summary>
      public static DateTimeOffset UnixTimestampToDateTimeOffset( this Double secondsSinceEpoch )
      {
         return TimeEpoch.UnixEpoch.EpochStart.AddSeconds( secondsSinceEpoch );
      }
   }
}
