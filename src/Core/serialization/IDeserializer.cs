namespace nexus.core.serialization
{
   public interface IDeserializer<in TFrom, out TTo>
   {
      TTo Deserialize( TFrom data );
   }

   public interface IGenericDeserializer<in TFrom>
   {
      T Deserialize<T>( TFrom source );
   }
}