// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
namespace nexus.core.logging.decorator
{
   /// <summary>
   /// Decorators can use reflection on the current call stack to trace the file and line of every <see cref="ILog" />
   /// (<see cref="Log" />) call
   /// </summary>
   public interface ILogEntryOriginPoint : IStackFrame
   {
   }
}