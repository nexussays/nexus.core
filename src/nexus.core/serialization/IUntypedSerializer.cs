using System;

namespace nexus.core.serialization
{
   /// <summary>
   /// Serializer without type information for use in collections or other places a generic interface can cause problems
   /// </summary>
   public interface IUntypedSerializer
   {
      Type From { get; }

      Type To { get; }

      Object Serialize( Object source );
   }

   public static class GenericSerializerExtensions
   {
      /// <summary>
      /// Return an <see cref="IUntypedSerializer" /> which retains the type information but is not a generic interface and can
      /// be used in collections or other places a generic interface causes problems
      /// </summary>
      public static IUntypedSerializer AsUntyped<TFrom, TTo>( this ISerializer<TFrom, TTo> serializer )
      {
         return new UntypedSerializer<TFrom, TTo>( serializer );
      }

      private sealed class UntypedSerializer<TFrom, TTo>
         : IUntypedSerializer,
           ISerializer<TFrom, TTo>
      {
         private readonly ISerializer<TFrom, TTo> m_serializer;

         public UntypedSerializer( ISerializer<TFrom, TTo> serializer )
         {
            m_serializer = serializer;
         }

         public Type From { get; } = typeof(TFrom);

         public Type To { get; } = typeof(TTo);

         public Object Serialize( Object source )
         {
            if(source == null)
            {
               return null;
            }
            if(source is TFrom)
            {
               return Serialize( (TFrom)source );
            }
            throw new ArgumentException(
               "Serializer {0} cannot serialize objects of type {1}".F(
                  m_serializer.GetType().FullName,
                  source.GetType().FullName ) );
         }

         public TTo Serialize( TFrom source )
         {
            return m_serializer.Serialize( source );
         }
      }
   }
}