// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.serialization
{
   /// <summary>
   /// Serializer without type information for use in collections or other places a generic interface can cause problems
   /// </summary>
   public interface IUntypedSerializer
   {
      Type From { get; }

      Type To { get; }

      Object Serialize( Object source );
   }
}