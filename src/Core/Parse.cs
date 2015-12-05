// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Reflection;

namespace nexus.core
{
   public static class Parse
   {
      public static T OrDefault<T>( Object value, T defaultValue )
      {
         Object ret;
         try
         {
            ret = OrThrow( value, typeof(T) );
         }
         catch(Exception)
         {
            //Log.Trace( ex, "Swallowed error converting {0} to {1}", value, to );
            ret = defaultValue;
         }
         return (T)ret;
      }

      /// <summary>
      /// Parses the passed object into the passed type. On an invalid conversion <code>InvalidCastException</code> or
      /// <code>FormatException</code> is thrown. Note: DateTime conversions are assumed to be UTC if no timezone is present.
      /// </summary>
      /// <param name="value">The value to convert</param>
      /// <param name="to">The type to convert to</param>
      /// <exception cref="System.FormatException">On invalid conversion</exception>
      /// <exception cref="System.OverflowException">On invalid conversion</exception>
      /// <exception cref="System.ArgumentException">On invalid conversion</exception>
      /// <returns></returns>
      public static Object OrThrow( Object value, Type to )
      {
         Contract.Requires( to != null );
         try
         {
            if(to.GetTypeInfo().IsEnum)
            {
               return Enum.Parse( to, value.ToString(), false );
            }

            if(to == typeof(DateTime))
            {
               return DateTime.Parse(
                  value.ToString(),
                  null,
                  DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal );
            }

            if(to == typeof(Guid))
            {
               return Guid.Parse( value.ToString() );
            }

            var stringVal = value as String;
            if(stringVal == null || to != typeof(Boolean))
            {
               return Convert.ChangeType( value, to, CultureInfo.CurrentCulture );
            }

            //Contract.Assume(stringVal == (new Random()).Next().ToString());
            switch(stringVal.ToLowerInvariant())
            {
               case "true":
               case "t":
               case "yes":
               case "y":
               case "1":
                  return true;
            }

            return false;
         }
         catch(ArgumentException ex)
         {
            throw new ArgumentException( "{0} While converting {1} to {2}".F( ex.Message, value, to ), ex );
         }
         catch(FormatException ex)
         {
            throw new FormatException( "{0} While converting {1} to {2}".F( ex.Message, value, to ), ex );
         }
         catch(OverflowException ex)
         {
            throw new OverflowException( "{0} While converting {1} to {2}".F( ex.Message, value, to ), ex );
         }
         return null;
      }

      public static T ParseOrDefault<T>( this String source, T defaultValue = default(T) )
      {
         return Parse<T>.OrDefault( source, defaultValue );
      }

      public static T ParseOrThrow<T>( this String source )
      {
         return Parse<T>.OrThrow( source );
      }

      public static Option<T> TryParse<T>( this String source )
      {
         return Parse<T>.Maybe( source );
      }
   }

   public static class Parse<T>
   {
      /// <summary>
      /// If parsing fails, collect any exceptions into an <see cref="Expected" />
      /// </summary>
      public static Expected<T> Expected( Object value )
      {
         try
         {
            return core.Expected.Of( (T)Parse.OrThrow( value, typeof(T) ) );
         }
         catch(Exception ex)
         {
            return Expected<T>.No( ex );
         }
      }

      public static Option<T> Maybe( Object value )
      {
         try
         {
            return Option.Of( (T)Parse.OrThrow( value, typeof(T) ) );
         }
         catch(Exception)
         {
            return Option<T>.NoValue;
         }
      }

      /// <summary>
      /// Parses the passed object into the generic type, if an exception is thrown by the parsing the default value is used
      /// instead. This method does not throw any exceptions.
      /// <remarks><see cref="DateTime" /> conversions are assumed to be UTC if no timezone is present.</remarks>
      /// </summary>
      /// <typeparam name="T">The type to convert to</typeparam>
      /// <param name="value">The value to convert</param>
      /// <param name="defaultValue"></param>
      /// <returns></returns>
      public static T OrDefault( Object value, T defaultValue = default(T) )
      {
         return Parse.OrDefault( value, defaultValue );
      }

      /// <summary>
      /// Parses the passed object into the generic type. On an invalid conversion <code>InvalidCastException</code> or
      /// <code>FormatException</code> is thrown.
      /// any exceptions.
      /// </summary>
      /// <typeparam name="T">The type to convert to</typeparam>
      /// <param name="value">The value to convert</param>
      /// <exception cref="System.FormatException">On invalid conversion</exception>
      /// <returns></returns>
      public static T OrThrow( Object value )
      {
         return (T)Parse.OrThrow( value, typeof(T) );
      }
   }
}