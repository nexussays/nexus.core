using System;

namespace nexus.core.logging.decorator
{
   /// <summary>
   /// Adds the provided object to every log entry regardless of the entry's context/state. This object should be immutable.
   /// </summary>
   public class ImmutableValueDecorator<T> : ILogEntryDecorator
   {
      public ImmutableValueDecorator( T data )
      {
         Data = data;
      }

      public T Data { get; }

      public Object Augment( ILogEntry entry )
      {
         return Data;
      }
   }
}