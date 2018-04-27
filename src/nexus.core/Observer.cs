// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.Contracts;
using nexus.core.resharper;

namespace nexus.core
{
   /// <summary>
   /// Utility methods to create <see cref="IObserver{T}" />
   /// </summary>
   public static class Observer
   {
      /// <summary>
      /// Create a new observer using the provided methods to implement <see cref="IObserver{T}" />
      /// </summary>
      public static IObserver<T> Create<T>( Action<T> onNext, Action onComplete = null,
                                            Action<Exception> onError = null )
      {
         return new ActionObserver<T>( onNext, onComplete, onError );
      }

      /// <summary>
      /// Subscribe to this <paramref name="observable" /> using the provided methods to create a new <see cref="IObserver{T}" />
      /// </summary>
      public static IDisposable Subscribe<T>( [NotNull] this IObservable<T> observable, Action<T> onNext,
                                              Action onComplete = null, Action<Exception> onError = null )
      {
         Contract.Requires( observable != null );
         return observable.Subscribe( Create( onNext, onComplete, onError ) );
      }

      /// <summary>
      /// Subscribe to this <paramref name="observable" /> creating a new <see cref="IObserver{T}" /> with only
      /// <see cref="IObserver{T}.OnCompleted" /> implemented
      /// </summary>
      public static IDisposable SubscribeOnComplete<T>( [NotNull] this IObservable<T> observable, Action onComplete )
      {
         Contract.Requires( observable != null );
         return observable.Subscribe( Create<T>( null, onComplete, null ) );
      }

      /// <summary>
      /// Subscribe to this <paramref name="observable" /> creating a new <see cref="IObserver{T}" /> with only
      /// <see cref="IObserver{T}.OnError" /> implemented
      /// </summary>
      public static IDisposable SubscribeOnError
         <T>( [NotNull] this IObservable<T> observable, Action<Exception> onError )
      {
         Contract.Requires( observable != null );
         return observable.Subscribe( Create<T>( null, null, onError ) );
      }

      /// <summary>
      /// Subscribe to this <paramref name="observable" /> creating a new <see cref="IObserver{T}" /> with only
      /// <see cref="IObserver{T}.OnNext" /> implemented
      /// </summary>
      public static IDisposable SubscribeOnNext<T>( [NotNull] this IObservable<T> observable, Action<T> onNext )
      {
         Contract.Requires( observable != null );
         return observable.Subscribe( Create( onNext, null, null ) );
      }
   }
}
