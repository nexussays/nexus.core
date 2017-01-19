namespace nexus.core.serialization
{
   /// <summary>
   /// Serialize an object to another object of the given type (e.g. <see cref="IResultSerializer{Byte[]}" />)
   /// </summary>
   public interface IResultSerializer<out TTo>
   {
      TTo Serialize<TFrom>( TFrom source );
   }
}