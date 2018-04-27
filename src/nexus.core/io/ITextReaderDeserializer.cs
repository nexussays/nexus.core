// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.IO;
using System.Threading.Tasks;

namespace nexus.core.io
{
   /// <summary>
   /// <see cref="IDeserializer{TSource}" /> with additional async methods. Deserialize from a <see cref="TextReader" /> to
   /// a call-time-specified type.
   /// </summary>
   /// <remarks>The extension methods on <see cref="SerializationUtils" /> provide a better API</remarks>
   public interface ITextReaderDeserializer
   {
      /// <summary>
      /// Deserialize from some source object to a call-time-specified type
      /// </summary>
      TResult Deserialize<TResult>( TextReader source );

      /// <summary>
      /// Non-generic version of <see cref="Deserialize{TResult}" />
      /// </summary>
      Object Deserialize( TextReader source, Type desiredReturnType );

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
