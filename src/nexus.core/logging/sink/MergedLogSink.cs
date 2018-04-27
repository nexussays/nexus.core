// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using nexus.core.resharper;

namespace nexus.core.logging.sink
{
   /// <summary>
   /// Takes two <see cref="ILogSink" /> instances and calls them both sequentially when <see cref="Handle" /> is called
   /// </summary>
   public class MergedLogSink : ILogSink
   {
      private readonly ILogSink[] m_sinks;

      /// <summary>
      /// </summary>
      public MergedLogSink( [NotNull] ILogSink first, [NotNull] ILogSink second, params ILogSink[] others )
      {
         m_sinks = new ILogSink[2 + (others?.Length ?? 0)];
         m_sinks[0] = first;
         m_sinks[1] = second;
         if(others != null)
         {
            for(var x = 0; x < others.Length; x++)
            {
               m_sinks[x + 2] = others[x];
            }
         }
      }

      /// <inheritdoc />
      public void Handle( params ILogEntry[] entries )
      {
         foreach(var sink in m_sinks)
         {
            sink.Handle( entries );
         }
      }
   }
}
