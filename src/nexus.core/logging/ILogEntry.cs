// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.core.logging
{
   public interface ILogEntry
   {
      /// <summary>
      /// A freeform list of objects that have been attached to this log message. It is up to attached <see cref="ILogSink" /> to
      /// make use of these attached values.
      /// </summary>
      IEnumerable<Object> Data { get; }

      /// TODO: determine if this should be broken up into a log ID and a contextual ID or if we just attach context in Data
      String LogId { get; }

      /// <summary>
      /// The unformatted log message.
      /// </summary>
      String Message { get; }

      /// <summary>
      /// The objects provided as arguments to string format <see cref="Message" />
      /// </summary>
      Object[] MessageArguments { get; }

      /// <summary>
      /// The severity of this log entry
      /// </summary>
      LogLevel Severity { get; }

      DateTime /*Int64*/ Timestamp { get; }
   }
}