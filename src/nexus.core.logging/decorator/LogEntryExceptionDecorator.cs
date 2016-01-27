using System;

namespace nexus.core.logging.decorator
{
   /// <summary>
   /// Serialize any attached <see cref="Exception" /> objects and attach as an <see cref="IException" />
   /// </summary>
   public class LogEntryExceptionDecorator : ILogEntryDecorator
   {
      private readonly IExceptionSerializer m_serializer;

      public LogEntryExceptionDecorator( IExceptionSerializer serializer )
      {
         if(serializer == null)
         {
            throw new ArgumentException(
               "{0} must be provided a valid {1}".F( GetType().Name, nameof( IExceptionSerializer ) ),
               nameof( serializer ) );
         }
         m_serializer = serializer;
      }

      public Object Augment( ILogEntry entry )
      {
         var ex = entry.GetData<Exception>();
         return ex != null ? m_serializer.Serialize( ex, null ) : null;
      }
   }
}