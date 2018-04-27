// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.core
{
   /// <summary>
   /// Indicates that the provided value can be updated with new data <typeparamref name="T" />
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public interface IUpdatable<in T>
   {
      /// <summary>
      /// Update this object with new data
      /// </summary>
      void Update( T value );
   }
}
