// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.logging.sink
{
   /// <summary>
   /// Writes the serialized <see cref="ILogEntry" /> to the <see cref="Console" />
   /// </summary>
   public class ConsoleLogSink : ILogSink
   {
      public void Handle( ILogEntry entry, Deferred<String> serializedEntry )
      {
         switch(entry.Severity)
         {
            case LogLevel.Error:
               Console.Error.WriteLine( serializedEntry.Value );
               break;
            default:
               Console.Out.WriteLine( serializedEntry.Value );
               break;
         }
      }
   }

   public static class ConsoleLogSinkExtensions
   {
      public static void AddConsoleLogSink( this ILogSource log )
      {
         log.AddSink( new ConsoleLogSink() );
      }
   }
}