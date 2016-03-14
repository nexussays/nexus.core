// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using JetBrains.Annotations;
using nexus.core.time;

namespace nexus.core.logging.serializer
{
   /// <summary>
   /// Serializes log entries to "[timestamp]
   /// </summary>
   public class DefaultLogEntrySerializer : ILogEntrySerializer
   {
      public DefaultLogEntrySerializer( IFormatProvider formatter = null )
      {
         FormatProvider = formatter;
      }

      public IFormatProvider FormatProvider { get; set; }

      public virtual String Serialize( ILogEntry entry )
      {
         var message = entry.FormatMessageAndArguments( FormatProvider );
         return Format(
            "{0} {1}{2,-7} {3}",
            entry.Timestamp.ToUnixTimestampInMilliseconds(),
            entry.LogId == null ? "" : "{0}: " + entry.LogId,
            Format( "[{0}]", entry.Severity ).ToUpperInvariant(),
            message ); //,
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