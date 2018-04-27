// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.core.resharper;

namespace nexus.core.logging.sink
{
   /// <summary>
   /// Implementation of <see cref="ILogSink" /> which passes log entries to an <see cref="Action" /> provided in ctor
   /// </summary>
   public sealed class ActionLogSink : ILogSink
   {
      private readonly Action<ILogEntry[]> m_handler;

      /// <summary>
      /// Create an <see cref="ILogSink" /> that will pass all entries to <paramref name="handler" />
      /// </summary>
      /// <param name="handler">Handler to receive a single of log entry</param>
      public ActionLogSink( [NotNull] Action<ILogEntry> handler )
         : this(
            entries =>
            {
               foreach(var entry in entries)
               {
                  handler( entry );
               }
            } )
      {
      }

      /// <summary>
      /// Create an <see cref="ILogSink" /> that will pass all entries to <paramref name="handler" />
      /// </summary>
      /// <param name="handler">Handler to receive a sequential list of log entries</param>
      public ActionLogSink( [NotNull] Action<ILogEntry[]> handler )
      {
         m_handler = handler ?? throw new ArgumentNullException( nameof(handler) );
      }

      /// <inheritdoc />
      public void Handle( params ILogEntry[] entries )
      {
         m_handler( entries );
      }
   }
}
