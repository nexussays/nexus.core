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
using nexus.core.time;

namespace nexus.core.logging
{
   /// <summary>
   /// The default/standard implementation of <see cref="ILog" /> and <see cref="ILogControl" />. If the current
   /// <see cref="CurrentLevel" /> is lower than the called log method then the log call will be counted but no output sent to
   /// the sinks.
   /// </summary>
   public sealed class SystemLog
      : ILog,
        ILogControl
   {
      private readonly IList<IObjectConverter> m_converters;
      private readonly IDictionary<LogLevel, Int32> m_entriesSkipped;
      private readonly IDictionary<LogLevel, Int32> m_entriesWritten;
      private readonly ILogEntry[] m_entryBuffer;
      private readonly Object m_lock = new Object();
      private readonly Boolean m_rethrowSinkExceptions;
      private readonly ISet<ILogSink> m_sinks;
      private readonly ITimeProvider m_timeProvider;
      private Int32 m_entrySequence;

      /// <summary>
      /// Create a new system log with the provided configuration
      /// </summary>
      /// <param name="time"></param>
      /// <param name="logBufferSize">
      /// The size of the rolling buffer used to store log entries so newly attached sinks don't lose any entries made before
      /// they were attached. This is only really necessary at start/launch time so unless you are expecting a lot of logging
      /// prior to attaching your sinks he default value is probably fine.
      /// </param>
      /// <param name="rethrowExceptionsFromSinks">
      /// Defaults to false, but when true any exceptions thrown by attached
      /// <see cref="ILogSink" /> will not be caught. Really only useful for debugging.
      /// </param>
      public SystemLog( ITimeProvider time, Byte logBufferSize, Boolean rethrowExceptionsFromSinks = false )
      {
         Contract.Requires<ArgumentNullException>( time != null );

         m_rethrowSinkExceptions = rethrowExceptionsFromSinks;
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
         m_converters = new List<IObjectConverter>();
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
      /// You should only access this in the main entry-point of your application code and **never** from libraries or normal app
      /// code.
      /// </summary>
      public static SystemLog Instance { get; } = new SystemLog( new DefaultTimeProvider(), 50, false );

      /// <summary>
      /// Mostly just needed for unit tests
      /// </summary>
      public Int32 LogBufferSize => m_entryBuffer.Length;

      /// <inheritDoc />
      public IEnumerable<IObjectConverter> ObjectConverters => new List<IObjectConverter>( m_converters );

      /// <inheritDoc />
      public IEnumerable<ILogSink> Sinks => new List<ILogSink>( m_sinks );

      /// <inheritDoc />
      public void AddConverter( IObjectConverter converter )
      {
         if(converter == null)
         {
            return;
         }

         lock(m_lock)
         {
            m_converters.Add( converter );
         }
      }

      /// <inheritDoc />
      public void AddSink( ILogSink sink )
      {
         if(sink == null)
         {
            return;
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

      /// <summary>
      /// Returns the number of entries of the given severity that have been discarded due to <see cref="CurrentLevel" />.
      /// </summary>
      public Int32 GetCountOfEntriesSkipped( LogLevel severity )
      {
         return m_entriesSkipped[severity];
      }

      /// <summary>
      /// Returns the number of entries of the given severity that have been written to the log thus far.
      /// </summary>
      public Int32 GetCountOfEntriesWritten( LogLevel severity )
      {
         return m_entriesWritten[severity];
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
      public Boolean RemoveConverter( IObjectConverter converter )
      {
         lock(m_lock)
         {
            return m_converters.Remove( converter );
         }
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
         if(severity < CurrentLevel)
         {
            lock(m_lock)
            {
               m_entriesSkipped[severity] += 1;
            }
         }
         else
         {
            // lock to thread-safely update the sequence number so it is correct even if a new entry is written while serializing the attached objects.
            Int32 seqNum;
            lock(m_lock)
            {
               // This could still be a problem if a sink is added in between incrementing the sequence and adding the entry to the buffer, but I'm willing to live with those odds
               m_entrySequence += 1;
               seqNum = m_entrySequence;
            }

            // get the time immediately in case serializing objects takes time
            var time = m_timeProvider.UtcNow.UtcDateTime;

            var data = new List<Object>();
            if(objects != null)
            {
               // run through all objects and see if any of them have converters, if so remove the object and add the serialized result in its place
               var queue = new Queue<Object>( objects );
               while(queue.Count > 0)
               {
                  var item = queue.Dequeue();
                  foreach(var converter in m_converters)
                  {
                     if(converter.CanConvertObjectOfType( item.GetType() ))
                     {
                        // add the converted value back to the queue in case there is further processing to perform
                        // TODO: Add a test that checks for circular converters that create an infinite loop
                        queue.Enqueue( converter.Convert( item ) );
                        item = null;
                        break;
                     }
                  }

                  if(item != null)
                  {
                     data.Add( item );
                  }
               }
            }

            // lock again to store the entry
            LogEntry entry;
            lock(m_lock)
            {
               m_entriesWritten[severity] += 1;
               entry = new LogEntry( Id, time, severity, message, messageArgs, data );
               m_entryBuffer[seqNum % m_entryBuffer.Length] = entry;
            }

            foreach(var sink in m_sinks)
            {
               try
               {
                  sink.Handle( entry, seqNum );
               }
               catch(Exception ex) when(!m_rethrowSinkExceptions)
               {
                  Debug.WriteLine( "** LOG [ERROR] in sink {0} ** : {1}".F( sink?.GetType().FullName ?? "null", ex ) );
               }
            }
         }
      }

      private sealed class LogEntry : ILogEntry
      {
         private readonly IList<Object> m_data;

         internal LogEntry( String id, DateTime time, LogLevel severity, String message, Object[] messageArguments,
                            IList<Object> attachedObjects )
         {
            LogId = id;
            MessageArguments = messageArguments;
            Severity = severity;
            Message = message;
            Timestamp = time;
            m_data = attachedObjects;
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