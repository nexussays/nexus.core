using System;
using NUnit.Framework;

namespace nexus.core.test
{
   [TestFixture]
   internal class CoreUtilsTest
   {
      [Test]
      public void utility_throw_method_works()
      {
         Assert.Throws<Exception>( CoreUtils.ThrowException );
      }

      [Test]
      public void utilitydivide_by_0_method_works()
      {
         Assert.Throws<DivideByZeroException>( () => CoreUtils.ThrowDivideByZero() );
      }
   }
}