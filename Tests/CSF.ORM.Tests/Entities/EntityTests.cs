using NUnit.Framework;
using CSF.Entities.Stubs;
using CSF.Entities;
using System;

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

        [Test, AutoMoqData]
        public void Set_IdentityValue_does_not_throw_for_value_which_may_implicitly_cast_to_identity_value_type(int newIdentity, LongIdEntity entity)
        {
            Assert.That(() => {
                            IEntity castIdentity = entity;
                            castIdentity.IdentityValue = newIdentity;
                        }, Throws.Nothing);
        }

        [Test, AutoMoqData]
        public void Set_IdentityValue_using_value_which_implicitly_casts_to_identity_value_type_sets_correct_value(int newIdentity, LongIdEntity entity)
        {
            Assert.That(() => {
                            IEntity castIdentity = entity;
                            castIdentity.IdentityValue = newIdentity;
                            return castIdentity.IdentityValue;
                        }, Is.EqualTo(newIdentity));
        }

        [Test, AutoMoqData]
        public void Set_IdentityValue_throws_when_an_invalid_value_is_provided(LongIdEntity entity)
        {
            Assert.That(() => {
                IEntity castIdentity = entity;
                castIdentity.IdentityValue = "Not a number";
            }, Throws.Exception);
        }
    }
}

