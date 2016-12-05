// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace nexus.core.test
{
   [TestFixture]
   internal class BytesTest
   {
      [TestCase( new Byte[] {} )]
      [TestCase( new Byte[] {0} )]
      [TestCase( new Byte[] {1} )]
      [TestCase( new Byte[] {128} )]
      [TestCase( new Byte[] {255} )]
      public void to_int16_throws_when_parsing_invalid_bytearray( Byte[] value )
      {
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToInt16( value, 0, ByteOrder.BigEndian ) );
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToInt16( value, 0, ByteOrder.LittleEndian ) );
      }

      [TestCase( new Byte[] {} )]
      [TestCase( new Byte[] {0} )]
      [TestCase( new Byte[] {1} )]
      [TestCase( new Byte[] {128} )]
      [TestCase( new Byte[] {255} )]
      [TestCase( new Byte[] {255, 255} )]
      [TestCase( new Byte[] {255, 255, 255} )]
      public void to_int32_throws_when_parsing_invalid_bytearray( Byte[] value )
      {
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToInt32( value, 0, ByteOrder.BigEndian ) );
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToInt32( value, 0, ByteOrder.LittleEndian ) );
      }

      [TestCase( new Byte[] {} )]
      [TestCase( new Byte[] {0} )]
      [TestCase( new Byte[] {1} )]
      [TestCase( new Byte[] {128} )]
      [TestCase( new Byte[] {255} )]
      [TestCase( new Byte[] {255, 255} )]
      [TestCase( new Byte[] {255, 255, 255, 255} )]
      [TestCase( new Byte[] {255, 255, 255, 255, 255} )]
      [TestCase( new Byte[] {255, 255, 255, 255, 255, 255} )]
      [TestCase( new Byte[] {255, 255, 255, 255, 255, 255, 255} )]
      public void to_int64_throws_when_parsing_invalid_bytearray( Byte[] value )
      {
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToInt64( value, 0, ByteOrder.BigEndian ) );
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToInt64( value, 0, ByteOrder.LittleEndian ) );
      }

      [TestCase( new Byte[] {} )]
      [TestCase( new Byte[] {0} )]
      [TestCase( new Byte[] {1} )]
      [TestCase( new Byte[] {128} )]
      [TestCase( new Byte[] {255} )]
      public void to_uint16_throws_when_parsing_invalid_bytearray( Byte[] value )
      {
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToUInt16( value, 0, ByteOrder.BigEndian ) );
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToUInt16( value, 0, ByteOrder.LittleEndian ) );
      }

      [TestCase( new Byte[] {} )]
      [TestCase( new Byte[] {0} )]
      [TestCase( new Byte[] {1} )]
      [TestCase( new Byte[] {128} )]
      [TestCase( new Byte[] {255} )]
      [TestCase( new Byte[] {255, 255} )]
      [TestCase( new Byte[] {255, 255, 255} )]
      public void to_uint32_throws_when_parsing_invalid_bytearray( Byte[] value )
      {
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToUInt32( value, 0, ByteOrder.BigEndian ) );
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToUInt32( value, 0, ByteOrder.LittleEndian ) );
      }

      [TestCase( new Byte[] {} )]
      [TestCase( new Byte[] {0} )]
      [TestCase( new Byte[] {1} )]
      [TestCase( new Byte[] {128} )]
      [TestCase( new Byte[] {255} )]
      [TestCase( new Byte[] {255, 255} )]
      [TestCase( new Byte[] {255, 255, 255, 255} )]
      [TestCase( new Byte[] {255, 255, 255, 255, 255} )]
      [TestCase( new Byte[] {255, 255, 255, 255, 255, 255} )]
      [TestCase( new Byte[] {255, 255, 255, 255, 255, 255, 255} )]
      public void to_uint64_throws_when_parsing_invalid_bytearray( Byte[] value )
      {
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToUInt64( value, 0, ByteOrder.BigEndian ) );
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToUInt64( value, 0, ByteOrder.LittleEndian ) );
      }

      [TestCase( new Byte[] {} )]
      [TestCase( new Byte[] {0} )]
      [TestCase( new Byte[] {1} )]
      [TestCase( new Byte[] {128} )]
      [TestCase( new Byte[] {255} )]
      [TestCase( new Byte[] {255, 255} )]
      [TestCase( new Byte[] {255, 255, 255} )]
      public void to_float32_throws_when_parsing_invalid_bytearray( Byte[] value )
      {
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToFloat32( value, 0, ByteOrder.BigEndian ) );
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToFloat32( value, 0, ByteOrder.LittleEndian ) );
      }

      [TestCase( new Byte[] {} )]
      [TestCase( new Byte[] {0} )]
      [TestCase( new Byte[] {1} )]
      [TestCase( new Byte[] {128} )]
      [TestCase( new Byte[] {255} )]
      [TestCase( new Byte[] {255, 255} )]
      [TestCase( new Byte[] {255, 255, 255, 255} )]
      [TestCase( new Byte[] {255, 255, 255, 255, 255} )]
      [TestCase( new Byte[] {255, 255, 255, 255, 255, 255} )]
      [TestCase( new Byte[] {255, 255, 255, 255, 255, 255, 255} )]
      public void to_float64_throws_when_parsing_invalid_bytearray( Byte[] value )
      {
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToFloat64( value, 0, ByteOrder.BigEndian ) );
         Assert.Throws<IndexOutOfRangeException>( () => Bytes.ToFloat64( value, 0, ByteOrder.LittleEndian ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 1}, 256 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {1, 0}, 1 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 128}, Int16.MinValue )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {128, 0}, 128 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 255}, -256 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 0}, 255 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 127}, Int16.MaxValue )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 128}, Int16.MinValue + 255 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255}, -1 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 1}, 1 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 128}, 128 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0}, Int16.MinValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 255}, 255 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 0}, -256 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {127, 255}, Int16.MaxValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 255}, Int16.MinValue + 255 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255}, -1 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0}, 0 )]
      public void to_bytes_correctly_converts_int16( ByteOrder order, Byte[] result, Int16 value )
      {
         Assert.That( Bytes.ToBytes( value, order ), Is.EqualTo( result ) );
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

      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0}, UInt64.MinValue )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 1, 0, 0, 0, 0, 0, 0}, (UInt64)256 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {1, 0, 0, 0, 0, 0, 0, 0}, (UInt64)1 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {128, 0, 0, 0, 0, 0, 0, 0}, (UInt64)128 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 0, 0, 0, 0, 0, 0, 0}, (UInt64)255 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255}, UInt64.MaxValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0}, UInt64.MinValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 1}, (UInt64)1 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 0, 0, 0, 1, 0}, (UInt64)256 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 128}, (UInt64)128 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 255}, (UInt64)255 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255}, UInt64.MaxValue )]
      public void to_bytes_correctly_converts_uint64( ByteOrder order, Byte[] result, UInt64 value )
      {
         Assert.That( Bytes.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      public void to_bytes_correctly_converts_float32( ByteOrder order, Byte[] result, Single value )
      {
         // TODO: Fix
         // Assert.That( Bytes.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      public void to_bytes_correctly_converts_float64( ByteOrder order, Byte[] result, Double value )
      {
         // TODO: Fix
         // Assert.That( Bytes.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 1}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1, 0}, 1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 128}, Int16.MinValue )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128, 0}, 128 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 0}, 255 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 127}, Int16.MaxValue )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 128}, Int16.MinValue + 255 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255}, -1 )]
      [TestCase( ByteOrder.LittleEndian, 1, new Byte[] {0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 2, new Byte[] {0, 0, 0, 1}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 3, new Byte[] {0, 0, 0, 0, 1}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 4, new Byte[] {0, 0, 0, 0, 0, 1}, 256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 1}, 1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 128}, 128 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128, 0}, Int16.MinValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128, 1}, Int16.MinValue + 1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {192, 0}, Int16.MinValue / 2 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 255}, 255 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {127, 255}, Int16.MaxValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128, 255}, Int16.MinValue + 255 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255}, -1 )]
      [TestCase( ByteOrder.BigEndian, 1, new Byte[] {0, 1, 0, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 2, new Byte[] {0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 3, new Byte[] {0, 0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 4, new Byte[] {0, 0, 0, 0, 1, 0}, 256 )]
      public void to_int16_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value,
                                                            Int16 result )
      {
         Assert.That( Bytes.ToInt16( value, startIndex, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0, 0, 0}, 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 1, 0, 0}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1, 0, 0, 0}, 1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0, 0, 128}, Int32.MinValue )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128, 0, 0, 0}, 128 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 0, 0, 0}, 255 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255, 127}, Int32.MaxValue )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 0, 0, 128}, Int32.MinValue + 255 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255, 255}, -1 )]
      [TestCase( ByteOrder.LittleEndian, 1, new Byte[] {0, 0, 1, 0, 0}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 2, new Byte[] {0, 0, 0, 1, 0, 0}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 3, new Byte[] {0, 0, 0, 0, 1, 0, 0}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 4, new Byte[] {0, 0, 0, 0, 0, 1, 0, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 1}, 1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 128}, 128 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128, 0, 0, 0}, Int32.MinValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128, 0, 0, 1}, Int32.MinValue + 1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {192, 0, 0, 0}, Int32.MinValue / 2 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 255}, 255 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {127, 255, 255, 255}, Int32.MaxValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128, 0, 0, 255}, Int32.MinValue + 255 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255, 255, 255}, -1 )]
      [TestCase( ByteOrder.BigEndian, 1, new Byte[] {0, 0, 0, 1, 0, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 2, new Byte[] {0, 0, 0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 3, new Byte[] {0, 0, 0, 0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 4, new Byte[] {0, 0, 0, 0, 0, 0, 1, 0}, 256 )]
      public void to_int32_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value,
                                                            Int32 result )
      {
         Assert.That( Bytes.ToInt32( value, startIndex, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0}, 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 1, 0, 0, 0, 0, 0, 0}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1, 0, 0, 0, 0, 0, 0, 0}, 1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 128}, Int64.MinValue )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128, 0, 0, 0, 0, 0, 0, 0}, 128 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 0, 0, 0, 0, 0, 0, 0}, 255 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255, 255, 255, 255, 255, 127}, Int64.MaxValue )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 0, 0, 0, 0, 0, 0, 128}, Int64.MinValue + 255 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255}, -1 )]
      [TestCase( ByteOrder.LittleEndian, 1, new Byte[] {0, 0, 1, 0, 0, 0, 0, 0, 0}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 2, new Byte[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 0}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 3, new Byte[] {0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 4, new Byte[] {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 1}, 1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 128}, 128 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128, 0, 0, 0, 0, 0, 0, 0}, Int64.MinValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128, 0, 0, 0, 0, 0, 0, 1}, Int64.MinValue + 1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {192, 0, 0, 0, 0, 0, 0, 0}, Int64.MinValue / 2 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 255}, 255 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {127, 255, 255, 255, 255, 255, 255, 255}, Int64.MaxValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128, 0, 0, 0, 0, 0, 0, 255}, Int64.MinValue + 255 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255}, -1 )]
      [TestCase( ByteOrder.BigEndian, 1, new Byte[] {0, 0, 0, 0, 0, 0, 0, 1, 0, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 2, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 3, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 4, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0}, 256 )]
      public void to_int64_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value,
                                                            Int64 result )
      {
         // TODO: Fix
         // Assert.That( Bytes.ToInt64( value, startIndex, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0}, (UInt16)0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0}, UInt16.MinValue )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 1}, (UInt16)256 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1, 0}, (UInt16)1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128, 0}, (UInt16)128 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 0}, (UInt16)255 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255}, UInt16.MaxValue )]
      [TestCase( ByteOrder.LittleEndian, 1, new Byte[] {0, 0, 1, 0}, (UInt16)256 )]
      [TestCase( ByteOrder.LittleEndian, 2, new Byte[] {0, 0, 0, 1}, (UInt16)256 )]
      [TestCase( ByteOrder.LittleEndian, 3, new Byte[] {0, 0, 0, 0, 1}, (UInt16)256 )]
      [TestCase( ByteOrder.LittleEndian, 4, new Byte[] {0, 0, 0, 0, 0, 1}, (UInt16)256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0}, (UInt16)0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0}, UInt16.MinValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 1}, (UInt16)1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {1, 0}, (UInt16)256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 128}, (UInt16)128 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 255}, (UInt16)255 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255}, UInt16.MaxValue )]
      [TestCase( ByteOrder.BigEndian, 1, new Byte[] {0, 1, 0, 0}, (UInt16)256 )]
      [TestCase( ByteOrder.BigEndian, 2, new Byte[] {0, 0, 1, 0}, (UInt16)256 )]
      [TestCase( ByteOrder.BigEndian, 3, new Byte[] {0, 0, 0, 1, 0}, (UInt16)256 )]
      [TestCase( ByteOrder.BigEndian, 4, new Byte[] {0, 0, 0, 0, 1, 0}, (UInt16)256 )]
      public void to_uint16_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value,
                                                             UInt16 result )
      {
         Assert.That( Bytes.ToUInt16( value, startIndex, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0, 0, 0}, UInt32.MinValue )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 1, 0, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1, 0, 0, 0}, (UInt32)1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128, 0, 0, 0}, (UInt32)128 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 0, 0, 0}, (UInt32)255 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255, 255}, UInt32.MaxValue )]
      [TestCase( ByteOrder.LittleEndian, 1, new Byte[] {0, 0, 1, 0, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.LittleEndian, 2, new Byte[] {0, 0, 0, 1, 0, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.LittleEndian, 3, new Byte[] {0, 0, 0, 0, 1, 0, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.LittleEndian, 4, new Byte[] {0, 0, 0, 0, 0, 1, 0, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0}, UInt32.MinValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 1}, (UInt32)1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 1, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 128}, (UInt32)128 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 255}, (UInt32)255 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255, 255, 255}, UInt32.MaxValue )]
      [TestCase( ByteOrder.BigEndian, 1, new Byte[] {0, 0, 0, 1, 0, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.BigEndian, 2, new Byte[] {0, 0, 0, 0, 1, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.BigEndian, 3, new Byte[] {0, 0, 0, 0, 0, 1, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.BigEndian, 4, new Byte[] {0, 0, 0, 0, 0, 0, 1, 0}, (UInt32)256 )]
      public void to_uint32_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value,
                                                             UInt32 result )
      {
         Assert.That( Bytes.ToUInt32( value, startIndex, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0}, UInt64.MinValue )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 1, 0, 0, 0, 0, 0, 0}, (UInt64)256 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1, 0, 0, 0, 0, 0, 0, 0}, (UInt64)1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128, 0, 0, 0, 0, 0, 0, 0}, (UInt64)128 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 0, 0, 0, 0, 0, 0, 0}, (UInt64)255 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255}, UInt64.MaxValue )]
      [TestCase( ByteOrder.LittleEndian, 1, new Byte[] {0, 0, 1, 0, 0, 0, 0, 0, 0}, (UInt64)256 )]
      [TestCase( ByteOrder.LittleEndian, 2, new Byte[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 0}, (UInt64)256 )]
      [TestCase( ByteOrder.LittleEndian, 3, new Byte[] {0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0}, (UInt64)256 )]
      [TestCase( ByteOrder.LittleEndian, 4, new Byte[] {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0}, (UInt64)256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0}, UInt64.MinValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 1}, (UInt64)1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 1, 0}, (UInt64)256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 128}, (UInt64)128 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 255}, (UInt64)255 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255}, UInt64.MaxValue )]
      [TestCase( ByteOrder.BigEndian, 1, new Byte[] {0, 0, 0, 0, 0, 0, 0, 1, 0, 0}, (UInt64)256 )]
      [TestCase( ByteOrder.BigEndian, 2, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0}, (UInt64)256 )]
      [TestCase( ByteOrder.BigEndian, 3, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0}, (UInt64)256 )]
      [TestCase( ByteOrder.BigEndian, 4, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0}, (UInt64)256 )]
      public void to_uint64_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value,
                                                             UInt64 result )
      {
         Assert.That( Bytes.ToUInt64( value, startIndex, order ), Is.EqualTo( result ) );
      }

      public void to_float32_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value,
                                                              Double result )
      {
         // TODO: Fix
         // Assert.That( Bytes.ToFloat32( value, startIndex, order ), Is.EqualTo( result ) );
      }

      public void to_float64_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value,
                                                              Double result )
      {
         // TODO: Fix
         // Assert.That( Bytes.ToFloat64( value, startIndex, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_int16_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => Bytes.ToInt16( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_int32_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => Bytes.ToInt32( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_int64_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => Bytes.ToInt64( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_uint16_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => Bytes.ToUInt16( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_uint32_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => Bytes.ToUInt32( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_uint64_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => Bytes.ToUInt64( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_float32_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => Bytes.ToFloat32( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_float64_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => Bytes.ToFloat64( value, startIndex, order ) );
      }
   }
}