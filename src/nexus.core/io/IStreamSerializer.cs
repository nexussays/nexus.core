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
   /// Serialize any generic type to the given <see cref="Stream" />
   /// </summary>
   /// <remarks>The extension methods on <see cref="SerializationUtils" /> provide a better API</remarks>
   public interface IStreamSerializer
   {
      /// <summary>
      /// Serialize any generic type to the given <see cref="Stream" />
      /// </summary>
      void Serialize<TFrom>( TFrom source, Stream output );

      /// <summary>
      /// Serialize any generic type to the given <see cref="Stream" />
      /// </summary>
      Task SerializeAsync<TFrom>( TFrom source, Stream output );
   }
}
