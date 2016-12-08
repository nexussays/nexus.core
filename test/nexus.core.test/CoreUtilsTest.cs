// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

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
         Assert.Throws<DivideByZeroException>( () => CoreUtils.ThrowDivideByZeroException() );
      }
   }
}