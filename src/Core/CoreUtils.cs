// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace nexus.core
{
   public static class CoreUtils
   {
      /// <summary>
      /// Copy <see cref="obj" /> into <see cref="destination" /> and return <see cref="obj" /> for easy chaining
      /// </summary>
      public static T Into<T>( this T obj, out T destination )
      {
         destination = obj;
         return obj;
      }

      /// <summary>
      /// Swap the two provided object pointers
      /// </summary>
      public static void Swap<T>( ref T x, ref T y )
      {
         T tmp = x;
         x = y;
         y = tmp;
      }

      /// <summary>
      /// Convert <see cref="Int32" /> to string as a given base number system.
      /// </summary>
      public static String ToString( this Int32 value, Int32 toBase )
      {
         return Convert.ToString( value, toBase );
      }

      /// <summary>
      /// Dispose of this object if it is not-null
      /// </summary>
      public static void TryDispose( this IDisposable disposable )
      {
         if(disposable != null)
         {
            disposable.Dispose();
         }
      }

      /// <summary>
      /// Dispose of this object if it is not-null and implements <see cref="IDisposable" />
      /// </summary>
      /// <param name="obj"></param>
      public static void TryDispose( this Object obj )
      {
         TryDispose( obj as IDisposable );
      }
   }
}