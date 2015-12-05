using System.IO;
using System.Threading.Tasks;

namespace nexus.core.serialization
{
   public interface IStreamSerializer<in T>
   {
      Task Serialize( Stream serializeTo, T data );

      // TODO: Implement IOutputStream
      //Task Serialize( IOutputStream serializeTo, T data );
   }
}