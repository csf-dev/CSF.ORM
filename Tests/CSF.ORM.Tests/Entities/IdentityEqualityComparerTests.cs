//
// IdentityEqualityComparerTests.cs
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

using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using CSF.Entities.Stubs;
using CSF.Entities;

namespace CSF.ORM.Entities
{
    [TestFixture,Parallelizable]
    public class IdentityEqualityComparerTests
    {
        [Test, AutoMoqData]
        public void Equals_returns_true_if_both_identities_are_reference_equal(IdentityEqualityComparer sut, IIdentity identity)
        {
            Assert.That(sut.Equals(identity, identity), Is.True);
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_if_left_identity_is_null(IdentityEqualityComparer sut, IIdentity identity)
        {
            Assert.That(sut.Equals(null, identity), Is.False);
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_if_right_identity_is_null(IdentityEqualityComparer sut, IIdentity identity)
        {
            Assert.That(sut.Equals(identity, null), Is.False);
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_if_entity_types_differ([Frozen] IGetsEntityTypeForIdentityEquality equalityTypeProvider,
                                                                IdentityEqualityComparer sut,
                                                                IIdentity identity1,
                                                                IIdentity identity2)
        {
            Mock.Get(identity1).SetupGet(x => x.Value).Returns(1);
            Mock.Get(identity1).SetupGet(x => x.EntityType).Returns(typeof(Cat));
            Mock.Get(equalityTypeProvider).Setup(x => x.GetIdentityEqualityType(typeof(Cat))).Returns(typeof(Animal));

            Mock.Get(identity2).SetupGet(x => x.Value).Returns(1);
            Mock.Get(identity2).SetupGet(x => x.EntityType).Returns(typeof(Dog));
            Mock.Get(equalityTypeProvider).Setup(x => x.GetIdentityEqualityType(typeof(Dog))).Returns(typeof(Person));

            Assert.That(sut.Equals(identity1, identity2), Is.False);
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_if_values_differ([Frozen] IGetsEntityTypeForIdentityEquality equalityTypeProvider,
                                                                IdentityEqualityComparer sut,
                                                                IIdentity identity1,
                                                                IIdentity identity2)
        {
            Mock.Get(identity1).SetupGet(x => x.Value).Returns(1);
            Mock.Get(identity1).SetupGet(x => x.EntityType).Returns(typeof(Cat));
            Mock.Get(equalityTypeProvider).Setup(x => x.GetIdentityEqualityType(typeof(Cat))).Returns(typeof(Animal));

            Mock.Get(identity2).SetupGet(x => x.Value).Returns(5);
            Mock.Get(identity2).SetupGet(x => x.EntityType).Returns(typeof(Dog));
            Mock.Get(equalityTypeProvider).Setup(x => x.GetIdentityEqualityType(typeof(Dog))).Returns(typeof(Animal));

            Assert.That(sut.Equals(identity1, identity2), Is.False);
        }

        [Test, AutoMoqData]
        public void Equals_returns_true_if_entity_types_and_values_are_equal([Frozen] IGetsEntityTypeForIdentityEquality equalityTypeProvider,
                                                                             IdentityEqualityComparer sut,
                                                                             IIdentity identity1,
                                                                             IIdentity identity2)
        {
            Mock.Get(identity1).SetupGet(x => x.Value).Returns(1);
            Mock.Get(identity1).SetupGet(x => x.EntityType).Returns(typeof(Cat));
            Mock.Get(equalityTypeProvider).Setup(x => x.GetIdentityEqualityType(typeof(Cat))).Returns(typeof(Animal));

            Mock.Get(identity2).SetupGet(x => x.Value).Returns(1);
            Mock.Get(identity2).SetupGet(x => x.EntityType).Returns(typeof(Dog));
            Mock.Get(equalityTypeProvider).Setup(x => x.GetIdentityEqualityType(typeof(Dog))).Returns(typeof(Animal));

            Assert.That(sut.Equals(identity1, identity2), Is.True);
        }

        [Test, AutoMoqData]
        public void GetHashCode_gets_hash_code_equal_to_hash_of_entity_type_and_identity_value([Frozen] IGetsEntityTypeForIdentityEquality equalityTypeProvider,
                                                                                               IdentityEqualityComparer sut,
                                                                                               IIdentity identity)
        {
            Mock.Get(identity).SetupGet(x => x.Value).Returns(55);
            Mock.Get(identity).SetupGet(x => x.EntityType).Returns(typeof(Cat));
            Mock.Get(equalityTypeProvider).Setup(x => x.GetIdentityEqualityType(typeof(Cat))).Returns(typeof(Animal));

            Assert.That(sut.GetHashCode(identity), Is.EqualTo(55.GetHashCode() ^ typeof(Animal).GetHashCode()));
        }
    }
}
