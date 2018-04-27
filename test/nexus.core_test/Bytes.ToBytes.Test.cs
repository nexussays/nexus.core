// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.core;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace nexus.core_test
{
   [TestFixture]
   internal partial class BytesTest
   {
      [TestCase( ByteOrder.LittleEndian, 0, ExpectedResult = new Byte[] {0, 0} )]
      [TestCase( ByteOrder.LittleEndian, 256, ExpectedResult = new Byte[] {0, 1} )]
      [TestCase( ByteOrder.LittleEndian, 1, ExpectedResult = new Byte[] {1, 0} )]
      [TestCase( ByteOrder.LittleEndian, Int16.MinValue, ExpectedResult = new Byte[] {0, 128} )]
      [TestCase( ByteOrder.LittleEndian, 128, ExpectedResult = new Byte[] {128, 0} )]
      [TestCase( ByteOrder.LittleEndian, -256, ExpectedResult = new Byte[] {0, 255} )]
      [TestCase( ByteOrder.LittleEndian, 255, ExpectedResult = new Byte[] {255, 0} )]
      [TestCase( ByteOrder.LittleEndian, Int16.MaxValue, ExpectedResult = new Byte[] {255, 127} )]
      [TestCase( ByteOrder.LittleEndian, Int16.MinValue + 255, ExpectedResult = new Byte[] {255, 128} )]
      [TestCase( ByteOrder.LittleEndian, -1, ExpectedResult = new Byte[] {255, 255} )]
      [TestCase( ByteOrder.BigEndian, 0, ExpectedResult = new Byte[] {0, 0} )]
      [TestCase( ByteOrder.BigEndian, 1, ExpectedResult = new Byte[] {0, 1} )]
      [TestCase( ByteOrder.BigEndian, 256, ExpectedResult = new Byte[] {1, 0} )]
      [TestCase( ByteOrder.BigEndian, 128, ExpectedResult = new Byte[] {0, 128} )]
      [TestCase( ByteOrder.BigEndian, Int16.MinValue, ExpectedResult = new Byte[] {128, 0} )]
      [TestCase( ByteOrder.BigEndian, 255, ExpectedResult = new Byte[] {0, 255} )]
      [TestCase( ByteOrder.BigEndian, -256, ExpectedResult = new Byte[] {255, 0} )]
      [TestCase( ByteOrder.BigEndian, Int16.MaxValue, ExpectedResult = new Byte[] {127, 255} )]
      [TestCase( ByteOrder.BigEndian, Int16.MinValue + 255, ExpectedResult = new Byte[] {128, 255} )]
      [TestCase( ByteOrder.BigEndian, -1, ExpectedResult = new Byte[] {255, 255} )]
      [TestCase( ByteOrder.BigEndian, 0, ExpectedResult = new Byte[] {0, 0} )]
      public Byte[] to_bytes_correctly_converts_int16( ByteOrder order, Int16 value )
      {
         return Bytes.ToBytes( value, order );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0, 0, 0}, 0 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 1, 0, 0}, 256 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {1, 0, 0, 0}, 1 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0, 0, 128}, Int32.MinValue )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {128, 0, 0, 0}, 128 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 0, 0, 0}, 255 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255, 255, 127}, Int32.MaxValue )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 0, 0, 128}, Int32.MinValue + 255 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255, 255, 255}, -1 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 1}, 1 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 128}, 128 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0, 0, 0}, Int32.MinValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0, 0, 1}, Int32.MinValue + 1 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {192, 0, 0, 0}, Int32.MinValue / 2 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 255}, 255 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {127, 255, 255, 255}, Int32.MaxValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0, 0, 255}, Int32.MinValue + 255 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255, 255, 255}, -1 )]
      public void to_bytes_correctly_converts_int32( ByteOrder order, Byte[] result, Int32 value )
      {
         Assert.That( Bytes.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0}, 0 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 1, 0, 0, 0, 0, 0, 0}, 256 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {1, 0, 0, 0, 0, 0, 0, 0}, 1 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 128}, Int64.MinValue )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {128, 0, 0, 0, 0, 0, 0, 0}, 128 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 0, 0, 0, 0, 0, 0, 0}, 255 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255, 255, 255, 255, 255, 255, 127}, Int64.MaxValue )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 0, 0, 0, 0, 0, 0, 128}, Int64.MinValue + 255 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255}, -1 )]
      [TestCase(
         ByteOrder.LittleEndian,
         new Byte[] {0x00, 0x00, 0x9C, 0x58, 0x4C, 0x49, 0x1F, 0xF2},
         -1000000000000000000 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 1}, 1 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 0, 0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 128}, 128 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0, 0, 0, 0, 0, 0, 0}, Int64.MinValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0, 0, 0, 0, 0, 0, 1}, Int64.MinValue + 1 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {192, 0, 0, 0, 0, 0, 0, 0}, Int64.MinValue / 2 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 255}, 255 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {127, 255, 255, 255, 255, 255, 255, 255}, Int64.MaxValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0, 0, 0, 0, 0, 0, 255}, Int64.MinValue + 255 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255}, -1 )]
      public void to_bytes_correctly_converts_int64( ByteOrder order, Byte[] result, Int64 value )
      {
         Assert.That( Bytes.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0}, UInt16.MinValue )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 1}, 256 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {1, 0}, 1 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {128, 0}, 128 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 0}, 255 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255}, UInt16.MaxValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0}, UInt16.MinValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 1}, 1 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 128}, 128 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 255}, 255 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255}, UInt16.MaxValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0}, 0 )]
      public void to_bytes_correctly_converts_uint16( ByteOrder order, Byte[] result, Int32 value )
      {
         Assert.That( Bytes.ToBytes( (UInt16)value, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0, 0, 0}, UInt32.MinValue )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 1, 0, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {1, 0, 0, 0}, (UInt32)1 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {128, 0, 0, 0}, (UInt32)128 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 0, 0, 0}, (UInt32)255 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255, 255, 255}, UInt32.MaxValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 0}, UInt32.MinValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 1}, (UInt32)1 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 1, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 128}, (UInt32)128 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 255}, (UInt32)255 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255, 255, 255}, UInt32.MaxValue )]
      public void to_bytes_correctly_converts_uint32( ByteOrder order, Byte[] result, UInt32 value )
      {
         Assert.That( Bytes.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, UInt64.MinValue, ExpectedResult = new Byte[] {0, 0, 0, 0, 0, 0, 0, 0} )]
      [TestCase( ByteOrder.LittleEndian, (UInt64)256, ExpectedResult = new Byte[] {0, 1, 0, 0, 0, 0, 0, 0} )]
      [TestCase( ByteOrder.LittleEndian, (UInt64)1, ExpectedResult = new Byte[] {1, 0, 0, 0, 0, 0, 0, 0} )]
      [TestCase( ByteOrder.LittleEndian, (UInt64)128, ExpectedResult = new Byte[] {128, 0, 0, 0, 0, 0, 0, 0} )]
      [TestCase( ByteOrder.LittleEndian, (UInt64)255, ExpectedResult = new Byte[] {255, 0, 0, 0, 0, 0, 0, 0} )]
      [TestCase(
         ByteOrder.LittleEndian,
         UInt64.MaxValue,
         ExpectedResult = new Byte[] {255, 255, 255, 255, 255, 255, 255, 255} )]
      [TestCase(
         ByteOrder.LittleEndian,
         17446744073709551616,
         ExpectedResult = new Byte[] {0x00, 0x00, 0x9C, 0x58, 0x4C, 0x49, 0x1F, 0xF2} )]
      [TestCase( ByteOrder.BigEndian, UInt64.MinValue, ExpectedResult = new Byte[] {0, 0, 0, 0, 0, 0, 0, 0} )]
      [TestCase( ByteOrder.BigEndian, (UInt64)1, ExpectedResult = new Byte[] {0, 0, 0, 0, 0, 0, 0, 1} )]
      [TestCase( ByteOrder.BigEndian, (UInt64)256, ExpectedResult = new Byte[] {0, 0, 0, 0, 0, 0, 1, 0} )]
      [TestCase( ByteOrder.BigEndian, (UInt64)128, ExpectedResult = new Byte[] {0, 0, 0, 0, 0, 0, 0, 128} )]
      [TestCase( ByteOrder.BigEndian, (UInt64)255, ExpectedResult = new Byte[] {0, 0, 0, 0, 0, 0, 0, 255} )]
      [TestCase(
         ByteOrder.BigEndian,
         UInt64.MaxValue,
         ExpectedResult = new Byte[] {255, 255, 255, 255, 255, 255, 255, 255} )]
      [TestCase(
         ByteOrder.BigEndian,
         17446744073709551616,
         ExpectedResult = new Byte[] {0xF2, 0x1F, 0x49, 0x4C, 0x58, 0x9C, 0x00, 0x00} )]
      public Byte[] to_bytes_correctly_converts_uint64( ByteOrder order, UInt64 value )
      {
         return Bytes.ToBytes( value, order );
      }
      /*
      // TODO: Fix
      [Ignore( "Test incomplete" )]
      public void to_bytes_correctly_converts_float32( ByteOrder order, Byte[] result, Single value )
      {
         Assert.That( Bytes.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      [Ignore("Test incomplete")]
      [TestCase(ByteOrder.LittleEndian, Double.MinValue, ExpectedResult = new Byte[] { 0, 0, 0, 0, 0, 0, 0, 0 })]
      [TestCase(ByteOrder.LittleEndian, (Double)256, ExpectedResult = new Byte[] { 0, 1, 0, 0, 0, 0, 0, 0 })]
      [TestCase(ByteOrder.LittleEndian, (Double)1, ExpectedResult = new Byte[] { 1, 0, 0, 0, 0, 0, 0, 0 })]
      [TestCase(ByteOrder.LittleEndian, (Double)128, ExpectedResult = new Byte[] { 128, 0, 0, 0, 0, 0, 0, 0 })]
      [TestCase(ByteOrder.LittleEndian, (Double)255, ExpectedResult = new Byte[] { 255, 0, 0, 0, 0, 0, 0, 0 })]
      [TestCase(
         ByteOrder.LittleEndian,
         Double.MaxValue,
         ExpectedResult = new Byte[] { 255, 255, 255, 255, 255, 255, 255, 255 })]
      [TestCase(
         ByteOrder.LittleEndian,
         17446744073709551616,
         ExpectedResult = new Byte[] { 0x00, 0x00, 0x9C, 0x58, 0x4C, 0x49, 0x1F, 0xF2 })]
      [TestCase(ByteOrder.BigEndian, Double.MinValue, ExpectedResult = new Byte[] { 0, 0, 0, 0, 0, 0, 0, 0 })]
      [TestCase(ByteOrder.BigEndian, (Double)1, ExpectedResult = new Byte[] { 0, 0, 0, 0, 0, 0, 0, 1 })]
      [TestCase(ByteOrder.BigEndian, (Double)256, ExpectedResult = new Byte[] { 0, 0, 0, 0, 0, 0, 1, 0 })]
      [TestCase(ByteOrder.BigEndian, (Double)128, ExpectedResult = new Byte[] { 0, 0, 0, 0, 0, 0, 0, 128 })]
      [TestCase(ByteOrder.BigEndian, (Double)255, ExpectedResult = new Byte[] { 0, 0, 0, 0, 0, 0, 0, 255 })]
      [TestCase(
         ByteOrder.BigEndian,
         Double.MaxValue,
         ExpectedResult = new Byte[] { 255, 255, 255, 255, 255, 255, 255, 255 })]
      [TestCase(
         ByteOrder.BigEndian,
         17446744073709551616,
         ExpectedResult = new Byte[] { 0xF2, 0x1F, 0x49, 0x4C, 0x58, 0x9C, 0x00, 0x00 })]
      public Byte[] to_bytes_correctly_converts_float64( ByteOrder order,  Double value )
      {
         return Bytes.ToBytes( value, order );
      }
      */
      // ReSharper disable NUnit.MethodWithParametersAndTestAttribute

      [Test]
      public void to_bytes_to_int16_equals_original_int16( [Random( Int16.MinValue, Int16.MaxValue, 10 )] Int16 value,
                                                           [Values( ByteOrder.BigEndian, ByteOrder.LittleEndian )]
                                                           ByteOrder endianness )
      {
         Assert.That( Bytes.ToInt16( Bytes.ToBytes( value, endianness ), 0, endianness ), Is.EqualTo( value ) );
      }

      [Test]
      public void to_bytes_to_int32_equals_original_int32( [Random( Int32.MinValue, Int32.MaxValue, 10 )] Int32 value,
                                                           [Values( ByteOrder.BigEndian, ByteOrder.LittleEndian )]
                                                           ByteOrder endianness )
      {
         Assert.That( Bytes.ToInt32( Bytes.ToBytes( value, endianness ), 0, endianness ), Is.EqualTo( value ) );
      }

      [Test]
      public void to_bytes_to_int64_equals_original_int64( [Random( Int64.MinValue, Int64.MaxValue, 10 )] Int64 value,
                                                           [Values( ByteOrder.BigEndian, ByteOrder.LittleEndian )]
                                                           ByteOrder endianness )
      {
         Assert.That( Bytes.ToInt64( Bytes.ToBytes( value, endianness ), 0, endianness ), Is.EqualTo( value ) );
      }

      [Test]
      public void to_bytes_to_uint16_equals_original_uint16(
         [Random( UInt16.MinValue, UInt16.MaxValue, 10 )] UInt16 value,
         [Values( ByteOrder.BigEndian, ByteOrder.LittleEndian )] ByteOrder endianness )
      {
         Assert.That( Bytes.ToUInt16( Bytes.ToBytes( value, endianness ), 0, endianness ), Is.EqualTo( value ) );
      }

      [Test]
      public void to_bytes_to_uint32_equals_original_uint32(
         [Random( UInt32.MinValue, UInt32.MaxValue, 10 )] UInt32 value,
         [Values( ByteOrder.BigEndian, ByteOrder.LittleEndian )] ByteOrder endianness )
      {
         Assert.That( Bytes.ToUInt32( Bytes.ToBytes( value, endianness ), 0, endianness ), Is.EqualTo( value ) );
      }

      [Test]
      public void to_bytes_to_uint64_equals_original_uint64(
         [Random( UInt64.MinValue, UInt64.MaxValue, 10 )] UInt64 value,
         [Values( ByteOrder.BigEndian, ByteOrder.LittleEndian )] ByteOrder endianness )
      {
         Assert.That( Bytes.ToUInt64( Bytes.ToBytes( value, endianness ), 0, endianness ), Is.EqualTo( value ) );
      }
      /*
      [Test]
      public void to_bytes_to_float32_equals_original_float32(
         [Random( Single.MinValue, Single.MaxValue, 10 )] Single value,
         [Values( ByteOrder.BigEndian, ByteOrder.LittleEndian )] ByteOrder endianness )
      {
         Assert.That( Bytes.ToFloat32( Bytes.ToBytes( value, endianness ), 0, endianness ), Is.EqualTo( value ) );
      }

      [Test]
      public void to_bytes_to_float64_equals_original_float64(
         [Random( Double.MinValue, Double.MaxValue, 10 )] Double value,
         [Values( ByteOrder.BigEndian, ByteOrder.LittleEndian )] ByteOrder endianness )
      {
         Assert.That( Bytes.ToFloat64( Bytes.ToBytes( value, endianness ), 0, endianness ), Is.EqualTo( value ) );
      }
      */
      // ReSharper restore NUnit.MethodWithParametersAndTestAttribute
   }
}
