// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using nexus.core.time;

namespace nexus.core.logging
{
   /// <summary>
   /// Allows manipulation of log settings
   /// </summary>
   public interface ILogControl
   {
      IEnumerable<ILogEntryDecorator> Decorators { get; }

      String Id { get; set; }

      /// <summary>
      /// The level of log entires to write. Only entries of this level and above will be written to the log, others will be
      /// silently dropped.
      /// </summary>
      LogLevel LogLevel { get; set; }

      ILogEntrySerializer Serializer { get; set; }

      IEnumerable<ILogSink> Sinks { get; }

      ITimeSource TimeSource { get; set; }

      void AddDecorator( ILogEntryDecorator decorator );

      /// <summary>
      /// Will dispatch logs to the provided listener after formatting them and checking that the current log level is met.
      /// </summary>
      /// <param name="sink"></param>
      void AddSink( ILogSink sink );

      Boolean RemoveDecorator( ILogEntryDecorator decorator );

      Boolean RemoveDecoratorOfType<T>() where T : ILogEntryDecorator;

      Boolean RemoveSink( ILogSink sink );

      Boolean RemoveSinkOfType<T>() where T : ILogSink;
   }
}