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
   /// <see cref="CurrentLevel" /> is lower than the called log method then the log call will be counted but no output sent to
   /// the
   /// sinks.
   /// </summary>
   public sealed class SystemLog
      : ILog,
        ILogControl
   {
      private readonly IDictionary<LogLevel, Int32> m_entriesSkipped;
      private readonly IDictionary<LogLevel, Int32> m_entriesWritten;
      private readonly ILogEntry[] m_entryBuffer;
      private readonly Object m_lock = new Object();
      private readonly IDictionary<Type, IUntypedSerializer> m_serializers;
      private readonly ISet<ILogSink> m_sinks;
      private readonly ITimeProvider m_timeProvider;
      private Int32 m_entrySequence;

      /// <summary>
      /// Create a new system log with the provided configuration
      /// </summary>
      /// <param name="time"></param>
      /// <param name="logBufferSize">
      /// The size of the rolling buffer used to store log entries so newly attached sinks don't lose
      /// any entries. This is only really necessary at start/launch time so unless you are expecting a lot of logging prior to
      /// attaching your sinks the default value is probably fine.
      /// </param>
      public SystemLog( ITimeProvider time, Int32 logBufferSize )
      {
         Contract.Requires( time != null );
         m_entrySequence = -1;
         m_entryBuffer = new ILogEntry[logBufferSize];
         m_entriesWritten = new Dictionary<LogLevel, Int32>
         {
            {LogLevel.Error, 0},
            {LogLevel.Warn, 0},
            {LogLevel.Info, 0},
            {LogLevel.Trace, 0}
         };
         m_entriesSkipped = new Dictionary<LogLevel, Int32>
         {
            {LogLevel.Error, 0},
            {LogLevel.Warn, 0},
            {LogLevel.Info, 0},
            {LogLevel.Trace, 0}
         };
         m_serializers = new Dictionary<Type, IUntypedSerializer>();
         m_timeProvider = time;
         CurrentLevel = LogLevel.Trace;
         m_sinks = new HashSet<ILogSink>();
      }

      /// <summary>
      /// The level of log entires to write. Only entries of this level and above will be written to the log, others will be
      /// silently dropped.
      /// </summary>
      public LogLevel CurrentLevel { get; set; }

      /// <inheritDoc />
      public String Id { get; set; }

      /// <summary>
      /// You should only access this in the main entry-point of your application code and **never** from libraries.
      /// </summary>
      public static SystemLog Instance { get; } = new SystemLog( new DefaultTimeProvider(), 50 );

      /// <summary>
      /// Mostly just needed for unit tests
      /// </summary>
      public Int32 LogBufferSize => m_entryBuffer.Length;

      /// <inheritDoc />
      public IEnumerable<ILogSink> Sinks => new List<ILogSink>( m_sinks );

      /// <summary>
      /// Add a log sink which be called each time a <see cref="ILogEntry" /> is created.
      /// </summary>
      public void AddSink( ILogSink sink )
      {
         if(sink == null)
         {
            throw new ArgumentNullException( nameof( sink ) );
         }

         List<Tuple<ILogEntry, Int32>> backlog = null;
         lock(m_lock)
         {
            m_sinks.Add( sink );

            // copy out any buffered log entries to a backlog for the sink
            if(m_entrySequence >= 0)
            {
               backlog = new List<Tuple<ILogEntry, Int32>>();
               if(m_entrySequence >= m_entryBuffer.Length)
               {
                  // if the sequence number is greater than the length of the buffer then we've wrapped around,
                  // so start at one past the latest entry (i.e., the oldest entry) and read to the end and then
                  // wrap back around to index 0 and read up until the current seqNum
                  var index = m_entrySequence % m_entryBuffer.Length;
                  for(Int32 x = index + 1, count = 1; x < m_entryBuffer.Length; ++x, ++count)
                  {
                     backlog.Add( Tuple.Create( m_entryBuffer[x], m_entrySequence - m_entryBuffer.Length + count ) );
                  }
                  for(var x = 0; x <= index; ++x)
                  {
                     backlog.Add( Tuple.Create( m_entryBuffer[x], m_entrySequence - index + x ) );
                  }
               }
               else
               {
                  for(var x = 0; x <= m_entrySequence; ++x)
                  {
                     backlog.Add( Tuple.Create( m_entryBuffer[x], x ) );
                  }
               }
            }
         }

         // now that we're outside of the lock, send all the backlog of buffered log entries to the new sink
         if(backlog?.Count > 0)
         {
            foreach(var entry in backlog)
            {
               try
               {
                  sink.Handle( entry.Item1, entry.Item2 );
               }
               catch(Exception ex)
               {
                  Debug.WriteLine( "** LOG [ERROR] in sink {0} ** : {1}".F( sink?.GetType().FullName ?? "null", ex ) );
               }
            }
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
         if(severity >= CurrentLevel)
         {
            // get the time immediately in case serializing objects takes time
            var time = m_timeProvider.UtcNow;

            // run through all objects and see if any of them have serializers, if so remove the object and add the serialized result in its place
            Queue<Object> queue = null;
            if(objects != null)
            {
               queue = new Queue<Object>( objects );
               while(queue.Count > 0)
               {
                  if(m_serializers.ContainsKey( queue.Peek().GetType() ))
                  {
                     var item = queue.Dequeue();
                     // add the serialized value to the queue in case there is further processing to perform
                     queue.Enqueue( m_serializers[item.GetType()].Serialize( item ) );
                  }
               }
            }

            Int32 seqNum;
            LogEntry entry;
            lock(m_lock)
            {
               m_entriesWritten[severity] += 1;
               m_entrySequence += 1;
               seqNum = m_entrySequence;
               entry = new LogEntry( Id, time, severity, message, messageArgs, queue?.ToArray() );
               m_entryBuffer[m_entrySequence % m_entryBuffer.Length] = entry;
            }

            foreach(var sink in m_sinks)
            {
               try
               {
                  sink.Handle( entry, seqNum );
               }
               catch(Exception ex)
               {
                  Debug.WriteLine( "** LOG [ERROR] in sink {0} ** : {1}".F( sink?.GetType().FullName ?? "null", ex ) );
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
            Severity = severity;
            Message = message;
            Timestamp = time;
            m_data = attachedObjects == null ? new List<Object>() : new List<Object>( attachedObjects );
         }

         public IEnumerable<Object> Data => m_data;

         public String LogId { get; }

         public String Message { get; }

         public Object[] MessageArguments { get; }

         public LogLevel Severity { get; }

         public DateTime Timestamp { get; }
      }
   }
}