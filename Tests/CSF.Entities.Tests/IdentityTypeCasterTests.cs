//
// IdentityCasterTests.cs
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
    public class IdentityTypeCasterTests
    {
        [Test, AutoMoqData]
        public void CastIdentity_returns_same_identity_when_types_are_same(IdentityTypeCaster sut)
        {
            IIdentity id = new Identity<long, Cat>(5);
            Assert.That(sut.CastIdentity<Cat>(id), Is.SameAs(id));
        }

        [Test, AutoMoqData]
        public void CastIdentity_returns_same_identity_when_downcast_would_be_valid(IdentityTypeCaster sut)
        {
            IIdentity id = new Identity<long, Cat>(5);
            Assert.That(sut.CastIdentity<Animal>(id), Is.SameAs(id));
        }

        [Test, AutoMoqData]
        public void CastIdentity_returns_new_identity_when_types_are_compatible_for_upcast(IdentityTypeCaster sut)
        {
            IIdentity id = new Identity<long, Animal>(5);
            var newId = sut.CastIdentity<Cat>(id);
            Assert.That(newId, Is.InstanceOf<Identity<long,Cat>>(), "Correct type");
            Assert.That(newId, Is.Not.SameAs(id), "Is a different object");
            Assert.That(newId.EntityType, Is.EqualTo(typeof(Cat)), "Correct entity type");
            Assert.That(newId.Value, Is.EqualTo(5), "Correct identity value");
        }

        [Test, AutoMoqData]
        public void CastIdentity_throws_exception_when_when_types_are_not_compatible_for_upcast(IdentityTypeCaster sut)
        {
            IIdentity id = new Identity<long, Person>(5);
            Assert.That(() => sut.CastIdentity<Cat>(id), Throws.InstanceOf<InvalidCastException>());
        }
    }
}
