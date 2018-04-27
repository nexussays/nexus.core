// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using nexus.core.resharper;

namespace nexus.core.logging
{
   /// <summary>
   /// Interface to write information to a log
   /// </summary>
   public interface ILog
   {
      /// <summary>
      /// Only entries of this level or higher will be written to the log, others will be silently dropped.
      /// <remarks>To modify this value, see the <see cref="ILogControl" /> corresponding to this <see cref="ILog" /></remarks>
      /// </summary>
      LogLevel CurrentLevel { get; }

      /// <summary>
      /// Write a log entry with the given data and severity
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      void Write( LogLevel severity, [NotNull] String debugMessage, params Object[] debugMessageArgs );

      /// <summary>
      /// Write a log entry with the given data and severity
      /// </summary>
      void Write( LogLevel severity, [NotNull] Object[] data );

      /// <summary>
      /// Write a log entry with the given data and severity
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      void Write( LogLevel severity, Object[] data, String debugMessage, params Object[] debugMessageArgs );
   }

   /// <summary>
   /// Extension methods for <see cref="ILog" /> to write a given log level
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class LogWriteExtensions
   {
      /// <summary>
      /// Write a <see cref="LogLevel.Trace" /> level log entry only when compiling with the DEBUG flag
      /// </summary>
      [Conditional( "DEBUG" )]
      public static void Debug( [NotNull] this ILog log, params Object[] objects )
      {
         Contract.Requires( log != null );
         log.Trace( objects );
      }

      /// <summary>
      /// Write a <see cref="LogLevel.Trace" /> level log entry only when compiling with the DEBUG flag
      /// </summary>
      [Conditional( "DEBUG" )]
      [StringFormatMethod( "debugMessage" )]
      public static void Debug( [NotNull] this ILog log, String debugMessage, params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Trace( debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Write a <see cref="LogLevel.Trace" /> level log entry only when compiling with the DEBUG flag
      /// </summary>
      [Conditional( "DEBUG" )]
      [StringFormatMethod( "debugMessage" )]
      public static void Debug( [NotNull] this ILog log, Object[] objects, String debugMessage,
                                params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Trace( objects, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log only when compiling with the DEBUG
      /// flag, (<c>new Object[] {exception}</c>)
      /// </summary>
      [Conditional( "DEBUG" )]
      [StringFormatMethod( "debugMessage" )]
      public static void Debug( [NotNull] this ILog log, Exception exception, String debugMessage = null,
                                params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Trace( new Object[] {exception}, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log, (<c>new Object[] {exception}</c>)
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      public static void Error( [NotNull] this ILog log, Exception exception, String debugMessage = null,
                                params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Error( new Object[] {exception}, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Error" /> level log data
      /// </summary>
      public static void Error( [NotNull] this ILog log, Object[] data )
      {
         Contract.Requires( log != null );
         log.Write( LogLevel.Error, data );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Error" /> level log data
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      public static void Error( [NotNull] this ILog log, String debugMessage, params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Write( LogLevel.Error, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Error" /> level log data
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      public static void Error( [NotNull] this ILog log, Object[] data, String debugMessage,
                                params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Write( LogLevel.Error, data, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log, (<c>new Object[] {exception}</c>)
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      public static void Info( [NotNull] this ILog log, Exception exception, String debugMessage = null,
                               params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Info( new Object[] {exception}, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Info" /> level log data
      /// </summary>
      public static void Info( [NotNull] this ILog log, Object[] data )
      {
         Contract.Requires( log != null );
         log.Write( LogLevel.Info, data );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Info" /> level log data
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      public static void Info( [NotNull] this ILog log, String debugMessage, params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Write( LogLevel.Info, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Info" /> level log data
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      public static void Info( [NotNull] this ILog log, Object[] data, String debugMessage,
                               params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Write( LogLevel.Info, data, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log, (<c>new Object[] {exception}</c>)
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      public static void Trace( [NotNull] this ILog log, Exception exception, String debugMessage = null,
                                params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Trace( new Object[] {exception}, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Trace" /> level log data
      /// </summary>
      public static void Trace( [NotNull] this ILog log, Object[] data )
      {
         Contract.Requires( log != null );
         log.Write( LogLevel.Trace, data );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Trace" /> level log data
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      public static void Trace( [NotNull] this ILog log, String debugMessage, params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Write( LogLevel.Trace, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Trace" /> level log data
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      public static void Trace( [NotNull] this ILog log, Object[] data, String debugMessage,
                                params Object[] debugMessageArgs )
      {
         log.Write( LogLevel.Trace, data, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log, (<c>new Object[] {exception}</c>)
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      public static void Warn( [NotNull] this ILog log, Exception exception, String debugMessage = null,
                               params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Warn( new Object[] {exception}, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Warn" /> level log data
      /// </summary>
      public static void Warn( [NotNull] this ILog log, Object[] data )
      {
         Contract.Requires( log != null );
         log.Write( LogLevel.Warn, data );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Warn" /> level log data
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      public static void Warn( [NotNull] this ILog log, String debugMessage, params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Write( LogLevel.Warn, debugMessage, debugMessageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Warn" /> level log data
      /// </summary>
      [StringFormatMethod( "debugMessage" )]
      public static void Warn( [NotNull] this ILog log, Object[] data, String debugMessage,
                               params Object[] debugMessageArgs )
      {
         Contract.Requires( log != null );
         log.Write( LogLevel.Warn, data, debugMessage, debugMessageArgs );
      }
   }
}
