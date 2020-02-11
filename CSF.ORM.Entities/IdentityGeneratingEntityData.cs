//
// IdentityGeneratingEntityData.cs
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
  /// Implementation of <see cref="EntityData"/> which generates identities for any entities which are added
  /// via the <see cref="EntityData.Add"/> method.
  /// </summary>
  public class IdentityGeneratingEntityData : EntityData
  {
    readonly IIdentityGenerator identityGenerator;

    /// <summary>
    /// Add the specified entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public override void Add<TEntity>(TEntity entity)
    {
      if(entity == null)
        throw new ArgumentNullException(nameof(entity));

      if(!entity.HasIdentity)
        identityGenerator.GenerateIdentity(entity);

      base.Add(entity);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityGeneratingEntityData"/> class.
    /// </summary>
    /// <param name="query">Query.</param>
    /// <param name="persister">Persister.</param>
    public IdentityGeneratingEntityData(IQuery query,
                                        IPersister persister) : this(query, persister, null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityGeneratingEntityData"/> class.
    /// </summary>
    /// <param name="query">Query.</param>
    /// <param name="persister">Persister.</param>
    /// <param name="identityGenerator">Identity generator.</param>
    public IdentityGeneratingEntityData(IQuery query,
                                        IPersister persister,
                                        IIdentityGenerator identityGenerator)
      : base(query, persister)
    {
      this.identityGenerator = identityGenerator?? new InMemoryIdentityGenerator();
    }
  }
}
