//
// ConnectionFactory.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2020 Craig Fowler
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
namespace CSF.ORM.InMemory
{
    /// <summary>
    /// An in-memory data-connection factory.
    /// </summary>
    public class ConnectionFactory : IGetsDataConnection
    {
        readonly DataStore sharedStore;

        /// <summary>
        /// Create a new data connection and return it.
        /// </summary>
        /// <returns>The connection.</returns>
        public IDataConnection GetConnection() => new DataConnection(sharedStore);

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionFactory"/> class.
        /// </summary>
        /// <param name="useSharedData">
        /// If set to <c>true</c> then all connections created from this factory will
        /// use a shared data-storage; if <c>false</c> then each will have its own independent data.
        /// </param>
        public ConnectionFactory(bool useSharedData = false)
        {
            if (useSharedData) sharedStore = new DataStore();
        }
    }
}
