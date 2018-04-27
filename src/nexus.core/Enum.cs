// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.core
{
   /// <summary>
   /// Utility methods to get data from and parse strings to enum values
   /// </summary>
   public static class Enum<T>
      where T : struct
   {
      //public static String Format( Object value, String format )
      //{
      //   return Enum.Format( typeof(T), value, format );
      //}
      /// <summary>
      /// Converts the specified value of a specified enumerated type to its equivalent string representation according to the
      /// specified format.
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <summary>
      /// Retrieves the name of the constant in the specified enumeration that has the specified value.
      /// </summary>
      /// <returns>
      /// A string containing the name of the enumerated type whose value is <paramref name="value" />; or null if no such
      /// constant is found.
      /// </returns>
      public static String GetName( Object value )
      {
         return Enum.GetName( typeof(T), value );
      }

      /// <summary>
      /// Retrieves an array of the names of the constants in a specified enumeration.
      /// </summary>
      /// <returns>
      /// A string array of the names of the constants in the enum.
      /// </returns>
      public static IEnumerable<String> GetNames()
      {
         return Enum.GetNames( typeof(T) );
      }

      /// <summary>
      /// Returns the underlying type of the enumeration.
      /// </summary>
      /// <returns>
      /// The underlying type of the enumeration.
      /// </returns>
      public static Type GetUnderlyingType()
      {
         return Enum.GetUnderlyingType( typeof(T) );
      }

      /// <inheritdoc cref="Enum.GetValues" />
      public static IEnumerable<T> GetValues()
      {
         return (T[])Enum.GetValues( typeof(T) );
      }

      /// <summary>
      /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
      /// </summary>
      /// <returns>
      /// true if a constant in enum has a value equal to <paramref name="value" />; otherwise, false.
      /// </returns>
      public static Boolean IsDefined( Object value )
      {
         return Enum.IsDefined( typeof(T), value );
      }

      /// <summary>
      /// Converts the string representation of the name or numeric value of one or
      /// more enumerated constants to an equivalent enumerated object. A parameter
      /// specifies whether the operation is case-sensitive.
      /// </summary>
      /// <param name="value">A string containing the name or value to convert.</param>
      /// <param name="defaultValue">If the given value cannot be parsed the default value is returned</param>
      /// <param name="ignoreCase">If true, ignore case; otherwise, regard case.</param>
      /// <param name="ignoreCharacters">Ignore these characters in the string. Useful for hyphens and underscores (eg, "-_ ")</param>
      /// <returns>An object of type T whose value is represented by value.</returns>
      /// <exception cref="System.ArgumentNullException">
      /// value is null.
      /// </exception>
      /// <exception cref="System.ArgumentException">
      /// T is not of type Enum; or value is null, an empty string, contains only whitespace,
      /// or is not one of the named values of the enumeration.
      /// </exception>
      /// <returns>An enum of type T whose value is represented by value.</returns>
      public static T Parse( String value, T? defaultValue = null, Boolean ignoreCase = false,
                             String ignoreCharacters = null )
      {
         // TODO: Split into a ParseOrDefault method with an optional default value and this with no such option and always throws
         try
         {
            if(value.IsNullOrEmpty())
            {
               // this is caught below, but still being nice with the error, because.
               throw new ArgumentException( "Cannot parse null or empty String to enum {0}".F( typeof(T).FullName ) );
            }

            if(ignoreCharacters != null)
            {
               value = value.StripCharacters( ignoreCharacters );
            }
            return (T)Enum.Parse( typeof(T), value, ignoreCase );
         }
         catch(Exception ex)
         {
            if(!(ex is ArgumentException))
            {
               // log exceptions that are unexpected, in case we don't have enough logic here
               //Log.Warn(ex, "Unexpected exception in Enum<T>.parse");
            }
            if(defaultValue.HasValue)
            {
               return defaultValue.Value;
            }
            throw new ArgumentException( "Could not parse \"{0}\" into enum {1}".F( value, typeof(T).FullName ) );
         }
      }

      /// <inheritdoc cref="Enum.ToObject" />
      public static T Parse( Object value )
      {
         var s = value as String;
         if(s != null)
         {
            return Parse( s );
         }
         return (T)Enum.ToObject( typeof(T), value );
      }

      /// <inheritdoc cref="Enum.ToObject" />
      public static T Parse( Byte value )
      {
         return (T)Enum.ToObject( typeof(T), value );
      }

      /// <inheritdoc cref="Enum.ToObject" />
      public static T Parse( SByte value )
      {
         return (T)Enum.ToObject( typeof(T), value );
      }

      /// <inheritdoc cref="Enum.ToObject" />
      public static T Parse( Int32 value )
      {
         return (T)Enum.ToObject( typeof(T), value );
      }

      /// <inheritdoc cref="Enum.ToObject" />
      public static T Parse( UInt32 value )
      {
         return (T)Enum.ToObject( typeof(T), value );
      }

      /// <inheritdoc cref="Enum.ToObject" />
      public static T Parse( Int64 value )
      {
         return (T)Enum.ToObject( typeof(T), value );
      }

      /// <inheritdoc cref="Enum.ToObject" />
      public static T Parse( UInt64 value )
      {
         return (T)Enum.ToObject( typeof(T), value );
      }

      /// <inheritdoc cref="Enum.ToObject" />
      public static T Parse( Int16 value )
      {
         return (T)Enum.ToObject( typeof(T), value );
      }

      /// <inheritdoc cref="Enum.ToObject" />
      public static T Parse( UInt16 value )
      {
         return (T)Enum.ToObject( typeof(T), value );
      }
   }
}
