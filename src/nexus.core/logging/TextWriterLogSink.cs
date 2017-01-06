using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace nexus.core.logging
{
   /// <summary>
   /// Convert each <see cref="ILogEntry" /> to a string and write it to a <see cref="TextWriter" />
   /// <example>
   ///       <code>
   /// public sealed class ConsoleLogSink : TextWriterLogSink
   /// {
   ///    public ConsoleLogSink( IObjectConverter&lt;ILogEntry, String&gt; serializer )
   ///             : base( serializer, Console.Out, Console.Error ) { }
   /// }
   /// </code>
   /// </example>
   /// </summary>
   public class TextWriterLogSink : ILogSink
   {
      private readonly TextWriter m_errorOutput;
      private readonly IObjectConverter<ILogEntry, String> m_logToString;
      private readonly TextWriter m_output;

      /// <param name="converter">Serializer from log entry to string</param>
      /// <param name="output">
      /// Serialized log entries will be written to this text writer if severity is lower than
      /// <see cref="LogLevel.Error" />.
      /// </param>
      /// <param name="errorOutput">
      /// If provided, log entries of severity <see cref="LogLevel.Error" /> will be written to this
      /// text writer. If not provided or null, <see cref="output" /> will be used
      /// </param>
      public TextWriterLogSink( IObjectConverter<ILogEntry, String> converter, TextWriter output,
                                TextWriter errorOutput = null )
      {
         Contract.Requires<ArgumentNullException>( output != null );
         m_logToString = converter;
         m_output = output;
         m_errorOutput = errorOutput ?? m_output;
      }

      public void Handle( ILogEntry entry, Int32 sequenceNumber )
      {
         try
         {
            // TODO: Check sequence number? or write it out?
            (entry.Severity == LogLevel.Error ? m_errorOutput : m_output).WriteLine( m_logToString.Convert( entry ) );
         }
         catch(Exception ex)
         {
            m_output.WriteLine(
               "** LOG [ERROR] in serializer {0} ** : {1}".F( m_logToString?.GetType().Name ?? "null", ex ) );
         }
      }
   }
}