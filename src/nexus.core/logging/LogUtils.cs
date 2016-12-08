// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;

namespace nexus.core.logging
{
   public static class LogUtils
   {
      /// <summary>
      /// Factory method to create <see cref="ILogSink" /> instance
      /// </summary>
      public static ILogSink CreateLogSink( Action<ILogEntry, Int32> handler )
      {
         Contract.Requires<ArgumentNullException>( handler != null );
         return new DynamicLogSink( handler );
      }

      /// <summary>
      /// Factory method to create <see cref="ILogSink" /> instance
      /// </summary>
      public static ILogSink CreateLogSink( Action<ILogEntry> handler )
      {
         Contract.Requires<ArgumentNullException>( handler != null );
         // ReSharper disable once PossibleNullReferenceException
         return new DynamicLogSink( ( e, s ) => handler( e ) );
      }

      private sealed class DynamicLogSink : ILogSink
      {
         private readonly Action<ILogEntry, Int32> m_handler;

         public DynamicLogSink( Action<ILogEntry, Int32> handler )
         {
            m_handler = handler;
         }

         public void Handle( ILogEntry entry, Int32 sequenceNumber )
         {
            m_handler( entry, sequenceNumber );
         }
      }
   }
}