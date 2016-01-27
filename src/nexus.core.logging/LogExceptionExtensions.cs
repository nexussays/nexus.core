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