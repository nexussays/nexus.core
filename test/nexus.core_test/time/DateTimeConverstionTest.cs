using System;
using nexus.core.time;
using NUnit.Framework;

namespace nexus.core_test.time
{
   [TestFixture]
   internal class DateTimeConverstionTest
   {
      [Test]
      public void milliseconds_converts_to_proper_datetime_value()
      {
         Assert.That(
            1492859778037.UnixTimestampInMillisecondsToDateTime(),
            Is.EqualTo( new DateTime( 2017, 4, 22, 11, 16, 18, 37 ) ) );
      }
   }
}