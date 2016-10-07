// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.core.exception;
using nexus.core.serialization;

namespace nexus.core.logging.decorator
{
   /// <summary>
   /// If the <see cref="ILogEntry" /> has an <see cref="Exception" /> attached, then serialize it to
   /// <see cref="IException" /> and decorate.
   /// </summary>
   public class LogEntryExceptionSerializerDecorator : ILogEntryDecorator
   {
      private readonly ISerializer<Exception, IException> m_serializer;

      public LogEntryExceptionSerializerDecorator( ISerializer<Exception, IException> serializer )
      {
         if(serializer == null)
         {
            throw new ArgumentException(
               "{0} must be provided a valid {1}".F( GetType().Name, nameof( ISerializer<Exception, IException> ) ),
               nameof( serializer ) );
         }
         m_serializer = serializer;
      }

      public Object Augment( ILogEntry entry )
      {
         var ex = entry.GetData<Exception>();
         return ex != null ? m_serializer.Serialize( ex ) : null;
      }
   }
}