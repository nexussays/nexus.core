// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;

namespace nexus.core
{
   /// <summary>
   /// Utility method to create a <see cref="KeyValuePair" />
   /// </summary>
   public static class Pair
   {
      public static KeyValuePair<TK, TV> Of<TK, TV>( TK key, TV value )
      {
         return new KeyValuePair<TK, TV>( key, value );
      }
   }
}