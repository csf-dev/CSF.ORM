using System;

namespace CSF.Entities
{
  /// <summary>
  /// Attribute used to demark the base type for an entity, where it is a subclass of another entity type.
  /// </summary>
  [Obsolete("Deprecated: Identity equality types are now determined automatically.")]
  public class BaseTypeAttribute : Attribute
  {
    #region properties

    /// <summary>
    /// Gets the base entity type.
    /// </summary>
    /// <value>The type.</value>
    public Type Type
    {
      get;
      private set;
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="CSF.Entities.BaseTypeAttribute"/> class.
    /// </summary>
    /// <param name="type">Type.</param>
    public BaseTypeAttribute(Type type)
    {
      Entity.RequireEntityType(type);
      Type = type;
    }

    #endregion
  }
}

