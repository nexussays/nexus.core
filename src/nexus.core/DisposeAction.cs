// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core
{
   public class DisposeAction : IDisposable
   {
      public static readonly DisposeAction None = new DisposeAction( () => { } );
      private Action m_action;

      public DisposeAction( Action action )
      {
         m_action = action;
      }

      public Boolean IsDisposed { get; private set; }

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