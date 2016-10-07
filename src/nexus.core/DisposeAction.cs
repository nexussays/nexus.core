using System;
using System.Threading;

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