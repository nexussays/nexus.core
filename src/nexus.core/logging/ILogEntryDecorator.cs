// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.logging
{
   /// <summary>
   /// Once registered with a <see cref="ILogControl" />, a <see cref="ILogEntryDecorator" /> allows augmenting the data of a
   /// <see cref="ILogEntry" /> with additional data
   /// </summary>
   [Obsolete]
   public interface ILogEntryDecorator
   {
      /// <summary>
      /// When provided a log entry, return an object to be attached to the log entry or null if nothing to attach
      /// </summary>
      /// <param name="entry">The new log entry</param>
      /// <returns></returns>
      Object Augment( ILogEntry entry );
   }
}