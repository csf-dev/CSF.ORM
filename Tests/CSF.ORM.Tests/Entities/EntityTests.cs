using NUnit.Framework;
using CSF.Entities.Stubs;
using CSF.Entities;

namespace CSF.ORM.Entities
{
    [TestFixture,Parallelizable]
    public class EntityTests
    {
        [Test, AutoMoqData]
        public void ToString_returns_string_in_correct_format_for_entity_which_has_identity()
        {
            var entity = new Person { Identity = 10 };
            Assert.That(() => entity.ToString(), Is.EqualTo("[Person#10]"));
        }

        [Test, AutoMoqData]
        public void ToString_returns_string_in_correct_format_for_entity_which_has_no_identity()
        {
            var entity = new Person();
            Assert.That(() => entity.ToString(), Is.EqualTo("[Person#(no identity)]"));
        }

        [Test, AutoMoqData]
        public void HasIdentity_returns_correct_result_for_entity_with_identity()
        {
            var entity = new Person { Identity = 10 };
            Assert.That(() => ((IEntity) entity).HasIdentity, Is.True);
        }

        [Test, AutoMoqData]
        public void HasIdentity_returns_correct_result_for_entity_without_identity()
        {
            var entity = new Person();
            Assert.That(() => ((IEntity)entity).HasIdentity, Is.False);
        }
    }
}

