//
// IdentityTypeProviderTests.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2020 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using CSF.Entities.Tests.Stubs;
using NUnit.Framework;

namespace CSF.Entities.Tests
{
    [TestFixture,Parallelizable]
    public class IdentityTypeProviderTests
    {
        [Test, AutoMoqData]
        public void GetIdentityType_returns_correct_type_for_entity_that_uses_generic_base_type(IdentityTypeProvider sut)
        {
            Assert.That(sut.GetIdentityType(typeof(Animal)), Is.EqualTo(typeof(int)));
        }

        [Test, AutoMoqData]
        public void GetIdentityType_returns_correct_type_for_entity_that_uses_generic_base_type_at_grandparent_level(IdentityTypeProvider sut)
        {
            Assert.That(sut.GetIdentityType(typeof(Cat)), Is.EqualTo(typeof(int)));
        }

        [Test, AutoMoqData]
        public void GetIdentityType_returns_correct_type_for_entity_that_does_not_use_generic_base_type_but_has_parameterless_ctor(IdentityTypeProvider sut)
        {
            Assert.That(sut.GetIdentityType(typeof(NonGenericEntityType)), Is.EqualTo(typeof(string)));
        }

        [Test, AutoMoqData]
        public void GetIdentityType_returns_correct_type_for_entity_that_does_not_use_generic_base_type_has_no_parameterless_ctor_but_has_IdentityType_attribute(IdentityTypeProvider sut)
        {
            Assert.That(sut.GetIdentityType(typeof(NonGenericEntityTypeWithNoParameterlessConstructor)), Is.EqualTo(typeof(long)));
        }

        [Test, AutoMoqData]
        public void GetIdentityType_returns_correct_null_for_entity_that_does_not_use_generic_base_type_has_no_parameterless_ctor_and_no_IdentityType_attribute(IdentityTypeProvider sut)
        {
            Assert.That(sut.GetIdentityType(typeof(NonGenericEntityTypeWithNoParameterlessConstructorAndNoAttribute)), Is.Null);
        }
    }
}
