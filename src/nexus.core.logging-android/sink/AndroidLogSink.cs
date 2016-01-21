// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace nexus.core.logging.sink
{
   public class AndroidLogSink : ILogSink
   {
      public void Handle( ILogEntry entry, Deferred<String> serializedEntry )
      {
         var ex = entry.GetData<IException>();
         var name = entry.LogId;
         var message = entry.FormatMessageAndArguments() + (ex != null ? ex.ToString() : "");
         switch(entry.Severity)
         {
            case LogLevel.Error:
               Android.Util.Log.Error( name, message );
               break;
            case LogLevel.Warn:
               Android.Util.Log.Warn( name, message );
               break;
            case LogLevel.Info:
               Android.Util.Log.Info( name, message );
               break;
            case LogLevel.Trace:
               Android.Util.Log.Debug( name, message );
               break;
         }
      }
   }
}