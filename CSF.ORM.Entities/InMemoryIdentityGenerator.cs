//
// InMemoryIdentityGenerator.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2017 Craig Fowler
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
using CSF.Entities;

namespace CSF.ORM.Entities
{
  /// <summary>
  /// In-memory implementation of <see cref="IIdentityGenerator"/>.  Please note that this is not suitable for
  /// production usage.
  /// </summary>
  public class InMemoryIdentityGenerator : IIdentityGenerator
  {
    static readonly INumberGenerator DefaultGenerator = new IncrementalNumberGenerator();
    readonly INumberGenerator numberGenerator;

    /// <summary>
    /// Generates a new identity.
    /// </summary>
    /// <returns>The generated identity.</returns>
    /// <param name="identityType">The identity type.</param>
    public object GenerateIdentity(Type identityType)
    {
      if(identityType == null)
        throw new ArgumentNullException(nameof(identityType));

      if(identityType == typeof(long))
      {
        return numberGenerator.GetLong();
      }
      if(identityType == typeof(int))
      {
        return numberGenerator.GetInt();
      }
      if(identityType == typeof(byte))
      {
        return numberGenerator.GetByte();
      }
      if(identityType == typeof(Guid))
      {
        return Guid.NewGuid();
      }
      if(identityType == typeof(string))
      {
        return Guid.NewGuid().ToString();
      }

      throw new ArgumentException($"The identity type {identityType.Name} is not supported by this generator.",
                                  nameof(identityType));
    }

    /// <summary>
    /// Generates a new identity.
    /// </summary>
    /// <returns>The generated identity.</returns>
    /// <typeparam name="T">The identity type.</typeparam>
    public T GenerateIdentity<T>()
    {
      return (T) GenerateIdentity(typeof(T));
    }

    /// <summary>
    /// Generates an identity for the given entity and stores it within that entity instance.
    /// </summary>
    /// <param name="entity">An entity for which to generate an identity.</param>
    public void GenerateIdentity(IEntity entity)
    {
      if(entity == null)
        throw new ArgumentNullException(nameof(entity));

      var identityType = entity.GetIdentityType();
      var identity = GenerateIdentity(identityType);
      entity.SetIdentity(identity);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryIdentityGenerator"/> class.
    /// </summary>
    /// <param name="numberGenerator">A number generator service.</param>
    public InMemoryIdentityGenerator(INumberGenerator numberGenerator = null)
    {
      this.numberGenerator = numberGenerator ?? DefaultGenerator;
    }
  }
}
