// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.core.logging
{
   public static class LogControlExtensions
   {
      /// <summary>
      /// Chainable version of setting <see cref="ILogControl.LogLevel" />
      /// </summary>
      public static T SetLogLevel<T>( this T log, LogLevel level ) where T : ILogControl
      {
         log.LogLevel = level;
         return log;
      }

      /// <summary>
      /// Chainable version of setting <see cref="ILogControl.Serializer" />
      /// </summary>
      public static ILogSource SetSerializer<T>( this ILogSource log ) where T : class, ILogEntrySerializer, new()
      {
         log.Serializer = new T();
         return log;
      }

      /// <summary>
      /// Chainable version of setting <see cref="ILogControl.Serializer" />
      /// </summary>
      public static T SetSerializer<T>( this T log, ILogEntrySerializer serializer ) where T : ILogControl
      {
         log.Serializer = serializer;
         return log;
      }

      /// <summary>
      /// Chainable version of setting <see cref="ILogControl.Serializer" />
      /// </summary>
      public static ILogControl SetSerializer<T>( this ILogControl log ) where T : class, ILogEntrySerializer, new()
      {
         log.Serializer = new T();
         return log;
      }

      /// <summary>
      /// Chainable version of <see cref="ILogControl.AddDecorator" />
      /// </summary>
      public static ILogSource WithDecorator<T>( this ILogSource log ) where T : class, ILogEntryDecorator, new()
      {
         log.AddDecorator( new T() );
         return log;
      }

      /// <summary>
      /// Chainable version of <see cref="ILogControl.AddDecorator" />
      /// </summary>
      public static ILogControl WithDecorator<T>( this ILogControl log ) where T : class, ILogEntryDecorator, new()
      {
         log.AddDecorator( new T() );
         return log;
      }

      /// <summary>
      /// Chainable version of <see cref="ILogControl.AddDecorator" />
      /// </summary>
      public static T WithDecorator<T>( this T log, ILogEntryDecorator decorator ) where T : ILogControl
      {
         log.AddDecorator( decorator );
         return log;
      }

      /// <summary>
      /// Chainable version of <see cref="ILogControl.AddSink" />
      /// </summary>
      public static ILogSource WithSink<T>( this ILogSource log ) where T : class, ILogSink, new()
      {
         log.AddSink( new T() );
         return log;
      }

      /// <summary>
      /// Chainable version of <see cref="ILogControl.AddSink" />
      /// </summary>
      public static ILogControl WithSink<T>( this ILogControl log ) where T : class, ILogSink, new()
      {
         log.AddSink( new T() );
         return log;
      }

      /// <summary>
      /// Chainable version of <see cref="ILogControl.AddSink" />
      /// </summary>
      public static T WithSink<T>( this T log, ILogSink sink ) where T : ILogControl
      {
         log.AddSink( sink );
         return log;
      }
   }
}