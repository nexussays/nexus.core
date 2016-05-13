// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using JetBrains.Annotations;
using nexus.core.logging.decorator;
using nexus.core.time;

namespace nexus.core.logging.serializer
{
   /// <summary>
   /// Serializes log entries to "Timestamp LogId: [Severity] Message"
   /// </summary>
   public class DefaultLogEntrySerializer : ILogEntrySerializer
   {
      public enum TimestampFormat
      {
         UnixTimeInMs,
         Iso8601
      }

      protected const String NEW_LINE = "\n"; //System.Environment.NewLine;

      public DefaultLogEntrySerializer( IFormatProvider formatter = null )
      {
         FormatProvider = formatter;
      }

      /// <summary>
      /// If true, any <see cref="IException" /> attached to the log entry will be emitted to the serialized string
      /// </summary>
      public Boolean DisplayExceptionIfAttached { get; set; }

      /// <summary>
      /// If true, any <see cref="ILogEntryOriginPoint" /> attached to the log entry will be emitted to the serialized string
      /// </summary>
      public Boolean DisplayOriginIfAttached { get; set; }

      public IFormatProvider FormatProvider { get; set; }

      public TimestampFormat Timestamp { get; set; }

      public virtual String Serialize( ILogEntry entry )
      {
         var message = entry.FormatMessageAndArguments( FormatProvider );
         var origin = "";
         if(DisplayOriginIfAttached)
         {
            origin = IfAttached<ILogEntryOriginPoint>(
               entry,
               o => Format( "<{0}.{1}:{2}> ", o.NamespaceQualifiedName(), o.MethodName, o.Line ) );
         }
         var ex = "";
         if(DisplayExceptionIfAttached)
         {
            ex = IfAttached<IException>( entry, exception => (message != null ? NEW_LINE : String.Empty) + exception );
         }
         return Format(
            "{0} {1,-7}{2}{3} {4}{5}",
            Timestamp == TimestampFormat.Iso8601
               ? "{0,-24:yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK}".F( entry.Timestamp )
               : entry.Timestamp.ToUnixTimestampInMilliseconds().ToString(),
            Format( "[{0}]", entry.Severity ).ToUpperInvariant(),
            entry.LogId.IsNullOrEmpty() ? "" : entry.LogId + ": ",
            origin,
            message,
            ex ); //,
         // TODO: Implement serialization flow for attached objects, allow submitting serializers and store ISet<T,ISerializer<T, String>>
         //String.Join( ", ", entry.Data.Select( x => x.ToString() ) ) );
      }

      /// <summary>
      /// Syntax-sugar utility method which calls <see cref="String.Format" /> with this instance's <see cref="FormatProvider" />
      /// </summary>
      [StringFormatMethod( "format" )]
      protected String Format( String format, params Object[] args )
      {
         return String.Format( FormatProvider, format, args );
      }

      /// <summary>
      /// Return result of format function if <see cref="entry" /> is not null, or <see cref="String.Empty" /> if it is null.
      /// </summary>
      protected String IfAttached<T>( ILogEntry entry, Func<T, String> formatFunc ) where T : class
      {
         var value = entry.GetData<T>();
         return value != null ? formatFunc( value ) : String.Empty;
      }

      /// <summary>
      /// Utility method which will return <see cref="String.Empty" /> if <see cref="value" /> is null and return the result of
      /// <see cref="formatFunc" /> if <see cref="value" /> is not null.
      /// </summary>
      protected String IfNotNull<T>( T value, Func<T, String> formatFunc ) where T : class
      {
         return value == null || (value is String && (value as String).IsNullOrEmpty())
            ? String.Empty
            : formatFunc( value );
      }

      /// <summary>
      /// Utility method which will return <see cref="String.Empty" /> if <see cref="value" /> is null or empty string and return
      /// the result of
      /// <see cref="formatFunc" /> if <see cref="value" /> has content.
      /// </summary>
      protected String IfNotNull( String value, Func<String, String> formatFunc )
      {
         return value.IsNullOrEmpty() ? String.Empty : formatFunc( value );
      }
   }
}