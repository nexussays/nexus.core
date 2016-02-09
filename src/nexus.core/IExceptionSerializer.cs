// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.core.serialization;

namespace nexus.core
{
   /// <summary>
   /// Serialize <see cref="Exception" /> to <see cref="IException" /> with overloads to provide a boolean indicating whether
   /// or not the exception has been handled
   /// </summary>
   public interface IExceptionSerializer
      : ISerializer<Tuple<Exception, Boolean?>, IException>,
        ISerializer<Exception, IException>
   {
      IException Serialize( Exception exception, Boolean? handled );
   }
}