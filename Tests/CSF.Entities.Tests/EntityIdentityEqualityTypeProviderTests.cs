//
// EntityIdentityEqualityTypeProviderTests.cs
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

using NUnit.Framework;
using CSF.Entities.Tests.Stubs;

namespace CSF.Entities.Tests
{
    [TestFixture,Parallelizable]
    public class EntityIdentityEqualityTypeProviderTests
    {
        [Test, AutoMoqData]
        public void GetIdentityEqualityType_should_return_Animal_for_a_cat_instance(EntityIdentityEqualityTypeProvider sut, Cat entity)
        {
            Assert.That(sut.GetIdentityEqualityType(entity), Is.EqualTo(typeof(Animal)));
        }

        [Test, AutoMoqData]
        public void GetIdentityEqualityType_should_return_Animal_for_a_dog_instance(EntityIdentityEqualityTypeProvider sut, Dog entity)
        {
            Assert.That(sut.GetIdentityEqualityType(entity), Is.EqualTo(typeof(Animal)));
        }

        [Test, AutoMoqData]
        public void GetIdentityEqualityType_should_return_Animal_for_an_animal_instance(EntityIdentityEqualityTypeProvider sut, Animal entity)
        {
            Assert.That(sut.GetIdentityEqualityType(entity), Is.EqualTo(typeof(Animal)));
        }

        [Test, AutoMoqData]
        public void GetIdentityEqualityType_should_return_Animal_for_the_cat_type(EntityIdentityEqualityTypeProvider sut)
        {
            Assert.That(sut.GetIdentityEqualityType(typeof(Cat)), Is.EqualTo(typeof(Animal)));
        }

        [Test, AutoMoqData]
        public void GetIdentityEqualityType_should_return_Animal_for_the_dog_type(EntityIdentityEqualityTypeProvider sut)
        {
            Assert.That(sut.GetIdentityEqualityType(typeof(Dog)), Is.EqualTo(typeof(Animal)));
        }

        [Test, AutoMoqData]
        public void GetIdentityEqualityType_should_return_Animal_for_the_animal_type(EntityIdentityEqualityTypeProvider sut)
        {
            Assert.That(sut.GetIdentityEqualityType(typeof(Animal)), Is.EqualTo(typeof(Animal)));
        }
    }
}
