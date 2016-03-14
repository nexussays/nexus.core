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
      public static ImmutableValueDecorator<T> Create<T>() where T : new()
      {
         return Create( new T() );
      }

      public static ImmutableValueDecorator<T> Create<T>( T value )
      {
         return new ImmutableValueDecorator<T>( value );
      }
   }
}