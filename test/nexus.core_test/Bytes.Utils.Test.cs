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
   internal partial class BytesTest
   {
      [TestCase(
         new Byte[] { 120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72 },
         new Byte[] { 120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72 })]
      [TestCase(
         new Byte[] { 62, 78, 65, 62, 84, 6, 247, 7, 4, 34, 56, 89, 78, 53, 6, 8, 5, 88, 12, 90, 2, 1, 75, 47 },
         new Byte[] { 62, 78, 65, 62, 84, 6, 247, 7, 4, 34, 56, 89, 78, 53, 6, 8, 5, 88, 12, 90, 2, 1, 75, 47 })]
      [TestCase(
         new Byte[] { 26, 8, 34, 5, 8, 0, 64, 0, 7, 53, 53, 2, 46, 74, 30, 32, 5, 0, 7, 54, 78, 3, 2, 58 },
         new Byte[] { 26, 8, 34, 5, 8, 0, 64, 0, 7, 53, 53, 2, 46, 74, 30, 32, 5, 0, 7, 54, 78, 3, 2, 58 })]
      public void equals_byte_array_returns_true_on_equal_bytearrays( Byte[] one, Byte[] two )
      {
         Assert.That(Bytes.EqualsByteArray(one, two), Is.True);
      }

      [TestCase(
         new Byte[] { 120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72 },
         new Byte[] { 62, 78, 65, 62, 84, 6, 247, 7, 4, 34, 56, 89, 78, 53, 6, 8, 5, 88, 12, 90, 2, 1, 75, 47 })]
      [TestCase(
         new Byte[] { 62, 78, 65, 62, 84, 6, 247, 7, 4, 34, 56, 89, 78, 53, 6, 8, 5, 88, 12, 90, 2, 1, 75, 47 },
         new Byte[] { 26, 8, 34, 5, 8, 0, 64, 0, 7, 53, 53, 2, 46, 74, 30, 32, 5, 0, 7, 54, 78, 3, 2, 58 })]
      [TestCase(
         new Byte[] { 26, 8, 34, 5, 8, 0, 64, 0, 7, 53, 53, 2, 46, 74, 30, 32, 5, 0, 7, 54, 78, 3, 2, 58 },
         new Byte[] { 120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72 })]
      public void equals_byte_array_returns_false_on_unequal_bytearrays( Byte[] one, Byte[] two )
      {
         Assert.That(Bytes.EqualsByteArray(one, two), Is.False);
      }

      [TestCase(null)]
      [TestCase(new Byte[] { })]
      public void is_null_or_empty_returns_true_on_null_and_empty_bytearrays( Byte[] value )
      {
         Assert.That(Bytes.IsNullOrEmpty(value), Is.True);
      }

      [TestCase(new Byte[] { 0 })]
      [TestCase(new Byte[] { 1 })]
      [TestCase(new Byte[] { 255 })]
      [TestCase(new Byte[] { 0, 0 })]
      [TestCase(new Byte[] { 120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72 })]
      public void is_null_or_empty_returns_false_on_bytearrays_with_at_least_one_index( Byte[] bytes )
      {
         Assert.That(Bytes.IsNullOrEmpty(bytes), Is.False);
      }

      [TestCase(null)]
      [TestCase(new Byte[] { })]
      public void is_null_or_empty_or_null_byte_returns_true_on_null_and_empty_bytearrays( Byte[] value )
      {
         Assert.That(Bytes.IsNullOrEmptyOrNullByte(value), Is.True);
      }

      [TestCase(new Byte[] { 0 })]
      public void is_null_or_empty_or_null_byte_returns_true_on_bytearrays_with_a_single_0_byte( Byte[] value )
      {
         Assert.That(Bytes.IsNullOrEmptyOrNullByte(value), Is.True);
      }

      [TestCase(new Byte[] { 1 })]
      [TestCase(new Byte[] { 255 })]
      [TestCase(new Byte[] { 0, 0 })]
      [TestCase(new Byte[] { 120, 39, 45, 36, 22, 78, 63, 54, 87, 91, 13, 8, 0, 80, 106, 79, 39, 44, 72 })]
      public void is_null_or_empty_or_null_byte_returns_false_on_bytearrays_with_at_least_one_index_that_is_not_0(
         Byte[] bytes )
      {
         Assert.That(Bytes.IsNullOrEmptyOrNullByte(bytes), Is.False);
      }

      public void Slice( Byte[] source, Int32 startByteIndex = 0 )
      {
         Assert.Fail();
         Bytes.Slice(source, startByteIndex);
      }

      public void Slice( Byte[] source, Int32 startByteIndex, Int32 endByteIndex )
      {
         Assert.Fail();
         Bytes.Slice(source, startByteIndex, endByteIndex);
      }

   }
}
