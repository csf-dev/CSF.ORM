using System;
using CSF;
using FluentNHibernate.Mapping;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Pre-created FluentNHibernate component mapping for the <see cref="Fraction"/> type.
  /// </summary>
  public class FractionMap : ComponentMap<Fraction>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CSF.Data.NHibernate.FractionMap"/> class.
    /// </summary>
    public FractionMap ()
    {
      Map(x => x.Numerator)
        .Column("numerator");

      Map(x => x.Denominator)
        .Column("denominator");
    }
  }
}

