using System;
using System.IO;

namespace nexus.core.serialization
{
   public interface ITextWriterSerializer
      : ISerializer<Object, String>,
        IDeserializer<String, Object>,
        IGenericDeserializer<TextReader>,
        IGenericDeserializer<String>
   {
      String Extension { get; }

      void Serialize( TextWriter serializeTo, Object output );
   }
}