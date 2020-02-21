using System;
namespace CSF.Entities
{
    /// <summary>
    /// Use this to decorate classes which should not be considered for the purpose of identity equality.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute should be applied to base classes which are used for all
    /// entities, for example "Layer Supertype" classes:
    /// https://www.martinfowler.com/eaaCatalog/layerSupertype.html
    /// </para>
    /// <para>
    /// If using "table per concrete class" mapping strategy to map entity inheritance
    /// hierarchies (in which the identities/primary keys of 'sibling' entities are
    /// independent and may thus overlap), then also apply this to any abstract
    /// base classes which they share.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class IgnoreForIdentityEqualityAttribute : Attribute { }
}
