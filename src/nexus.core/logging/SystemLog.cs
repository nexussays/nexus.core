// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
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
      private readonly IDictionary<LogLevel, Int32> m_entriesSkipped;
      private readonly IDictionary<LogLevel, Int32> m_entriesWritten;
      private readonly ILogEntry[] m_entryBuffer;
      private readonly Object m_lock = new Object();
      private readonly IDictionary<Type, IUntypedSerializer> m_serializers;
      private readonly ISet<ILogSink> m_sinks;
      private readonly ITimeProvider m_timeProvider;
      private Int32 m_entrySequence;

      public SystemLog( ITimeProvider time )
      {
         Contract.Requires( time != null );
         m_entrySequence = -1;
         m_entryBuffer = new ILogEntry[50];
         m_entriesWritten = new Dictionary<LogLevel, Int32>();
         m_entriesSkipped = new Dictionary<LogLevel, Int32>();
         m_serializers = new Dictionary<Type, IUntypedSerializer>();
         m_timeProvider = time;
         LogLevel = LogLevel.Trace;
         m_sinks = new HashSet<ILogSink>();
      }

      /// <inheritDoc />
      public String Id { get; set; }

      /// <summary>
      /// You should only access this in the main entry-point of your application code and **never** from libraries.
      /// </summary>
      public static SystemLog Instance { get; } = new SystemLog( new DefaultTimeProvider() );

      /// <summary>
      /// The level of log entires to write. Only entries of this level and above will be written to the log, others will be
      /// silently dropped.
      /// </summary>
      public LogLevel LogLevel { get; set; }

      /// <inheritDoc />
      public IEnumerable<ILogSink> Sinks => new List<ILogSink>( m_sinks );

      /// <summary>
      /// Add a log sink which be called each time a <see cref="ILogEntry" /> is created.
      /// </summary>
      /// <param name="sink"></param>
      public void AddSink( ILogSink sink )
      {
         if(sink == null)
         {
            throw new ArgumentNullException( nameof( sink ) );
         }

         Int32 seqNum;
         lock(m_lock)
         {
            m_sinks.Add( sink );
            seqNum = m_entrySequence;
         }

         // write out any buffered log entries to the sink. This isn't thread safe but oh well.
         if(seqNum > 0)
         {
            if(seqNum >= m_entryBuffer.Length)
            {
               // if the sequence number is greater than the length of the buffer then we've wrapped around,
               // so start at one past the latest entry (i.e., the oldest entry) and read to the end and then
               // wrap back around to index 0 and read up until the current seqNum
               var startIndex = (seqNum + 1) % m_entryBuffer.Length;
               /*
               // TODO: Finish implementing
               for(var x = 0; x < m_entryBuffer.Length; ++x)
               {
                  sink.Handle(m_entryBuffer[startIndex + x], startIndex);
               }
               */
            }
            else
            {
               for(var x = 0; x <= seqNum; ++x)
               {
                  try
                  {
                     sink.Handle( m_entryBuffer[x], x );
                  }
                  catch(Exception ex)
                  {
                     Debug.WriteLine(
                        "** LOG [ERROR] in sink {0} ** : {1}".F( sink?.GetType().FullName ?? "null", ex ) );
                  }
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
         if(severity >= LogLevel)
         {
            // get the time immediately in case serializing objects takes time
            var time = m_timeProvider.UtcNow;

            // run through all objects and see if any of them have serializers, if so remove the object and add the serialized result in its place
            var queue = new Queue<Object>( objects );
            while(queue.Count > 0)
            {
               if(m_serializers.ContainsKey( queue.Peek().GetType() ))
               {
                  var item = queue.Dequeue();
                  // add the serialized value to the queue in case there is further processing to perform
                  queue.Enqueue( m_serializers[item.GetType()].Serialize( item ) );
               }
            }

            Int32 seqNum;
            LogEntry entry;
            lock(m_lock)
            {
               m_entriesWritten[severity] += 1;
               m_entrySequence += 1;
               seqNum = m_entrySequence;
               entry = new LogEntry( Id, time, severity, message, messageArgs, queue.ToArray() );
               m_entryBuffer[seqNum % m_entryBuffer.Length] = entry;
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
            m_data = new List<Object>( attachedObjects );
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
      }
   }
}