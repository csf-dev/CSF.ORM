using System;
using CSF.PersistenceTester.Tests.NHibernate;
using NUnit.Framework;
using CSF.EqualityRules;
using System.Linq;
using CSF.ORM;
using NHibernate;
using CSF.PersistenceTester.Tests.Autofixture;

namespace CSF.PersistenceTester.Tests
{
    [TestFixture,NonParallelizable]
    public class PersistenceTesterIntegrationTests
    {
        [Test, AutoMoqData]
        public void Persistence_test_passes_for_a_correctly_mapped_entity([NoRecursion] SampleEntity entity,
                                                                          ConnectionFactoryProvider factoryProvider,
                                                                          SchemaCreator schemaCreator)
        {
            var factory = factoryProvider.GetConnectionFactory();
            var result = TestPersistence.UsingConnectionProvider(factory)
                .WithSetup(s => CreateSchema(s, schemaCreator))
                .WithEntity(entity)
                .WithEqualityRule((EqualityBuilder<SampleEntity> r) =>
                {
                    return r
                        .ForProperty(x => x.RelatedEntity,
                                     c => c.UsingComparer(new EqualityBuilder<EntityWithRelationship>()
                                                              .ForProperty(x => x.Identity)
                                                              .ForAllOtherProperties(p => p.Ignore())
                                                              .Build()))
                        .ForAllOtherProperties();
                });

            Assert.That(() =>
            {
                Assert.That(result, Persisted.Successfully());
            }, Throws.Nothing);
        }

        [Test, AutoMoqData]
        public void Persistence_test_fails_when_the_setup_throws_an_exception([NoRecursion] SampleEntity entity,
                                                                              ConnectionFactoryProvider factoryProvider,
                                                                              SchemaCreator schemaCreator)
        {
            var factory = factoryProvider.GetConnectionFactory();
            var result = TestPersistence.UsingConnectionProvider(factory)
                .WithSetup(s =>
                {
                    CreateSchema(s, schemaCreator);
                    throw new InvalidOperationException("Sample exception");
                })
                .WithEntity(entity)
                .WithEqualityRule(r => r.ForAllOtherProperties());

            Assert.That(() =>
            {
                Assert.That(result, Persisted.Successfully());
            }, Throws.InstanceOf<AssertionException>());
            Assert.That(result?.SetupException, Is.Not.Null);
        }

        [Test, AutoMoqData]
        public void Persistence_test_fails_for_an_incorrectly_mapped_entity(EntityWithUnmappedProperty entity,
                                                                            ConnectionFactoryProvider factoryProvider,
                                                                            SchemaCreator schemaCreator)
        {
            var factory = factoryProvider.GetConnectionFactory();
            var result = TestPersistence.UsingConnectionProvider(factory)
                .WithSetup(s => CreateSchema(s, schemaCreator))
                .WithEntity(entity)
                .WithEqualityRule(r => r.ForAllOtherProperties());


            Assert.That(() =>
            {
                Assert.That(result, Persisted.Successfully());
            }, Throws.InstanceOf<AssertionException>());
            Assert.That(result?.EqualityResult?.RuleResults?.Where(x => !x.Passed).Count(), Is.EqualTo(1));
        }

        [Test, AutoMoqData]
        public void Persistence_test_fails_for_an_entity_which_cannot_be_saved(EntityWithBadlyNamedProperty entity,
                                                                               ConnectionFactoryProvider factoryProvider,
                                                                               SchemaCreator schemaCreator)
        {
            var factory = factoryProvider.GetConnectionFactory();
            var result = TestPersistence.UsingConnectionProvider(factory)
                .WithSetup(s => CreateSchema(s, schemaCreator))
                .WithEntity(entity)
                .WithEqualityRule(r => r.ForAllOtherProperties());

            Assert.That(() =>
            {
                Assert.That(result, Persisted.Successfully());
            }, Throws.InstanceOf<AssertionException>());
            Assert.That(result?.SaveException, Is.Not.Null);
        }

        void CreateSchema(IDataConnection c, SchemaCreator schemaCreator)
        {
            if (!(c is IHasNativeImplementation nativeSession))
                throw new ArgumentException($"{nameof(IDataConnection)} must provide an NHibernate {nameof(ISession)}.");

            var session = (ISession) nativeSession.NativeImplementation;
            schemaCreator.CreateSchema(session.Connection);
        }
    }
}
