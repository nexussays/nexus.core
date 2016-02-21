// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using JetBrains.Annotations;

namespace nexus.core.logging
{
   public static class LogExtensions
   {
      [Conditional( "DEBUG" )]
      public static void Debug( this ILog log, params Object[] objects )
      {
         Contract.Requires( log != null );
         log.Trace( objects );
      }

      [StringFormatMethod( "message" )]
      [Conditional( "DEBUG" )]
      public static void Debug( this ILog log, String message, params Object[] messageArgs )
      {
         Contract.Requires( log != null );
         log.Trace( message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      [Conditional( "DEBUG" )]
      public static void Debug( this ILog log, Object[] objects, String message, params Object[] messageArgs )
      {
         Contract.Requires( log != null );
         log.Trace( message, messageArgs );
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
         catch(FormatException ex)
         {
            return "** LOG [ERROR] in formatter ** string={0} arg_length={1} error={2}".F(
               message,
               args != null ? args.Length.ToString() : "null",
               ex.Message );
         }
      }

      /// <summary>
      /// Chainable version of setting <see cref="ILogControl.LogLevel" />
      /// </summary>
      public static T SetLogLevel<T>( this T log, LogLevel level ) where T : ILogControl
      {
         log.LogLevel = level;
         return log;
      }

      /// <summary>
      /// Chainable version of setting <see cref="ILogControl.Serializer" />
      /// </summary>
      public static ILogSource SetSerializer<T>( this ILogSource log ) where T : class, ILogEntrySerializer, new()
      {
         log.Serializer = new T();
         return log;
      }

      /// <summary>
      /// Chainable version of setting <see cref="ILogControl.Serializer" />
      /// </summary>
      public static T SetSerializer<T>( this T log, ILogEntrySerializer serializer ) where T : ILogControl
      {
         log.Serializer = serializer;
         return log;
      }

      /// <summary>
      /// Chainable version of setting <see cref="ILogControl.Serializer" />
      /// </summary>
      public static ILogControl SetSerializer<T>( this ILogControl log ) where T : class, ILogEntrySerializer, new()
      {
         log.Serializer = new T();
         return log;
      }

      /// <summary>
      /// Chainable version of <see cref="ILogControl.AddDecorator" />
      /// </summary>
      public static ILogSource WithDecorator<T>( this ILogSource log ) where T : class, ILogEntryDecorator, new()
      {
         log.AddDecorator( new T() );
         return log;
      }

      /// <summary>
      /// Chainable version of <see cref="ILogControl.AddDecorator" />
      /// </summary>
      public static ILogControl WithDecorator<T>( this ILogControl log ) where T : class, ILogEntryDecorator, new()
      {
         log.AddDecorator( new T() );
         return log;
      }

      /// <summary>
      /// Chainable version of <see cref="ILogControl.AddDecorator" />
      /// </summary>
      public static T WithDecorator<T>( this T log, ILogEntryDecorator decorator ) where T : ILogControl
      {
         log.AddDecorator( decorator );
         return log;
      }

      /// <summary>
      /// Chainable version of <see cref="ILogControl.AddSink" />
      /// </summary>
      public static ILogSource WithSink<T>( this ILogSource log ) where T : class, ILogSink, new()
      {
         log.AddSink( new T() );
         return log;
      }

      /// <summary>
      /// Chainable version of <see cref="ILogControl.AddSink" />
      /// </summary>
      public static ILogControl WithSink<T>( this ILogControl log ) where T : class, ILogSink, new()
      {
         log.AddSink( new T() );
         return log;
      }

      /// <summary>
      /// Chainable version of <see cref="ILogControl.AddSink" />
      /// </summary>
      public static T WithSink<T>( this T log, ILogSink sink ) where T : ILogControl
      {
         log.AddSink( sink );
         return log;
      }
   }
}