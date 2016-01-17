using System;
using nexus.core;
using nexus.core.logging;
using nexus.core.logging.sink;
using Log = Android.Util.Log;

namespace clover.core.android.logging.sink
{
   public class AndroidLogSink : ILogSink
   {
      public void Handle( ILogEntry entry, Deferred<String> serializedEntry )
      {
         var ex = entry.GetData<IException>();
         //var ctx = entry.GetData<IApplicationContext>();
         //var name = ctx != null ? ctx.ApplicationName : "Android";
         var name = entry.LogId;
         var message = entry.FormatMessageAndArguments() + (ex != null ? ex.ToString() : "");
         switch(entry.Severity)
         {
            case LogLevel.Error:
               Log.Error( name, message );
               break;
            case LogLevel.Warn:
               Log.Warn( name, message );
               break;
            case LogLevel.Info:
               Log.Info( name, message );
               break;
            case LogLevel.Trace:
               Log.Debug( name, message );
               break;
         }
      }
   }
}