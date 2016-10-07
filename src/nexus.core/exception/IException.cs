// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.core.exception
{
   /// <summary>
   /// Interface for Exceptions that isn't a sealed class tied to .Net
   /// </summary>
   public interface IException
   {
      String ClassName { get; }

      IException InnerError { get; }

      String Message { get; }

      /// <summary>
      /// A detailed breakdown of the stack, if available. Typically only one of <see cref="StackFrames" /> or
      /// <see cref="StackTrace" /> will be set.
      /// </summary>
      IEnumerable<IStackFrame> StackFrames { get; }

      /// <summary>
      /// A text representation of the stack frames. Typically only one of <see cref="StackFrames" /> or
      /// <see cref="StackTrace" /> will be set.
      /// </summary>
      /// TODO: Remove the need for this and parse stacktrace strings on each platform
      String StackTrace { get; }
   }
}