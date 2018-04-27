// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.core.logging
{
   /// <summary>
   /// Equality comparers for <see cref="ILogEntry" />. See: <see cref="Full" />, <see cref="SequenceId" />
   /// </summary>
   public static class LogEntryComparers
   {
      /// <inheritdoc cref="FullLogEntryComparer" />
      public static readonly FullLogEntryComparer Full = new FullLogEntryComparer();

      /// <inheritdoc cref="LogEntrySequenceComparer" />
      public static readonly IEqualityComparer<ILogEntry> SequenceId = new LogEntrySequenceComparer();

      /// <summary>
      /// Compare all fields aside from <see cref="ILogEntry.Data" />
      /// </summary>
      public sealed class FullLogEntryComparer : IEqualityComparer<ILogEntry>
      {
         /// <inheritdoc />
         public Boolean Equals( ILogEntry lhs, ILogEntry rhs )
         {
            if(ReferenceEquals( lhs, rhs ))
            {
               return true;
            }
            return !ReferenceEquals( null, rhs ) && !ReferenceEquals( null, lhs ) &&
                   String.Equals( lhs.DebugMessage, rhs.DebugMessage ) && lhs.SequenceId == rhs.SequenceId &&
                   lhs.Severity == rhs.Severity && lhs.Timestamp.Equals( rhs.Timestamp );
         }

         /// <inheritdoc />
         public Int32 GetHashCode( ILogEntry entry )
         {
            unchecked
            {
               var hashCode = entry.DebugMessage != null ? entry.DebugMessage.GetHashCode() : 0;
               hashCode = (hashCode * 397) ^ entry.SequenceId;
               hashCode = (hashCode * 397) ^ (Int32)entry.Severity;
               hashCode = (hashCode * 397) ^ entry.Timestamp.GetHashCode();
               return hashCode;
            }
         }
      }

      /// <summary>
      /// When comparing <see cref="ILogEntry" /> from the same log, simply check their <see cref="ILogEntry.SequenceId" /> to
      /// compare them
      /// </summary>
      public sealed class LogEntrySequenceComparer
         : IEqualityComparer<ILogEntry>,
           IComparer<ILogEntry>
      {
         /// <inheritdoc />
         public Int32 Compare( ILogEntry lhs, ILogEntry rhs )
         {
            if(ReferenceEquals( lhs, rhs ) || ReferenceEquals( null, rhs ) || ReferenceEquals( null, lhs ))
            {
               return 0;
            }
            return lhs.SequenceId.CompareTo( rhs.SequenceId );
         }

         /// <inheritdoc />
         public Boolean Equals( ILogEntry lhs, ILogEntry rhs )
         {
            if(ReferenceEquals( lhs, rhs ))
            {
               return true;
            }
            return !ReferenceEquals( null, rhs ) && !ReferenceEquals( null, lhs ) &&
                   lhs.SequenceId.Equals( rhs.SequenceId );
         }

         /// <inheritdoc />
         public Int32 GetHashCode( ILogEntry entry )
         {
            return entry.SequenceId.GetHashCode();
         }
      }
   }
}
