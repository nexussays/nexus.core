using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using nexus.core.resharper;

namespace nexus.core.logging
{
   /// <summary>
   /// Immutable implementation of <see cref="ILogEntry" /> which implementes <see cref="DebugMessage" /> as
   /// <see cref="Deferred{String}" />
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Advanced )]
   public sealed class DeferredMessageLogEntry
      : ILogEntry,
        IEquatable<ILogEntry>
   {
      [NotNull]
      private readonly IEqualityComparer<ILogEntry> m_comparer;
      private Deferred<String> m_debugMessage;

      /// <inheritdoc cref="DeferredMessageLogEntry" />
      public DeferredMessageLogEntry( Int32 sequenceId, DateTime time, LogLevel severity, IEnumerable<Object> data,
                                      Func<String> debugMessage, IEqualityComparer<ILogEntry> comparer = null )
      {
         Severity = severity;
         m_debugMessage = debugMessage;
         SequenceId = sequenceId;
         Timestamp = time;
         Data = data.ToArray();
         m_comparer = comparer ?? LogEntryComparers.SequenceId;
      }

      /// <inheritdoc />
      public Object[] Data { get; }

      /// <inheritdoc />
      public String DebugMessage => m_debugMessage.Value;

      /// <inheritdoc />
      public Int32 SequenceId { get; }

      /// <inheritdoc />
      public LogLevel Severity { get; }

      /// <inheritdoc />
      public DateTime Timestamp { get; }

      /// <inheritdoc />
      public override Boolean Equals( Object obj )
      {
         return Equals( obj as ILogEntry );
      }

      /// <inheritdoc />
      public Boolean Equals( ILogEntry other )
      {
         return m_comparer.Equals( this, other );
      }

      /// <inheritdoc />
      public override Int32 GetHashCode()
      {
         return m_comparer.GetHashCode( this );
      }

      /// <inheritdoc />
      public override String ToString()
      {
         return this.FormatAsString( LogUtils.TimestampFormatType.Iso8601, true );
      }
   }
}