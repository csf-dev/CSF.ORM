using System;
using CSF;
using NHibernate.Mapping.ByCode.Conformist;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Pre-created component mapping for the <see cref="Fraction"/> type.
  /// </summary>
  public class FractionMap : ComponentMapping<Fraction>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CSF.Data.NHibernate.FractionMap"/> class.
    /// </summary>
    public FractionMap ()
    {
      Property(x => x.Numerator);

      Property(x => x.Denominator);
    }
  }
}

