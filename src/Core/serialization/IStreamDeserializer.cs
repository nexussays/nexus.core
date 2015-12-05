using System.IO;

namespace nexus.core.serialization
{
   public interface IStreamDeserializer
   {
      // TODO: Implement IInputStream
      //T Deserialize<T>( IInputStream source );

      T Deserialize<T>( Stream source );
   }

   public interface IStreamDeserializer<out T>
   {
      // TODO: Implement IInputStream
      //T Deserialize( IInputStream source );

      T Deserialize( Stream source );
   }
}