using NUnit.Framework;
using CSF.Entities.Tests.Stubs;

namespace CSF.Entities.Tests
{
    [TestFixture,Parallelizable]
    public class IdentityTests
    {
        [Test, AutoMoqData]
        public void Equals_returns_true_for_two_equal_identities()
        {
            var identity1 = new Identity<long, Cat>(5);
            var identity2 = new Identity<long, Cat>(5);
            Assert.That(identity1.Equals(identity2), Is.True);
        }

        [Test, AutoMoqData]
        public void Equals_returns_true_for_two_identities_with_compatible_entity_types()
        {
            var identity1 = new Identity<long, Cat>(5);
            var identity2 = new Identity<long, Dog>(5);
            Assert.That(identity1.Equals(identity2), Is.True);
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_for_two_identities_with_compatible_entity_types_but_differing_values()
        {
            var identity1 = new Identity<long, Cat>(10);
            var identity2 = new Identity<long, Dog>(20);
            Assert.That(identity1.Equals(identity2), Is.False);
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_for_two_identities_with_incompatible_entity_types()
        {
            var identity1 = new Identity<long, Cat>(5);
            var identity2 = new Identity<long, Person>(5);
            Assert.That(identity1.Equals(identity2), Is.False);
        }

        [Test, AutoMoqData]
        public void Equals_operator_returns_true_for_two_equal_identities()
        {
            var identity1 = new Identity<long, Cat>(5);
            var identity2 = new Identity<long, Cat>(5);
            Assert.That(identity1 == identity2, Is.True);
        }

        [Test, AutoMoqData]
        public void Equals_operator_returns_true_for_two_identities_with_compatible_entity_types()
        {
            var identity1 = new Identity<long, Cat>(5);
            var identity2 = new Identity<long, Dog>(5);
            Assert.That(identity1 == identity2, Is.True);
        }

        [Test, AutoMoqData]
        public void Equals_operator_returns_false_for_two_identities_with_compatible_entity_types_but_differing_values()
        {
            var identity1 = new Identity<long, Cat>(10);
            var identity2 = new Identity<long, Dog>(20);
            Assert.That(identity1 == identity2, Is.False);
        }

        [Test, AutoMoqData]
        public void Equals_operator_returns_false_for_two_identities_with_incompatible_entity_types()
        {
            var identity1 = new Identity<long, Cat>(5);
            var identity2 = new Identity<long, Person>(5);
            Assert.That(identity1 == identity2, Is.False);
        }

        [Test, AutoMoqData]
        public void Create_creates_identity_with_correct_value()
        {
            var identity = Identity.Create<Cat>(5);
            Assert.That(identity.Value, Is.EqualTo(5));
        }

        [Test, AutoMoqData]
        public void Parse_returns_identity_with_correct_value()
        {
            var identity = Identity.Parse<Cat>("66");
            Assert.That(identity.Value, Is.EqualTo(66));
        }

        [Test, AutoMoqData]
        public void Cast_returns_appropriate_identity()
        {
            var identity = new Identity<long,Animal>(5);
            Assert.That(() => Identity.Cast<Cat>(identity).Value, Is.EqualTo(5));
        }

        [Test, AutoMoqData]
        public void GetValueAsString_returns_correct_value()
        {
            var identity = new Identity<long, Cat>(5);
            Assert.That(identity.GetValueAsString(), Is.EqualTo("5"));
        }

        [Test, AutoMoqData]
        public void ToString_returns_correct_value()
        {
            var identity = new Identity<long, Cat>(5);
            Assert.That(identity.ToString(), Is.EqualTo("[Cat#5]"));
        }

        [Test, AutoMoqData]
        public void Create_does_not_throw_when_a_compatible_identity_value_is_provided()
        {
            Assert.That(() => Identity.Create<LongIdEntity>(5), Throws.Nothing);
        }
    }
}

