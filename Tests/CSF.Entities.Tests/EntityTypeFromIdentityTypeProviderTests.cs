//
// EntityTypeFromIdentityTypeProviderTests.cs
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
    public class EntityTypeFromIdentityTypeProviderTests
    {
        [Test, AutoMoqData]
        public void GetEntityType_returns_correct_entity_type_for_generic_identity_interface(EntityTypeFromIdentityTypeProvider sut)
        {
            Assert.That(sut.GetEntityType(typeof(IIdentity<Cat>)), Is.EqualTo(typeof(Cat)));
        }

        [Test, AutoMoqData]
        public void GetEntityType_returns_correct_entity_type_for_generic_identity_class(EntityTypeFromIdentityTypeProvider sut)
        {
            Assert.That(sut.GetEntityType(typeof(Identity<long,Cat>)), Is.EqualTo(typeof(Cat)));
        }

        [Test, AutoMoqData]
        public void GetEntityType_returns_null_for_nongeneric_identity_interface(EntityTypeFromIdentityTypeProvider sut)
        {
            Assert.That(sut.GetEntityType(typeof(IIdentity)), Is.Null);
        }
    }
}
