using System;

namespace CSF.Data.NHibernate
{
  /// <summary>
  /// Class wrapping a database value which will be lazy-loaded on first access.
  /// </summary>
  public class FutureValueWrapper<TValue> : IFutureValue<TValue>
  {
    #region fields

    private Lazy<TValue> _getterFunction;

    #endregion

    #region properties

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The value.</value>
    public virtual TValue Value
    {
      get {
        return _getterFunction.Value;
      }
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Data.NHibernate.FutureValueWrapper{TValue}"/> class.
    /// </summary>
    /// <param name="getter">Getter.</param>
    public FutureValueWrapper(Func<TValue> getter)
    {
      if(getter == null)
      {
        throw new ArgumentNullException(nameof(getter));
      }

      _getterFunction = new Lazy<TValue>(getter);
    }

    #endregion
  }
}

