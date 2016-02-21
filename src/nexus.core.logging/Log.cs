// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics;
using JetBrains.Annotations;

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
      public static void Debug( Object[] objects, String format, params Object[] messageArgs )
      {
         s_instance.Trace( objects, format, messageArgs );
      }

      public static void Error( params Object[] objects )
      {
         s_instance.Error( objects );
      }

      [StringFormatMethod( "format" )]
      public static void Error( String format, params Object[] args )
      {
         s_instance.Error( format, args );
      }

      [StringFormatMethod( "format" )]
      public static void Error( Object[] objects, String format, params Object[] messageArgs )
      {
         s_instance.Error( objects, format, messageArgs );
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
      public static void Info( Object[] objects, String format, params Object[] messageArgs )
      {
         s_instance.Info( objects, format, messageArgs );
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
      public static void Trace( Object[] objects, String format, params Object[] messageArgs )
      {
         s_instance.Trace( objects, format, messageArgs );
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
      public static void Warn( Object[] objects, String format, params Object[] messageArgs )
      {
         s_instance.Warn( objects, format, messageArgs );
      }
   }
}