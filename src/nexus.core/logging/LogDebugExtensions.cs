// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using nexus.core.exception;
using nexus.core.resharper;

namespace nexus.core.logging
{
   public static class LogDebugExtensions
   {
      [Conditional( "DEBUG" )]
      public static void Debug( this ILog log, params Object[] objects )
      {
         Contract.Requires( log != null );
         log.Trace( objects );
      }

      [Conditional( "DEBUG" )]
      [StringFormatMethod( "message" )]
      public static void Debug( this ILog log, String message, params Object[] messageArgs )
      {
         Contract.Requires( log != null );
         log.Trace( message, messageArgs );
      }

      [Conditional( "DEBUG" )]
      [StringFormatMethod( "message" )]
      public static void Debug( this ILog log, Object[] objects, String message, params Object[] messageArgs )
      {
         Contract.Requires( log != null );
         log.Trace( message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log
      /// </summary>
      [Conditional( "DEBUG" )]
      [StringFormatMethod( "message" )]
      public static void Debug( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Trace( exception, message, messageArgs );
      }
   }
}