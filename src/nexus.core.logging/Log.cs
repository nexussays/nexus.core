// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Diagnostics;

namespace nexus.core.logging
{
   /// <summary>
   /// Static global <see cref="ILogSource" />
   /// </summary>
   public static class Log
   {
      private static readonly ILogSource s_instance;

      static Log()
      {
         // store a reference to save on the lookup since it will be happening a lot
         s_instance = LogSource.Instance;
      }

      /// <summary>
      /// The level of log entires to write. Only entries of this level and above will be written to the log, others will be
      /// silently dropped.
      /// </summary>
      public static LogLevel LogLevel
      {
         get { return s_instance.LogLevel; }
         set { s_instance.LogLevel = value; }
      }

      //public static String LogName
      //{
      //   get { return s_instance.LogName; }
      //   set { s_instance.LogName = value; }
      //}

      [Conditional( "DEBUG" )]
      public static void Debug( params Object[] objects )
      {
         s_instance.Trace( objects );
      }

      [StringFormatMethod( "format" )]
      [Conditional( "DEBUG" )]
      public static void Debug( String format, params Object[] args )
      {
         s_instance.Trace( format, args );
      }

      [StringFormatMethod( "format" )]
      [Conditional( "DEBUG" )]
      public static void Debug( Exception ex, Boolean exceptionHandled, String format = null, params Object[] args )
      {
         s_instance.Trace( ex, exceptionHandled, message: format, messageArgs: args );
      }

      public static void Error( params Object[] objects )
      {
         s_instance.Error( objects );
      }

      [StringFormatMethod( "format" )]
      public static void Error( Exception ex, Boolean exceptionHandled, String format = null, params Object[] args )
      {
         s_instance.Error( ex, exceptionHandled, message: format, messageArgs: args );
      }

      [StringFormatMethod( "format" )]
      public static void Error( String format, params Object[] args )
      {
         s_instance.Error( format, args );
      }

      public static void Info( params Object[] objects )
      {
         s_instance.Info( objects );
      }

      [StringFormatMethod( "format" )]
      public static void Info( String format, params Object[] args )
      {
         s_instance.Info( format, args );
      }

      [StringFormatMethod( "format" )]
      public static void Info( Exception ex, Boolean exceptionHandled, String format = null, params Object[] args )
      {
         s_instance.Info( ex, exceptionHandled, message: format, messageArgs: args );
      }

      public static void Trace( params Object[] objects )
      {
         s_instance.Trace( objects );
      }

      [StringFormatMethod( "format" )]
      public static void Trace( String format, params Object[] args )
      {
         s_instance.Trace( format, args );
      }

      [StringFormatMethod( "format" )]
      public static void Trace( Exception ex, Boolean exceptionHandled, String format = null, params Object[] args )
      {
         s_instance.Trace( ex, exceptionHandled, message: format, messageArgs: args );
      }

      public static void Warn( params Object[] objects )
      {
         s_instance.Warn( objects );
      }

      [StringFormatMethod( "format" )]
      public static void Warn( String format, params Object[] args )
      {
         s_instance.Warn( format, args );
      }

      [StringFormatMethod( "format" )]
      public static void Warn( Exception ex, Boolean exceptionHandled, String format = null, params Object[] args )
      {
         s_instance.Warn( ex, exceptionHandled, message: format, messageArgs: args );
      }
   }
}