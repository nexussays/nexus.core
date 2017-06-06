using System;
using System.Diagnostics.Contracts;
using System.IO;
using nexus.core.resharper;

namespace nexus.core.logging.sink
{
   /// <summary>
   /// Convert each <see cref="ILogEntry" /> to a string and write it to a <see cref="TextWriter" />
   /// </summary>
   /// <example>
   ///    <code>public sealed class ConsoleLogSink : TextWriterLogSink
   /// {
   ///    public ConsoleLogSink( IObjectConverter&lt;ILogEntry, String&gt; serializer )
   ///             : base( serializer, false, Console.Out, Console.Error ) { }
   /// }</code>
   /// </example>
   public class TextWriterLogSink
      : ILogSink,
        IDisposable
   {
      private readonly Boolean m_disposeWriters;
      private readonly TextWriter m_errorOutput;
      private readonly IObjectConverter<ILogEntry, String> m_logEntryToString;
      private readonly TextWriter m_output;

      /// <param name="logEntryToString">Serializer from log entry to string</param>
      /// <param name="disposeWriters">
      /// If <c>true</c>, this sink owns the provided writers so <paramref name="output" /> and
      /// <paramref name="errorOutput" /> will be disposed when this sink is. If <c>false</c>, the writers are maneged externally
      /// and calling <see cref="Dispose" /> on this writer will ne a no-op.
      /// </param>
      /// <param name="output">
      /// Serialized log entries will be written to this text writer if severity is lower than
      /// <see cref="LogLevel.Error" />.
      /// </param>
      /// <param name="errorOutput">
      /// If provided, log entries of severity <see cref="LogLevel.Error" /> will be written to this
      /// text writer. If not provided or null, <paramref name="output" /> will be used
      /// </param>
      public TextWriterLogSink( IObjectConverter<ILogEntry, String> logEntryToString, Boolean disposeWriters,
                                [NotNull] TextWriter output, TextWriter errorOutput = null )
      {
         Contract.Requires( output != null );
         Contract.Requires<ArgumentNullException>( output != null );

         m_disposeWriters = disposeWriters;
         m_logEntryToString = logEntryToString;
         m_output = output;
         m_errorOutput = errorOutput ?? m_output;
      }

      /// <inheritdoc />
      public void Dispose()
      {
         if(m_disposeWriters)
         {
            m_output.Dispose();
            m_errorOutput?.Dispose();
         }
      }

      /// <inheritdoc />
      public void Handle( ILogEntry entry )
      {
         // TODO: Check sequence number? or write it out?
         (entry.Severity == LogLevel.Error ? m_errorOutput : m_output).WriteLine( m_logEntryToString.Convert( entry ) );
      }
   }
}