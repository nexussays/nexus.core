// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.core.text;
using NUnit.Framework;

namespace nexus.core_test.text
{
   [TestFixture]
   internal class Base16EncoderTest
   {
      private Base16Encoder m_encoder;

      [OneTimeSetUp]
      public void Setup()
      {
         m_encoder = Base16Encoder.Lowercase;
      }

      [TestCase( new Byte[] {120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72} )]
      [TestCase( new Byte[] {62, 78, 65, 62, 84, 6, 247, 7, 4, 34, 56, 89, 78, 53, 6, 8, 5, 88, 12, 90, 2, 1, 75, 47} )]
      [TestCase( new Byte[] {26, 8, 34, 5, 8, 0, 64, 0, 7, 53, 53, 2, 46, 74, 30, 32, 5, 0, 7, 54, 78, 3, 2, 58} )]
      public void serializing_and_deserializing_bytearray_results_in_original_data( Byte[] data )
      {
         var foo = m_encoder.Encode( data );
         TestContext.Out.WriteLine( data[0].EncodeToBase16String() + " full=" + foo );
         Assert.That( m_encoder.Decode( m_encoder.Encode( data ) ), Is.EqualTo( data ) );
      }

      [TestCase( new[] {'0'} )]
      [TestCase( new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g'} )]
      public void instantiating_base16encoder_with_invalid_symboltable_throws_argumentexception( Char[] symbols )
      {
         // ReSharper disable once ObjectCreationAsStatement
         Assert.Throws<ArgumentException>( () => new Base16Encoder( symbols ) );
      }

      public void instantiating_base16encoder_with_null_symboltable_throws_argumentnullexception()
      {
         // ReSharper disable once ObjectCreationAsStatement
         // ReSharper disable once AssignNullToNotNullAttribute
         Assert.Throws<ArgumentNullException>( () => new Base16Encoder( null ) );
      }
   }
}
