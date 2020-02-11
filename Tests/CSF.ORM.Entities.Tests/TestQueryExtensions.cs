//
// TestQueryExtensions.cs
//
// Author:
//       Craig Fowler <craig@craigfowler.me.uk>
//
// Copyright (c) 2017 Craig Fowler
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
using Moq;
using CSF.Entities;
using CSF.ORM.Entities.Tests.Stubs;

namespace CSF.ORM.Entities.Tests
{
  [TestFixture, Parallelizable(ParallelScope.Children | ParallelScope.Self)]
  public class TestQueryExtensions
  {
    [Test, AutoMoqData]
    public void Theorise_does_not_usequery_for_null_identity(IQuery query)
    {
      // Arrange
      IIdentity<Person> identity = null;

      // Act
      query.Theorise(identity);

      // Assert
      Mock.Get(query).Verify(x => x.Theorise<Person>(It.IsAny<object>()), Times.Never());
    }

    [Test, AutoMoqData]
    public void Theorise_usesquery_when_identity_is_not_null(IQuery query)
    {
      // Arrange
      IIdentity<Person> identity = Identity.Create<Person>(5);

      // Act
      query.Theorise(identity);

      // Assert
      Mock.Get(query).Verify(x => x.Theorise<Person>(It.IsAny<object>()), Times.Once());
    }

    [Test, AutoMoqData]
    public void Theorise_returns_null_for_null_identity(IQuery query)
    {
      // Arrange
      IIdentity<Person> identity = null;

      // Act
      var result = query.Theorise(identity);

      // Assert
      Assert.IsNull(result);
    }

    [Test, AutoMoqData]
    public void Get_does_not_usequery_for_null_identity(IQuery query)
    {
      // Arrange
      IIdentity<Person> identity = null;

      // Act
      query.Get(identity);

      // Assert
      Mock.Get(query).Verify(x => x.Get<Person>(It.IsAny<object>()), Times.Never());
    }

    [Test, AutoMoqData]
    public void Get_usesquery_when_identity_is_not_null(IQuery query)
    {
      // Arrange
      IIdentity<Person> identity = Identity.Create<Person>(5);

      // Act
      query.Get(identity);

      // Assert
      Mock.Get(query).Verify(x => x.Get<Person>(It.IsAny<object>()), Times.Once());
    }

    [Test, AutoMoqData]
    public void Get_returns_null_for_null_identity(IQuery query)
    {
      // Arrange
      IIdentity<Person> identity = null;

      // Act
      var result = query.Get(identity);

      // Assert
      Assert.IsNull(result);
    }
  }
}

