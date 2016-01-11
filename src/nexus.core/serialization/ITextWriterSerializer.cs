// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
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