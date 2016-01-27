// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.core.logging
{
   internal class LogEntry : ILogEntry
   {
      private readonly IList<Object> m_data;

      internal LogEntry( String id, DateTime time, LogLevel severity, String message, Object[] messageArguments,
                         Object[] attachedObjects )
      {
         LogId = id;
         MessageArguments = messageArguments;
         m_data = new List<Object>();
         if(attachedObjects != null)
         {
            foreach(var o in attachedObjects)
            {
               m_data.Add( o );
            }
         }
         Severity = severity;
         Message = message;
         Timestamp = time;
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

      internal void AttachObject<T>( T obj ) where T : class
      {
         m_data.Add( obj );
      }
   }
}