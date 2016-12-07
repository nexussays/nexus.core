// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;

namespace nexus.core.logging.serializer
{
   public static class LogEntrySerializer
   {
      public static ILogEntrySerializer Create( Func<ILogEntry, String> serializer )
      {
         return new DynamicLogEntrySerializer( serializer );
      }

      private sealed class DynamicLogEntrySerializer : ILogEntrySerializer
      {
         private readonly Func<ILogEntry, String> m_serializer;

         public DynamicLogEntrySerializer( Func<ILogEntry, String> serializer )
         {
            Contract.Requires( serializer != null );
            m_serializer = serializer;
         }

         public String Serialize( ILogEntry source )
         {
            return m_serializer( source );
         }
      }
   }
}