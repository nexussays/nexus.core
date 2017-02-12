// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;
using nexus.core.resharper;
using nexus.core.time;

namespace nexus.core.logging.sink
{
   /// <summary>
   /// Formats <see cref="ILogEntry" /> as a string: "<see cref="ILogEntry.Timestamp" /> [<see cref="ILogEntry.Severity" />]
   /// <see cref="ILogEntry.LogId" />: <see cref="ILogEntry.Message" /> <see cref="ILogEntry.Data" />"
   /// </summary>
   public class LogEntryToStringConverter : IObjectConverter<ILogEntry, String>
   {
      public enum TimestampFormat
      {
         UnixTimeInMs,
         Iso8601
      }

      public LogEntryToStringConverter( IFormatProvider formatter = null )
      {
         FormatProvider = formatter;
         Timestamp = TimestampFormat.UnixTimeInMs;
      }

      public IFormatProvider FormatProvider { get; set; }

      public TimestampFormat Timestamp { get; set; }

      public virtual String Convert( ILogEntry entry )
      {
         return Format(
            "{0} {1,-7}{2} {3} {4}",
            Timestamp == TimestampFormat.Iso8601
               ? "{0,-24:yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK}".F( entry.Timestamp )
               : entry.Timestamp.ToUnixTimestampInMilliseconds().ToString(),
            Format( "[{0}]", entry.Severity ).ToUpperInvariant(),
            entry.LogId.IsNullOrEmpty() ? "unknown" : entry.LogId + ": ",
            entry.FormatMessageAndArguments( FormatProvider ),
            entry.Data.Select( x => "{0}={1}".F( x?.GetType().Name, x ) ).Join( " " ) );
      }

      /// <summary>
      /// Syntax-sugar utility method which calls <see cref="String.Format" /> with this instance's <see cref="FormatProvider" />
      /// </summary>
      [StringFormatMethod( "format" )]
      protected String Format( String format, params Object[] args )
      {
         return String.Format( FormatProvider, format, args );
      }
   }
}