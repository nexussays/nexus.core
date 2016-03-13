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
using JetBrains.Annotations;
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
      /// The singleton instance of <see cref="ILogSource" /> backing the static methods on <see cref="Log" />. You should only
      /// access this in the main entry-point of your application code and **never** from libraries.
      /// </summary>
      public static ILogSource Instance { get; } = new LogSource(
         new DateTimeTimeSource(),
         new DefaultLogEntrySerializer() );

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
      public void Error( Object[] objects )
      {
         CreateLogEntry( LogLevel.Error, objects, null, null );
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Error( String message, params Object[] messageArgs )
      {
         CreateLogEntry( LogLevel.Error, null, message, messageArgs );
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Error( Object[] objects, String message, params Object[] messageArgs )
      {
         CreateLogEntry( LogLevel.Error, objects, message, messageArgs );
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
      public void Info( Object[] objects )
      {
         if(LogLevel.Info >= LogLevel)
         {
            CreateLogEntry( LogLevel.Info, objects, null, null );
         }
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Info( String message, params Object[] messageArgs )
      {
         if(LogLevel.Info >= LogLevel)
         {
            CreateLogEntry( LogLevel.Info, null, message, messageArgs );
         }
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Info( Object[] objects, String message, params Object[] messageArgs )
      {
         if(LogLevel.Info >= LogLevel)
         {
            CreateLogEntry( LogLevel.Info, objects, message, messageArgs );
         }
      }

      /// <inheritDoc />
      public Boolean RemoveDecorator( ILogEntryDecorator decorator )
      {
         lock(m_lock)
         {
            return m_decorators.Remove( decorator );
         }
      }

      /// <inheritDoc />
      public Boolean RemoveDecoratorOfType<T>() where T : ILogEntryDecorator
      {
         var type = typeof(T);
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
      public Boolean RemoveSink( ILogSink sink )
      {
         lock(m_lock)
         {
            return m_sinks.Remove( sink );
         }
      }

      /// <inheritDoc />
      public Boolean RemoveSinkOfType<T>() where T : ILogSink
      {
         var type = typeof(T);
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
      public void Trace( Object[] objects )
      {
         if(LogLevel.Trace >= LogLevel)
         {
            CreateLogEntry( LogLevel.Trace, objects, null, null );
         }
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Trace( String message, params Object[] messageArgs )
      {
         if(LogLevel.Trace >= LogLevel)
         {
            CreateLogEntry( LogLevel.Trace, null, message, messageArgs );
         }
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Trace( Object[] objects, String message, params Object[] messageArgs )
      {
         if(LogLevel.Trace >= LogLevel)
         {
            CreateLogEntry( LogLevel.Trace, objects, message, messageArgs );
         }
      }

      /// <inheritDoc />
      public void Warn( Object[] objects )
      {
         if(LogLevel.Warn >= LogLevel)
         {
            CreateLogEntry( LogLevel.Warn, objects, null, null );
         }
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Warn( String message, params Object[] messageArgs )
      {
         if(LogLevel.Warn >= LogLevel)
         {
            CreateLogEntry( LogLevel.Warn, null, message, messageArgs );
         }
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Warn( Object[] objects, String message, params Object[] messageArgs )
      {
         if(LogLevel.Warn >= LogLevel)
         {
            CreateLogEntry( LogLevel.Warn, objects, message, messageArgs );
         }
      }

      private void CreateLogEntry( LogLevel level, Object[] structuredData, String message, Object[] messageArgs )
      {
         // TODO: create new level that runs through all objects in structuredData and serializes them to another type
         // Eg, for the exception serializer it would be nice to add it at this top-level and not have to serialize at the call-site
         var entry = new LogEntry( Id, TimeSource.UtcNow, level, message, messageArgs, structuredData );
         foreach(var decorator in m_decorators)
         {
            try
            {
               var decoration = decorator.Augment( entry );
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