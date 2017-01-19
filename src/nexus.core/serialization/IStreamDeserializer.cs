// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.IO;
using System.Threading.Tasks;

namespace nexus.core.serialization
{
   /// <summary>
   /// <see cref="IDeserializer{Stream}" /> with additional async methods. Deserialize from a <see cref="Stream" /> to a
   /// call-time-specified type
   /// </summary>
   /// <remarks>The extension methods on <see cref="SerializationUtils"/> provide a better API</remarks>
   public interface IStreamDeserializer : IDeserializer<Stream>
   {
      Task<T> DeserializeAsync<T>( Stream source );

      Task<Object> DeserializeAsync( Stream source, Type desiredReturnType );
   }
}