// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.time
{
   /// <inheritdoc />
   public class DefaultTimeProvider : ITimeProvider
   {
      /// <inheritdoc />
      public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
   }
}
