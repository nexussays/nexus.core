﻿// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
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
         Contract.Requires( log != null );
         log.Trace( objects );
      }

      /// <summary>
      /// Write a <see cref="LogLevel.Trace" /> level log entry only when compiling with the DEBUG flag
      /// </summary>
      [Conditional( "DEBUG" )]
      [StringFormatMethod( "message" )]
      public static void Debug( this ILog log, String message, params Object[] messageArgs )
      {
         Contract.Requires( log != null );
         log.Trace( message, messageArgs );
      }

      /// <summary>
      /// Write a <see cref="LogLevel.Trace" /> level log entry only when compiling with the DEBUG flag
      /// </summary>
      [Conditional( "DEBUG" )]
      [StringFormatMethod( "message" )]
      public static void Debug( this ILog log, Object[] objects, String message, params Object[] messageArgs )
      {
         Contract.Requires( log != null );
         log.Trace( objects, message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log only when compiling with the DEBUG
      /// flag, (<c>new Object[] {exception}</c>)
      /// </summary>
      [Conditional( "DEBUG" )]
      [StringFormatMethod( "message" )]
      public static void Debug( this ILog log, Exception exception, String message = null, params Object[] messageArgs )
      {
         Contract.Requires( log != null );
         log.Trace( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log, (<c>new Object[] {exception}</c>)
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Error( this ILog log, Exception exception, String message = null, params Object[] messageArgs )
      {
         Contract.Requires( log != null );
         log.Error( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log, (<c>new Object[] {exception}</c>)
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Info( this ILog log, Exception exception, String message = null, params Object[] messageArgs )
      {
         Contract.Requires( log != null );
         log.Info( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log, (<c>new Object[] {exception}</c>)
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Trace( this ILog log, Exception exception, String message = null, params Object[] messageArgs )
      {
         Contract.Requires( log != null );
         log.Trace( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log, (<c>new Object[] {exception}</c>)
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Warn( this ILog log, Exception exception, String message = null, params Object[] messageArgs )
      {
         Contract.Requires( log != null );
         log.Warn( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Write to the log with the given severity. Utility class if you have <paramref name="severity" /> as a variable and wish
      /// to use that to determine the level to write
      /// </summary>
      public static void Write( this ILog log, LogLevel severity, params Object[] objects )
      {
         Contract.Requires( log != null );
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
         Contract.Requires( log != null );
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
         Contract.Requires( log != null );
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