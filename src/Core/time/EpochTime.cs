// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.time
{
   public enum EpochTime
   {
      /// <summary>
      /// Error. Should not be used.
      /// </summary>
      None = 0,
      NTP = 1,
      Unix = 2
   }

   public static class EpochTimeExtensions
   {
      public static DateTime NtpEpoch { get; } = new DateTime( 1900, 1, 1, 0, 0, 0, DateTimeKind.Utc );

      public static DateTime UnixEpoch { get; } = new DateTime( 1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc );

      public static DateTime EpochStart( this EpochTime epoch )
      {
         switch(epoch)
         {
            case EpochTime.NTP:
               return NtpEpoch;
            case EpochTime.Unix:
               return UnixEpoch;
            default:
               throw new ArgumentException( $"No epoch start time available for \"{epoch}\"", nameof( epoch ) );
         }
      }
   }
}