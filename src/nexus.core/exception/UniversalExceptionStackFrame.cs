// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.exception
{
   /// <summary>
   /// A standardized stack frame structure that can be used on any platform
   /// </summary>
   public sealed class UniversalExceptionStackFrame : IStackFrame
   {
      /// <summary>
      /// </summary>
      public UniversalExceptionStackFrame( String fileName, Int32 lineNumber, Int32 columnNumber, String methodName,
                                           String className, String namespaceName, String assemblyName,
                                           String assemblyVersion )
      {
         FileName = fileName;
         Line = lineNumber;
         Column = columnNumber;
         MethodName = methodName;
         ClassName = className;
         Namespace = namespaceName;
         AssemblyName = assemblyName;
         AssemblyVersion = assemblyVersion;
      }

      /// <inheritdoc />
      public String AssemblyName { get; }

      /// <inheritdoc />
      public String AssemblyVersion { get; }

      /// <inheritdoc />
      public String ClassName { get; }

      /// <inheritdoc />
      public Int32 Column { get; }

      /// <inheritdoc />
      public String FileName { get; }

      /// <inheritdoc />
      public Int32 Line { get; }

      /// <inheritdoc />
      public String MethodName { get; }

      /// <inheritdoc />
      public String Namespace { get; }

      /// <inheritdoc cref="object.ToString" />
      public override String ToString()
      {
         return "at " + (Namespace == null ? "" : Namespace + ".") + ClassName + "." + MethodName +
                (FileName != null ? " in {0}:{1}".F( FileName, Line ) : "");
      }
   }
}
