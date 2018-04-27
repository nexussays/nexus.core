// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
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
      /// <summary>
      /// When objects are provided to the log they will be run through all these converters
      /// </summary>
      private readonly IList<IObjectConverter> m_converters;
      /// <summary>
      /// Keep track of log entries that have been dropped because the log level was higher than the entry
      /// </summary>
      private readonly IDictionary<LogLevel, Int32> m_entriesSkipped;
      /// <summary>
      /// The number of entries that have been written for each <see cref="LogLevel" />
      /// </summary>
      private readonly IDictionary<LogLevel, Int32> m_entriesWritten;
      private readonly ILogEntry[] m_entryBuffer;
      private readonly Object m_lock = new Object();
      private readonly Boolean m_rethrowSinkExceptions;
      private readonly ISet<ILogSink> m_sinks;
      private readonly ITimeProvider m_timeProvider;
      private Int32 m_entrySequence;

      /// <summary>
      /// Create a new system log with the provided configuration. You should not instantiate this, see: <see cref="Instance" />
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
      public SystemLog( [NotNull] ITimeProvider time, Byte logBufferSize, Boolean rethrowExceptionsFromSinks = false )
      {
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
         m_timeProvider = time ?? throw new ArgumentNullException( nameof(time) );
         CurrentLevel = LogLevel.Trace;
         m_sinks = new HashSet<ILogSink>();
      }

      /// <summary>
      /// The level of log entires to write. Only entries of this level and above will be written to the log, others will be
      /// silently dropped.
      /// </summary>
      public LogLevel CurrentLevel { get; set; }

      /// <inheritdoc />
      public IFormatProvider DebugMessageFormatProvider { get; set; }

      /// <summary>
      /// You should only access this in the main entry-point of your application code and **never** from libraries or normal app
      /// code.
      /// </summary>
      public static SystemLog Instance { get; } = new SystemLog( new DefaultTimeProvider(), 50, false );

      /// <summary>
      /// Mostly just needed for unit tests
      /// </summary>
      public Int32 LogBufferSize => m_entryBuffer.Length;

      /// <inheritDoc cref="ILogControl.ObjectConverters" />
      public IEnumerable<IObjectConverter> ObjectConverters => new List<IObjectConverter>( m_converters );

      /// <inheritDoc cref="ILogControl.Sinks" />
      public IEnumerable<ILogSink> Sinks => new List<ILogSink>( m_sinks );

      /// <inheritDoc cref="ILogControl.AddConverter" />
      public IDisposable AddConverter( IObjectConverter converter )
      {
         if(converter == null)
         {
            return DisposeAction.None;
         }

         lock(m_lock)
         {
            m_converters.Add( converter );
         }
         return new DisposeAction( () => RemoveConverter( converter ) );
      }

      /// <inheritDoc cref="ILogControl.AddSink" />
      public IDisposable AddSink( ILogSink sink )
      {
         if(sink == null)
         {
            return DisposeAction.None;
         }

         // add the sink and collect any log entries that have been written prior to this sink being added so
         // we can catch it up.
         List<ILogEntry> backlog = null;
         lock(m_lock)
         {
            m_sinks.Add( sink );

            // copy out any buffered log entries to a backlog for the sink
            if(m_entrySequence >= 0)
            {
               backlog = new List<ILogEntry>();
               if(m_entrySequence >= m_entryBuffer.Length)
               {
                  // if the sequence number is greater than the length of the buffer then we've wrapped around,
                  // so start at one past the latest entry (i.e., the oldest entry) and read to the end and then
                  // wrap back around to index 0 and read up until the current seqNum
                  var index = m_entrySequence % m_entryBuffer.Length;
                  /*
                  for(Int32 x = index + 1, count = 1; x < m_entryBuffer.Length; ++x, ++count)
                  {
                     backlog.Add( Tuple.Create( m_entryBuffer[x], m_entrySequence - m_entryBuffer.Length + count ) );
                  }
                  for(var x = 0; x <= index; ++x)
                  {
                     backlog.Add( Tuple.Create( m_entryBuffer[x], m_entrySequence - index + x ) );
                  }
                  */
                  for(var x = index + 1; x < m_entryBuffer.Length; ++x)
                  {
                     backlog.Add( m_entryBuffer[x] );
                  }
                  for(var x = 0; x <= index; ++x)
                  {
                     backlog.Add( m_entryBuffer[x] );
                  }
               }
               else
               {
                  for(var x = 0; x <= m_entrySequence; ++x)
                  {
                     //backlog.Add( Tuple.Create( m_entryBuffer[x], x ) );
                     backlog.Add( m_entryBuffer[x] );
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
                  sink.Handle( entry );
               }
               catch(Exception ex)
               {
                  Debug.WriteLine( "** LOG [ERROR] in sink {0} ** : {1}".F( sink?.GetType().FullName ?? "null", ex ) );
               }
            }
         }
         return new DisposeAction( () => RemoveSink( sink ) );
      }

      /// <summary>
      /// Returns the number of entries of the given <see cref="LogLevel" />s that have been discarded due to
      /// <see cref="CurrentLevel" />.
      /// </summary>
      public Int32 GetNumberEntriesSkipped( params LogLevel[] severity )
      {
         if(severity == null || severity.Length == 0)
         {
            return 0;
         }
         return severity.Length == 1
            ? m_entriesSkipped[severity[0]]
            : new HashSet<LogLevel>( severity ).Aggregate( 0, ( count, level ) => count + m_entriesSkipped[level] );
      }

      /// <summary>
      /// Returns the number of entries of the given <see cref="LogLevel" />s that have been written to the log thus far.
      /// </summary>
      public Int32 GetNumberEntriesWritten( params LogLevel[] severity )
      {
         if(severity == null || severity.Length == 0)
         {
            return 0;
         }
         return severity.Length == 1
            ? m_entriesWritten[severity[0]]
            : new HashSet<LogLevel>( severity ).Aggregate( 0, ( count, level ) => count + m_entriesWritten[level] );
      }

      /// <inheritDoc cref="ILogControl.RemoveConverter" />
      public Boolean RemoveConverter( IObjectConverter converter )
      {
         lock(m_lock)
         {
            return m_converters.Remove( converter );
         }
      }

      /// <inheritDoc cref="ILogControl.RemoveSink" />
      public Boolean RemoveSink( ILogSink sink )
      {
         lock(m_lock)
         {
            return m_sinks.Remove( sink );
         }
      }

      /// <inheritDoc cref="ILogControl.RemoveSink" />
      public Boolean RemoveSinkOfType<T>()
         where T : ILogSink
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

      /// <inheritdoc />
      public void Write( LogLevel severity, String debugMessage, params Object[] debugMessageArgs )
      {
         CreateLogEntry( severity, null, debugMessage, debugMessageArgs );
      }

      /// <inheritdoc />
      public void Write( LogLevel severity, Object[] data )
      {
         CreateLogEntry( severity, data, null, null );
      }

      /// <inheritdoc />
      public void Write( LogLevel severity, Object[] data, String debugMessage, params Object[] debugMessageArgs )
      {
         CreateLogEntry( severity, data, debugMessage, debugMessageArgs );
      }

      private void CreateLogEntry( LogLevel severity, Object[] objects, String message, Object[] messageArguments )
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
            // TODO: Create a producer/consumer queue to process and send the log entries to sinks?
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

            // lock again to create and store the entry
            DeferredMessageLogEntry entry;
            lock(m_lock)
            {
               m_entriesWritten[severity] += 1;
               entry = new DeferredMessageLogEntry(
                  seqNum,
                  time,
                  severity,
                  data,
                  () =>
                  {
                     try
                     {
                        return message != null && messageArguments != null && messageArguments.Length > 0
                           ? String.Format(
                              DebugMessageFormatProvider ?? CultureInfo.InvariantCulture,
                              message,
                              messageArguments )
                           : message;
                     }
                     catch( /*Format*/Exception ex)
                     {
                        return "** LOG [ERROR] in formatter ** string={0} arg_length={1} error={2}".F(
                           message,
                           messageArguments != null ? messageArguments.Length.ToString() : "null",
                           ex.Message );
                     }
                  } );
               m_entryBuffer[seqNum % m_entryBuffer.Length] = entry;
            }

            foreach(var sink in m_sinks)
            {
               try
               {
                  sink.Handle( entry );
               }
               catch(Exception ex) when(!m_rethrowSinkExceptions)
               {
                  Debug.WriteLine( "** LOG [ERROR] in sink {0} ** : {1}".F( sink?.GetType().FullName ?? "null", ex ) );
               }
            }
         }
      }
   }
}
