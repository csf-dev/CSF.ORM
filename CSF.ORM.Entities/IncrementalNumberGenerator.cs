//
// IncrementalNumberGenerator.cs
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

using System.Threading;

namespace CSF.ORM
{
    /// <summary>
    /// Implementation of <see cref="IGeneratesNumbers"/> which generates numeric values incrementally.
    /// </summary>
    public class IncrementalNumberGenerator : IGeneratesNumbers
    {
        long nextNumber = 1;

        /// <summary>
        /// Gets a 64 bit integer.
        /// </summary>
        /// <returns>The generated number.</returns>
        public long GetLong() => Interlocked.Increment(ref nextNumber);

        /// <summary>
        /// Gets a 32 bit integer.
        /// </summary>
        /// <returns>The generated number.</returns>
        public int GetInt() => unchecked((int)GetLong());

        /// <summary>
        /// Gets an 8 bit integer.
        /// </summary>
        /// <returns>The generated number.</returns>
        public byte GetByte() => unchecked((byte)GetLong());
    }
}
