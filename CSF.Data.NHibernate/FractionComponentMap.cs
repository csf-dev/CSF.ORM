using System;
using CSF;
using NHibernate.Mapping.ByCode.Conformist;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Pre-created component mapping for the <see cref="Fraction"/> type.
  /// </summary>
  public class FractionComponentMap : ComponentMapping<Fraction>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CSF.Data.NHibernate.FractionComponentMap"/> class.
    /// </summary>
    public FractionComponentMap ()
    {
      Property(x => x.Numerator);

      Property(x => x.Denominator);
    }
  }
}

