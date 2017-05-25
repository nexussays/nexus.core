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
   /// A paradoxical bridge class that implements both <see cref="IDictionary{TKey,TValue}" /> and
   /// <see cref="IImmutableDictionary{TKey,TValue}" />
   /// </summary>
   public class MutableDictionary<TKey, TValue>
      : IImmutableDictionary<TKey, TValue>,
        IDictionary<TKey, TValue>
   {
      private readonly IDictionary<TKey, TValue> m_dict;

      /// <summary>
      /// </summary>
      public MutableDictionary( IDictionary<TKey, TValue> dict = null )
      {
         m_dict = dict ?? new Dictionary<TKey, TValue>();
      }

      /// <inheritdoc />
      public TValue this[ TKey key ] => m_dict[key];

      TValue IDictionary<TKey, TValue>.this[ TKey key ]
      {
         get { return m_dict[key]; }
         set { m_dict[key] = value; }
      }

      /// <inheritdoc />
      public Int32 Count => m_dict.Count;

      /// <inheritdoc />
      public Boolean IsReadOnly => m_dict.IsReadOnly;

      /// <inheritdoc />
      public IEnumerable<TKey> Keys => m_dict.Keys;

      /// <inheritdoc />
      public IEnumerable<TValue> Values => m_dict.Values;

      ICollection<TKey> IDictionary<TKey, TValue>.Keys => m_dict.Keys;

      ICollection<TValue> IDictionary<TKey, TValue>.Values => m_dict.Values;

      /// <inheritdoc />
      public void Add( KeyValuePair<TKey, TValue> item )
      {
         m_dict.Add( item );
      }

      /// <inheritdoc />
      public void Add( TKey key, TValue value )
      {
         m_dict.Add( key, value );
      }

      /// <inheritdoc />
      public void Clear()
      {
         m_dict.Clear();
      }

      /// <inheritdoc />
      public Boolean Contains( KeyValuePair<TKey, TValue> item )
      {
         return m_dict.Contains( item );
      }

      /// <inheritdoc />
      public void CopyTo( KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex )
      {
         m_dict.CopyTo( array, arrayIndex );
      }

      /// <inheritdoc />
      public Option<TValue> Get( TKey key )
      {
         return m_dict.TryGet( key );
      }

      /// <inheritdoc />
      public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
      {
         return m_dict.GetEnumerator();
      }

      /// <inheritdoc />
      public Boolean Remove( KeyValuePair<TKey, TValue> item )
      {
         return m_dict.Remove( item );
      }

      /// <inheritdoc />
      public Boolean Remove( TKey key )
      {
         return m_dict.Remove( key );
      }

      /// <inheritdoc />
      public Boolean TryGetValue( TKey key, out TValue value )
      {
         return m_dict.TryGetValue( key, out value );
      }

      private Boolean ContainsKey( TKey key )
      {
         return m_dict.ContainsKey( key );
      }

      Boolean IDictionary<TKey, TValue>.ContainsKey( TKey key )
      {
         return ContainsKey( key );
      }

      Boolean IImmutableDictionary<TKey, TValue>.ContainsKey( TKey key )
      {
         return ContainsKey( key );
      }

      /// <inheritdoc />
      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }
   }
}