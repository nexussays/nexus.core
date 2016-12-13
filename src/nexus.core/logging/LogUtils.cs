// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace nexus.core.logging
{
   public static class LogUtils
   {
      public static void AddConverter( this ILogControl log, Func<Object, Object> convert,
                                       Func<Type, Boolean> canConvert )
      {
         Contract.Requires<ArgumentNullException>( log != null );
         Contract.Requires<ArgumentNullException>( convert != null );
         Contract.Requires<ArgumentNullException>( canConvert != null );
         // ReSharper disable once PossibleNullReferenceException
         log.AddConverter( ObjectConverter.Create( convert, canConvert ) );
      }

      public static void AddConverter<TFrom, TTo>( this ILogControl log, Func<TFrom, TTo> convert )
      {
         Contract.Requires<ArgumentNullException>( log != null );
         Contract.Requires<ArgumentNullException>( convert != null );
         // ReSharper disable once PossibleNullReferenceException
         log.AddConverter( ObjectConverter.Create( convert ).AsUntyped() );
      }

      public static void AddConverter<F, T>( this ILogControl log, IObjectConverter<F, T> converter )
      {
         Contract.Requires<ArgumentNullException>( log != null );
         Contract.Requires<ArgumentNullException>( converter != null );
         // ReSharper disable once PossibleNullReferenceException
         log.AddConverter( converter.AsUntyped() );
      }

      public static void AddSink( this ILogControl log, Action<ILogEntry, Int32> handler )
      {
         Contract.Requires<ArgumentNullException>( log != null );
         Contract.Requires<ArgumentNullException>( handler != null );
         // ReSharper disable once PossibleNullReferenceException
         log.AddSink( CreateLogSink( handler ) );
      }

      public static void AddSink( this ILogControl log, Action<ILogEntry> handler )
      {
         Contract.Requires<ArgumentNullException>( log != null );
         Contract.Requires<ArgumentNullException>( handler != null );
         // ReSharper disable once PossibleNullReferenceException
         log.AddSink( CreateLogSink( handler ) );
      }

      /// <summary>
      /// Factory method to create <see cref="ILogSink" /> instance
      /// </summary>
      public static ILogSink CreateLogSink( Action<ILogEntry, Int32> handler )
      {
         Contract.Requires<ArgumentNullException>( handler != null );
         return new DynamicLogSink( handler );
      }

      /// <summary>
      /// Factory method to create <see cref="ILogSink" /> instance
      /// </summary>
      public static ILogSink CreateLogSink( Action<ILogEntry> handler )
      {
         Contract.Requires<ArgumentNullException>( handler != null );
         // ReSharper disable once PossibleNullReferenceException
         return new DynamicLogSink( ( e, s ) => handler( e ) );
      }

      /// <summary>
      /// Apply <see cref="String.Format(IFormatProvider,String,object[])" /> over <see cref="ILogEntry.Message" /> and
      /// <see cref="ILogEntry.MessageArguments" /> while checking for null, invalid, and empty arguments. This method catches
      /// any thrown exceptions and returns an error message.
      /// </summary>
      /// <param name="entry">The log entry to format</param>
      /// <param name="formatter">The format provider to use, or <see cref="CultureInfo.InvariantCulture" /> if null</param>
      public static String FormatMessageAndArguments( this ILogEntry entry, IFormatProvider formatter = null )
      {
         Contract.Requires( entry != null );
         var message = entry.Message;
         var args = entry.MessageArguments;
         try
         {
            return message != null && args != null && args.Length > 0
               ? String.Format( formatter ?? CultureInfo.InvariantCulture, message, args )
               : message;
         }
         catch( /*Format*/Exception ex)
         {
            return "** LOG [ERROR] in formatter ** string={0} arg_length={1} error={2}".F(
               message,
               args != null ? args.Length.ToString() : "null",
               ex.Message );
         }
      }

      /// <summary>
      /// Utility method to return the first object from <paramref name="entry" /> (<see cref="ILogEntry.Data" />) of the given
      /// type. The first object
      /// of the given type will be returned; if you expect multiple objects of the same type, iterate over the entry's data
      /// yourself.
      /// </summary>
      public static T GetData<T>( this ILogEntry entry ) where T : class
      {
         foreach(var obj in entry.Data)
         {
            if(obj is T)
            {
               return (T)obj;
            }
         }
         return null;
      }

      private sealed class DynamicLogSink : ILogSink
      {
         private readonly Action<ILogEntry, Int32> m_handler;

         public DynamicLogSink( Action<ILogEntry, Int32> handler )
         {
            m_handler = handler;
         }

         public void Handle( ILogEntry entry, Int32 sequenceNumber )
         {
            m_handler( entry, sequenceNumber );
         }
      }
   }
}