// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using nexus.core.serialization;

namespace nexus.core.logging
{
   /// <summary>
   /// Allows manipulation of log settings
   /// </summary>
   public interface ILogControl
   {
      /// <summary>
      /// The level of log entires to write. Only entries of this level and above will be written to the log, others will
      /// dropped.
      /// </summary>
      LogLevel CurrentLevel { get; set; }

      String Id { get; set; }

      IEnumerable<IObjectConverter> ObjectConverters { get; }

      IEnumerable<ILogSink> Sinks { get; }

      /// <summary>
      /// Add a converter which will convert any viable objects attached to log entries
      /// </summary>
      void AddConverter( IObjectConverter converter );

      /// <summary>
      /// Will dispatch logs to the provided listener after formatting them and checking that the current log level is met.
      /// </summary>
      void AddSink( ILogSink sink );

      Boolean RemoveConverter( IObjectConverter converter );

      Boolean RemoveSink( ILogSink sink );
   }
}