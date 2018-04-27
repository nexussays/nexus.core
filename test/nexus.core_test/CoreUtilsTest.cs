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
   internal class CoreUtilsTest
   {
      [Test]
      public void divide_by_0_method_throws_divide_by_0_exception()
      {
         Assert.Throws<DivideByZeroException>( () => CoreUtils.ThrowDivideByZeroException() );
      }

      [Test]
      public void throw_exception_method_throws_exception()
      {
         Assert.Throws<Exception>( CoreUtils.ThrowException );
      }
   }
}
