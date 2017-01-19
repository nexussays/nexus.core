// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.IO;
using System.Threading.Tasks;

namespace nexus.core.serialization
{
   /// <summary>
   /// <see cref="IOutputSerializer{TTo}" /> with additional async serialization method
   /// </summary>
   /// <remarks>The extension methods on <see cref="SerializationUtils" /> provide a better API</remarks>
   public interface IStreamSerializer : IOutputSerializer<Stream>
   {
      Task SerializeAsync<TFrom>( TFrom source, Stream to );
   }
}