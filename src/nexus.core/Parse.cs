// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Reflection;
using nexus.core.resharper;

namespace nexus.core
{
   /// <summary>
   /// Utility methods to to type conversions with methods to catch exceptions and return default values or return
   /// <see cref="Option{T}" />
   /// </summary>
   public static class Parse
   {
      /// <summary>
      /// Convert <paramref name="value" /> to the provided type or return <paramref name="defaultValue" /> if an
      /// exception is thrown.
      /// </summary>
      /// <returns>
      /// The result from converting <paramref name="value" /> or <paramref name="defaultValue" /> if an exception is
      /// thrown.
      /// </returns>
      public static T OrDefault<T>( Object value, T defaultValue )
      {
         Object ret;
         try
         {
            ret = OrThrow( value, typeof(T) );
         }
         catch(Exception ex)
         {
            Debug.WriteLine( ex.ToString() );
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
      public static Object OrThrow( Object value, [NotNull] Type to )
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
               case "1": return true;
               default: return false;
            }
         }
         catch(ArgumentException ex)
         {
            throw new ArgumentException( "{0} While converting {1} to {2}".F( ex.Message, value ?? "null", to ), ex );
         }
         catch(FormatException ex)
         {
            throw new FormatException( "{0} While converting {1} to {2}".F( ex.Message, value ?? "null", to ), ex );
         }
         catch(OverflowException ex)
         {
            throw new OverflowException( "{0} While converting {1} to {2}".F( ex.Message, value ?? "null", to ), ex );
         }
      }

      /// <summary>
      /// Parse the given string to the provided type or return <paramref name="defaultValue" /> if an exception is thrown.
      /// </summary>
      public static T ParseOrDefault<T>( this String source, T defaultValue = default(T) )
      {
         return Parse<T>.OrDefault( source, defaultValue );
      }

      /// <summary>
      /// Parse the given string to the provided type, throwing any exceptions up the stack
      /// </summary>
      public static T ParseOrThrow<T>( this String source )
      {
         return Parse<T>.OrThrow( source );
      }

      /// <summary>
      /// Try to parse the given string to the provided type, exceptions will be swallowed and an empty
      /// <see cref="Option{String}" /> will be returned
      /// </summary>
      public static Option<T> TryParse<T>( this String source )
      {
         return Parse<T>.Maybe( source );
      }
   }

   /// <summary>
   /// Static utility methods to parse values into other types
   /// </summary>
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
            return core.Expected.No<T>( ex );
         }
      }

      /// <summary>
      /// Parse <paramref name="value" /> and return <see cref="Option{T}.NoValue" /> on failure
      /// </summary>
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
      /// Parses the passed object into the generic type, if an exception is thrown, the default value is used
      /// instead. This method does not throw any exceptions.
      /// <remarks><see cref="DateTime" /> conversions are assumed to be UTC if no timezone is present.</remarks>
      /// </summary>
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
      /// <param name="value">The value to convert</param>
      /// <exception cref="System.FormatException">On invalid conversion</exception>
      /// <returns></returns>
      public static T OrThrow( Object value )
      {
         return (T)Parse.OrThrow( value, typeof(T) );
      }
   }
}
