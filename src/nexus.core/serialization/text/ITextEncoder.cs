using System;
using System.Text;
using nexus.core.serialization.binary;

namespace nexus.core.serialization.text
{
   /// <summary>
   /// Use <see cref="ITextEncoder" /> when your source data is a string and you want to convert that information
   /// losslessly to a byte array.
   /// This is the string/char equivalent to to <see cref="ITextEncoder" />.
   /// <see cref="IBinaryEncoder" /> can
   /// 1. Encode an arbitrary string to formatted bytes
   /// 2. Decode formatted bytes to the original string
   /// </summary>
   public interface ITextEncoder
      : ISerializer<String, Byte[]>,
        IDeserializer<Byte[], String>
   {
      Encoding Encoding { get; }

      Char[] DeserializeChars( Byte[] input );
   }
}