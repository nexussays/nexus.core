// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using nexus.core.exception;
using nexus.core.Properties.resharper;
using nexus.core.serialization;

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

      [StringFormatMethod( "message" )]
      public static void Error( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Error( new Object[] {exception}, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void ErrorException( this ILog log, ISerializer<Exception, IException> serializer,
                                         Exception exception, String message = null, params Object[] messageArgs )
      {
         log.Error( new Object[] {serializer.Serialize( exception )}, message, messageArgs );
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

      [StringFormatMethod( "message" )]
      public static void Info( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Info( new Object[] {exception}, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void InfoException( this ILog log, ISerializer<Exception, IException> serializer,
                                        Exception exception, String message = null, params Object[] messageArgs )
      {
         log.Info( new Object[] {serializer.Serialize( exception )}, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Trace( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Trace( new Object[] {exception}, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void TraceException( this ILog log, ISerializer<Exception, IException> serializer,
                                         Exception exception, String message = null, params Object[] messageArgs )
      {
         log.Trace( new Object[] {serializer.Serialize( exception )}, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Warn( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Warn( new Object[] {exception}, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void WarnException( this ILog log, ISerializer<Exception, IException> serializer,
                                        Exception exception, String message = null, params Object[] messageArgs )
      {
         log.Warn( new Object[] {serializer.Serialize( exception )}, message, messageArgs );
      }
   }
}