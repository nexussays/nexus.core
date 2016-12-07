// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics;
using nexus.core.exception;
using nexus.core.resharper;

namespace nexus.core.logging
{
   /// <summary>
   /// Static global <see cref="ILog" /> writing to <see cref="SystemLog" />
   /// </summary>
   public static class Log
   {
      public static LogLevel LogLevel => SystemLog.Instance.LogLevel;

      [Conditional( "DEBUG" )]
      public static void Debug( Object[] objects )
      {
         SystemLog.Instance.Trace( objects );
      }

      [StringFormatMethod( "message" )]
      [Conditional( "DEBUG" )]
      public static void Debug( String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Trace( message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      [Conditional( "DEBUG" )]
      public static void Debug( Object[] objects, String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Trace( objects, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      [Conditional( "DEBUG" )]
      public static void Debug( IException exception, String message = null, params Object[] messageArgs )
      {
         // ReSharper disable once InvokeAsExtensionMethod
         LogExtensions.Trace( SystemLog.Instance, exception, message, messageArgs );
      }

      public static void Error( Object[] objects )
      {
         SystemLog.Instance.Error( objects );
      }

      [StringFormatMethod( "message" )]
      public static void Error( String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Error( message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Error( Object[] objects, String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Error( objects, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Error( IException exception, String message = null, params Object[] messageArgs )
      {
         // ReSharper disable once InvokeAsExtensionMethod
         LogExtensions.Error( SystemLog.Instance, exception, message, messageArgs );
      }

      public static void Info( Object[] objects )
      {
         SystemLog.Instance.Info( objects );
      }

      [StringFormatMethod( "message" )]
      public static void Info( String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Info( message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Info( Object[] objects, String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Info( objects, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Info( IException exception, String message = null, params Object[] messageArgs )
      {
         // ReSharper disable once InvokeAsExtensionMethod
         LogExtensions.Info( SystemLog.Instance, exception, message, messageArgs );
      }

      public static void Trace( Object[] objects )
      {
         SystemLog.Instance.Trace( objects );
      }

      [StringFormatMethod( "message" )]
      public static void Trace( String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Trace( message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Trace( Object[] objects, String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Trace( objects, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Trace( IException exception, String message = null, params Object[] messageArgs )
      {
         // ReSharper disable once InvokeAsExtensionMethod
         LogExtensions.Trace( SystemLog.Instance, exception, message, messageArgs );
      }

      public static void Warn( Object[] objects )
      {
         SystemLog.Instance.Warn( objects );
      }

      [StringFormatMethod( "message" )]
      public static void Warn( String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Warn( message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Warn( Object[] objects, String message, params Object[] messageArgs )
      {
         SystemLog.Instance.Warn( objects, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Warn( IException exception, String message = null, params Object[] messageArgs )
      {
         // ReSharper disable once InvokeAsExtensionMethod
         LogExtensions.Warn( SystemLog.Instance, exception, message, messageArgs );
      }
   }
}