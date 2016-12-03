using System;
using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace nexus.core.test
{
   [TestFixture]
   internal class ByteUtilsTest
   {
      [TestCase( new Byte[] {120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72},
          new Byte[] {120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72} )]
      [TestCase( new Byte[] {62, 78, 65, 62, 84, 6, 247, 7, 4, 34, 56, 89, 78, 53, 6, 8, 5, 88, 12, 90, 2, 1, 75, 47},
          new Byte[] {62, 78, 65, 62, 84, 6, 247, 7, 4, 34, 56, 89, 78, 53, 6, 8, 5, 88, 12, 90, 2, 1, 75, 47} )]
      [TestCase( new Byte[] {26, 8, 34, 5, 8, 0, 64, 0, 7, 53, 53, 2, 46, 74, 30, 32, 5, 0, 7, 54, 78, 3, 2, 58},
          new Byte[] {26, 8, 34, 5, 8, 0, 64, 0, 7, 53, 53, 2, 46, 74, 30, 32, 5, 0, 7, 54, 78, 3, 2, 58} )]
      public void equal_bytes_arrays_are_equal( Byte[] one, Byte[] two )
      {
         Assert.That( ByteUtils.EqualsByteArray( one, two ), Is.True );
      }

      [TestCase( new Byte[] {120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72},
          new Byte[] {62, 78, 65, 62, 84, 6, 247, 7, 4, 34, 56, 89, 78, 53, 6, 8, 5, 88, 12, 90, 2, 1, 75, 47} )]
      [TestCase( new Byte[] {62, 78, 65, 62, 84, 6, 247, 7, 4, 34, 56, 89, 78, 53, 6, 8, 5, 88, 12, 90, 2, 1, 75, 47},
          new Byte[] {26, 8, 34, 5, 8, 0, 64, 0, 7, 53, 53, 2, 46, 74, 30, 32, 5, 0, 7, 54, 78, 3, 2, 58} )]
      [TestCase( new Byte[] {26, 8, 34, 5, 8, 0, 64, 0, 7, 53, 53, 2, 46, 74, 30, 32, 5, 0, 7, 54, 78, 3, 2, 58},
          new Byte[] {120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72} )]
      public void unequal_bytes_arrays_are_not_equal( Byte[] one, Byte[] two )
      {
         Assert.That( ByteUtils.EqualsByteArray( one, two ), Is.False );
      }

      [TestCase( new Byte[] {0} )]
      [TestCase( new Byte[] {1} )]
      [TestCase( new Byte[] {255} )]
      [TestCase( new Byte[] {0, 0} )]
      [TestCase( new Byte[] {120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72} )]
      public void bytearrays_with_at_least_one_byte_are_not_nullorempty( Byte[] bytes )
      {
         Assert.That( ByteUtils.IsNullOrEmpty( bytes ), Is.False );
      }

      [TestCase( new Byte[] {1} )]
      [TestCase( new Byte[] {255} )]
      [TestCase( new Byte[] {0, 0} )]
      [TestCase( new Byte[] {120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72} )]
      public void bytearrays_with_at_least_one_nonzero_byte_are_notnulloremptyornullbyte( Byte[] bytes )
      {
         Assert.That( ByteUtils.IsNullOrEmptyOrNullByte( bytes ), Is.False );
      }

      public void Slice( Byte[] source, Int32 startByteIndex = 0 )
      {
         Assert.Fail();
         ByteUtils.Slice( source, startByteIndex );
      }

      public void Slice( Byte[] source, Int32 startByteIndex, Int32 endByteIndex )
      {
         Assert.Fail();
         ByteUtils.Slice( source, startByteIndex, endByteIndex );
      }

      [Test]
      public void bytearray_with_single_zero_is_nullbyte()
      {
         Assert.That( ByteUtils.IsNullOrEmptyOrNullByte( new Byte[] {0} ), Is.True );
      }

      [Test]
      public void empty_bytearray_is_null_or_empty()
      {
         Assert.That( ByteUtils.IsNullOrEmpty( new Byte[] {} ), Is.True );
         Assert.That( ByteUtils.IsNullOrEmptyOrNullByte( new Byte[] {} ), Is.True );
      }

      [Test]
      public void null_bytearray_is_null_or_empty()
      {
         Assert.That( ByteUtils.IsNullOrEmpty( null ), Is.True );
         Assert.That( ByteUtils.IsNullOrEmptyOrNullByte( null ), Is.True );
      }

      #region invalid byte array conversions

      [TestCase( new Byte[] {} )]
      [TestCase( new Byte[] {0} )]
      [TestCase( new Byte[] {1} )]
      [TestCase( new Byte[] {128} )]
      [TestCase( new Byte[] {255} )]
      public void invalid_bytearrays_throw_when_converting_to_int16( Byte[] value )
      {
         Assert.Throws<IndexOutOfRangeException>( () => ByteUtils.ToInt16( value, 0, ByteOrder.BigEndian ) );
         Assert.Throws<IndexOutOfRangeException>( () => ByteUtils.ToInt16( value, 0, ByteOrder.LittleEndian ) );
      }

      [TestCase( new Byte[] {} )]
      [TestCase( new Byte[] {0} )]
      [TestCase( new Byte[] {1} )]
      [TestCase( new Byte[] {128} )]
      [TestCase( new Byte[] {255} )]
      [TestCase( new Byte[] {255, 255} )]
      [TestCase( new Byte[] {255, 255, 255} )]
      public void invalid_bytearrays_throw_when_converting_to_int32( Byte[] value )
      {
         Assert.Throws<IndexOutOfRangeException>( () => ByteUtils.ToInt32( value, 0, ByteOrder.BigEndian ) );
         Assert.Throws<IndexOutOfRangeException>( () => ByteUtils.ToInt32( value, 0, ByteOrder.LittleEndian ) );
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
      public void invalid_bytearrays_throw_when_converting_to_int64( Byte[] value )
      {
         Assert.Throws<IndexOutOfRangeException>( () => ByteUtils.ToInt64( value, 0, ByteOrder.BigEndian ) );
         Assert.Throws<IndexOutOfRangeException>( () => ByteUtils.ToInt64( value, 0, ByteOrder.LittleEndian ) );
      }

      #endregion

      #region to bytes conversions

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
      public void int16_converts_to_correct_bytes( ByteOrder order, Byte[] result, Int16 value )
      {
         Assert.That( ByteUtils.ToBytes( value, order ), Is.EqualTo( result ) );
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
      public void int32_converts_to_correct_bytes( ByteOrder order, Byte[] result, Int32 value )
      {
         Assert.That( ByteUtils.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0}, 0 )]
      public void int64_converts_to_correct_bytes( ByteOrder order, Byte[] result, Int64 value )
      {
         Assert.Fail();
         Assert.That( ByteUtils.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0}, UInt16.MinValue )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 1}, 256 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {1, 0}, 1 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {128, 0}, 128 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 0}, 255 )]
      [TestCase( ByteOrder.LittleEndian, new Byte[] {255, 255}, UInt16.MaxValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0}, UInt16.MinValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 1}, 1 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 128}, 128 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 255}, 255 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {255, 255}, UInt16.MaxValue )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0}, 0 )]
      public void uint16_converts_to_correct_bytes( ByteOrder order, Byte[] result, Int32 value )
      {
         Assert.That( ByteUtils.ToBytes( (UInt16)value, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0}, 0 )]
      public void uint32_converts_to_correct_bytes( ByteOrder order, Byte[] result, UInt32 value )
      {
         Assert.Fail();
         Assert.That( ByteUtils.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0}, 0 )]
      public void uint64_converts_to_correct_bytes( ByteOrder order, Byte[] result, UInt64 value )
      {
         Assert.Fail();
         Assert.That( ByteUtils.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0}, 0 )]
      public void float32_converts_to_correct_bytes( ByteOrder order, Byte[] result, Single value )
      {
         Assert.Fail();
         Assert.That( ByteUtils.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, new Byte[] {0, 0}, 0 )]
      public void float64_converts_to_correct_bytes( ByteOrder order, Byte[] result, Double value )
      {
         Assert.Fail();
         Assert.That( ByteUtils.ToBytes( value, order ), Is.EqualTo( result ) );
      }

      #endregion

      #region valid byte arrays conversions

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
      public void valid_bytearrays_convert_to_int16( ByteOrder order, Int32 startIndex, Byte[] value, Int16 result )
      {
         Assert.That( ByteUtils.ToInt16( value, startIndex, order ), Is.EqualTo( result ) );
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
      public void valid_bytearrays_convert_to_int32( ByteOrder order, Int32 startIndex, Byte[] value, Int32 result )
      {
         Assert.That( ByteUtils.ToInt32( value, startIndex, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, null, 0 )]
      public void valid_bytearrays_convert_to_int64( ByteOrder order, Int32 startIndex, Byte[] value, Int64 result )
      {
         Assert.That( ByteUtils.ToInt64( value, startIndex, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 0}, UInt16.MinValue )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {0, 1}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {1, 0}, 1 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {128, 0}, 128 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 0}, 255 )]
      [TestCase( ByteOrder.LittleEndian, 0, new Byte[] {255, 255}, UInt16.MaxValue )]
      [TestCase( ByteOrder.LittleEndian, 1, new Byte[] {0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 2, new Byte[] {0, 0, 0, 1}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 3, new Byte[] {0, 0, 0, 0, 1}, 256 )]
      [TestCase( ByteOrder.LittleEndian, 4, new Byte[] {0, 0, 0, 0, 0, 1}, 256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0}, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 0}, UInt16.MinValue )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 1}, 1 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 128}, 128 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {0, 255}, 255 )]
      [TestCase( ByteOrder.BigEndian, 0, new Byte[] {255, 255}, UInt16.MaxValue )]
      [TestCase( ByteOrder.BigEndian, 1, new Byte[] {0, 1, 0, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 2, new Byte[] {0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 3, new Byte[] {0, 0, 0, 1, 0}, 256 )]
      [TestCase( ByteOrder.BigEndian, 4, new Byte[] {0, 0, 0, 0, 1, 0}, 256 )]
      public void valid_bytearrays_convert_to_uint16( ByteOrder order, Int32 startIndex, Byte[] value, Int32 result )
      {
         Assert.That( ByteUtils.ToUInt16( value, startIndex, order ), Is.EqualTo( (UInt16)result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, null, 0 )]
      public void valid_bytearrays_convert_to_uint32( ByteOrder order, Int32 startIndex, Byte[] value, Int32 result )
      {
         Assert.That( ByteUtils.ToUInt32( value, startIndex, order ), Is.EqualTo( (UInt32)result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, null, 0 )]
      public void valid_bytearrays_convert_to_uint64( ByteOrder order, Int32 startIndex, Byte[] value, UInt64 result )
      {
         Assert.That( ByteUtils.ToUInt64( value, startIndex, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, null, 0 )]
      public void valid_bytearrays_convert_to_float32( ByteOrder order, Int32 startIndex, Byte[] value, Double result )
      {
         Assert.That( ByteUtils.ToSingle( value, startIndex, order ), Is.EqualTo( result ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null, 0 )]
      [TestCase( ByteOrder.BigEndian, 0, null, 0 )]
      public void valid_bytearrays_convert_to_float64( ByteOrder order, Int32 startIndex, Byte[] value, Double result )
      {
         Assert.That( ByteUtils.ToDouble( value, startIndex, order ), Is.EqualTo( result ) );
      }

      #endregion

      #region null bytes arrays conversions

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void null_bytearrays_throw_when_converting_to_int16( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => ByteUtils.ToInt16( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void null_bytearrays_throw_when_converting_to_int32( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => ByteUtils.ToInt32( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void null_bytearrays_throw_when_converting_to_int64( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => ByteUtils.ToInt64( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void null_bytearrays_throw_when_converting_to_uint16( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => ByteUtils.ToUInt16( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void null_bytearrays_throw_when_converting_to_uint32( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => ByteUtils.ToUInt32( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void null_bytearrays_throw_when_converting_to_uint64( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => ByteUtils.ToUInt64( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void null_bytearrays_throw_when_converting_to_float32( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => ByteUtils.ToSingle( value, startIndex, order ) );
      }

      [TestCase( ByteOrder.LittleEndian, 0, null )]
      [TestCase( ByteOrder.BigEndian, 0, null )]
      public void null_bytearrays_throw_when_converting_to_float64( ByteOrder order, Int32 startIndex, Byte[] value )
      {
         Assert.Throws<NullReferenceException>( () => ByteUtils.ToDouble( value, startIndex, order ) );
      }

      #endregion
   }
}