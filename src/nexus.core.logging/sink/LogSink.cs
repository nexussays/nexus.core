// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;

namespace nexus.core.logging.sink
{
   public static class LogSink
   {
      public static ILogSink Create( Action<ILogEntry, Deferred<String>> handler )
      {
         return new DynamicLogSink( handler );
      }

      public static ILogSink Create( Action<ILogEntry> handler )
      {
         return new DynamicLogSink( ( e, s ) => handler( e ) );
      }

      private sealed class DynamicLogSink : ILogSink
      {
         private readonly Action<ILogEntry, Deferred<String>> m_handler;

         public DynamicLogSink( Action<ILogEntry, Deferred<String>> handler )
         {
            Contract.Requires( handler != null );
            m_handler = handler;
         }

         public void Handle( ILogEntry entry, Deferred<String> serializedEntry )
         {
            m_handler( entry, serializedEntry );
         }
      }
   }
}