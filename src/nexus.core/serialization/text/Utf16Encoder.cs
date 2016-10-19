using System;
using System.Text;

namespace nexus.core.serialization.text
{
   public class Utf16Encoder : TextEncoder
   {
      public static readonly ITextEncoder WithBOM = new Utf16Encoder( true, false );
      public static readonly ITextEncoder WithoutBOM = new Utf16Encoder( false, false );

      public Utf16Encoder( Boolean useBom, Boolean bigEndian )
         : base( new UnicodeEncoding( bigEndian, useBom ) )
      {
      }
   }
}