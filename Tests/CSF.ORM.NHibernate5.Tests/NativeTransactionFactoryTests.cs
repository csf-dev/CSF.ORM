//
// NativeTransactionFactoryTests.cs
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
using AutoFixture.NUnit3;
using CSF.ORM.NHibernate;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace CSF.ORM.NHibernate5.Tests
{
    [TestFixture,Parallelizable]
    public class NativeTransactionFactoryTests
    {
        [Test, AutoMoqData]
        public void GetTransaction_creates_transaction_from_session(ISession session, NativeTransactionFactory sut, global::NHibernate.ITransaction expected)
        {
            Mock.Get(session).Setup(x => x.BeginTransaction()).Returns(expected);
            Assert.That(() => sut.GetTransaction(session), Is.SameAs(expected));
        }
    }
}
