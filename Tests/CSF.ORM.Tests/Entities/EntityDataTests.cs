//
// EntityDataTests.cs
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
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using CSF.Entities;
using CSF.ORM.Stubs;
using Moq;
using NUnit.Framework;

namespace CSF.ORM.Entities
{
    [TestFixture, Parallelizable]
    public class EntityDataTests
    {
        [Test, AutoMoqData]
        public void Add_adds_entity_using_identity_if_not_null([Frozen] IPersister persister, EntityData sut, Car entity)
        {
            Mock.Get(persister)
                .Setup(x => x.Add(entity, It.IsAny<object>()))
                .Returns((Car e, object obj) => obj);

            entity.Identity = 5;
            sut.Add(entity);

            Mock.Get(persister).Verify(x => x.Add(entity, 5L), Times.Once);
        }

        [Test, AutoMoqData]
        public void Add_adds_entity_without_identity_if_it_is_null([Frozen] IPersister persister, EntityData sut, Car entity)
        {
            Mock.Get(persister)
                .Setup(x => x.Add(entity, It.IsAny<object>()))
                .Returns((Car e, object obj) => obj);

            entity.Identity = 0;
            sut.Add(entity);

            Mock.Get(persister).Verify(x => x.Add(entity, null), Times.Once);
        }

        [Test, AutoMoqData]
        public void Add_returns_result_from_persister_add([Frozen] IPersister persister, EntityData sut, Car entity)
        {
            Mock.Get(persister).Setup(x => x.Add(entity, null)).Returns(5);
            Mock.Get(persister).Setup(x => x.Add(entity, It.IsAny<object>())).Returns(5);

            var result = sut.Add(entity);

            Assert.That(result, Is.EqualTo(new IdentityFactory().Create(typeof(Car), 5L)));
        }

        [Test, AutoMoqData]
        public async Task AddAsync_adds_entity_using_identity_if_not_null([Frozen] IPersister persister, EntityData sut, Car entity)
        {
            Mock.Get(persister)
                .Setup(x => x.AddAsync(entity, It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .Returns((Car e, object obj, CancellationToken t) => Task.FromResult(obj));

            entity.Identity = 5;
            await sut.AddAsync(entity);

            Mock.Get(persister).Verify(x => x.AddAsync(entity, 5L, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test, AutoMoqData]
        public async Task AddAsync_adds_entity_without_identity_if_it_is_null([Frozen] IPersister persister, EntityData sut, Car entity)
        {
            Mock.Get(persister)
                .Setup(x => x.AddAsync(entity, It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .Returns((Car e, object obj, CancellationToken t) => Task.FromResult(obj));

            entity.Identity = 0;
            await sut.AddAsync(entity);

            Mock.Get(persister).Verify(x => x.AddAsync(entity, null, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test, AutoMoqData]
        public async Task AddAsync_returns_result_from_persister_add([Frozen] IPersister persister, EntityData sut, Car entity)
        {
            Mock.Get(persister).Setup(x => x.AddAsync(entity, null, It.IsAny<CancellationToken>())).Returns(Task.FromResult((object) 5L));
            Mock.Get(persister).Setup(x => x.AddAsync(entity, It.IsAny<object>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((object) 5L));

            var result = await sut.AddAsync(entity);

            Assert.That(result, Is.EqualTo(new IdentityFactory().Create(typeof(Car), 5L)));
        }
    }
}
