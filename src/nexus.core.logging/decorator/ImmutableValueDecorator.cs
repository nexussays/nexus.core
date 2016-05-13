// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core.logging.decorator
{
   /// <summary>
   /// Adds the provided object to every log entry regardless of the entry's context/state. This object should be immutable.
   /// </summary>
   public class ImmutableValueDecorator<T> : ILogEntryDecorator
   {
      public ImmutableValueDecorator( T value )
      {
         Value = value;
      }

      public T Value { get; }

      public Object Augment( ILogEntry entry )
      {
         return Value;
      }
   }

   public static class ImmutableValueDecorator
   {
      /// <summary>
      /// Syntax sugar for <see cref="ImmutableValueDecorator" /> ctor
      /// </summary>
      public static ImmutableValueDecorator<T> Create<T>() where T : new()
      {
         return Create( new T() );
      }

      /// <summary>
      /// Syntax sugar for <see cref="ImmutableValueDecorator" /> ctor
      /// </summary>
      public static ImmutableValueDecorator<T> Create<T>( T value )
      {
         return new ImmutableValueDecorator<T>( value );
      }
   }
}