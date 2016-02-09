// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.logging
{
   public static class LogExceptionExtensions
   {
      [StringFormatMethod( "message" )]
      public static void ErrorHandledException( this ILog log, Exception exception, String message = null,
                                                params Object[] messageArgs )
      {
         log.Error( new Object[] {exception}, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void InfoHandledException( this ILog log, Exception exception, String message = null,
                                               params Object[] messageArgs )
      {
         log.Info( new Object[] {exception}, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void TraceHandledException( this ILog log, Exception exception, String message = null,
                                                params Object[] messageArgs )
      {
         log.Trace( new Object[] {exception}, message, messageArgs );
      }

      [StringFormatMethod( "message" )]
      public static void WarnHandledException( this ILog log, Exception exception, String message = null,
                                               params Object[] messageArgs )
      {
         log.Warn( new Object[] {exception}, message, messageArgs );
      }
   }
}