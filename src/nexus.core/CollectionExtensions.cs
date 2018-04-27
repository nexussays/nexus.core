// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using nexus.core.resharper;

namespace nexus.core
{
   /// <summary>
   /// Extension methods for doing things with arrays, lists, and dictionaries
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class CollectionExtensions
   {
      /// <param name="source">The list to shuffle in-place</param>
      /// <param name="rand">Receives the max value and returns a random number less than that value</param>
      private static void Shuffle<T>( [NotNull] this IList<T> source, Func<Int32, Int32> rand )
      {
         var i = source.Count;
         while(i > 1)
         {
            var j = rand( i-- );
            var temp = source[i];
            source[i] = source[j];
            source[j] = temp;
         }
      }

      /// <param name="source">The array to shuffle in-place</param>
      /// <param name="rand">Receives the max value and returns a random number less than that value</param>
      private static void Shuffle<T>( [NotNull] this T[] source, Func<Int32, Int32> rand )
      {
         var i = source.Length;
         while(i > 1)
         {
            var j = rand( i-- );
            var temp = source[i];
            source[i] = source[j];
            source[j] = temp;
         }
      }

      /// <summary>
      /// Add all of the provided <paramref name="items" /> to the provided <paramref name="collection" />. If
      /// <paramref name="collection" /> is a <see cref="List{T}" /> then <see cref="List{T}.AddRange" /> will be called,
      /// otherwise <paramref name="items" /> will be iterated over with a <c>foreach</c> and each item added individually to
      /// <paramref name="collection" />
      /// </summary>
      public static void AddAll<T>( [NotNull] this ICollection<T> collection, IEnumerable<T> items )
      {
         Contract.Requires( collection != null );
         if(items == null)
         {
            return;
         }

         if(collection is List<T> list)
         {
            list.AddRange( items );
         }
         else
         {
            foreach(var value in items)
            {
               collection.Add( value );
            }
         }
      }

      /// <summary>
      /// Retrieve a value and remove it from the dictionary, the resulting option will be empty if the requested key did not
      /// exist
      /// </summary>
      public static Option<TVal> Extract<TKey, TVal>( this IDictionary<TKey, TVal> dict, TKey key )
      {
         if(dict == null || !dict.TryGetValue( key, out TVal result ))
         {
            return Option<TVal>.NoValue;
         }
         dict.Remove( key );
         return Option.Of( result );
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
         return dict != null && dict.TryGetValue( key, out TVal result )
            ? result
            : (defaultValue != null ? defaultValue() : default(TVal));
      }

      /// <summary>
      /// Retrieve a value from the dictionary. If the given key does not exist, the provided value will be returned.
      /// </summary>
      /// <returns>
      /// The value in the dicitonary at the given key, or the provided value.
      /// </returns>
      public static TVal Get<TKey, TVal>( this IDictionary<TKey, TVal> dict, TKey key, TVal defaultValue )
      {
         return dict != null && dict.TryGetValue( key, out TVal result ) ? result : defaultValue;
      }

      /// <summary>
      /// Syntax sugar for retrieving a string value from a dictionary
      /// </summary>
      public static TOut GetAs<TOut>( this IDictionary<String, Object> dict, String key,
                                      Func<TOut> defaultValue = null )
      {
         return GetAs<String, TOut>( dict, key, defaultValue );
      }

      /// <summary>
      /// Syntax sugar for retrieving a string value from a dictionary
      /// </summary>
      public static TOut GetAs<TOut>( this IDictionary<String, Object> dict, String key, TOut defaultValue )
      {
         return GetAs<String, TOut>( dict, key, () => defaultValue );
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

      /// <summary>
      /// Retrieve a key value pair from the dictionary
      /// </summary>
      public static KeyValuePair<TKey, TValue> GetPair
         <TKey, TValue>( [NotNull] this IDictionary<TKey, TValue> dict, [NotNull] TKey key )
      {
         Contract.Requires( dict != null );
         return Pair.Of( key, dict[key] );
      }

      /// <summary>
      /// Set the values of <paramref name="dict" /> using a key value pair <paramref name="item" />
      /// </summary>
      public static void Set<TKey, TVal>( [NotNull] this IDictionary<TKey, TVal> dict, KeyValuePair<TKey, TVal> item )
      {
         Contract.Requires( dict != null );
         dict[item.Key] = item.Value;
      }
      /// <summary>
      /// Set the values of <paramref name="dict" /> for each key value pair in <paramref name="items" />
      /// </summary>
      public static void SetAll<TKey, TVal>( [NotNull] this IDictionary<TKey, TVal> dict,
                                             IEnumerable<KeyValuePair<TKey, TVal>> items )
      {
         Contract.Requires( dict != null );
         if(items == null)
         {
            return;
         }
         foreach(var item in items)
         {
            dict.Set( item );
         }
      }

      /// <summary>
      /// Randomize the elements of the array using the provided <paramref name="rand" />
      /// </summary>
      public static void Shuffle<T>( this T[] source, Random rand )
      {
         Shuffle( source, rand.Next );
      }

      /// <summary>
      /// Randomize the elements of the array using the provided randomization function <paramref name="rand" />
      /// </summary>
      public static void Shuffle<T>( this T[] source, Func<Double> rand )
      {
         Shuffle( source, i => (Int32)Math.Floor( rand() * i ) );
      }

      /// <summary>
      /// Randomize the elements of the list using the provided <paramref name="rand" />
      /// </summary>
      public static void Shuffle<T>( this IList<T> source, Random rand )
      {
         Shuffle( source, rand.Next );
      }

      /// <summary>
      /// Randomize the elements of the list using the provided randomization function <paramref name="rand" />
      /// </summary>
      public static void Shuffle<T>( this IList<T> source, Func<Double> rand )
      {
         Shuffle( source, i => (Int32)Math.Floor( rand() * i ) );
      }

      /// <summary>
      /// Return an <see cref="Option" /> to distinguish between a key having an assigned value of null versus not being
      /// assigned.
      /// </summary>
      public static Option<TVal> TryGet<TKey, TVal>( [NotNull] this IDictionary<TKey, TVal> dict, TKey key )
      {
         Contract.Requires( dict != null );
         return dict.TryGetValue( key, out TVal result ) ? Option.Of( result ) : Option<TVal>.NoValue;
      }
   }
}
