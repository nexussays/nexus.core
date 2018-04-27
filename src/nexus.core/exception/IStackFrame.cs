// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;
using System.Reflection;

namespace nexus.core.exception
{
   /// <summary>
   /// Interface to represent a stack frame in memory
   /// </summary>
   public interface IStackFrame
   {
      /// <summary>
      /// The name of the <see cref="Assembly" /> the executing code is from
      /// </summary>
      String AssemblyName { get; }

      /// <summary>
      /// The version of the <see cref="Assembly" /> the executing code is from
      /// </summary>
      String AssemblyVersion { get; }

      /// <summary>
      /// The class name the executing code is defined in
      /// </summary>
      String ClassName { get; }

      /// <summary>
      /// The column in the source file the executing code is on
      /// </summary>
      Int32 Column { get; }

      /// <summary>
      /// The name of the source file of the executing code
      /// </summary>
      String FileName { get; }

      /// <summary>
      /// The line in the source file the exeucting code is from
      /// </summary>
      Int32 Line { get; }

      /// <summary>
      /// The name of the method that is running
      /// </summary>
      String MethodName { get; }

      /// <summary>
      /// The namespace that contains the class and method of the executing code
      /// </summary>
      String Namespace { get; }
   }

   /// <summary>
   /// Extension methods for <see cref="IStackFrame" />
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class StackFrameExtensions
   {
      /// <summary>
      /// Return the full namespace, class name, and assembly info of this stack frame
      /// </summary>
      public static String AssemblyQualifiedName( this IStackFrame frame )
      {
         return frame == null
            ? ""
            : frame.NamespaceQualifiedName() + "," + frame.AssemblyName + "@" + frame.AssemblyVersion;
      }

      /// <summary>
      /// Return the full namespace and class name of this stack frame
      /// </summary>
      public static String NamespaceQualifiedName( this IStackFrame frame )
      {
         return frame == null ? "" : (frame.Namespace.IsNullOrEmpty() ? "" : frame.Namespace + ".") + frame.ClassName;
      }
   }
}
