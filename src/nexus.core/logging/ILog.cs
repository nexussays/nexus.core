// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.core.resharper;

namespace nexus.core.logging
{
   /// <summary>
   /// Interface to write information to a log
   /// </summary>
   public interface ILog
   {
      /// <summary>
      /// A unique name for this log, if desired
      /// </summary>
      String Id { get; }

      /// <summary>
      /// Only entries of this level or higher will be written to the log, others will be silently dropped.
      /// </summary>
      LogLevel LogLevel { get; }

      void Error( Object[] objects );

      [StringFormatMethod( "message" )]
      void Error( String message, params Object[] messageArgs );

      [StringFormatMethod( "message" )]
      void Error( Object[] objects, String message, params Object[] messageArgs );

      void Info( Object[] objects );

      [StringFormatMethod( "message" )]
      void Info( String message, params Object[] messageArgs );

      [StringFormatMethod( "message" )]
      void Info( Object[] objects, String message, params Object[] messageArgs );

      void Trace( Object[] objects );

      [StringFormatMethod( "message" )]
      void Trace( String message, params Object[] messageArgs );

      [StringFormatMethod( "message" )]
      void Trace( Object[] objects, String message, params Object[] messageArgs );

      void Warn( Object[] objects );

      [StringFormatMethod( "message" )]
      void Warn( String message, params Object[] messageArgs );

      [StringFormatMethod( "message" )]
      void Warn( Object[] objects, String message, params Object[] messageArgs );
   }
}