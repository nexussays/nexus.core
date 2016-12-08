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
using nexus.core.resharper;

namespace nexus.core.logging
{
   public static class LogExtensions
   {
      /// <summary>
      /// Write a <see cref="LogLevel.Trace" /> level log entry only when compiling with the DEBUG flag
      /// </summary>
      [Conditional( "DEBUG" )]
      public static void Debug( this ILog log, params Object[] objects )
      {
         log.Trace( objects );
      }

      /// <summary>
      /// Write a <see cref="LogLevel.Trace" /> level log entry only when compiling with the DEBUG flag
      /// </summary>
      [Conditional( "DEBUG" )]
      [StringFormatMethod( "message" )]
      public static void Debug( this ILog log, String message, params Object[] messageArgs )
      {
         log.Trace( message, messageArgs );
      }

      /// <summary>
      /// Write a <see cref="LogLevel.Trace" /> level log entry only when compiling with the DEBUG flag
      /// </summary>
      [Conditional( "DEBUG" )]
      [StringFormatMethod( "message" )]
      public static void Debug( this ILog log, Object[] objects, String message, params Object[] messageArgs )
      {
         log.Trace( objects, message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log only when compiling with the DEBUG
      /// flag, (<c>new Object[] {exception}</c>)
      /// </summary>
      [Conditional( "DEBUG" )]
      [StringFormatMethod( "message" )]
      public static void Debug( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Trace( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log, (<c>new Object[] {exception}</c>)
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Error( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Error( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Apply <see cref="String.Format(IFormatProvider,String,object[])" /> over <see cref="ILogEntry.Message" /> and
      /// <see cref="ILogEntry.MessageArguments" /> while checking for null, invalid, and empty arguments. This method catches
      /// any thrown exceptions and returns an error message.
      /// </summary>
      /// <param name="entry">The log entry to format</param>
      /// <param name="formatter">The format provider to use, or <see cref="CultureInfo.InvariantCulture" /> if null</param>
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

      /// <summary>
      /// Utility method to return the first object from <paramref name="entry" /> (<see cref="ILogEntry.Data" />) of the given
      /// type. The first object
      /// of the given type will be returned; if you expect multiple objects of the same type, iterate over the entry's data
      /// yourself.
      /// </summary>
      public static T GetData<T>( this ILogEntry entry ) where T : class
      {
         foreach(var obj in entry.Data)
         {
            if(obj is T)
            {
               return (T)obj;
            }
         }
         return null;
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log, (<c>new Object[] {exception}</c>)
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Info( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Info( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log, (<c>new Object[] {exception}</c>)
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Trace( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Trace( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log, (<c>new Object[] {exception}</c>)
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Warn( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Warn( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Write to the log with the given severity. Utility class if you have <paramref name="severity" /> as a variable and wish
      /// to use that to determine the level to write
      /// </summary>
      public static void Write( this ILog log, LogLevel severity, params Object[] objects )
      {
         switch(severity)
         {
            case LogLevel.Error:
               log.Error( objects );
               break;
            case LogLevel.Warn:
               log.Warn( objects );
               break;
            case LogLevel.Info:
               log.Info( objects );
               break;
            case LogLevel.Trace:
            default:
               log.Trace( objects );
               break;
         }
      }

      /// <summary>
      /// Write to the log with the given severity. Utility class if you have <paramref name="severity" /> as a variable and wish
      /// to use that to determine the level to write
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Write( this ILog log, LogLevel severity, String message, params Object[] messageArgs )
      {
         switch(severity)
         {
            case LogLevel.Error:
               log.Error( message, messageArgs );
               break;
            case LogLevel.Warn:
               log.Warn( message, messageArgs );
               break;
            case LogLevel.Info:
               log.Info( message, messageArgs );
               break;
            case LogLevel.Trace:
            default:
               log.Trace( message, messageArgs );
               break;
         }
      }

      /// <summary>
      /// Write to the log with the given severity. Utility class if you have <paramref name="severity" /> as a variable and wish
      /// to use that to determine the level to write
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Write( this ILog log, LogLevel severity, Object[] objects, String message,
                                params Object[] messageArgs )
      {
         switch(severity)
         {
            case LogLevel.Error:
               log.Error( message, messageArgs );
               break;
            case LogLevel.Warn:
               log.Warn( message, messageArgs );
               break;
            case LogLevel.Info:
               log.Info( message, messageArgs );
               break;
            case LogLevel.Trace:
            default:
               log.Trace( message, messageArgs );
               break;
         }
      }
   }
}