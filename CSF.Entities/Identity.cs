//
// Identity.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2015 CSF Software Limited
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;

namespace CSF.Entities
{
  /// <summary>
  /// Static functionality supporting the <see cref="T:Identity{TIdentity}"/> type.
  /// </summary>
  public static class Identity
  {
    #region constants

    internal static readonly Type OpenGenericIdentity = typeof(Identity<,>);

    #endregion

    #region methods

    /// <summary>
    /// Creates an <see cref="IIdentity"/> for the given entity type, identity type and identity value.
    /// </summary>
    /// <param name="entityType">Entity type.</param>
    /// <param name="identityType">Identity type.</param>
    /// <param name="identityValue">Identity value.</param>
    public static IIdentity Create(Type entityType, Type identityType, object identityValue)
    {
      if(entityType == null)
      {
        throw new ArgumentNullException(nameof(entityType));
      }
      if(identityType == null)
      {
        throw new ArgumentNullException(nameof(identityType));
      }

      if(Object.Equals(GetDefaultValue(identityType), identityValue))
      {
        return null;
      }

      var closedIdentityType = OpenGenericIdentity.MakeGenericType(identityType, entityType);
      return (IIdentity) Activator.CreateInstance(closedIdentityType, new [] { identityValue });
    }

    /// <summary>
    /// Create an entity-typed identity instance from a given identity value.
    /// </summary>
    /// <param name="identityValue">Identity value.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public static IIdentity<TEntity> Create<TEntity>(object identityValue)
      where TEntity : IEntity
    {
      return (IIdentity<TEntity>) Create(typeof(TEntity), GetIdentityType<TEntity>(), identityValue);
    }

    /// <summary>
    /// Determines whether the two given entity instances are identity-equal or not.
    /// </summary>
    /// <param name="first">The first entity.</param>
    /// <param name="second">The second entity.</param>
    /// <typeparam name="TFirstEntity">The first entity type.</typeparam>
    /// <typeparam name="TSecondEntity">The second entity type.</typeparam>
    public static bool Equals<TFirstEntity,TSecondEntity>(TFirstEntity first, TSecondEntity second)
      where TFirstEntity : IEntity
      where TSecondEntity : IEntity
    {
      bool
        firstNull = ReferenceEquals(first, null),
        secondNull = ReferenceEquals(second, null);

      if(firstNull && secondNull)
      {
        return true;
      }
      else if(firstNull || secondNull)
      {
        return false;
      }

      var firstIdentity = first.GetIdentity();
      var secondIdentity = second.GetIdentity();

      if(firstIdentity == null
         || secondIdentity == null)
      {
        return false;
      }

      return Object.Equals(firstIdentity, secondIdentity);
    }

    /// <summary>
    /// Gets the identity type for a given entity type.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method makes two attempts to get the identity type for a given entity,
    /// If that entity implements <see cref="T:Entity{TIdentity}"/> then the identity type is taken from the generic
    /// type argument of the base type.
    /// If the entity does not implement that generic base type, but has a public parameterless constructor, then
    /// a new instance of the entity type is constructed and the <see cref="IEntity.GetIdentityType()"/> method is used
    /// to get the identity type.
    /// </para>
    /// <para>
    /// If neither of those two mechanisms are successful, then this method returns <c>null</c>.
    /// </para>
    /// </remarks>
    /// <returns>The identity type.</returns>
    /// <param name="entityType">Entity type.</param>
    /// <exception cref="ArgumentException">If the entity type does not implement <see cref="IEntity"/>.</exception>
    public static Type GetIdentityType(Type entityType)
    {
      Entity.RequireEntityType(entityType);

      Type output;

      if(TryGetIdentityTypeFromGenericEntityType(entityType, out output))
      {
        return output;
      }

      if(TryGetIdentityTypeFromNewInstance(entityType, out output))
      {
        return output;
      }

      return null;
    }

