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
   public class DebugLogSink : ILogSink
   {
      /// <inheritDoc />
      public void Handle( ILogEntry entry, Deferred<String> serializedEntry )
      {
         Debug.WriteLine( serializedEntry.Value );
      }
   }
}