using System;
using nexus.core.serialization;

namespace nexus.core
{
   /// <summary>
   /// Take a native exception and a boolean indicating whether or not it has been handled and serialize it to an
   /// <see cref="IException" />
   /// </summary>
   public interface IExceptionSerializer : ISerializer<Tuple<Exception, Boolean?>, IException>
   {
      IException Serialize( Exception exception, Boolean? handled );
   }
}