//
// IdentityFactoryTests.cs
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
    [TestFixture, Parallelizable]
    public class IdentityFactoryTests
    {
        [Test, AutoMoqData]
        public void Create_creates_identity_with_correct_entity_type(IdentityFactory sut)
        {
            Assert.That(() => sut.Create(typeof(Cat), typeof(int), 5)?.EntityType, Is.EqualTo(typeof(Cat)));
        }

        [Test, AutoMoqData]
        public void Create_creates_identity_with_correct_value_type(IdentityFactory sut)
        {
            Assert.That(() => sut.Create(typeof(Cat), typeof(int), 5)?.IdentityType, Is.EqualTo(typeof(int)));
        }

        [Test, AutoMoqData]
        public void Create_creates_identity_with_correct_value(IdentityFactory sut)
        {
            Assert.That(() => sut.Create(typeof(Cat), typeof(int), 5)?.Value, Is.EqualTo(5));
        }

        [Test, AutoMoqData]
        public void Create_returns_null_when_value_is_default_of_data_type(IdentityFactory sut)
        {
            Assert.That(() => sut.Create(typeof(Cat), typeof(int), 0), Is.Null);
        }
    }
}
