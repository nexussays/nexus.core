using System;

namespace nexus.core
{
   /// <summary>
   /// <see cref="IObserver{T}" /> which takes <see cref="Action{T}" /> in ctor for each
   /// </summary>
   public sealed class ActionObserver<T> : IObserver<T>
   {
      private readonly Action m_onComplete;
      private readonly Action<Exception> m_onError;
      private readonly Action<T> m_onNext;

      /// <summary>
      /// </summary>
      public ActionObserver( Action<T> onNext, Action onComplete = null, Action<Exception> onError = null )
      {
         m_onNext = onNext;
         m_onComplete = onComplete;
         m_onError = onError;
      }

      /// <inheritdoc />
      public void OnCompleted()
      {
         m_onComplete?.Invoke();
      }

      /// <inheritdoc />
      public void OnError( Exception error )
      {
         m_onError?.Invoke( error );
      }

      /// <inheritdoc />
      public void OnNext( T value )
      {
         m_onNext?.Invoke( value );
      }

      /// <summary>
      /// Create a <see cref="ActionObserver{T}" /> from an <see cref="Action" /> <paramref name="onNext" />
      /// </summary>
      public static implicit operator ActionObserver<T>( Action<T> onNext )
      {
         return new ActionObserver<T>( onNext );
      }
   }
}