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

      public override String ToString()
      {
         return "{1}: {0}{2}{3}{4}".F(
            ClassName,
            Message,
            StackFrames == null
               ? (StackTrace == null ? "" : $"\n{StackTrace}")
               : "\n" + String.Join( "\n", StackFrames.Select( x => x.ToString() ) ),
            InnerError != null ? "\n" + InnerError : "" );
      }
   }
}