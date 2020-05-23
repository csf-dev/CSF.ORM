using System;

namespace CSF.PersistenceTester.Tests
{
    public class SampleEntity
    {
        public virtual long Identity { get; set; }
        public virtual string StringProperty { get; set; }
        public virtual EntityWithRelationship RelatedEntity { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is SampleEntity other)) return false;

            return Equals(Identity, other.Identity)
                   && String.Equals(StringProperty, other.StringProperty, StringComparison.InvariantCulture);
        }

        public override int GetHashCode()
        {
            return Identity.GetHashCode() ^ (StringProperty ?? String.Empty).GetHashCode();
        }

        public override string ToString() => $"[{nameof(SampleEntity)}:{StringProperty}]";
    }
}
