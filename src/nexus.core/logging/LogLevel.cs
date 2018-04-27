// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.core.logging
{
   /// <summary>
   /// Defines available log levels (or categories) used to provide additional context and to organize and filter your
   /// logging output.
   /// </summary>
   public enum LogLevel
   {
      /// <summary>
      /// <see cref="LogLevel" /> of <see cref="Trace" /> is by far the most commonly used severity and should provide context to
      /// understand the flow of the application and the steps leading up to errors and warnings. These messages should be useful
      /// in debugging the application in both development and in troubleshooting customer-reported issues in production. The
      /// value, location, and number of Trace statements will change over time the application evolves (eg, it is often helpful
      /// early in an application's life to log user input such as changing displays or tabs).
      /// </summary>
      Trace = 0,

      /// <summary>
      /// <see cref="LogLevel" /> of <see cref="Info" /> indicates important information that should be logged under normal
      /// conditions such as state changes to your application, successful initialization, services starting and stopping, or
      /// successful completion of significant transactions. Viewing a log showing Info entries and above should give a quick
      /// overview of major state changes in the process providing top-level context for understanding any warnings or errors
      /// that also occur.
      /// </summary>
      Info = 1,

      /// <summary>
      /// <see cref="LogLevel" /> of <see cref="Warn" /> indicates a potential cause of concern. For example, expected transient
      /// environmental conditions such as the loss of network or loss database connectivity, missing secondary data, switching
      /// from a primary to backup server, should be logged as Warn, not Error. Viewing a log filtered to show only Warn and
      /// Error entries may give quick insight into early hints at the root cause of a subsequent error. Warn should be used
      /// sparingly so that such entries don't become meaningless. For example, loss of network access should be a Warn, or
      /// possibly and Error, in a server application, but might be simply an Info in a desktop or mobile app.
      /// </summary>
      Warn = 2,

      /// <summary>
      /// <see cref="LogLevel" /> of <see cref="Error" /> indicates a problem that needs to be investigated. Developers should be
      /// notified automatically, but don't need to be dragged out of bed. By filtering a log to look at Error entries and above
      /// you get an overview of error frequency and can quickly identify the initiating failure that might have resulted in a
      /// cascade of additional errors. Tracking error rates versus application usage can yield useful quality metrics such as
      /// MTBF which can be used to assess overall quality. For example, this metric might help inform decisions about whether or
      /// not another beta testing cycle is needed before a release.
      /// </summary>
      Error = 3
   }
}
