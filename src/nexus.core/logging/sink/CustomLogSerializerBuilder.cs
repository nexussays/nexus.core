// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace nexus.core.logging.sink
{
   /// <summary>
   /// A builder to dynamically create a new <see cref="IObjectConverter{TFrom,TTo}" />. Add steps and call
   /// <see cref="Build" /> when complete.
   /// </summary>
   public sealed class CustomLogSerializerBuilder : IEnumerable<Func<ILogEntry, String>>
   {
      private readonly IList<Func<ILogEntry, String>> m_formatSteps;

      /// <summary>
      /// </summary>
      public CustomLogSerializerBuilder()
      {
         m_formatSteps = new List<Func<ILogEntry, String>>();
      }

      /// <summary>
      /// Syntax sugar for <code>new CustomLogSerializerBuilder()</code>
      /// </summary>
      public static CustomLogSerializerBuilder Create => new CustomLogSerializerBuilder();

      /// <summary>
      /// The format steps for thie builder
      /// </summary>
      public IEnumerable<Func<ILogEntry, String>> FormatSteps => m_formatSteps;

      /// <summary>
      /// Construct your log serializer from the steps you've built
      /// </summary>
      public IObjectConverter<ILogEntry, String> Build()
      {
         var steps = new List<Func<ILogEntry, String>>( m_formatSteps );
         return ObjectConverter.Create(
            ( ILogEntry entry ) =>
            {
               // TODO: Can have this write out to expression tree or something eventually
               var builder = new StringBuilder();
               foreach(var formatter in steps)
               {
                  builder.Append( formatter( entry ) );
               }
               return builder.ToString();
            } );
      }

      /// <inheritdoc cref="IEnumerable{T}" />
      public IEnumerator<Func<ILogEntry, String>> GetEnumerator()
      {
         return m_formatSteps.GetEnumerator();
      }

      /// <summary>
      /// Append a format step to the end of the current list
      /// </summary>
      public CustomLogSerializerBuilder Then( Func<ILogEntry, String> formatStep )
      {
         m_formatSteps.Add( formatStep );
         return this;
      }

      /// <summary>
      /// If an object of the provided type is attached to a given entry then <paramref name="formatStep" /> will be executed,
      /// otherwise <paramref name="elseFormatStep" /> will be executed (or no-op if <paramref name="elseFormatStep" /> is
      /// <c>null</c>)
      /// </summary>
      public CustomLogSerializerBuilder ThenForData<T>( Func<ILogEntry, T, String> formatStep,
                                                        Func<ILogEntry, String> elseFormatStep = null )
         where T : class
      {
         return Then(
            entry =>
            {
               var value = entry.GetData<T>();
               return value != null
                  ? formatStep( entry, value )
                  : (elseFormatStep != null ? elseFormatStep( entry ) : String.Empty);
            } );
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return ((IEnumerable)m_formatSteps).GetEnumerator();
      }
   }
}
