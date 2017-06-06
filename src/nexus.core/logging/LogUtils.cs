// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using nexus.core.resharper;

namespace nexus.core.logging
{
   public static class LogUtils
   {
      /// <inheritdoc cref="ILogControl.AddConverter" />
      public static void AddConverter( this ILogControl log, Func<Object, Object> convert,
                                       Func<Type, Boolean> canConvert )
      {
         Contract.Requires<ArgumentNullException>( log != null );
         Contract.Requires<ArgumentNullException>( convert != null );
         Contract.Requires<ArgumentNullException>( canConvert != null );
         // ReSharper disable once PossibleNullReferenceException
         log.AddConverter( ObjectConverter.Create( convert, canConvert ) );
      }

      /// <inheritdoc cref="ILogControl.AddConverter" />
      public static void AddConverter<TFrom, TTo>( this ILogControl log, Func<TFrom, TTo> convert )
      {
         Contract.Requires<ArgumentNullException>( log != null );
         Contract.Requires<ArgumentNullException>( convert != null );
         // ReSharper disable once PossibleNullReferenceException
         log.AddConverter( ObjectConverter.Create( convert ).AsUntyped() );
      }

      /// <inheritdoc cref="ILogControl.AddConverter" />
      public static void AddConverter<F, T>( this ILogControl log, IObjectConverter<F, T> converter )
      {
         Contract.Requires<ArgumentNullException>( log != null );
         Contract.Requires<ArgumentNullException>( converter != null );
         // ReSharper disable once PossibleNullReferenceException
         log.AddConverter( converter.AsUntyped() );
      }

  

      /// <inheritdoc cref="ILogControl.AddSink" />
      public static void AddSink( this ILogControl log, [NotNull] Action<ILogEntry> handler )
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
      /// Utility method to return the first object from <paramref name="entry" /> (<see cref="ILogEntry.Data" />) of the given
      /// type. The first object
      /// of the given type will be returned; if you expect multiple objects of the same type, iterate over the entry's data
      /// yourself.
      /// </summary>
      public static T GetData<T>( this ILogEntry entry )
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