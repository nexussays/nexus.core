// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections;
using System.Collections.Generic;

namespace nexus.core
{
   public class ImmutableDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>
   {
      protected readonly IDictionary<TKey, TValue> m_data;

      public ImmutableDictionary( IDictionary<TKey, TValue> source = null )
      {
         m_data = source ?? new Dictionary<TKey, TValue>();
      }

      public TValue this[ TKey key ]
      {
         get { return m_data[key]; }
      }

      public Int32 Count
      {
         get { return m_data.Count; }
      }

      public IEnumerable<TKey> Keys
      {
         get { return m_data.Keys; }
      }

      public IEnumerable<TValue> Values
      {
         get { return m_data.Values; }
      }

      public Boolean ContainsKey( TKey key )
      {
         return m_data.ContainsKey( key );
      }

      public Option<TValue> Get( TKey key )
      {
         return m_data.TryGet( key );
      }

      public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
      {
         return m_data.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return ((IEnumerable)m_data).GetEnumerator();
      }
   }
}