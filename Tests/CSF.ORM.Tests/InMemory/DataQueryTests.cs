//
// InMemoryQueryTests.cs
//
// Author:
//       Craig Fowler <craig@craigfowler.me.uk>
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

using NUnit.Framework;
using CSF.ORM.Tests.Stubs;
using CSF.ORM.InMemory;
using System.Linq;
using AutoFixture.NUnit3;

namespace CSF.ORM.Tests.InMemory
{
    [TestFixture, Parallelizable]
    public class DataQueryTests
    {
        [Test, AutoMoqData]
        public void Query_gets_queryable_of_items_of_correct_type([Frozen] DataStore store,
                                                                  DataQuery sut,
                                                                  Person item1,
                                                                  Person item2)
        {
            AddToStore(store, item1);
            AddToStore(store, item2);

            Assert.That(sut.Query<Person>().ToList(), Is.EquivalentTo(new[] { item1, item2 }));
        }

        [Test, AutoMoqData]
        public void Query_does_not_include_items_of_other_types([Frozen] DataStore store,
                                                                DataQuery sut,                                                           Person item1,
                                                                Person item2,
                                                                Animal animal1,
                                                                Animal animal2)
        {
            AddToStore(store, item1);
            AddToStore(store, item2);
            AddToStore(store, animal1);
            AddToStore(store, animal2);

            Assert.That(sut.Query<Person>().ToList(), Is.EquivalentTo(new[] { item1, item2 }));
        }

        [Test, AutoMoqData]
        public void Get_retrieves_correct_item([Frozen] DataStore store,
                                                                DataQuery sut,
             Person item1,
                                               Person item2)
        {
            AddToStore(store, item1);
            AddToStore(store, item2);

            Assert.That(sut.Get<Person>(item1.Identity), Is.SameAs(item1));
        }

        void AddToStore(DataStore store, Person person)
        {
            var id = person.Identity;
            store.Items.Add(new DataItem(typeof(Person), id, person));
        }

        void AddToStore(DataStore store, Animal animal)
        {
            var id = animal.Identity;
            store.Items.Add(new DataItem(typeof(Animal), id, animal));
        }
    }
}

