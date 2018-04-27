// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core
{
   /// <summary>
   /// Create a <see cref="IDisposable" /> that will execute the provided action once when dispose is called. Subsequent calls
   /// to <c>Dispose()</c> will do nothing
   /// </summary>
   public sealed class DisposeAction : IDisposable
   {
      /// <summary>
      /// An empty <see cref="IDisposable" />
      /// </summary>
      public static readonly DisposeAction None = new DisposeAction( () => { } );

      private Action m_action;

      /// <summary>
      /// Create a <see cref="IDisposable" /> that will execute the provided action once when dispose is called. Subsequent calls
      /// to <c>Dispose()</c> will do nothing
      /// </summary>
      public DisposeAction( Action action )
      {
         m_action = action;
      }

      /// <summary>
      /// True if <see cref="Dispose" /> has been called
      /// </summary>
      public Boolean IsDisposed { get; private set; }

      /// <inheritDoc />
      public void Dispose()
      {
         if(IsDisposed)
         {
            return;
         }
         m_action?.Invoke();
         m_action = null;
         IsDisposed = true;
      }
   }
}
