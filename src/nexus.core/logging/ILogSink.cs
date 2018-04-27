// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using nexus.core.resharper;

namespace nexus.core.logging
{
   /// <summary>
   /// Instances of <see cref="ILogSink" /> can be registered to a log with <see cref="ILogControl.AddSink" />, after which
   /// the sink will receive every entry that passes the log's log level.
   /// </summary>
   public interface ILogSink
   {
      /// <summary>
      /// This sink will handle the given <see cref="ILogEntry" /> however it sees fit
      /// </summary>
      /// <param name="entries">The current log entry</param>
      void Handle( [NotNull] [ItemNotNull] params ILogEntry[] entries );
   }
}
