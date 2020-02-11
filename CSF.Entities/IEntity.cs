//
// IEntity.cs
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
  /// <para>Base non-generic interface for domain entities.</para>
  /// </summary>
  public interface IEntity
  {
    #region properties
    
    /// <summary>
    /// <para>Gets a value which indicates whether or not the current instance has an identity.</para>
    /// </summary>
    bool HasIdentity { get; }

    /// <summary>
    /// Gets the identity value for the current instance.
    /// </summary>
    /// <value>The identity value.</value>
    object IdentityValue { get; }

    #endregion

    #region methods

    /// <summary>
    /// Sets the identity for the current instance.
    /// </summary>
    /// <param name="identity">Identity.</param>
    void SetIdentity(object identity);

    /// <summary>
    /// Sets the identity for the current instance.
    /// </summary>
    /// <param name="identity">Identity.</param>
    void SetIdentity(IIdentity identity);

    /// <summary>
    /// Gets the <c>System.Type</c> of the identity value represented by <see cref="Identity"/>.
    /// </summary>
    /// <returns>The identity type.</returns>
    Type GetIdentityType();

    /// <summary>
    /// Gets a <c>System.Type</c> of the current <see cref="IEntity"/> for the purpose of entity equality testing.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Consider a hierarchy of Person -&gt; Employee.  Employee is a subclass of Person and both are entities.
    /// Employee ID=5 should be equal to Person ID=5.  Even though the entity types are different, "an employee is a
    /// person" so the equality test should consider them to be equivalent.
    /// </para>
    /// <para>
    /// This method gets the entity type which the current instance should use for purposes of this equality test.
    /// In the example above, for both the Employee and Person classes, it would return Person.
    /// </para>
    /// </remarks>
    /// <returns>The entity equality type.</returns>
    Type GetIdentityEqualityEntityType();

    /// <summary>
    /// Gets a value indicating whether the current instance and a given other entity are identity-equal.
    /// </summary>
    /// <returns><c>true</c>, if the current instance is identity-equal to the given instance, <c>false</c> otherwise.</returns>
    /// <param name="other">Other.</param>
    bool IdentityEquals(IEntity other);

    #endregion
  }
}

