// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;

namespace nexus.core
{
   /// <summary>
   /// Utility method to create a <see cref="KeyValuePair{TKey,TValue}" /> with <see cref="Of{TKey,TValue}" />. That's all :)
   /// </summary>
   public static class Pair
   {
      /// <summary>
      /// Utility method to create a <see cref="KeyValuePair{TKey,TValue}" />
      /// </summary>
      public static KeyValuePair<TKey, TValue> Of<TKey, TValue>( TKey key, TValue value )
      {
         return new KeyValuePair<TKey, TValue>( key, value );
      }
   }
}
