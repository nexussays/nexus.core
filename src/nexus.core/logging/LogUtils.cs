// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using nexus.core.resharper;
using nexus.core.time;

namespace nexus.core.logging
{
   /// <summary>
   /// Utility methods for <see cref="ILog" />, <see cref="ILogControl" />, and <see cref="ILogEntry" />
   /// </summary>
   public static class LogUtils
   {
      /// <summary>
      /// The format to use for time stamps
      /// </summary>
      public enum TimestampFormatType
      {
         /// <summary>
         /// Output a long numeric value representing time since epoch
         /// </summary>
         UnixTimeInMs,
         /// <summary>
         /// Output ISO8601 format
         /// </summary>
         Iso8601
      }

      /// <inheritdoc cref="ILogControl.AddConverter" />
      public static void AddConverter( [NotNull] this ILogControl log, [NotNull] Func<Object, Object> convert,
                                       [NotNull] Func<Type, Boolean> canConvert )
      {
         Contract.Requires<ArgumentNullException>( log != null );
         Contract.Requires<ArgumentNullException>( convert != null );
         Contract.Requires<ArgumentNullException>( canConvert != null );
         // ReSharper disable once PossibleNullReferenceException
         log.AddConverter( ObjectConverter.Create( convert, canConvert ) );
      }

      /// <inheritdoc cref="ILogControl.AddConverter" />
      public static void AddConverter<TFrom, TTo>( [NotNull] this ILogControl log, [NotNull] Func<TFrom, TTo> convert )
      {
         Contract.Requires<ArgumentNullException>( log != null );
         Contract.Requires<ArgumentNullException>( convert != null );
         // ReSharper disable once PossibleNullReferenceException
         log.AddConverter( ObjectConverter.Create( convert ).AsUntyped() );
      }

      /// <inheritdoc cref="ILogControl.AddConverter" />
      public static void AddConverter<F, T>( [NotNull] this ILogControl log,
                                             [NotNull] IObjectConverter<F, T> converter )
      {
         Contract.Requires<ArgumentNullException>( log != null );
         Contract.Requires<ArgumentNullException>( converter != null );
         // ReSharper disable once PossibleNullReferenceException
         log.AddConverter( converter.AsUntyped() );
      }

      /// <inheritdoc cref="ILogControl.AddSink" />
      public static void AddSink( [NotNull] this ILogControl log, [NotNull] Action<ILogEntry> handler )
      {
         Contract.Requires<ArgumentNullException>( log != null );
         Contract.Requires<ArgumentNullException>( handler != null );
         // ReSharper disable once PossibleNullReferenceException
         log.AddSink( CreateLogSink( handler ) );
      }

      /// <summary>
      /// Factory method to create <see cref="ILogSink" /> instance
      /// </summary>
      public static ILogSink CreateLogSink( [NotNull] Action<ILogEntry> handler )
      {
         Contract.Requires<ArgumentNullException>( handler != null );
         return new DynamicLogSink( handler );
      }

      /// <summary>
      /// Format the log entry as a string: "<see cref="ILogEntry.Timestamp" /> [<see cref="ILogEntry.Severity" />]
      /// <see cref="ILogEntry.DebugMessage" /> <see cref="ILogEntry.Data" />"
      /// </summary>
      public static String FormatAsString( [NotNull] this ILogEntry entry,
                                           TimestampFormatType timestampFormat = TimestampFormatType.UnixTimeInMs,
                                           Boolean displayAnonymousTypeName = false )
      {
         return String.Format(
            "{0} {1,-7} {2} {3}",
            timestampFormat == TimestampFormatType.Iso8601
               ? entry.Timestamp.ToIso8601String()
               : entry.Timestamp.ToUnixTimestampInMilliseconds().ToString(),
            $"[{entry.Severity}]".ToUpperInvariant(),
            entry.DebugMessage,
            displayAnonymousTypeName
               ? entry.Data.Select( x => "{0}={1}".F( x?.GetType().Name, x ) ).Join( " " )
               : entry.Data.Select(
                  x =>
                  {
                     var type = x?.GetType();
                     var name = type?.Name;
                     var info = type?.GetTypeInfo();
                     if(info != null && info.GetCustomAttributes<CompilerGeneratedAttribute>().Any() &&
                        info.IsGenericType && type.Name.Contains( "AnonymousType" ) &&
                        (type.Name.StartsWith( "<>" ) || type.Name.StartsWith( "VB$" )))
                     {
                        return x + "";
                     }
                     return "{0}={1}".F( name, x );
                  } ).Join( " " ) );
      }

      /// <summary>
      /// Utility method to return the first object from <paramref name="entry" /> (<see cref="ILogEntry.Data" />) of the given
      /// type. The first object
      /// of the given type will be returned; if you expect multiple objects of the same type, iterate over the entry's data
      /// yourself.
      /// </summary>
      public static T GetData<T>( [NotNull] this ILogEntry entry )
         where T : class
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

      [EditorBrowsable( EditorBrowsableState.Never )]
      private sealed class DynamicLogSink : ILogSink
      {
         private readonly Action<ILogEntry> m_handler;

         public DynamicLogSink( Action<ILogEntry> handler )
         {
            m_handler = handler;
         }

         public void Handle( ILogEntry entry )
         {
            m_handler( entry );
         }
      }
   }
}