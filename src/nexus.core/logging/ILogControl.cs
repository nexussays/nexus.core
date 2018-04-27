// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Globalization;

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

      /// <summary>
      /// The format provider to use for the debug message and arguments. Defaults to <see cref="CultureInfo.InvariantCulture" />
      /// </summary>
      IFormatProvider DebugMessageFormatProvider { get; set; }

      /// <summary>
      /// A list of the <see cref="IObjectConverter" /> added to this log
      /// </summary>
      IEnumerable<IObjectConverter> ObjectConverters { get; }

      /// <summary>
      /// A list of the <see cref="ILogSink" /> added to this log
      /// </summary>
      IEnumerable<ILogSink> Sinks { get; }

      /// <summary>
      /// Add a converter which will convert any viable objects attached to log entries. Returns <see cref="IDisposable" /> which
      /// removes <paramref name="converter" /> upon calling <see cref="IDisposable.Dispose" /> -- note that this just removes
      /// the converter from this <see cref="ILogControl" /> it does not dispose of the converter itself.
      /// </summary>
      IDisposable AddConverter( IObjectConverter converter );

      /// <summary>
      /// Will dispatch logs to the provided listener after formatting them and checking that the current log level is met.
      /// Returns <see cref="IDisposable" /> which removes <paramref name="sink" /> upon calling
      /// <see cref="IDisposable.Dispose" /> -- note that this just removes the sink from this <see cref="ILogControl" /> it does
      /// not dispose of the sink itself.
      /// </summary>
      IDisposable AddSink( ILogSink sink );

      /// <summary>
      /// Remove the given <see cref="IObjectConverter" /> from this log, and return success
      /// </summary>
      Boolean RemoveConverter( IObjectConverter converter );

      /// <summary>
      /// Remove the given <see cref="ILogSink" /> from this log, and return success
      /// </summary>
      Boolean RemoveSink( ILogSink sink );
   }
}
