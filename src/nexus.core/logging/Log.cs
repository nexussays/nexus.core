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
   /// Static global <see cref="ILog" />
   /// </summary>
   public static class Log
   {
      private static readonly ILog s_instance;

      static Log()
      {
         // store a reference to save on the lookup since it will be happening a lot
         s_instance = SystemLog.Instance;
      }

      public static LogLevel LogLevel => s_instance.LogLevel;

      [Conditional( "DEBUG" )]
      public static void Debug( Object[] objects )
      {
         s_instance.Trace( objects );
      }

      [StringFormatMethod( "message" )]
      [Conditional( "DEBUG" )]
      public static void Debug( String message, params Object[] messageArgs )
      {
         s_instance.Trace( message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      [Conditional( "DEBUG" )]
      public static void Debug( Object[] objects, String message, params Object[] messageArgs )
      {
         s_instance.Trace( objects, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      [Conditional( "DEBUG" )]
      public static void Debug( IException exception, String message = null, params Object[] messageArgs )
      {
         // ReSharper disable once InvokeAsExtensionMethod
         LogExtensions.Trace( s_instance, exception, message, messageArgs );
      }

      public static void Error( Object[] objects )
      {
         s_instance.Error( objects );
      }

      [StringFormatMethod( "message" )]
      public static void Error( String message, params Object[] messageArgs )
      {
         s_instance.Error( message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Error( Object[] objects, String message, params Object[] messageArgs )
      {
         s_instance.Error( objects, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Error( IException exception, String message = null, params Object[] messageArgs )
      {
         // ReSharper disable once InvokeAsExtensionMethod
         LogExtensions.Error( s_instance, exception, message, messageArgs );
      }

      public static void Info( Object[] objects )
      {
         s_instance.Info( objects );
      }

      [StringFormatMethod( "message" )]
      public static void Info( String message, params Object[] messageArgs )
      {
         s_instance.Info( message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Info( Object[] objects, String message, params Object[] messageArgs )
      {
         s_instance.Info( objects, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Info( IException exception, String message = null, params Object[] messageArgs )
      {
         // ReSharper disable once InvokeAsExtensionMethod
         LogExtensions.Info( s_instance, exception, message, messageArgs );
      }

      public static void Trace( Object[] objects )
      {
         s_instance.Trace( objects );
      }

      [StringFormatMethod( "message" )]
      public static void Trace( String message, params Object[] messageArgs )
      {
         s_instance.Trace( message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Trace( Object[] objects, String message, params Object[] messageArgs )
      {
         s_instance.Trace( objects, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Trace( IException exception, String message = null, params Object[] messageArgs )
      {
         // ReSharper disable once InvokeAsExtensionMethod
         LogExtensions.Trace( s_instance, exception, message, messageArgs );
      }

      public static void Warn( Object[] objects )
      {
         s_instance.Warn( objects );
      }

      [StringFormatMethod( "message" )]
      public static void Warn( String message, params Object[] messageArgs )
      {
         s_instance.Warn( message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Warn( Object[] objects, String message, params Object[] messageArgs )
      {
         s_instance.Warn( objects, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void Warn( IException exception, String message = null, params Object[] messageArgs )
      {
         // ReSharper disable once InvokeAsExtensionMethod
         LogExtensions.Warn( s_instance, exception, message, messageArgs );
      }
   }
}