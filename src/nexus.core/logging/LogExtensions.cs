// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.core.exception;
using nexus.core.resharper;

namespace nexus.core.logging
{
   public static class LogExtensions
   {
      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Error( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Error( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Info( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Info( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Trace( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Trace( new Object[] {exception}, message, messageArgs );
      }

      /// <summary>
      /// Utility method to wrap the exception in an object array and write it to the log
      /// </summary>
      [StringFormatMethod( "message" )]
      public static void Warn( this ILog log, IException exception, String message = null, params Object[] messageArgs )
      {
         log.Warn( new Object[] {exception}, message, messageArgs );
      }
   }
}