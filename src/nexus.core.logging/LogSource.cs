// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using nexus.core.logging.serializer;
using nexus.core.time;

namespace nexus.core.logging
{
   /// <summary>
   /// The default/standard implementation of <see cref="ILogSource" />. If the current <see cref="LogLevel" /> is lower than
   /// the
   /// called log method then the log call will be a no-op. Aside from that, this is basically a literal implementation of
   /// <see cref="ILogSource" /> in that it is not doing anything other than creating an <see cref="ILogEntry" /> and passing
   /// it
   /// through each <see cref="ILogEntryDecorator" /> and to each <see cref="ILogSink" />.
   /// </summary>
   public sealed class LogSource : ILogSource
   {
      private static readonly ILogSource s_instance = new LogSource(
         new DateTimeTimeSource(),
         new DefaultLogEntrySerializer() );

      private readonly ISet<ILogEntryDecorator> m_decorators;
      private readonly Object m_lock = new Object();
      private readonly ISet<ILogSink> m_sinks;

      public LogSource( ITimeSource timeSource, ILogEntrySerializer serializer )
      {
         Contract.Requires( timeSource != null );
         Contract.Requires( serializer != null );

         TimeSource = timeSource;
         Serializer = serializer;
         LogLevel = LogLevel.Info;
         m_sinks = new HashSet<ILogSink>();
         m_decorators = new HashSet<ILogEntryDecorator>();
      }

      /// <inheritDoc />
      public IEnumerable<ILogEntryDecorator> Decorators => m_decorators;

      /// <inheritDoc />
      public String Id { get; set; }

      /// <summary>
      /// The level of log entires to write. Only entries of this level and above will be written to the log, others will be
      /// silently dropped.
      /// </summary>
      public LogLevel LogLevel { get; set; }

      /// <inheritDoc />
      public ILogEntrySerializer Serializer { get; set; }

      /// <inheritDoc />
      public IEnumerable<ILogSink> Sinks => m_sinks;

      /// <summary>
      /// A <see cref="ITimeSource" /> used to determine log entry timestamps
      /// </summary>
      public ITimeSource TimeSource { get; set; }

      /// <summary>
      /// The global <see cref="ILogSource" /> instance which is backing <see cref="Log" />. This should never need to be
      /// accessed outside application and environment initialization!
      /// </summary>
      public static ILogSource Instance => s_instance;

      /// <summary>
      /// Add a dcorator which can add additional data to <see cref="ILogEntry" /> before it is sent to <see cref="ILogSink" />
      /// or <see cref="ILogEntrySerializer" />.
      /// </summary>
      /// <param name="decorator"></param>
      public void AddDecorator( ILogEntryDecorator decorator )
      {
         lock(m_lock)
         {
            m_decorators.Add( decorator );
         }
      }

      /// <summary>
      /// Add a log sink which be called each time a <see cref="ILogEntry" /> is created.
      /// </summary>
      /// <param name="sink"></param>
      public void AddSink( ILogSink sink )
      {
         lock(m_lock)
         {
            m_sinks.Add( sink );
         }
      }

      /// <inheritDoc />
      public void Error( params Object[] objects )
      {
         CreateLogEntry( LogLevel.Error, null, null, null, true, objects );
      }

      /// <inheritDoc />
      public void Error( Exception exception, Boolean isExceptionHandled, String message = null,
                         params Object[] messageArgs )
      {
         CreateLogEntry( LogLevel.Error, message, messageArgs, exception, isExceptionHandled, null );
      }

      /// <inheritDoc />
      public void Error( String message, params Object[] messageArgs )
      {
         CreateLogEntry( LogLevel.Error, message, messageArgs, null, true, null );
      }

      /// <inheritDoc />
      public T GetOrAddDecorator<T>() where T : class, ILogEntryDecorator, new()
      {
         var decorator = m_decorators.FirstOrDefault( x => x.GetType() == typeof(T) ) as T;
         if(decorator == null)
         {
            decorator = new T();
            AddDecorator( decorator );
         }
         return decorator;
      }

      /// <inheritDoc />
      public void Info( params Object[] objects )
      {
         if(LogLevel.Info >= LogLevel)
         {
            CreateLogEntry( LogLevel.Info, null, null, null, true, objects );
         }
      }

      /// <inheritDoc />
      public void Info( String message, params Object[] messageArgs )
      {
         if(LogLevel.Info >= LogLevel)
         {
            CreateLogEntry( LogLevel.Info, message, messageArgs, null, true, null );
         }
      }

