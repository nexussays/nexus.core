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
   public interface ILogEntryDecorator
   {
      /// <summary>
      /// When provided a log entry and possible exception data, return an object to be added to the log entry.
      /// </summary>
      /// <param name="entry">The new log entry</param>
      /// <param name="ex">Any exception passed to the exception methods of <see cref="ILog" /></param>
      /// <param name="exceptionHandled"></param>
      /// <returns></returns>
      Object Augment( ILogEntry entry, Exception ex, Boolean exceptionHandled );
   }
}