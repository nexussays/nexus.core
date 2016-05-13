// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics;

namespace nexus.core.logging.sink
{
   /// <summary>
   /// Send all log data to <see cref="Debug.WriteLine(string)" />
   /// </summary>
   internal class DebugLogSink : ILogSink
   {
      /// <inheritDoc />
      /// <remarks>
      /// Will not work in Release mode
      /// </remarks>
      public void Handle( ILogEntry entry, Deferred<String> serializedEntry )
      {
         Debug.WriteLine( serializedEntry.Value );
      }
   }

   public static class DebugLogSinkExtensions
   {
      [Conditional( "DEBUG" )]
      public static void AddDebugLogSink( this ILogSource log )
      {
         log.AddSink( new DebugLogSink() );
      }
   }
}