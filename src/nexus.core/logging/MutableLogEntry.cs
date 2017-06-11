using System;
using System.Collections.Generic;
using System.ComponentModel;
using nexus.core.resharper;

namespace nexus.core.logging
{
   /// <summary>
   /// Mutable <see cref="ILogEntry" />, sets <see cref="ILogEntry.Data" /> to an empty array to start.
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Advanced )]
   public sealed class MutableLogEntry
      : ILogEntry,
        IEquatable<ILogEntry>
   {
      [NotNull]
      private readonly IEqualityComparer<ILogEntry> m_comparer;

      /// <inheritdoc cref="MutableLogEntry" />
      public MutableLogEntry()
         : this( null )
      {
      }

      /// <inheritdoc cref="MutableLogEntry" />
      public MutableLogEntry( IEqualityComparer<ILogEntry> comparer )
      {
         m_comparer = comparer ?? LogEntryComparers.SequenceId;
      }

      /// <inheritdoc />
      public Object[] Data { get; set; } = { };

      /// <inheritdoc />
      public String DebugMessage { get; set; }

      /// <inheritdoc />
      public Int32 SequenceId { get; set; }

      /// <inheritdoc />
      public LogLevel Severity { get; set; }

      /// <inheritdoc />
      public DateTime Timestamp
      {
         get; // { return m_timestamp; }
         set; // { m_timestamp = value.ToUniversalTime(); }
      }

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
         return this.FormatAsString( LogUtils.TimestampFormatType.Iso8601 ); //, true );
      }
   }
}