      /// <inheritDoc />
      public void Info( Exception exception, Boolean isExceptionHandled, String message = null,
                        params Object[] messageArgs )
      {
         if(LogLevel.Info >= LogLevel)
         {
            CreateLogEntry( LogLevel.Info, message, messageArgs, exception, isExceptionHandled, null );
         }
      }

      /// <inheritDoc />
      public void RemoveDecorator( ILogEntryDecorator decorator )
      {
         lock(m_lock)
         {
            m_decorators.Remove( decorator );
         }
      }

      /// <inheritDoc />
      public Boolean RemoveDecorator<T>() where T : class, ILogEntryDecorator, new()
      {
         Type type = typeof(T);
         lock(m_lock)
         {
            var decorator = m_decorators.FirstOrDefault( x => x.GetType() == type );
            if(decorator != null)
            {
               m_decorators.Remove( decorator );
               return true;
            }
         }
         return false;
      }

      /// <inheritDoc />
      public void RemoveSink( ILogSink sink )
      {
         lock(m_lock)
         {
            m_sinks.Remove( sink );
         }
      }

      /// <inheritDoc />
      public Boolean RemoveSink<T>() where T : class, ILogSink, new()
      {
         Type type = typeof(T);
         lock(m_lock)
         {
            var sink = m_sinks.FirstOrDefault( x => x.GetType() == type );
            if(sink != null)
            {
               m_sinks.Remove( sink );
               return true;
            }
         }
         return false;
      }

      /// <inheritDoc />
      public void Trace( params Object[] objects )
      {
         if(LogLevel.Trace >= LogLevel)
         {
            CreateLogEntry( LogLevel.Trace, null, null, null, true, objects );
         }
      }

      /// <inheritDoc />
      public void Trace( String message, params Object[] messageArgs )
      {
         if(LogLevel.Trace >= LogLevel)
         {
            CreateLogEntry( LogLevel.Trace, message, messageArgs, null, true, null );
         }
      }

      /// <inheritDoc />
      public void Trace( Exception exception, Boolean isExceptionHandled, String message = null,
                         params Object[] messageArgs )
      {
         if(LogLevel.Trace >= LogLevel)
         {
            CreateLogEntry( LogLevel.Trace, message, messageArgs, exception, isExceptionHandled, null );
         }
      }

      /// <inheritDoc />
      public void Warn( params Object[] objects )
      {
         if(LogLevel.Warn >= LogLevel)
         {
            CreateLogEntry( LogLevel.Warn, null, null, null, true, objects );
         }
      }

      /// <inheritDoc />
      public void Warn( String message, params Object[] messageArgs )
      {
         if(LogLevel.Warn >= LogLevel)
         {
            CreateLogEntry( LogLevel.Warn, message, messageArgs, null, true, null );
         }
      }

      /// <inheritDoc />
      public void Warn( Exception exception, Boolean isExceptionHandled, String message = null,
                        params Object[] messageArgs )
      {
         if(LogLevel.Warn >= LogLevel)
         {
            CreateLogEntry( LogLevel.Warn, message, messageArgs, exception, isExceptionHandled, null );
         }
      }

      private void CreateLogEntry( LogLevel level, String message, Object[] messageArgs, Exception exception,
                                   Boolean exceptionHandled, IEnumerable<Object> additionalData )
      {
         var entry = new LogEntry( Id, TimeSource.UtcNow, level, message, messageArgs, additionalData );
         foreach(var decorator in m_decorators)
         {
            try
            {
               var decoration = decorator.Augment( entry, exception, exceptionHandled );
               if(decoration != null)
               {
                  entry.AttachObject( decoration );
               }
            }
            catch(Exception ex)
            {
               Debug.WriteLine(
                  "** LOG [ERROR] in decorator {0} **\n{1}".F(
                     decorator != null ? decorator.GetType().Name : "null",
                     ex ) );
            }
         }
         var serialized = new Deferred<String>(
            () =>
            {
               try
               {
                  return Serializer.Serialize( entry );
               }
               catch(Exception ex)
               {
                  return
                     "** LOG [ERROR] in serializer {0} **\n{1}".F(
                        Serializer != null ? Serializer.GetType().Name : "null",
                        ex );
               }
            } );
         foreach(var sink in m_sinks)
         {
            try
            {
               sink.Handle( entry, serialized );
            }
            catch(Exception ex)
            {
               Debug.WriteLine(
                  "** LOG [ERROR] in sink {0} **\n{1}".F( sink != null ? sink.GetType().Name : "null", ex ) );
            }
         }
      }
   }
}