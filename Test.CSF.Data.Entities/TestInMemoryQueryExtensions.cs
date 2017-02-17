//
// Test.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
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
using System;
using CSF.Data;
using CSF.Entities;
using CSF.Data.Entities;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Test.CSF
{
  public class TestInMemoryQueryExtensions
  {
    #region fields

    InMemoryQuery sut;

    #endregion

    #region setup

    [SetUp]
    public void Setup ()
    {
      sut = new Mock<InMemoryQuery>() { CallBase = true }.Object;
    }

    #endregion

    #region tests

    [Test]
    public void Add_adds_an_entity_with_its_identity ()
    {
      // Arrange
      object identityValue = 5;
      var entity = Mock.Of<IEntity>(x => x.IdentityValue == identityValue);

      // Act
      sut.Add(entity);

      // Assert
      Mock.Get(sut).Verify(x => x.Add(entity, identityValue), Times.Once());
    }

    [Test]
    public void Add_adds_collection_of_entities_with_their_identities ()
    {
      // Arrange
      object identityValueOne = 6, identityValueTwo = 7;
      var entityOne = Mock.Of<IEntity>(x => x.IdentityValue == identityValueOne);
      var entityTwo = Mock.Of<IEntity>(x => x.IdentityValue == identityValueTwo);
      IEnumerable<IEntity> collection = new [] { entityOne, entityTwo };

      // Act
      sut.Add(collection);

      // Assert
      Mock.Get(sut).Verify(x => x.Add(collection, It.IsAny<Func<IEntity,object>>()), Times.Once());
    }

    #endregion
  }
}
