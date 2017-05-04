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
using Test.CSF.Stubs;

namespace Test.CSF
{
  public class TestInMemoryQueryExtensions
  {
    [Test,AutoMoqData]
    public void Add_adds_an_entity_with_its_identity([AlwaysMock] InMemoryQuery query,
                                                     Person entity)
    {
      // Arrange
      var identityValue = entity.Identity;

      // Act
      query.Add(entity);

      // Assert
      Mock.Get(query).Verify(x => x.Add(entity, identityValue), Times.Once());
    }

    [Test,AutoMoqData]
    public void Add_adds_collection_of_entities_with_their_identities([AlwaysMock] InMemoryQuery query,
                                                                      Person entityOne,
                                                                      Person entityTwo)
    {
      // Arrange
      IEnumerable<IEntity> collection = new [] { entityOne, entityTwo };

      // Act
      query.Add(collection);

      // Assert
      Mock.Get(query).Verify(x => x.Add(collection, It.IsAny<Func<IEntity,object>>()), Times.Once());
    }

    [Test,AutoMoqData]
    public void Delete_deletes_an_entity_by_its_identity([AlwaysMock] InMemoryQuery query,
                                                         IIdentity<Person> identity,
                                                         object identityValue)
    {
      // Arrange
      Mock.Get(identity).SetupGet(x => x.Value).Returns(identityValue);

      // Act
      query.DeleteEntity(identity);

      // Assert
      Mock.Get(query).Verify(x => x.Delete<Person>(identityValue), Times.Once());
    }
  }
}
