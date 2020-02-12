using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Class wrapping a database value which will be lazy-loaded on first access, based upon a collection of items.
  /// </summary>
  public class EnumerableFutureValueWrapper<TValue> : IFutureValue<TValue>
  {
    #region fields

    private Lazy<IEnumerable<TValue>> _getterFunction;

    #endregion

    #region properties

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The value.</value>
    public virtual TValue Value
    {
      get {
        var output = _getterFunction.Value;

        if(output == null)
        {
          return default(TValue);
        }

        return output.FirstOrDefault();
      }
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Data.NHibernate.EnumerableFutureValueWrapper{TValue}"/> class.
    /// </summary>
    /// <param name="getter">Getter.</param>
    public EnumerableFutureValueWrapper(Func<IEnumerable<TValue>> getter)
    {
      if(getter == null)
      {
        throw new ArgumentNullException(nameof(getter));
      }

      _getterFunction = new Lazy<IEnumerable<TValue>>(getter);
    }

    #endregion
  }
}

