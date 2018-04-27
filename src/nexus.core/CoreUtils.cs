// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using System.Threading;
using nexus.core.resharper;

namespace nexus.core
{
   /// <summary>
   /// Some basic utility functions
   /// </summary>
   public static class CoreUtils
   {
      /// <summary>
      /// Copy <paramref name="obj" /> into <paramref name="destination" /> and return <paramref name="obj" /> for easy chaining
      /// </summary>
      public static T Into<T>( this T obj, out T destination )
      {
         destination = obj;
         return obj;
      }

      /// <summary>
      /// True if the given <paramref name="type" /> is <see cref="Nullable{T}" />
      /// </summary>
      public static Boolean IsNullableType( [NotNull] this Type type )
      {
         Contract.Requires( type != null );
         return type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
      }

      /// <summary>
      /// True if the given type is <see cref="Nullable{T}" />
      /// </summary>
      public static Boolean IsNullableType<T>()
      {
         return IsNullableType( typeof(T) );
      }

      /// <summary>
      /// True if the given <paramref name="type" /> is <see cref="Option{T}" />
      /// </summary>
      public static Boolean IsOptionType( [NotNull] this Type type )
      {
         Contract.Requires( type != null );
         return type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Option<>);
      }

      /// <summary>
      /// True if the given type is <see cref="Option{T}" />
      /// </summary>
      public static Boolean IsOptionType<T>()
      {
         return IsOptionType( typeof(T) );
      }

      /// <summary>
      /// Swap the two provided object references
      /// </summary>
      public static void Swap<T>( ref T x, ref T y )
         where T : class
      {
         y = Interlocked.Exchange( ref x, y );
         //var tmp = x;
         //x = y;
         //y = tmp;
      }

      /// <summary>
      /// Useful to generate an exception from library code to test exception handling
      /// <code>return 100 / 0;</code>
      /// </summary>
      public static Int32 ThrowDivideByZeroException()
      {
         var zero = 0;
         return 100 / zero;
      }

      /// <summary>
      /// Useful to generate an exception from library code to test exception handling
      /// <code>throw new Exception("Manually thrown exception.");</code>
      /// </summary>
      public static void ThrowException()
      {
         throw new Exception( "Manually thrown exception." );
      }

      /// <summary>
      /// Dispose of this object if it is not-null
      /// </summary>
      public static void TryDispose( this IDisposable disposable )
      {
         disposable?.Dispose();
      }

      /// <summary>
      /// Dispose of this object if it implements <see cref="IDisposable" /> and is not-null
      /// </summary>
      public static void TryDispose( this Object obj )
      {
         TryDispose( obj as IDisposable );
      }
   }
}
