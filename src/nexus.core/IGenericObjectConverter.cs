namespace nexus.core
{
   /// <summary>
   /// Serialize from any source type to a fixed result type <typeparamref name="TResult" />. If you want the source type to
   /// be fixed as well, see <see cref="IObjectConverter{TFrom,TTo}" />
   /// </summary>
   public interface IGenericObjectConverter<out TResult>
   {
      /// <summary>
      /// Serialize from one type to another
      /// </summary>
      TResult Convert<TSource>( TSource source );
   }
}