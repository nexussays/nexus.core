// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;

namespace nexus.core.exception
{
   /// <summary>
   /// An imeplmentation of <see cref="IException" /> that works across all supported platforms
   /// </summary>
   public sealed class UniversalException : IException
   {
      /// <summary>
      /// </summary>
      public UniversalException( String message, String fullClassName, IException innerException,
                                 IEnumerable<IStackFrame> buildStackTrace = null, String stackTrace = null )
      {
         Message = message;
         StackFrames = buildStackTrace;
         ClassName = fullClassName;
         InnerError = innerException;
         StackTrace = stackTrace;
      }

      /// <inheritdoc />
      public String ClassName { get; }

      /// <inheritdoc />
      public IException InnerError { get; }

      /// <inheritdoc />
      public String Message { get; }

      /// <inheritdoc />
      public IEnumerable<IStackFrame> StackFrames { get; }

      /// <inheritdoc />
      public String StackTrace { get; }

      /// <summary>
      /// Output the exception and complete stack trace
      /// </summary>
      public override String ToString()
      {
         return Message + ": " + ClassName +
                (StackFrames != null
                   ? "\n" + String.Join( "\n", StackFrames.Select( x => x.ToString() ) )
                   : (StackTrace != null ? $"\n{StackTrace}" : "")) + (InnerError != null ? "\n" + InnerError : "");
      }
   }
}
