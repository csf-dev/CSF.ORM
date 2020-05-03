using NUnit.Framework;
using System.Linq;
using CSF.ORM.InMemory;
using CSF.ORM.Stubs;

namespace CSF.ORM
{
    [TestFixture, Parallelizable(ParallelScope.Children)]
    public class QueryableExtensionsTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            QueryableExtensions.EagerFetchingProvider = new NoOpEagerFetcher();
            QueryableExtensions.LazyQueryingProvider = new NoOpLazyResultProvider();
            QueryableExtensions.AsyncQueryingProvider = new SynchronousAsyncQueryProvider();
        }

        [Test,AutoMoqData]
        public void FetchChildren_does_not_raise_exception(DataQuery queryProvider)
        {
            var query = (from store in queryProvider.Query<Person>()
                         select store)
              .FetchChildren(x => x.Pets);

            Assert.That(() => query.ToList(), Throws.Nothing);
        }

        [Test, AutoMoqData]
        public void FetchChildren_returns_non_null_result(DataQuery queryProvider)
        {
            var query = (from store in queryProvider.Query<Person>()
                         select store)
              .FetchChildren(x => x.Pets);

            Assert.That(() => query.ToList(), Is.Not.Null);
        }

        [Test, AutoMoqData]
        public void FetchChild_does_not_raise_exception(DataQuery queryProvider)
        {
            var query = (from store in queryProvider.Query<Animal>()
                         select store)
              .FetchChild(x => x.Owner);

            Assert.That(() => query.ToList(), Is.Not.Null);
        }

        [Test, AutoMoqData]
        public void FetchChild_returns_non_null_result(DataQuery queryProvider)
        {
            var query = (from store in queryProvider.Query<Animal>()
                         select store)
              .FetchChild(x => x.Owner);

            Assert.That(() => query.ToList(), Throws.Nothing);
        }

        [Test, AutoMoqData]
        public void ToLazy_does_not_raise_exception(DataQuery queryProvider)
        {
            var query1 = (from store in queryProvider.Query<Animal>()
                         select store)
              .ToLazy();
            var query2 = (from store in queryProvider.Query<Person>()
                         select store)
              .ToLazy();

            Assert.That(() => query2.Value, Throws.Nothing, "Query 2");
            Assert.That(() => query1.Value, Throws.Nothing, "Query 1");
        }

        [Test, AutoMoqData]
        public void ToLazy_returns_non_null_result(DataQuery queryProvider)
        {
            var query1 = (from store in queryProvider.Query<Animal>()
                          select store)
              .ToLazy();
            var query2 = (from store in queryProvider.Query<Person>()
                          select store)
              .ToLazy();

            Assert.That(() => query2.Value, Is.Not.Null, "Query 2");
            Assert.That(() => query1.Value, Is.Not.Null, "Query 1");
        }

        [Test, AutoMoqData]
        public void ToLazyValue_does_not_raise_exception(DataQuery queryProvider)
        {
            var query1 = (from store in queryProvider.Query<Animal>()
                          select store)
              .ToLazyValue(x => x.Count());
            var query2 = (from store in queryProvider.Query<Person>()
                          select store)
              .ToLazyValue(x => x.Any());

            Assert.That(() => query2.Value, Throws.Nothing, "Query 2");
            Assert.That(() => query1.Value, Throws.Nothing, "Query 1");
        }

        [Test, AutoMoqData]
        public void ToLazyValue_returns_non_null_result(DataQuery queryProvider)
        {
            var query1 = (from store in queryProvider.Query<Animal>()
                          select store)
              .ToLazyValue(x => x.Count());
            var query2 = (from store in queryProvider.Query<Person>()
                          select store)
              .ToLazyValue(x => x.Any());

            Assert.That(() => query2.Value, Is.Not.Null, "Query 2");
            Assert.That(() => query1.Value, Is.Not.Null, "Query 1");
        }
    }
}

