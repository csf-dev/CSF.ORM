//
// EntityIdentityEqualityComparerTests.cs
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

using CSF.Entities.Tests.Stubs;
using NUnit.Framework;

namespace CSF.Entities.Tests
{
    [TestFixture,Parallelizable]
    public class EntityIdentityEqualityComparerTests
    {
        [Test, AutoMoqData]
        public void Equals_returns_true_for_reference_equal_objects(EntityIdentityEqualityComparer sut, Cat entity)
        {
            Assert.That(sut.Equals(entity, entity), Is.True);
        }

        [Test, AutoMoqData]
        public void Equals_returns_true_if_identities_are_equal(EntityIdentityEqualityComparer sut, Cat entity1, Cat entity2)
        {
            entity1.Identity = 5;
            entity2.Identity = 5;
            Assert.That(sut.Equals(entity1, entity2), Is.True);
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_if_identities_are_not_equal(EntityIdentityEqualityComparer sut, Cat entity1, Cat entity2)
        {
            entity1.Identity = 5;
            entity2.Identity = 10;
            Assert.That(sut.Equals(entity1, entity2), Is.False);
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_if_types_are_not_compatible(EntityIdentityEqualityComparer sut, Cat entity1, Person entity2)
        {
            entity1.Identity = 5;
            entity2.Identity = 5;
            Assert.That(sut.Equals(entity1, entity2), Is.False);
        }

        [Test, AutoMoqData]
        public void Equals_returns_true_if_types_are_compatible(EntityIdentityEqualityComparer sut, Cat entity1, Animal entity2)
        {
            entity1.Identity = 5;
            entity2.Identity = 5;
            Assert.That(sut.Equals(entity1, entity2), Is.True);
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_if_first_entity_is_null(EntityIdentityEqualityComparer sut, Cat entity)
        {
            Assert.That(sut.Equals(null, entity), Is.False);
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_if_second_entity_is_null(EntityIdentityEqualityComparer sut, Cat entity)
        {
            Assert.That(sut.Equals(entity, null), Is.False);
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_if_first_entity_has_no_identity(EntityIdentityEqualityComparer sut, Cat entity1, Animal entity2)
        {
            entity1.Identity = 0;
            entity2.Identity = 5;
            Assert.That(sut.Equals(entity1, entity2), Is.False);
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_if_second_entity_has_no_identity(EntityIdentityEqualityComparer sut, Cat entity1, Animal entity2)
        {
            entity1.Identity = 5;
            entity2.Identity = 0;
            Assert.That(sut.Equals(entity1, entity2), Is.False);
        }

        [Test, AutoMoqData]
        public void GetHashCode_returns_identity_hash_code(EntityIdentityEqualityComparer sut, Cat entity)
        {
            entity.Identity = 22;
            var identity = entity.GetIdentity();
            Assert.That(sut.GetHashCode(entity), Is.EqualTo(identity.GetHashCode()));
        }
    }
}
