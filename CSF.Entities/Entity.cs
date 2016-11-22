//
// EntityExtensions.cs
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
using System.Reflection;
using System.Linq;

namespace CSF.Entities
{
  /// <summary>
  /// Extension methods for entity types.
  /// </summary>
  public static class Entity
  {
    #region constants

    private static readonly Type
      EntityBaseType = typeof(IEntity),
      GenericEntityType = typeof(Entity<>);

    #endregion

    #region methods

    /// <summary>
    /// Gets the identity for a given entity instance.
    /// </summary>
    /// <returns>The identity.</returns>
    /// <param name="entity">Entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public static IIdentity<TEntity> GetIdentity<TEntity>(this TEntity entity) where TEntity : IEntity
    {
      return (IIdentity<TEntity>) Identity.Create(entity.GetType(),
                                                  entity.GetIdentityType(),
                                                  entity.IdentityValue);
    }

    /// <summary>
    /// Gets the <c>System.Type</c> for the entity (for the purpose of equality comparisons).
    /// </summary>
    /// <returns>The entity type for equality comparisons.</returns>
    /// <param name="entity">Entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public static Type GetEqualityType<TEntity>(this TEntity entity) where TEntity : IEntity
    {
      return GetEqualityType(typeof(TEntity));
    }

    /// <summary>
    /// Gets the <c>System.Type</c> for the entity (for the purpose of equality comparisons).
    /// </summary>
    /// <returns>The entity type for equality comparisons.</returns>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public static Type GetEqualityType<TEntity>() where TEntity : IEntity
    {
      return GetEqualityType(typeof(TEntity));
    }

    /// <summary>
    /// Gets the <c>System.Type</c> for the entity (for the purpose of equality comparisons).
    /// </summary>
    /// <returns>The entity type for equality comparisons.</returns>
    /// <param name="entityType">Entity type.</param>
    public static Type GetEqualityType(Type entityType)
    {
      Entity.RequireEntityType(entityType);

      var attrib = entityType.GetCustomAttribute<BaseTypeAttribute>(true);
      return (attrib != null)? attrib.Type : entityType;
    }

    /// <summary>
    /// Determines whether the equality types of two given entity types are the same.
    /// </summary>
    /// <returns><c>true</c>, if the equality types are the same, <c>false</c> otherwise.</returns>
    /// <param name="firstType">The first entity type.</param>
    /// <param name="secondType">The second entity type.</param>
    public static bool AreEqualityTypesSame(Type firstType, Type secondType)
    {
      if(firstType == null)
      {
        throw new ArgumentNullException(nameof(firstType));
      }
      if(secondType == null)
      {
        throw new ArgumentNullException(nameof(secondType));
      }

      Type
        firstEntityType = GetEqualityType(firstType),
        secondEntityType = GetEqualityType(secondType);

      return firstEntityType == secondEntityType;
    }

    /// <summary>
    /// Raises an exception if the <paramref name="potentialEntityType"/> is either <c>null</c> or does not implement
    /// <see cref="IEntity"/>.
    /// </summary>
    /// <param name="potentialEntityType">A type which may or may not be an entity type.</param>
    internal static void RequireEntityType(Type potentialEntityType)
    {
      if(potentialEntityType == null)
      {
        throw new ArgumentNullException(nameof(potentialEntityType));
      }
      if(!EntityBaseType.IsAssignableFrom(potentialEntityType))
      {
        string message = String.Format(Resources.ExceptionMessages.TypeMustBeEntityTypeFormat,
                                       potentialEntityType.FullName,
                                       EntityBaseType.FullName);
        throw new ArgumentException(message, nameof(potentialEntityType));
      }
    }

    /// <summary>
    /// Raises an exception if the <paramref name="potentialEntityType"/> is either <c>null</c> or does not implement
    /// the generic entity base type.
    /// </summary>
    /// <param name="potentialEntityType">A type which may or may not be an entity type.</param>
    internal static Type GetGenericEntityType(Type potentialEntityType)
    {
      if(potentialEntityType == null)
      {
        throw new ArgumentNullException(nameof(potentialEntityType));
      }

      var currentType = potentialEntityType;

      while(currentType != null)
      {
        if(currentType.IsGenericType
           && currentType.GetGenericTypeDefinition() == GenericEntityType)
        {
          break;
        }

        currentType = currentType.BaseType;
      }

      return currentType;
    }

    #endregion
  }
}

