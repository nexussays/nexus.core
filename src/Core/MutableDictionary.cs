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
   /// <summary>
   /// A paradox? Nay! Merely a bridge class that implements both <see cref="IDictionary{TKey,TValue}" /> and
   /// <see cref="IImmutableDictionary{TKey,TValue}" />
   /// </summary>
   public class MutableDictionary<TKey, TValue>
      : IImmutableDictionary<TKey, TValue>,
        IDictionary<TKey, TValue>
   {
      private readonly IDictionary<TKey, TValue> m_dict;

      public MutableDictionary( IDictionary<TKey, TValue> dict = null )
      {
         m_dict = dict ?? new Dictionary<TKey, TValue>();
      }

      public TValue this[ TKey key ]
      {
         get { return m_dict[key]; }
      }

      TValue IDictionary<TKey, TValue>.this[ TKey key ]
      {
         get { return m_dict[key]; }
         set { m_dict[key] = value; }
      }

      public Int32 Count
      {
         get { return m_dict.Count; }
      }

      public Boolean IsReadOnly
      {
         get { return m_dict.IsReadOnly; }
      }

      public IEnumerable<TKey> Keys
      {
         get { return m_dict.Keys; }
      }

      public IEnumerable<TValue> Values
      {
         get { return m_dict.Values; }
      }

      ICollection<TKey> IDictionary<TKey, TValue>.Keys
      {
         get { return m_dict.Keys; }
      }

      ICollection<TValue> IDictionary<TKey, TValue>.Values
      {
         get { return m_dict.Values; }
      }

      public void Add( KeyValuePair<TKey, TValue> item )
      {
         m_dict.Add( item );
      }

      public void Add( TKey key, TValue value )
      {
         m_dict.Add( key, value );
      }

      public void Clear()
      {
         m_dict.Clear();
      }

      public Boolean Contains( KeyValuePair<TKey, TValue> item )
      {
         return m_dict.Contains( item );
      }

      public Boolean ContainsKey( TKey key )
      {
         return m_dict.ContainsKey( key );
      }

      public void CopyTo( KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex )
      {
         m_dict.CopyTo( array, arrayIndex );
      }

      public Option<TValue> Get( TKey key )
      {
         return m_dict.TryGet( key );
      }

      public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
      {
         return m_dict.GetEnumerator();
      }

      public Boolean Remove( KeyValuePair<TKey, TValue> item )
      {
         return m_dict.Remove( item );
      }

      public Boolean Remove( TKey key )
      {
         return m_dict.Remove( key );
      }

      public Boolean TryGetValue( TKey key, out TValue value )
      {
         return m_dict.TryGetValue( key, out value );
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return ((IEnumerable)m_dict).GetEnumerator();
      }
   }
}