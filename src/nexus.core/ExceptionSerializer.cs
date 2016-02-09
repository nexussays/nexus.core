// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.core
{
   public class ExceptionSerializer : IExceptionSerializer
   {
      public static readonly ExceptionSerializer Instance = new ExceptionSerializer();

      public IException Serialize( Exception exception )
      {
         return Serialize( exception, null );
      }

      public IException Serialize( Tuple<Exception, Boolean?> source )
      {
         return Serialize( source.Item1, source.Item2 );
      }

      public IException Serialize( Exception exception, Boolean? handled )
      {
         var exceptionType = exception.GetType();
         return new UniversalException
         {
            Message = exception.Message,
            StackTrace = exception.StackTrace,
            ClassName = exceptionType.FullName,
            Handled = handled,
            InnerError = exception.InnerException != null ? Serialize( exception.InnerException ) : null
         };
      }

      private sealed class UniversalException : IException
      {
         public String ClassName { get; internal set; }

         public Boolean? Handled { get; internal set; }

         public IException InnerError { get; internal set; }

         public String Message { get; internal set; }

         public IEnumerable<IStackFrame> StackFrames => null;

         public String StackTrace { get; internal set; }

         public override String ToString()
         {
            return "{1}: {0}{2}\n{3}{4}".F(
               Handled.HasValue ? (Handled.Value ? "[ HANDLED ] " : "[UNHANDLED] ") : "",
               ClassName,
               Message,
               StackTrace,
               InnerError != null ? "\n" + InnerError : "" );
         }
      }
   }
}