//
// ConnectionFactoryTests.cs
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
using CSF.ORM.InMemory;
using NUnit.Framework;

namespace CSF.ORM.InMemory
{
    [TestFixture,Parallelizable]
    public class ConnectionFactoryTests
    {
        [Test, AutoMoqData]
        public void Constructor_with_shared_data_created_connections_with_same_store()
        {
            var sut = new ConnectionFactory(true);
            var conn1 = (IHasNativeImplementation) sut.GetConnection();
            var conn2 = (IHasNativeImplementation) sut.GetConnection();

            Assert.That(conn1.NativeImplementation, Is.SameAs(conn2.NativeImplementation));
        }

        [Test, AutoMoqData]
        public void Constructor_without_shared_data_created_connections_with_different_store()
        {
            var sut = new ConnectionFactory(false);
            var conn1 = (IHasNativeImplementation) sut.GetConnection();
            var conn2 = (IHasNativeImplementation) sut.GetConnection();

            Assert.That(conn1.NativeImplementation, Is.Not.SameAs(conn2.NativeImplementation));
        }
    }
}
