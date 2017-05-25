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
   /// <see cref="IDeserializer{TextReader}" /> with additional async methods. Deserialize from a <see cref="TextReader" /> to
   /// a call-time-specified type.
   /// </summary>
   /// <remarks>The extension methods on <see cref="SerializationUtils" /> provide a better API</remarks>
   public interface ITextReaderDeserializer : IDeserializer<TextReader>
   {
      /// <summary>
      /// Deserialize on object of type <typeparamref name="T" /> from <paramref name="source" />
      /// </summary>
      Task<T> DeserializeAsync<T>( TextReader source );

      /// <summary>
      /// Deserialize on object of type <paramref name="desiredReturnType" /> from <paramref name="source" />
      /// </summary>
      Task<Object> DeserializeAsync( TextReader source, Type desiredReturnType );
   }
}