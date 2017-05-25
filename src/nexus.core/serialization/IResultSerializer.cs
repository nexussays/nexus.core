namespace nexus.core.serialization
{
   /// <summary>
   /// Serialize an object to another object of the given type. Similar to <see cref="IObjectConverter{TFrom,TTo}" />
   /// </summary>
   public interface IResultSerializer<out TTo>
   {
      /// <summary>
      /// Serialize from one type to another
      /// </summary>
      TTo Serialize<TFrom>( TFrom source );
   }
}