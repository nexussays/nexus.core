// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace nexus.core
{
   public static class CollectionUtils
   {
      public static void Add<TKey, TVal>( this ICollection<KeyValuePair<TKey, TVal>> dict, KeyValuePair<TKey, TVal> item )
      {
         Contract.Requires( dict != null );
         dict.Add( item );
      }

      public static void AddAll<T>( this ICollection<T> collection, IEnumerable<T> items )
      {
         Contract.Requires( collection != null );
         if(items != null)
         {
            if(collection is List<T>)
            {
               ((List<T>)collection).AddRange( items );
            }
            else
            {
               foreach(var value in items)
               {
                  collection.Add( value );
               }
            }
         }
      }

      /// <summary>
      /// Retrieve a value and remove it from the dictionary
      /// </summary>
      public static Option<TVal> Extract<TKey, TVal>( this IDictionary<TKey, TVal> dict, TKey key )
      {
         TVal result;
         if(dict != null && dict.TryGetValue( key, out result ))
         {
            dict.Remove( key );
            return Option.Of( result );
         }
         return Option<TVal>.NoValue;
      }

      /// <summary>
      /// Retrieve a value from the dictionary. If the given key does not exist, the default for the given type will be returned
      /// or, if a function is provided, the return value of the function will be returned.
      /// </summary>
      /// <returns>
      /// The value in the dictionary at the given key, the return value of the provided function, or the default for
      /// the given type if no function is provided.
      /// </returns>
      public static TVal Get<TKey, TVal>( this IDictionary<TKey, TVal> dict, TKey key, Func<TVal> defaultValue = null )
      {
         TVal result;
         if(dict != null && dict.TryGetValue( key, out result ))
         {
            return result;
         }
         return defaultValue != null ? defaultValue() : default(TVal);
      }

      /// <summary>
      /// Retrieve a value from the dictionary. If the given key does not exist, the provided value will be returned.
      /// </summary>
      /// <returns>
      /// The value in the dicitonary at the given key, or the provided value.
      /// </returns>
      public static TVal Get<TKey, TVal>( this IDictionary<TKey, TVal> dict, TKey key, TVal defaultValue )
      {
         TVal result;
         if(dict != null && dict.TryGetValue( key, out result ))
         {
            return result;
         }
         return defaultValue;
      }

      public static TOut GetAs<TKey, TOut>( this IDictionary<TKey, Object> dict, TKey key, TOut defaultValue )
      {
         return GetAs( dict, key, () => defaultValue );
      }

      public static TOut GetAs<TOut>( this IDictionary<String, Object> dict, String key, Func<TOut> defaultValue = null )
      {
         return GetAs<String, TOut>( dict, key, defaultValue );
      }

      public static TOut GetAs<TOut>( this IDictionary<Object, Object> dict, Object key, Func<TOut> defaultValue = null )
      {
         return GetAs<Object, TOut>( dict, key, defaultValue );
      }

      /// <summary>
      /// Gets the value at the given key and casts it to the provided type. If the value at the given key cannot be cast or is
      /// the default for the given type (null for reference types) then the provided function will be executed and its value
      /// returned instead.
      /// </summary>
      /// <typeparam name="TKey"></typeparam>
      /// <typeparam name="TOut"></typeparam>
      /// <param name="dict"></param>
      /// <param name="key"></param>
      /// <param name="defaultValue">
      /// Function which returns the value to use in case the value in the dictionary is the deafult
      /// value for the given type, or cannot be cast to the given type.
      /// </param>
      /// <exception cref="InvalidCastException">
      /// If there is no default value provided and the value from the dictionary cannot
      /// be cast to the provided type
      /// </exception>
      /// <returns></returns>
      public static TOut GetAs<TKey, TOut>( this IDictionary<TKey, Object> dict, TKey key,
                                            Func<TOut> defaultValue = null )
      {
         if(defaultValue == null)
         {
            var val = Get( dict, key );
            try
            {
               return (TOut)val;
            }
            catch(InvalidCastException)
            {
               throw new InvalidCastException(
                  "Cannot convert '{1}' at key '{0}' from '{3}' to {2}".F( key, val, typeof(TOut), val.GetType() ) );
            }
            catch(NullReferenceException)
            {
               throw new InvalidCastException( "Cannot convert 'null' at key '{0}' to {1}".F( key, typeof(TOut) ) );
            }
         }
         else
         {
            var val = Get( dict, key );
            TOut castVal;
            try
            {
               castVal = ReferenceEquals( null, val ) || val.Equals( default(TOut) ) ? defaultValue() : (TOut)val;
            }
            catch(InvalidCastException)
            {
               castVal = defaultValue();
            }
            return castVal;
         }
      }

      public static KeyValuePair<TKey, TValue> GetPair<TKey, TValue>( this IImmutableDictionary<TKey, TValue> dict,
                                                                      TKey key )
      {
         Contract.Requires( dict != null );
         return Pair.Of( key, dict[key] );
      }

      public static KeyValuePair<TKey, TValue> GetPair<TKey, TValue>( this IDictionary<TKey, TValue> dict, TKey key )
      {
         Contract.Requires( dict != null );
         return Pair.Of( key, dict[key] );
      }

      public static void Set<TKey, TVal>( this IDictionary<TKey, TVal> dict, KeyValuePair<TKey, TVal> item )
      {
         Contract.Requires( dict != null );
         dict[item.Key] = item.Value;
      }

      public static void SetAll<TKey, TVal>( this IDictionary<TKey, TVal> dict,
                                             IEnumerable<KeyValuePair<TKey, TVal>> items )
      {
         Contract.Requires( dict != null );
         if(items != null)
         {
            foreach(var item in items)
            {
               dict.Set( item );
            }
         }
      }

      public static ImmutableDictionary<Tk, Tv> ToImmutable<Tk, Tv>( this IDictionary<Tk, Tv> source )
      {
         return new ImmutableDictionary<Tk, Tv>( source );
      }

      /// <summary>
      /// Return an <see cref="Option" /> to distinguish between a key having an assigned value of null versus not being
      /// assigned.
      /// </summary>
      public static Option<TVal> TryGet<TKey, TVal>( this IDictionary<TKey, TVal> dict, TKey key )
      {
         Contract.Requires( dict != null );
         TVal result;
         return dict.TryGetValue( key, out result ) ? Option.Of( result ) : Option<TVal>.NoValue;
      }
   }
}