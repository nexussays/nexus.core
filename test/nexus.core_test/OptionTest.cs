// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.core;
using NUnit.Framework;

namespace nexus.core_test
{
   [TestFixture]
   public class OptionTest
   {
      [SetUp]
      public void SetUp()
      {
      }

      [TearDown]
      public void TearDown()
      {
      }

      private void assertInvalidValue<T>( Option<T> opt )
      {
         Assert.Throws(
            typeof(InvalidOperationException),
            () =>
            {
               var x = opt.Value;
            } );
      }

      [Test]
      public void no_value_is_not_the_same_as_null()
      {
         Assert.That( new Option<String>( null ).HasValue, Is.True );
         Assert.That( new Option<String>().HasValue, Is.False );
         Assert.That( new Option<Object>( null ).HasValue, Is.True );
         Assert.That( new Option<Object>().HasValue, Is.False );
      }

      [Test]
      public void option_of_int_is_equal_to_same_int_value()
      {
         var opt1 = Option.Of( 0 );
         Assert.That( opt1.Value, Is.Zero );
      }

      [Test]
      public void option_of_string_is_equal_to_same_string_value()
      {
         var m2 = Option.Of( "foo" );
         Assert.That( m2, Is.EqualTo( "foo" ) );

         var opt = Option.Of( String.Empty );
         Assert.That( opt.Value, Is.EqualTo( String.Empty ) );
      }

      [Test]
      public void should_report_having_value()
      {
         var m1 = new Option<String>( "foo" );
         var m2 = Option.NoValue;
         var m3 = new Option<String>();
         Assert.That( m1.HasValue, Is.True );
         Assert.That( m2.HasValue, Is.False );
         Assert.That( m3.HasValue, Is.False );
      }

      [Test]
      public void should_throw_if_resolving_no_value()
      {
         assertInvalidValue( new Option<Int32>() );
         assertInvalidValue( Option.NoValue );
         assertInvalidValue( new Option<String>() );
      }

      [Test]
      public void typed_no_value_should_not_equal_any_no_value()
      {
         var m1 = Option.NoValue;
         var m2 = Option<Int32>.NoValue;
         var m3 = Option<String>.NoValue;
         Assert.That( m1, Is.Not.EqualTo( m2 ) );
         Assert.That( m2, Is.Not.EqualTo( m3 ) );
         Assert.That( m1, Is.Not.EqualTo( m3 ) );
      }
   }
}
