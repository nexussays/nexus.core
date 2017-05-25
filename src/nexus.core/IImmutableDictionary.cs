// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.core
{
   /// <summary>
   /// An immutable data structure with <see cref="IDictionary{TKey,TValue}" />-like semantics
   /// </summary>
   public interface IImmutableDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
   {
      /// <summary>
      /// Retrienve the given key value
      /// </summary>
      TValue this[ TKey key ] { get; }

      /// <summary>
      /// The number of items in this dictionary
      /// </summary>
      Int32 Count { get; }

      /// <summary>
      /// All the keys in this dictionary
      /// </summary>
      IEnumerable<TKey> Keys { get; }

      /// <summary>
      /// All the values in this dictionary
      /// </summary>
      IEnumerable<TValue> Values { get; }

      /// <summary>
      /// <c>true</c> if this dictionary contains the given key
      /// </summary>
      /// <param name="key"></param>
      /// <returns></returns>
      Boolean ContainsKey( TKey key );

      /// <summary>
      /// Retrieve the given key value or <see cref="Option{TValue}.NoValue" />
      /// </summary>
      Option<TValue> Get( TKey key );
   }
}