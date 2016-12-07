// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.core.serialization;

namespace nexus.core.logging
{
   /// <summary>
   /// Just a strongly-typed interface for <see cref="ISerializer{ILogEntry,String}"/>
   /// </summary>
   public interface ILogEntrySerializer : ISerializer<ILogEntry, String>
   {
   }
}