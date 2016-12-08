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
   /// TODO: Implement: ILog CreateChildLog( String id );
   public interface ILog
   {
      /// <summary>
      /// Only entries of this level or higher will be written to the log, others will be silently dropped.
      /// </summary>
      LogLevel CurrentLevel { get; }

      /// <summary>
      /// A unique name for this log within the application, if desired
      /// </summary>
      String Id { get; }

      /// <summary>
      /// Write <see cref="LogLevel.Error" /> level log data
      /// </summary>
      void Error( Object[] objects );

      /// <summary>
      /// Write <see cref="LogLevel.Error" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      void Error( String message, params Object[] messageArgs );

      /// <summary>
      /// Write <see cref="LogLevel.Error" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      void Error( Object[] objects, String message, params Object[] messageArgs );

      /// <summary>
      /// Write <see cref="LogLevel.Info" /> level log data
      /// </summary>
      void Info( Object[] objects );

      /// <summary>
      /// Write <see cref="LogLevel.Info" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      void Info( String message, params Object[] messageArgs );

      /// <summary>
      /// Write <see cref="LogLevel.Info" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      void Info( Object[] objects, String message, params Object[] messageArgs );

      /// <summary>
      /// Write <see cref="LogLevel.Trace" /> level log data
      /// </summary>
      void Trace( Object[] objects );

      /// <summary>
      /// Write <see cref="LogLevel.Trace" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      void Trace( String message, params Object[] messageArgs );

      /// <summary>
      /// Write <see cref="LogLevel.Trace" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      void Trace( Object[] objects, String message, params Object[] messageArgs );

      /// <summary>
      /// Write <see cref="LogLevel.Warn" /> level log data
      /// </summary>
      void Warn( Object[] objects );

      /// <summary>
      /// Write <see cref="LogLevel.Warn" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      void Warn( String message, params Object[] messageArgs );

      /// <summary>
      /// Write <see cref="LogLevel.Warn" /> level log data
      /// </summary>
      [StringFormatMethod( "message" )]
      void Warn( Object[] objects, String message, params Object[] messageArgs );
   }
}