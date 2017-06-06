// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

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
      /// <param name="entry">The current log entry</param>
      /// TODO: Add UpdateContext(Foo foo); method so we can apply the static context to the sink and the sink can deal with how it handles that
      void Handle( ILogEntry entry );
   }
}