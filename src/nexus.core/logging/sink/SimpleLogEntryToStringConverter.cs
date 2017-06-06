// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;
using nexus.core.time;

namespace nexus.core.logging.sink
{
   /// <summary>
   /// Formats <see cref="ILogEntry" /> as a string: "<see cref="ILogEntry.Timestamp" /> [<see cref="ILogEntry.Severity" />]
   /// <see cref="ILogEntry.DebugMessage" /> <see cref="ILogEntry.Data" />"
   /// </summary>
   public class SimpleLogEntryToStringConverter : IObjectConverter<ILogEntry, String>
   {
      /// <summary>
      /// The format to use for time stamps
      /// </summary>
      public enum TimestampFormatType
      {
         /// <summary>
         /// Output a long numeric value representing time since epoch
         /// </summary>
         UnixTimeInMs,
         /// <summary>
         /// Output ISO8601 format
         /// </summary>
         Iso8601
      }

      /// <summary>
      /// </summary>
      public SimpleLogEntryToStringConverter()
      {
         TimestampFormat = TimestampFormatType.UnixTimeInMs;
      }

      /// <summary>
      /// The timestamp format, see <see cref="TimestampFormatType" />
      /// </summary>
      public TimestampFormatType TimestampFormat { get; set; }

      /// <summary>
      /// Convert the log entry to a string
      /// </summary>
      public virtual String Convert( ILogEntry entry )
      {
         return String.Format(
            "{0} {1,-7} {2} {3}",
            TimestampFormat == TimestampFormatType.Iso8601
               ? entry.Timestamp.ToIso8601String()
               : entry.Timestamp.ToUnixTimestampInMilliseconds().ToString(),
            $"[{entry.Severity}]".ToUpperInvariant(),
            entry.DebugMessage,
            entry.Data.Select( x => "{0}={1}".F( x?.GetType().Name, x ) ).Join( " " ) );
      }
   }
}