    /// <summary>
    /// Gets the identity type for a given entity type.
    /// </summary>
    /// <returns>The identity type.</returns>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public static Type GetIdentityType<TEntity>() where TEntity : IEntity
    {
      return GetIdentityType(typeof(TEntity));
    }

    /// <summary>
    /// Casts an identity instance to a more specific entity type.
    /// </summary>
    /// <param name="identity">The input identity.</param>
    /// <typeparam name="TCast">The entity type for which to cast the identity.</typeparam>
    public static IIdentity<TCast> Cast<TCast>(this IIdentity identity) where TCast : IEntity
    {
      if(identity == null)
      {
        throw new ArgumentNullException(nameof(identity));
      }

      if(!identity.EntityType.IsAssignableFrom(typeof(TCast)))
      {
        string message = String.Format(Resources.ExceptionMessages.CastTypeMustImplementEntityTypeFormat,
                                       identity.EntityType.Name,
                                       typeof(TCast).Name);
        throw new ArgumentException(message, nameof(identity));
      }

      return Identity.Create<TCast>(identity.Value);
    }

    /// <summary>
    /// Converts a string identity value into an entity identity.
    /// </summary>
    /// <param name="value">The string identity value.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public static IIdentity<TEntity> Parse<TEntity>(string value) where TEntity : IEntity
    {
      if(value == null)
      {
        throw new ArgumentNullException(nameof(value));
      }

      var identityType = GetIdentityType(typeof(TEntity));
      var identityValue = Convert.ChangeType(value, identityType);
      return Create<TEntity>(identityValue);
    }

    /// <summary>
    /// Converts a string identity value into an entity identity.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="value">The string identity value.</param>
    public static IIdentity Parse(Type entityType, string value)
    {
      if(entityType == null)
      {
        throw new ArgumentNullException(nameof(entityType));
      }
      if(value == null)
      {
        throw new ArgumentNullException(nameof(value));
      }

      var identityType = GetIdentityType(entityType);
      var identityValue = Convert.ChangeType(value, identityType);
      return Create(entityType, identityType, identityValue);
    }

    /// <summary>
    /// Raises an exception if the <paramref name="value"/> is the default for its data-type.
    /// </summary>
    /// <param name="value">The identity value.</param>
    /// <typeparam name="TIdentity">The data type for the potential identity value.</typeparam>
    internal static void RequireNotDefaultValue<TIdentity>(TIdentity value)
    {
      if(Object.Equals(value, default(TIdentity)))
      {
        string message = String.Format(Resources.ExceptionMessages.MustNotBeDefaultForDataTypeFormat,
                                       typeof(TIdentity).Name);
        throw new ArgumentException(message, nameof(value));
      }
    }

    private static bool TryGetIdentityTypeFromGenericEntityType(Type entityType, out Type identityType)
    {
      var genericEntityType = Entity.GetGenericEntityType(entityType);

      if(genericEntityType == null)
      {
        identityType = null;
        return false;
      }

      identityType = genericEntityType.GenericTypeArguments[0];
      return true;
    }

    private static bool TryGetIdentityTypeFromNewInstance(Type entityType, out Type identityType)
    {
      var parameterlessCtor = entityType.GetConstructor(Type.EmptyTypes);

      if(parameterlessCtor == null)
      {
        identityType = null;
        return false;
      }
    
      var instance = (IEntity) Activator.CreateInstance(entityType);
      identityType = instance.GetIdentityType();
      return true;
    }

    /// <summary>
    /// Gets the default value for a given <c>System.Type</c>.
    /// </summary>
    /// <returns>The default value.</returns>
    /// <param name="type">Type.</param>
    private static object GetDefaultValue(Type type)
    {
      if(type == null)
      {
        throw new ArgumentNullException(nameof(type));
      }

      if(type.IsValueType)
      {
        return Activator.CreateInstance(type);
      }

      return null;
    }

    #endregion
  }
}

