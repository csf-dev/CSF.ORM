//
// IdentityParserTests.cs
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

using CSF.Entities.Stubs;
using CSF.Entities;
using NUnit.Framework;

namespace CSF.ORM.Entities
{
    [TestFixture,Parallelizable]
    public class IdentityParserTests
    {
        [Test, AutoMoqData]
        public void Parse_gets_identity_when_value_can_be_converted(IdentityParser sut)
        {
            Assert.That(sut.Parse(typeof(Cat), "55"), Is.EqualTo(new Identity<int, Cat>(55)));
        }

        [Test, AutoMoqData]
        public void Parse_returns_null_when_value_cannot_be_converted(IdentityParser sut)
        {
            Assert.That(sut.Parse(typeof(Cat), "Nope"), Is.Null);
        }
    }
}
