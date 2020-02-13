//
// IEagerlyFetchesFromQuery.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2020 
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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CSF.ORM
{
  /// <summary>
  /// A service which provides functionality related to eagerly-fetching information from a query.
  /// </summary>
  public interface IEagerlyFetchesFromQuery
  {
    /// <summary>
    /// Eagerly fetches a 'child' object from the queried object.
    /// </summary>
    /// <returns>An object equivalent to the original query.</returns>
    /// <param name="query">A query.</param>
    /// <param name="getterExpression">An expression indicating the child object to be fetched.</param>
    /// <typeparam name="TQueried">The queried type.</typeparam>
    /// <typeparam name="TChild">The type of the child object which is to be fetched.</typeparam>
    IQueryableWithEagerFetching<TQueried, TChild> FetchChild<TQueried, TChild>(IQueryable<TQueried> query,
                                                                               Expression<Func<TQueried,TChild>> getterExpression);

    /// <summary>
    /// Eagerly fetches a collection of 'child' objects from the queried object.
    /// </summary>
    /// <returns>An object equivalent to the original query.</returns>
    /// <param name="query">A query.</param>
    /// <param name="getterExpression">An expression indicating the collection of child objects to be fetched.</param>
    /// <typeparam name="TQueried">The queried type.</typeparam>
    /// <typeparam name="TChild">The type of the child objects which are to be fetched.</typeparam>
    IQueryableWithEagerFetching<TQueried, TChild> FetchChildren<TQueried, TChild>(IQueryable<TQueried> query,
                                                                                  Expression<Func<TQueried, IEnumerable<TChild>>> getterExpression);

    /// <summary>
    /// Eagerly fetches a 'grandchild' object from child object(s) which have themselves been fetched.
    /// </summary>
    /// <returns>An object equivalent to the original query.</returns>
    /// <param name="query">A query in which a child object or collection of objects has been fetched.</param>
    /// <param name="getterExpression">An expression indicating the grandchild object to be fetched.</param>
    /// <typeparam name="TQueried">The queried type.</typeparam>
    /// <typeparam name="TChild">The type of the child object which has been fetched.</typeparam>
    /// <typeparam name="TGrandchild">The type of the grandchild object which is to be fetched.</typeparam>
    IQueryableWithEagerFetching<TQueried, TGrandchild> FetchGrandchild<TQueried, TChild, TGrandchild>(IQueryableWithEagerFetching<TQueried, TChild> query,
                                                                                                      Expression<Func<TChild, TGrandchild>> getterExpression);

    /// <summary>
    /// Eagerly fetches a collection of 'grandchild' objects from child object(s) which have themselves been fetched.
    /// </summary>
    /// <returns>An object equivalent to the original query.</returns>
    /// <param name="query">A query in which a child object or collection of objects has been fetched.</param>
    /// <param name="getterExpression">An expression indicating the collection of grandchild objects to be fetched.</param>
    /// <typeparam name="TQueried">The queried type.</typeparam>
    /// <typeparam name="TChild">The type of the child object which has been fetched.</typeparam>
    /// <typeparam name="TGrandchild">The type of the grandchild objects which are to be fetched.</typeparam>
    IQueryableWithEagerFetching<TQueried, TGrandchild> FetchGrandchildren<TQueried, TChild, TGrandchild>(IQueryableWithEagerFetching<TQueried, TChild> query,
                                                                                                         Expression<Func<TChild, IEnumerable<TGrandchild>>> getterExpression);
  }
}
