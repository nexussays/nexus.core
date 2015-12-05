namespace nexus.core.serialization
{
   public interface ISerializer<in TFrom, out TTo>
   {
      TTo Serialize( TFrom data );
   }
}