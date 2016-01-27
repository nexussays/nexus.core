// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.core
{
   /// <summary>
   /// Interface for Exceptions that isn't a sealed class tied to .Net
   /// </summary>
   public interface IException
   {
      String ClassName { get; }

      /// <summary>
      /// Indicates whether or not the exception was handled by the application code in order to determine severity of the
      /// exception. Handled exceptions are less severe because they don't impact the user experience like an unhandled exception
      /// would.
      /// </summary>
      Boolean Handled { get; }

      IException InnerError { get; }

      String Message { get; }

      /// <summary>
      /// A detailed breakdown of the stack, if available. Typically only one of <see cref="StackFrames" /> or
      /// <see cref="StackTrace" /> will be set.
      /// </summary>
      IEnumerable<IExceptionStackFrame> StackFrames { get; }

      /// <summary>
      /// A text representation of the stack frames. Typically only one of <see cref="StackFrames" /> or
      /// <see cref="StackTrace" /> will be set.
      /// </summary>
      String StackTrace { get; }
   }

   public interface IExceptionStackFrame
   {
      String AssemblyName { get; }

      String ClassName { get; }

      Int32 Column { get; }

      String FileName { get; }

      Int32 Line { get; }

      String MethodName { get; }

      String Namespace { get; }
   }

   public static class ExceptionExtensions
   {
      public static String AssemblyQualifiedName( this IExceptionStackFrame frame )
      {
         return frame == null ? "" : frame.NamespaceQualifiedName() + ", " + frame.AssemblyName;
      }

      public static String NamespaceQualifiedName( this IExceptionStackFrame frame )
      {
         return frame == null ? "" : (frame.Namespace.IsNullOrEmpty() ? "" : frame.Namespace + ".") + frame.ClassName;
      }
   }
}