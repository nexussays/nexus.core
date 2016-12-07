using System;
using System.IO;
using nexus.core.serialization;

namespace nexus.core.logging.sink
{
   /// <summary>
   /// Serialize each log entry to a string and output to the provided textWriter
   /// </summary>
   public sealed class StringLogSink : ILogSink
   {
      private readonly TextWriter m_errorOutput;
      private readonly TextWriter m_output;
      private readonly ISerializer<ILogEntry, String> m_serializer;

      /// <param name="serializer">Serializer from log entry to string</param>
      /// <param name="output">
      /// Serialized log entries will be written to this text writer if severity is lower than
      /// <see cref="LogLevel.Error" />.
      /// </param>
      /// <param name="errorOutput">
      /// If provided, log entries of severity <see cref="LogLevel.Error" /> will be written to this
      /// text writer. If not provided or null, <see cref="output" /> will be used
      /// </param>
      public StringLogSink( ISerializer<ILogEntry, String> serializer, TextWriter output, TextWriter errorOutput = null )
      {
         m_serializer = serializer;
         m_output = output;
         m_errorOutput = errorOutput ?? m_output;
      }

      public void Handle( ILogEntry entry, Int32 sequenceNumber )
      {
         try
         {
            var str = m_serializer.Serialize( entry );
            if(entry.Severity == LogLevel.Error)
            {
               m_errorOutput.WriteLine( str );
            }
            else
            {
               m_output.WriteLine( str );
            }
         }
         catch(Exception ex)
         {
            m_output.WriteLine(
               "** LOG [ERROR] in serializer {0} ** : {1}".F( m_serializer?.GetType().Name ?? "null", ex ) );
         }
      }
   }
}