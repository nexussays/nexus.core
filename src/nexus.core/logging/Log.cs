// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics;
using nexus.core.resharper;

namespace nexus.core.logging
{
   /// <summary>
   /// Static global <see cref="ILog" /> backed by <see cref="SystemLog" />
   /// </summary>
   public static class Log
   {
      /// <summary>
      /// The current log level, read-only.
      /// </summary>
      public static LogLevel CurrentLevel => SystemLog.Instance.CurrentLevel;

      /// <summary>
      /// Write a <see cref="LogLevel.Trace" /> level log entry only when compiling with the DEBUG flag
      /// </summary>
      [Conditional( "DEBUG" )]
      public static void Debug( Object[] objects )
      {
         SystemLog.Instance.Trace( objects );
      }

      /// <summary>
      /// Write a <see cref="LogLevel.Trace" /> level log entry only when compiling with the DEBUG flag
      /// </summary>
      [StringFormatMethod( "message" )]
      [Conditional( "DEBUG" )]
      public static void Debug( String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Trace( message, messageArgs );
      }

      /// <summary>
      /// Write a <see cref="LogLevel.Trace" /> level log entry only when compiling with the DEBUG flag
      /// </summary>
      [StringFormatMethod( "message" )]
      [Conditional( "DEBUG" )]
      public static void Debug( Object[] objects, String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Trace( objects, message, messageArgs );
      }

      /// <summary>
      /// Write a <see cref="LogLevel.Trace" /> level log entry only when compiling with the DEBUG flag
      /// </summary>
      [StringFormatMethod( "message" )]
      [Conditional( "DEBUG" )]
      public static void Debug( Exception exception, String message = null, params Object[] messageArgs )
      {
         SystemLog.Instance.Trace( exception, message, messageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Error" /> level log data
      /// </summary>
      public static void Error( Object[] objects )
      {
         SystemLog.Instance.Error( objects );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Error" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Error( String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Error( message, messageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Error" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Error( Object[] objects, String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Error( objects, message, messageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Error" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Error( Exception exception, String message = null, params Object[] messageArgs )
      {
         SystemLog.Instance.Error( exception, message, messageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Info" /> level log data
      /// </summary>
      public static void Info( Object[] objects )
      {
         SystemLog.Instance.Info( objects );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Info" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Info( String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Info( message, messageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Info" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Info( Object[] objects, String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Info( objects, message, messageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Info" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Info( Exception exception, String message = null, params Object[] messageArgs )
      {
         SystemLog.Instance.Info( exception, message, messageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Trace" /> level log data
      /// </summary>
      public static void Trace( Object[] objects )
      {
         SystemLog.Instance.Trace( objects );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Trace" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Trace( String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Trace( message, messageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Trace" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Trace( Object[] objects, String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Trace( objects, message, messageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Trace" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Trace( Exception exception, String message = null, params Object[] messageArgs )
      {
         SystemLog.Instance.Trace( exception, message, messageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Warn" /> level log data
      /// </summary>
      public static void Warn( Object[] objects )
      {
         SystemLog.Instance.Warn( objects );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Warn" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Warn( String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Warn( message, messageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Warn" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Warn( Object[] objects, String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Warn( objects, message, messageArgs );
      }

      /// <summary>
      /// Write <see cref="LogLevel.Warn" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Warn( Exception exception, String message = null, params Object[] messageArgs )
      {
         SystemLog.Instance.Warn( exception, message, messageArgs );
      }
   }
}
