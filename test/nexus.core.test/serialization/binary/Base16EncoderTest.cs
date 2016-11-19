// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.core.serialization.binary;
using NUnit.Framework;

namespace nexus.core.test.serialization.binary
{
   [TestFixture]
   internal class Base16EncoderTest
   {
      private readonly Base16Encoder m_encoder;

      public Base16EncoderTest()
      {
         m_encoder = Base16Encoder.Instance;
      }

      [TestCase( new Byte[] {120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72} )]
      [TestCase( new Byte[] {62, 78, 65, 62, 84, 6, 247, 7, 4, 34, 56, 89, 78, 53, 6, 8, 5, 88, 12, 90, 2, 1, 75, 47} )]
      [TestCase( new Byte[] {26, 8, 34, 5, 8, 0, 64, 0, 7, 53, 53, 2, 46, 74, 30, 32, 5, 0, 7, 54, 78, 3, 2, 58} )]
      public void serializing_and_deserializing_bytearray_results_in_original_data( Byte[] data )
      {
         Assert.That( m_encoder.Deserialize( m_encoder.Serialize( data ) ), Is.EqualTo( data ) );
      }
   }
}