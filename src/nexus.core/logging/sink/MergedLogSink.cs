// Copyright Malachi Griffie
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
      private readonly ILogSink m_first;
      private readonly ILogSink m_second;

      /// <summary>
      /// </summary>
      public MergedLogSink( [NotNull] ILogSink first, [NotNull] ILogSink second )
      {
         m_first = first;
         m_second = second;
      }

      /// <inheritdoc />
      public void Handle( ILogEntry entry )
      {
         m_first.Handle( entry );
         m_second.Handle( entry );
      }
   }
}