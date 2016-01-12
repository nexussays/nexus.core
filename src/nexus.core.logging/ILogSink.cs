// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace nexus.core.logging
{
   /// <summary>
   /// Instances of <see cref="ILogSink" /> can be registered to a log with <see cref="ILogControl.AddSink" />, after which
   /// the sink will receive every entry written to the log.
   /// </summary>
   public interface ILogSink
   {
      void Handle( ILogEntry entry, Deferred<String> serializedEntry );
   }
}