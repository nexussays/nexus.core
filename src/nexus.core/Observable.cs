// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.core
{
   /// <summary>
   /// Simple implementation of <see cref="IObservable{T}" />
   /// </summary>
   public class Observable<T>
      : IObservable<T>,
        IUpdatable<T>,
        IDisposable
   {
      private readonly List<IObserver<T>> m_observers;

      /// <summary>
      /// </summary>
      public Observable()
      {
         m_observers = new List<IObserver<T>>();
      }

      /// <summary>
      /// <c>true</c> if this observable has been disposed and can no longer be used or a <see cref="ObjectDisposedException" />
      /// will be thrown
      /// </summary>
      public Boolean IsDisposed { get; private set; }

      /// <inheritdoc />
      public virtual void Dispose()
      {
         if(IsDisposed)
         {
            return;
         }

         IsDisposed = true;
         var observers = new List<IObserver<T>>( m_observers );
         foreach(var observer in observers)
         {
            observer.OnCompleted();
         }
         m_observers.Clear();
      }

      /// <inheritdoc />
      public virtual void Error( Exception ex )
      {
         ThrowIfDisposed();

         var observers = new List<IObserver<T>>( m_observers );
         foreach(var observer in observers)
         {
            observer.OnError( ex );
         }
      }

      /// <inheritdoc />
      public virtual IDisposable Subscribe( IObserver<T> observer )
      {
         ThrowIfDisposed();

         m_observers.Add( observer );
         return new DisposeAction( () => m_observers.Remove( observer ) );
      }

      /// <inheritdoc />
      public virtual void Update( T value )
      {
         ThrowIfDisposed();

         var observers = new List<IObserver<T>>( m_observers );
         foreach(var observer in observers)
         {
            observer.OnNext( value );
         }
      }

      /// <summary>
      /// Throw <see cref="ObjectDisposedException" /> if <see cref="IsDisposed" /> is <c>true</c>
      /// </summary>
      protected void ThrowIfDisposed()
      {
         if(IsDisposed)
         {
            throw new ObjectDisposedException( "Cannot perform operations on disposed {0}".F( GetType().Name ) );
         }
      }
   }
}
