// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Text;

namespace nexus.core.logging.serializer
{
   public class CustomLogEntrySerializer : ILogEntrySerializer
   {
      private readonly List<Func<ILogEntry, String>> m_formatters;

      public CustomLogEntrySerializer()
      {
         m_formatters = new List<Func<ILogEntry, String>>();
      }

      public IEnumerable<Func<ILogEntry, String>> Formatters => m_formatters;

      public void AddStep( Func<ILogEntry, String> formatter )
      {
         m_formatters.Add( formatter );
      }

      /// <summary>
      /// If the provided object is attached to a given entry then this step will be executed, otherwise not
      /// </summary>
      public void AddStepIfAttached<T>( Func<ILogEntry, T, String> formatter ) where T : class
      {
         AddStep(
            entry =>
            {
               var value = entry.GetData<T>();
               return value != null ? formatter( entry, value ) : String.Empty;
            } );
      }

      public String Serialize( ILogEntry entry )
      {
         var builder = new StringBuilder();
         foreach(var formatter in m_formatters)
         {
            builder.Append( formatter( entry ) );
         }
         return builder.ToString();
      }
   }
}