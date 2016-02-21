// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace nexus.core
{
   public static class StringUtils
   {
      /// <summary>
      /// Wraps String.Format() for more convenient use. eg - "Value {0}".@F(argument)
      /// </summary>
      [System.Diagnostics.Contracts.Pure]
      [StringFormatMethod( "format" )]
      public static String F( this String format, params Object[] args )
      {
         Contract.Requires( format != null );
         Contract.Requires( args != null );
         Contract.Ensures( Contract.Result<String>() != null );
         return String.Format( format, args );
      }

      /// <summary>
      /// String.IsNullOrEmpty(value) as an extension method
      /// </summary>
      [System.Diagnostics.Contracts.Pure]
      public static Boolean IsNullOrEmpty( this String value )
      {
         return String.IsNullOrEmpty( value );
      }

      /// <summary>
      /// String.IsNullOrWhiteSpace(value) as an extension method
      /// </summary>
      [System.Diagnostics.Contracts.Pure]
      public static Boolean IsNullOrWhiteSpace( this String value )
      {
         //return (value == null || (value.Trim().Length == 0));
         return String.IsNullOrWhiteSpace( value );
      }

      /// <summary>
      /// Removes any of the characters in stripValues from the source string
      /// </summary>
      /// <param name="source"></param>
      /// <param name="stripValues"></param>
      /// <returns>The new string</returns>
      [System.Diagnostics.Contracts.Pure]
      public static String StripCharacters( this String source, String stripValues )
      {
         return source.IsNullOrEmpty() ? source : Regex.Replace( source, "[" + Regex.Escape( stripValues ) + "]", "" );
      }

      /// <summary>
      /// Calls regex.IsMatch() but returns false if the input is null instead of throwing an ArgumentNullException
      /// </summary>
      /// <param name="regex"></param>
      /// <param name="input"></param>
      /// <param name="startAt">The character position at which to start the search. </param>
      /// <returns></returns>
      [System.Diagnostics.Contracts.Pure]
      public static Boolean Test( this Regex regex, String input, Int32? startAt = null )
      {
         if(startAt.HasValue)
         {
            return input != null && regex.IsMatch( input, startAt.Value );
         }
         return input != null && regex.IsMatch( input );
      }
   }
}