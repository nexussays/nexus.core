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
using nexus.core.resharper;
using nexus.core.serialization;
using nexus.core.time;

namespace nexus.core.logging
{
   /// <summary>
   /// The default/standard implementation of <see cref="ILog" /> and <see cref="ILogControl" />. If the current
   /// <see cref="LogLevel" /> is lower than the called log method then the log call will be counted but no output sent to the
   /// sinks.
   /// </summary>
   public sealed class SystemLog
      : ILog,
        ILogControl
   {
      private static readonly SystemLog s_source = new SystemLog( new DefaultTimeProvider() );

      private readonly ISet<ILogEntryDecorator> m_decorators;
      private readonly IDictionary<LogLevel, Int32> m_entriesSkipped;
      private readonly IDictionary<LogLevel, Int32> m_entriesWritten;
      private readonly ILogEntry[] m_entryBuffer;
      private readonly Object m_lock = new Object();
      private readonly IDictionary<Type, IGenericSerializer> m_serializers;
      private readonly ISet<ILogSink> m_sinks;
      private readonly ITimeProvider m_timeProvider;

      public SystemLog( ITimeProvider timeSource )
      {
         Contract.Requires( timeSource != null );
         m_entryBuffer = new ILogEntry[50];
         m_entriesWritten = new Dictionary<LogLevel, Int32>();
         m_entriesSkipped = new Dictionary<LogLevel, Int32>();
         m_serializers = new Dictionary<Type, IGenericSerializer>();
         m_timeProvider = timeSource;
         LogLevel = LogLevel.Trace;
         m_sinks = new HashSet<ILogSink>();
         m_decorators = new HashSet<ILogEntryDecorator>();
      }

      /// <inheritDoc />
      public IEnumerable<ILogEntryDecorator> Decorators => new List<ILogEntryDecorator>( m_decorators );

      /// <inheritDoc />
      public String Id { get; set; }

      /// <summary>
      /// You should only access this in the main entry-point of your application code and **never** from libraries.
      /// </summary>
      public static ILog Instance => s_source;

      /// <summary>
      /// You should only access this in the main entry-point of your application code and **never** from libraries.
      /// </summary>
      public static ILogControl InstanceControl => s_source;

      /// <summary>
      /// The level of log entires to write. Only entries of this level and above will be written to the log, others will be
      /// silently dropped.
      /// </summary>
      public LogLevel LogLevel { get; set; }

      /// <inheritDoc />
      public IEnumerable<ILogSink> Sinks => new List<ILogSink>( m_sinks );

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
            // TODO: Write out any buffered log entries to the sink
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
         CreateLogEntry( LogLevel.Info, objects, null, null );
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Info( String message, params Object[] messageArgs )
      {
         CreateLogEntry( LogLevel.Info, null, message, messageArgs );
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Info( Object[] objects, String message, params Object[] messageArgs )
      {
         CreateLogEntry( LogLevel.Info, objects, message, messageArgs );
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
         CreateLogEntry( LogLevel.Trace, objects, null, null );
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Trace( String message, params Object[] messageArgs )
      {
         CreateLogEntry( LogLevel.Trace, null, message, messageArgs );
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Trace( Object[] objects, String message, params Object[] messageArgs )
      {
         CreateLogEntry( LogLevel.Trace, objects, message, messageArgs );
      }

      /// <inheritDoc />
      public void Warn( Object[] objects )
      {
         CreateLogEntry( LogLevel.Warn, objects, null, null );
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Warn( String message, params Object[] messageArgs )
      {
         CreateLogEntry( LogLevel.Warn, null, message, messageArgs );
      }

      /// <inheritDoc />
      [StringFormatMethod( "message" )]
      public void Warn( Object[] objects, String message, params Object[] messageArgs )
      {
         CreateLogEntry( LogLevel.Warn, objects, message, messageArgs );
      }

      private void CreateLogEntry( LogLevel severity, Object[] objects, String message, Object[] messageArgs )
      {
         if(severity >= LogLevel)
         {
            var seqNum = -1;
            lock(m_lock)
            {
               m_entriesWritten[severity] += 1;
               seqNum = m_entriesWritten.Values.Sum();
            }

            // TODO: create new level that runs through all objects in structuredData and serializes them to another type
            // Eg, for the exception serializer it would be nice to add it at this top-level and not have to serialize at the call-site
            var queue = new Queue<Object>( objects );
            while(queue.Count > 0)
            {
               var item = queue.Dequeue();
               if(m_serializers.ContainsKey( item.GetType() ))
               {
                  // add the serialized value to the queue in case there is further processing to perform
                  queue.Enqueue( m_serializers[item.GetType()].Serialize( item ) );
               }
            }
            var entry = new LogEntry( Id, m_timeProvider.UtcNow, severity, message, messageArgs, objects );
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
            foreach(var sink in m_sinks)
            {
               try
               {
                  sink.Handle( entry, seqNum );
               }
               catch(Exception ex)
               {
                  Debug.WriteLine(
                     "** LOG [ERROR] in sink {0} **\n{1}".F( sink != null ? sink.GetType().Name : "null", ex ) );
               }
            }
         }
         else
         {
            lock(m_lock)
            {
               m_entriesSkipped[severity] += 1;
            }
         }
      }

      private sealed class LogEntry : ILogEntry
      {
         private readonly IList<Object> m_data;

         internal LogEntry( String id, DateTime time, LogLevel severity, String message, Object[] messageArguments,
                            Object[] attachedObjects )
         {
            LogId = id;
            MessageArguments = messageArguments;
            m_data = new List<Object>();
            if(attachedObjects != null)
            {
               foreach(var o in attachedObjects)
               {
                  m_data.Add( o );
               }
            }
            Severity = severity;
            Message = message;
            Timestamp = time;
         }

         public IEnumerable<Object> Data => m_data;

         public String LogId { get; }

         public String Message { get; }

         public Object[] MessageArguments { get; }

         public LogLevel Severity { get; }

         public DateTime Timestamp { get; }

         /// <summary>
         /// Get the first object from <see cref="ILogEntry.Data" /> that is of type {T}
         /// </summary>
         public T GetData<T>() where T : class
         {
            foreach(var obj in m_data)
            {
               if(obj is T)
               {
                  return (T)obj;
               }
            }
            return null;
         }

         internal void AttachObject<T>( T obj ) where T : class
         {
            m_data.Add( obj );
         }
      }
   }
}