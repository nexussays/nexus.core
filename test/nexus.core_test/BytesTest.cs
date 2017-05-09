// Copyright Malachi Griffie
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
   internal class BytesTest
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

      //[Ignore( "Test incomplete" )]
      //public void to_bytes_correctly_converts_float32( ByteOrder order, Byte[] result, Single value )
      //{
      //   // TODO: Fix
      //   Assert.That( Bytes.ToBytes( value, order ), Is.EqualTo( result ) );
      //}

      //[Ignore( "Test incomplete" )]
      //public void to_bytes_correctly_converts_float64( ByteOrder order, Byte[] result, Double value )
      //{
      //   // TODO: Fix
      //   Assert.That( Bytes.ToBytes( value, order ), Is.EqualTo( result ) );
      //}

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] { }, 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0}, 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1}, (Int16)0x0001 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128}, (Int16)0x0080 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255}, (Int16)0x00ff )]
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
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] { }, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0}, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {1}, unchecked((Int16)0x0100) )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128}, unchecked((Int16)0x8000) )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255}, unchecked((Int16)0xff00) )]
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

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] { }, 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0}, 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1}, 1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1}, 0x00000001 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128}, 0x00000080 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255}, 0x000000ff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255}, 0x0000ffff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255}, 0x00ffffff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 1}, 256 )]
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
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] { }, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0}, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {1}, 0x01000000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128}, Int32.MinValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255}, unchecked((Int32)0xff000000) )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255}, unchecked((Int32)0xffff0000) )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255, 255}, unchecked((Int32)0xffffff00) )]
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

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] { }, ExpectedResult = 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0}, ExpectedResult = 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1}, ExpectedResult = 1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1}, ExpectedResult = 0x0000000000000001 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128}, ExpectedResult = 0x0000000000000080 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255}, ExpectedResult = 0x00000000000000ff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255}, ExpectedResult = 0x000000000000ffff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255}, ExpectedResult = 0x0000000000ffffff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255, 255}, ExpectedResult = 0x00000000ffffffff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255, 255, 255}, ExpectedResult = 0x000000ffffffffff )]
      [TestCase(
         ByteOrder.LittleEndian,
         0,
         new Byte[] {255, 255, 255, 255, 255, 255},
         ExpectedResult = 0x0000ffffffffffff )]
      [TestCase(
         ByteOrder.LittleEndian,
         0,
         new Byte[] {255, 255, 255, 255, 255, 255, 255},
         ExpectedResult = 0x00ffffffffffffff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0}, ExpectedResult = 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 1}, ExpectedResult = 256 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 1, 0, 0, 0, 0, 0, 0}, ExpectedResult = 256 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = 1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 128}, ExpectedResult = Int64.MinValue )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = 128 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = 255 )]
      [TestCase(
         ByteOrder.LittleEndian,
         0,
         new Byte[] {255, 255, 255, 255, 255, 255, 255, 127},
         ExpectedResult = Int64.MaxValue )]
      [TestCase(
         ByteOrder.LittleEndian,
         0,
         new Byte[] {255, 0, 0, 0, 0, 0, 0, 128},
         ExpectedResult = Int64.MinValue + 255 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255}, ExpectedResult = -1 )]
      [TestCase( ByteOrder.LittleEndian, 1, new Byte[] {0, 0, 1, 0, 0, 0, 0, 0, 0}, ExpectedResult = 256 )]
      [TestCase( ByteOrder.LittleEndian, 2, new Byte[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 0}, ExpectedResult = 256 )]
      [TestCase( ByteOrder.LittleEndian, 3, new Byte[] {0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0}, ExpectedResult = 256 )]
      [TestCase( ByteOrder.LittleEndian, 4, new Byte[] {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0}, ExpectedResult = 256 )]
      [TestCase(
         ByteOrder.LittleEndian,
         0,
         new Byte[] {0x00, 0x00, 0x9C, 0x58, 0x4C, 0x49, 0x1F, 0xF2},
         ExpectedResult = -1000000000000000000L )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] { }, ExpectedResult = 0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0}, ExpectedResult = 0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {1}, ExpectedResult = 0x0100000000000000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128}, ExpectedResult = Int64.MinValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255}, ExpectedResult = unchecked((Int64)0xff00000000000000) )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255}, ExpectedResult = unchecked((Int64)0xffff000000000000) )]
      [TestCase(
         ByteOrder.BigEndian,
         0,
         new Byte[] {255, 255, 255},
         ExpectedResult = unchecked((Int64)0xffffff0000000000) )]
      [TestCase(
         ByteOrder.BigEndian,
         0,
         new Byte[] {255, 255, 255, 255},
         ExpectedResult = unchecked((Int64)0xffffffff00000000) )]
      [TestCase(
         ByteOrder.BigEndian,
         0,
         new Byte[] {255, 255, 255, 255, 255},
         ExpectedResult = unchecked((Int64)0xffffffffff000000) )]
      [TestCase(
         ByteOrder.BigEndian,
         0,
         new Byte[] {255, 255, 255, 255, 255, 255},
         ExpectedResult = unchecked((Int64)0xffffffffffff0000) )]
      [TestCase(
         ByteOrder.BigEndian,
         0,
         new Byte[] {255, 255, 255, 255, 255, 255, 255},
         ExpectedResult = unchecked((Int64)0xffffffffffffff00) )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = 0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 1}, ExpectedResult = 1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 1, 0}, ExpectedResult = 256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 128}, ExpectedResult = 128 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = Int64.MinValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128, 0, 0, 0, 0, 0, 0, 1}, ExpectedResult = Int64.MinValue + 1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {192, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = Int64.MinValue / 2 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 255}, ExpectedResult = 255 )]
      [TestCase(
         ByteOrder.BigEndian,
         0,
         new Byte[] {127, 255, 255, 255, 255, 255, 255, 255},
         ExpectedResult = 9223372036854775807 )]
      [TestCase(
         ByteOrder.BigEndian,
         0,
         new Byte[] {128, 0, 0, 0, 0, 0, 0, 255},
         ExpectedResult = Int64.MinValue + 255 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255}, ExpectedResult = -1 )]
      [TestCase( ByteOrder.BigEndian, 1, new Byte[] {0, 0, 0, 0, 0, 0, 0, 1, 0, 0}, ExpectedResult = 256 )]
      [TestCase( ByteOrder.BigEndian, 2, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0}, ExpectedResult = 256 )]
      [TestCase( ByteOrder.BigEndian, 3, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0}, ExpectedResult = 256 )]
      [TestCase( ByteOrder.BigEndian, 4, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0}, ExpectedResult = 256 )]
      [TestCase(
         ByteOrder.BigEndian,
         0,
         new Byte[] {0xF2, 0x1F, 0x49, 0x4C, 0x58, 0x9C, 0x00, 0x00},
         ExpectedResult = -1000000000000000000 )]
      public Int64 to_int64_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         return Bytes.ToInt64( value, startIndex, order );
      }

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] { }, (UInt16)0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0}, (UInt16)0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1}, (UInt16)0x0001 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128}, (UInt16)0x0080 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255}, (UInt16)0x00ff )]
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
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] { }, (UInt16)0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0}, (UInt16)0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {1}, (UInt16)0x0100 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128}, (UInt16)0x8000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255}, (UInt16)0xff00 )]
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

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] { }, (UInt32)0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0}, (UInt32)0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1}, (UInt32)1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1}, (UInt32)0x00000001 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128}, (UInt32)0x00000080 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255}, (UInt32)0x000000ff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255}, (UInt32)0x0000ffff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255}, (UInt32)0x00ffffff )]
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
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] { }, (UInt32)0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0}, (UInt32)0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {1}, (UInt32)0x01000000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128}, 0x80000000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255}, 0xFF000000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255}, 0xFFFF0000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255, 255}, 0xFFFFFF00 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255, 255, 255}, 0xFFFFFFFF )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0}, UInt32.MinValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 1}, (UInt32)1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 1, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 128}, (UInt32)128 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 255}, (UInt32)255 )]
      [TestCase( ByteOrder.BigEndian, 1, new Byte[] {0, 0, 0, 1, 0, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.BigEndian, 2, new Byte[] {0, 0, 0, 0, 1, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.BigEndian, 3, new Byte[] {0, 0, 0, 0, 0, 1, 0}, (UInt32)256 )]
      [TestCase( ByteOrder.BigEndian, 4, new Byte[] {0, 0, 0, 0, 0, 0, 1, 0}, (UInt32)256 )]
      public void to_uint32_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value,
                                                             UInt32 result )
      {
         Assert.That( Bytes.ToUInt32( value, startIndex, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] { }, ExpectedResult = (UInt64)0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0}, ExpectedResult = (UInt64)0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1}, ExpectedResult = (UInt64)1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1}, ExpectedResult = (UInt64)0x0000000000000001 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128}, ExpectedResult = (UInt64)0x0000000000000080 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255}, ExpectedResult = (UInt64)0x00000000000000ff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255}, ExpectedResult = (UInt64)0x000000000000ffff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255, 255}, ExpectedResult = (UInt64)0x0000000000ffffff )]
      [TestCase(
         ByteOrder.LittleEndian,
         0,
         new Byte[] {255, 255, 255, 255},
         ExpectedResult = (UInt64)0x00000000ffffffff )]
      [TestCase(
         ByteOrder.LittleEndian,
         0,
         new Byte[] {255, 255, 255, 255, 255},
         ExpectedResult = (UInt64)0x000000ffffffffff )]
      [TestCase(
         ByteOrder.LittleEndian,
         0,
         new Byte[] {255, 255, 255, 255, 255, 255},
         ExpectedResult = (UInt64)0x0000ffffffffffff )]
      [TestCase(
         ByteOrder.LittleEndian,
         0,
         new Byte[] {255, 255, 255, 255, 255, 255, 255},
         ExpectedResult = (UInt64)0x00ffffffffffffff )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = UInt64.MinValue )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 1, 0, 0, 0, 0, 0, 0}, ExpectedResult = (UInt64)256 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = (UInt64)1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = (UInt64)128 )]
      [TestCase(
         ByteOrder.LittleEndian,
         0,
         new Byte[] {0, 0, 0, 0, 0, 0, 0, 128},
         ExpectedResult = 9223372036854775808 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = (UInt64)255 )]
      [TestCase(
         ByteOrder.LittleEndian,
         0,
         new Byte[] {255, 255, 255, 255, 255, 255, 255, 255},
         ExpectedResult = UInt64.MaxValue )]
      [TestCase( ByteOrder.LittleEndian, 1, new Byte[] {0, 0, 1, 0, 0, 0, 0, 0, 0}, ExpectedResult = (UInt64)256 )]
      [TestCase( ByteOrder.LittleEndian, 2, new Byte[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 0}, ExpectedResult = (UInt64)256 )]
      [TestCase(
         ByteOrder.LittleEndian,
         3,
         new Byte[] {0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
         ExpectedResult = (UInt64)256 )]
      [TestCase(
         ByteOrder.LittleEndian,
         4,
         new Byte[] {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
         ExpectedResult = (UInt64)256 )]
      [TestCase(
         ByteOrder.LittleEndian,
         0,
         new Byte[] {0x00, 0x00, 0x9C, 0x58, 0x4C, 0x49, 0x1F, 0xF2},
         ExpectedResult = 17446744073709551616 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] { }, ExpectedResult = (UInt64)0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0}, ExpectedResult = (UInt64)0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {1}, ExpectedResult = (UInt64)0x0100000000000000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {128}, ExpectedResult = 0x8000000000000000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255}, ExpectedResult = 0xff00000000000000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255}, ExpectedResult = 0xffff000000000000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255, 255}, ExpectedResult = 0xffffff0000000000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255, 255, 255}, ExpectedResult = 0xffffffff00000000 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255, 255, 255, 255}, ExpectedResult = 0xffffffffff000000 )]
      //[TestCase(
      //   ByteOrder.BigEndian,
      //   0,
      //   new Byte[] {255, 255, 255, 255, 255, 255},
      //   ExpectedResult = unchecked((Int64)0xffffffffffff0000) )]
      //[TestCase(
      //   ByteOrder.BigEndian,
      //   0,
      //   new Byte[] {255, 255, 255, 255, 255, 255, 255},
      //   ExpectedResult = unchecked((Int64)0xffffffffffffff00) )]
      //[TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = 0 )]
      //[TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 1}, ExpectedResult = (UInt64)1 )]
      //[TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 1, 0}, ExpectedResult = (UInt64)256 )]
      //[TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 128}, ExpectedResult = (UInt64)128 )]
      //[TestCase( ByteOrder.BigEndian, 0, new Byte[] {128, 0, 0, 0, 0, 0, 0, 0}, ExpectedResult = 9223372036854775808 )]
      //[TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0, 0, 0, 0, 0, 0, 255}, ExpectedResult = (UInt64)255 )]
      //[TestCase(
      //   ByteOrder.BigEndian,
      //   0,
      //   new Byte[] {255, 255, 255, 255, 255, 255, 255, 255},
      //   ExpectedResult = UInt64.MaxValue )]
      //[TestCase( ByteOrder.BigEndian, 1, new Byte[] {0, 0, 0, 0, 0, 0, 0, 1, 0, 0}, ExpectedResult = (UInt64)256 )]
      //[TestCase( ByteOrder.BigEndian, 2, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0}, ExpectedResult = (UInt64)256 )]
      //[TestCase( ByteOrder.BigEndian, 3, new Byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0}, ExpectedResult = (UInt64)256 )]
      //[TestCase(
      //   ByteOrder.BigEndian,
      //   4,
      //   new Byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
      //   ExpectedResult = (UInt64)256 )]
      //[TestCase(
      //   ByteOrder.BigEndian,
      //   0,
      //   new Byte[] {0xF2, 0x1F, 0x49, 0x4C, 0x58, 0x9C, 0x00, 0x00},
      //   ExpectedResult = 17446744073709551616 )]
      public UInt64 to_uint64_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         return Bytes.ToUInt64( value, startIndex, order );
      }

      [Ignore( "Test incomplete" )]
      public Single to_float32_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         // TODO: add test case data
         return Bytes.ToFloat32( value, startIndex, order );
      }

      [Ignore( "Test incomplete" )]
      public Double to_float64_properly_parses_valid_bytearray( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         // TODO: add test case data
         return Bytes.ToFloat64( value, startIndex, order );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_int16_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<ArgumentNullException>( () => Bytes.ToInt16( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_int32_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<ArgumentNullException>( () => Bytes.ToInt32( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_int64_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<ArgumentNullException>( () => Bytes.ToInt64( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_uint16_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<ArgumentNullException>( () => Bytes.ToUInt16( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_uint32_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<ArgumentNullException>( () => Bytes.ToUInt32( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_uint64_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<ArgumentNullException>( () => Bytes.ToUInt64( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_float32_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<ArgumentNullException>( () => Bytes.ToFloat32( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void to_float64_throws_when_parsing_null( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<ArgumentNullException>( () => Bytes.ToFloat64( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {23, 56} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 128} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {93, 56} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0} )]
      public void to_int16_to_bytes_equals_the_original_bytes( ByteOrder order, Byte[] value )
      {
         Assert.That( Bytes.ToBytes( Bytes.ToInt16( value, 0, order ), order ), Is.EqualTo( value ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255, 255, 255} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 8, 93, 56} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0, 0, 128} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255, 255, 255} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 82, 93, 56} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0, 0, 0} )]
      public void to_int32_to_bytes_equals_the_original_bytes( ByteOrder order, Byte[] value )
      {
         Assert.That( Bytes.ToBytes( Bytes.ToInt32( value, 0, order ), order ), Is.EqualTo( value ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0x17, 0, 0x52, 0, 0x8, 0x5d, 0x38} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 128} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0x00, 0x00, 0x9C, 0x58, 0x4C, 0x49, 0x1F, 0xF2} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0x17, 0, 0x52, 0, 0x8, 0x5d, 0x38} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0, 0, 0, 0, 0, 0, 0} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0xF2, 0x1F, 0x49, 0x4C, 0x58, 0x9C, 0x00, 0x00} )]
      public void to_int64_to_bytes_equals_the_original_bytes( ByteOrder order, Byte[] value )
      {
         var val = Bytes.ToInt64( value, 0, order );
         var res = Bytes.ToBytes( val, order );
         TestContext.Out.Write(
            "order={1} int64={0} bytes={2} res={3} c={4}".F(
               val,
               order,
               value.EncodeToBase16String(),
               res.EncodeToBase16String(),
               BitConverter.ToInt64( value, 0 ) ) );
         Assert.That( res, Is.EqualTo( value ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {93, 56} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 128} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {23, 56} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0} )]
      public void to_uint16_to_bytes_equals_the_original_bytes( ByteOrder order, Byte[] value )
      {
         Assert.That( Bytes.ToBytes( Bytes.ToUInt16( value, 0, order ), order ), Is.EqualTo( value ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255, 255, 255} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 8, 93, 56} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0, 0, 128} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255, 255, 255} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 23, 93, 56} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0, 0, 0} )]
      public void to_uint32_to_bytes_equals_the_original_bytes( ByteOrder order, Byte[] value )
      {
         Assert.That( Bytes.ToBytes( Bytes.ToUInt32( value, 0, order ), order ), Is.EqualTo( value ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 23, 0, 82, 0, 8, 93, 56} )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0, 0, 0, 0, 0, 0, 128} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255, 255, 255, 255, 255, 255, 255} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 23, 0, 82, 0, 8, 93, 56} )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {128, 0, 0, 0, 0, 0, 0, 0} )]
      public void to_uint64_to_bytes_equals_the_original_bytes( ByteOrder order, Byte[] value )
      {
         Assert.That( Bytes.ToBytes( Bytes.ToUInt64( value, 0, order ), order ), Is.EqualTo( value ) );
      }

      //[Ignore( "Test incomplete" )]
      //public void to_float32_to_bytes_equals_the_original_bytes( ByteOrder order, Byte[] value )
      //{
      //   // TODO: Fix
      //   Assert.That( Bytes.ToBytes( Bytes.ToFloat32( value, 0, order ), order ), Is.EqualTo( value ) );
      //}

      //[Ignore( "Test incomplete" )]
      //public void to_float64_to_bytes_equals_the_original_bytes( ByteOrder order, Byte[] value )
      //{
      //   // TODO: Fix
      //   Assert.That( Bytes.ToBytes( Bytes.ToFloat64( value, 0, order ), order ), Is.EqualTo( value ) );
      //}

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

      //[Ignore( "Test incomplete" )]
      //public void to_bytes_to_float32_equals_original_float32(
      //   [Random( Single.MinValue, Single.MaxValue, 10 )] Single value,
      //   [Values( ByteOrder.BigEndian, ByteOrder.LittleEndian )] ByteOrder endianness )
      //{
      //   // TODO: Implement
      //   Assert.That( Bytes.ToFloat32( Bytes.ToBytes( value, endianness ), 0, endianness ), Is.EqualTo( value ) );
      //}

      //[Ignore( "Test incomplete" )]
      //public void to_bytes_to_float64_equals_original_float64(
      //   [Random( Double.MinValue, Double.MaxValue, 10 )] Double value,
      //   [Values( ByteOrder.BigEndian, ByteOrder.LittleEndian )] ByteOrder endianness )
      //{
      //   // TODO: Implement
      //   Assert.That( Bytes.ToFloat64( Bytes.ToBytes( value, endianness ), 0, endianness ), Is.EqualTo( value ) );
      //}

      // ReSharper restore NUnit.MethodWithParametersAndTestAttribute
   }
}