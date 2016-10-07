// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.logging.sink
{
   /// <summary>
   /// Write all log entries to <see cref="Android.Util.Log" />
   /// </summary>
   public class AndroidLogSink : ILogSink
   {
      private readonly String m_defaultLogId;

      /// <summary>
      /// </summary>
      /// <param name="defaultLogId">Wll be used if <see cref="ILogEntry.LogId" /> is null</param>
      public AndroidLogSink( String defaultLogId = null )
      {
         m_defaultLogId = defaultLogId;
      }

      public void Handle( ILogEntry entry, Deferred<String> serializedEntry )
      {
         var message = serializedEntry.Value; //entry.FormatMessageAndArguments();
         var id = entry.LogId ?? m_defaultLogId;
         switch(entry.Severity)
         {
            case LogLevel.Error:
               Android.Util.Log.Error( id, message );
               break;
            case LogLevel.Warn:
               Android.Util.Log.Warn( id, message );
               break;
            case LogLevel.Info:
               Android.Util.Log.Info( id, message );
               break;
            case LogLevel.Trace:
            default:
               Android.Util.Log.Debug( id, message );
               break;
         }
      }
   }

   public static class AndroidLogSinkExtensions
   {
      /// <summary>
      /// Attach <see cref="AndroidLogSink" /> to write all log entries to <see cref="Android.Util.Log" />
      /// </summary>
      public static void AddAndroidLogSink( this ILogControl log )
      {
         log.AddSink( new AndroidLogSink() );
      }
   }
}