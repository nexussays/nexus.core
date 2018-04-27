// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.logging
{
   /// <summary>
   /// Represents a single entry to an <see cref="ILog" />
   /// </summary>
   // TODO: add log ID. determine if this should be broken up into a log ID and a contextual ID or if we just attach context in Data
   //String LogId { get; }
   public interface ILogEntry : IEquatable<ILogEntry>
   {
      /// <summary>
      /// A freeform list of objects that have been attached to this log message. It is up to attached <see cref="ILogSink" /> to
      /// make use of these attached values.
      /// </summary>
      Object[] Data { get; }

      /// <summary>
      /// The unformatted log message.
      /// </summary>
      String DebugMessage { get; }

      /// <summary>
      /// The number of this log entry within its parent log
      /// </summary>
      Int32 SequenceId { get; }

      /// <summary>
      /// The severity of this log entry
      /// </summary>
      LogLevel Severity { get; }

      /// <summary>
      /// The time, in UTC, when this log entry was created
      /// </summary>
      DateTime /*Int64*/ Timestamp { get; }
   }
}
