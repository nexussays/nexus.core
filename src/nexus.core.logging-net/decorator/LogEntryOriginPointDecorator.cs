// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Diagnostics;

namespace nexus.core.logging.decorator
{
   /// <summary>
   /// Will include the namespace, class, method, and line number of the originating log call. This impacts performance, so
   /// only enable in development or when debugging.
   /// </summary>
   public class LogEntryOriginPointDecorator : ILogEntryDecorator
   {
      public Object Augment( ILogEntry entry, Exception ex, Boolean exceptionHandled )
      {
         //var frame = GetCallingStackFrame( skipFrames + 1 );
         var frame = GetCallingStackFrame( true, ex );
         var sourceMethod = frame.GetMethod();
         if(sourceMethod != null && sourceMethod.ReflectedType != null)
         {
            var sourceType = sourceMethod.ReflectedType;
            //if(sourceType != null) // code contracts says this is always true and fails with an error if it exists
            return new LogEntryOriginPoint
            {
               AssemblyName = sourceType.Assembly.FullName,
               Namespace = sourceType.Namespace,
               ClassName = sourceType.Name,
               MethodName = sourceMethod.Name,
               Line = frame.GetFileLineNumber(),
               Column = frame.GetFileColumnNumber()
            };
         }
         return null;
      }

      private StackFrame GetCallingStackFrame( Int32 skipFrames )
      {
         return new StackFrame( skipFrames + 1 );
      }

      private StackFrame GetCallingStackFrame( Boolean detailedInfo, Exception ex )
      {
         var stackTrace = ex != null ? new StackTrace( ex, detailedInfo ) : null;
         if(stackTrace == null || stackTrace.FrameCount == 0)
         {
            //reflect on the IL execution to find the source of this Log
            stackTrace = new StackTrace( 2, detailedInfo );
         }

         var frameNumber = 0;
         var frame = stackTrace.GetFrame( frameNumber );
         // get the proper stack frame by ignoring any ILog implementations
         while(frameNumber < stackTrace.FrameCount &&
               (typeof(ILog).IsAssignableFrom( frame.GetMethod().ReflectedType ) ||
                typeof(Log) == frame.GetMethod().ReflectedType))
         {
            frame = stackTrace.GetFrame( (++frameNumber) );
         }
         return frame;
      }

      /// <summary>
      /// Extra data to augment a <see cref="ILogEntry" />
      /// </summary>
      public sealed class LogEntryOriginPoint : ILogEntryOriginPoint
      {
         public String AssemblyName { get; internal set; }

         public String ClassName { get; internal set; }

         public Int32 Column { get; internal set; }

         public String FileName { get; internal set; }

         public Int32 Line { get; internal set; }

         public String MethodName { get; internal set; }

         public String Namespace { get; internal set; }
      }
   }
}