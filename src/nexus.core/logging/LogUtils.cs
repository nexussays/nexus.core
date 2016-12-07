// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using nexus.core.serialization;

namespace nexus.core.logging
{
   public static class LogUtils
   {
      /// <summary>
      /// Factory method to create <see cref="ILogSink" /> instance
      /// </summary>
      public static ILogSink CreateLogSink( Action<ILogEntry, Int32> handler )
      {
         Contract.Requires<ArgumentNullException>( handler != null );
         return new DynamicLogSink( handler );
      }

      /// <summary>
      /// Factory method to create <see cref="ILogSink" /> instance
      /// </summary>
      public static ILogSink CreateLogSink( Action<ILogEntry> handler )
      {
         Contract.Requires<ArgumentNullException>( handler != null );
         // ReSharper disable once PossibleNullReferenceException
         return new DynamicLogSink( ( e, s ) => handler( e ) );
      }

      /// <summary>
      /// Factory method to create log entry serializer
      /// </summary>
      public static ISerializer<ILogEntry, String> CreateStringSerializer( Func<ILogEntry, String> serializer )
      {
         Contract.Requires<ArgumentNullException>( serializer != null );
         return new DynamicLogEntrySerializer( serializer );
      }

      /// <summary>
      /// Apply <see cref="String.Format(IFormatProvider,String,object[])" /> over <see cref="ILogEntry.Message" /> and
      /// <see cref="ILogEntry.MessageArguments" />; checking for null, invalid, and empty arguments. Catches any thrown
      /// <see cref="FormatException" /> and returns a string-formatted version of the exception.
      /// </summary>
      /// <param name="entry">The log entry to format</param>
      /// <param name="formatter">The format provider to use, or <see cref="CultureInfo.InvariantCulture" /> if null</param>
      /// <returns></returns>
      public static String FormatMessageAndArguments( this ILogEntry entry, IFormatProvider formatter = null )
      {
         Contract.Requires( entry != null );
         var message = entry.Message;
         var args = entry.MessageArguments;
         try
         {
            return message != null && args != null && args.Length > 0
               ? String.Format( formatter ?? CultureInfo.InvariantCulture, message, args )
               : message;
         }
         catch( /*Format*/Exception ex)
         {
            return "** LOG [ERROR] in formatter ** string={0} arg_length={1} error={2}".F(
               message,
               args != null ? args.Length.ToString() : "null",
               ex.Message );
         }
      }

      private sealed class DynamicLogEntrySerializer : ISerializer<ILogEntry, String>
      {
         private readonly Func<ILogEntry, String> m_serializer;

         public DynamicLogEntrySerializer( Func<ILogEntry, String> serializer )
         {
            m_serializer = serializer;
         }

         public String Serialize( ILogEntry source )
         {
            return m_serializer( source );
         }
      }

      private sealed class DynamicLogSink : ILogSink
      {
         private readonly Action<ILogEntry, Int32> m_handler;

         public DynamicLogSink( Action<ILogEntry, Int32> handler )
         {
            m_handler = handler;
         }

         public void Handle( ILogEntry entry, Int32 sequenceNumber )
         {
            m_handler( entry, sequenceNumber );
         }
      }
   }
}