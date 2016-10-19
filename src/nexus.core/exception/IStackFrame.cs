// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.exception
{
   public interface IStackFrame
   {
      String AssemblyName { get; }

      String AssemblyVersion { get; }

      String ClassName { get; }

      Int32 Column { get; }

      String FileName { get; }

      Int32 Line { get; }

      String MethodName { get; }

      String Namespace { get; }
   }

   public static class StackFrameExtensions
   {
      public static String AssemblyQualifiedName( this IStackFrame frame )
      {
         return frame == null ? "" : frame.NamespaceQualifiedName() + ", " + frame.AssemblyName;
      }

      public static String NamespaceQualifiedName( this IStackFrame frame )
      {
         return frame == null ? "" : (frame.Namespace.IsNullOrEmpty() ? "" : frame.Namespace + ".") + frame.ClassName;
      }
   }
}