// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.exception
{
   /// <summary>
   /// Serialize an <see cref="Exception" /> to <see cref="IException" /> with a textual (ie, untyped) stacktrace
   /// </summary>
   public sealed class NativeExceptionConverter : IObjectConverter<Exception, IException>
   {
      /// <summary>
      /// Singleton instance of <see cref="NativeExceptionConverter" />
      /// </summary>
      public static readonly NativeExceptionConverter Instance = new NativeExceptionConverter();

      /// <inheritdoc />
      public IException Convert( Exception exception )
      {
         return new UniversalException(
            exception.Message,
            exception.GetType().FullName,
            exception.InnerException != null ? Convert( exception.InnerException ) : null,
            null,
            exception.StackTrace );
      }
   }
}
