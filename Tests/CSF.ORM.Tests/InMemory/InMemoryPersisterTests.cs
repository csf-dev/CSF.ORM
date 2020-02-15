//
// InMemoryPersisterTests.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2020 
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
using CSF.ORM.InMemory;
using CSF.ORM.Tests.Stubs;
using NUnit.Framework;

namespace CSF.ORM.Tests.InMemory
{
    [TestFixture,Parallelizable]
    public class InMemoryPersisterTests
    {
        [Test, AutoMoqData]
        public void Add_adds_item_to_store([Frozen] InMemoryDataStore store,
                                           InMemoryPersister sut,
                                           Person item,
                                           long identity)
        {
            sut.Add(item, identity);
            Assert.That(store.Items, Has.One.Matches<InMemoryDataItem>(x => x.ValueType == typeof(Person)
                                                                            && Equals(x.Identity, identity)
                                                                            && ReferenceEquals(x.Value, item)));
        }

        [Test, AutoMoqData]
        public void Add_adds_item_to_store_using_actual_type_not_generic_type([Frozen] InMemoryDataStore store,
                                                                              InMemoryPersister sut,
                                                                              Employee item,
                                                                              long identity)
        {
            sut.Add((Person) item, identity);
            Assert.That(store.Items, Has.One.Matches<InMemoryDataItem>(x => x.ValueType == typeof(Employee)
                                                                            && Equals(x.Identity, identity)
                                                                            && ReferenceEquals(x.Value, item)));
        }

        [Test, AutoMoqData]
        public void Addding_the_same_item_twice_does_not_create_a_duplicate([Frozen] InMemoryDataStore store,
                                                                            InMemoryPersister sut,
                                                                            Person item,
                                                                            long identity)
        {
            sut.Add(item, identity);
            sut.Add(item, identity);
            Assert.That(store.Items, Has.Count.EqualTo(1));
        }

        [Test, AutoMoqData]
        public void BulkAdd_may_add_more_than_one_item([Frozen] InMemoryDataStore store,
                                                       InMemoryPersister sut,
                                                       Person item1,
                                                       Person item2)
        {

        }
    }
}
