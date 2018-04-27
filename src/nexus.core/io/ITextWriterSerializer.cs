// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.IO;
using System.Threading.Tasks;

namespace nexus.core.io
{
   /// <summary>
   /// Serialize any generic type to a <see cref="TextWriter" />
   /// </summary>
   /// <remarks>The extension methods on <see cref="SerializationUtils" /> provide a better API</remarks>
   public interface ITextWriterSerializer
   {
      /// <summary>
      /// Serialize any generic type to the given <see cref="TextWriter" />
      /// </summary>
      void Serialize<TFrom>( TFrom source, TextWriter output );

      /// <summary>
      /// Serialize any generic type to the given <see cref="TextWriter" />
      /// </summary>
      Task SerializeAsync<TFrom>( TFrom source, TextWriter to );
   }
}
