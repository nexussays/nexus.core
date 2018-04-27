// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.logging.sink
{
   /// <summary>
   /// Formats <see cref="ILogEntry" /> as a string: "<see cref="ILogEntry.Timestamp" /> [<see cref="ILogEntry.Severity" />]
   /// <see cref="ILogEntry.DebugMessage" /> <see cref="ILogEntry.Data" />"
   /// </summary>
   public class SimpleLogEntryToStringConverter : IObjectConverter<ILogEntry, String>
   {
      /// <summary>
      /// </summary>
      public SimpleLogEntryToStringConverter(
         LogExtensions.TimestampFormatType timestampFormat = LogExtensions.TimestampFormatType.UnixTimeInMs )
      {
         TimestampFormat = timestampFormat;
      }

      /// <summary>
      /// The timestamp format, see <see cref="LogExtensions.TimestampFormatType" />
      /// </summary>
      public LogExtensions.TimestampFormatType TimestampFormat { get; }

      /// <summary>
      /// Convert the log entry to a string
      /// </summary>
      public virtual String Convert( ILogEntry entry )
      {
         return entry.FormatAsString( TimestampFormat );
      }
   }
}
