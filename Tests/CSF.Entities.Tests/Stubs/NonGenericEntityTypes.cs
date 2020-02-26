using System;

namespace CSF.Entities.Tests.Stubs
{
    public class NonGenericEntityType : IEntity
    {
        public bool HasIdentity => IdentityValue != null;

        public object IdentityValue { get; set; }

        bool IEntity.HasIdentity => HasIdentity;

        object IEntity.IdentityValue
        {
            get { return IdentityValue; }
            set { IdentityValue = value; }
        }

        Type IEntity.IdentityType => typeof(string);
    }

    [IdentityType(typeof(long))]
    public class NonGenericEntityTypeWithNoParameterlessConstructor : IEntity
    {
        public bool HasIdentity => IdentityValue != null;

        public object IdentityValue { get; set; }

        bool IEntity.HasIdentity => HasIdentity;

        object IEntity.IdentityValue
        {
            get { return IdentityValue; }
            set { IdentityValue = value; }
        }

        Type IEntity.IdentityType => typeof(object);

        public NonGenericEntityTypeWithNoParameterlessConstructor(string someValue)
        {
            // Intentional no-op
        }
    }

    public class NonGenericEntityTypeWithNoParameterlessConstructorAndNoAttribute : IEntity
    {
        public bool HasIdentity => IdentityValue != null;

        public object IdentityValue { get; set; }

        bool IEntity.HasIdentity => HasIdentity;

        object IEntity.IdentityValue
        {
            get { return IdentityValue; }
            set { IdentityValue = value; }
        }

        Type IEntity.IdentityType => typeof(object);

        public NonGenericEntityTypeWithNoParameterlessConstructorAndNoAttribute(string someValue)
        {
            // Intentional no-op
        }
    }

}

