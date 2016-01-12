// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace nexus.core.logging
{
   /// <summary>
   /// A complete log interface.
   /// </summary>
   public interface ILogSource
      : ILogControl,
        ILog
   {
      new String Id { get; set; }

      /// <summary>
      /// The level of log entires to write. Only entries of this level and above will be written to the log, others will be
      /// silently dropped.
      /// </summary>
      /// <remarks>
      /// We need to redefine <see cref="LogLevel" /> because <see cref="ILogControl" />
      /// defines it with setters and <see cref="ILog" /> defines just the getter and since we extend both interfaces the
      /// compiler doesn't know which one we want.
      /// </remarks>
      new LogLevel LogLevel { get; set; }
   }
}