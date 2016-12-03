// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.core
{
   public interface IImmutableDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
   {
      TValue this[ TKey key ] { get; }

      Int32 Count { get; }

      IEnumerable<TKey> Keys { get; }

      IEnumerable<TValue> Values { get; }

      Boolean ContainsKey( TKey key );

      Option<TValue> Get( TKey key );
   }
}