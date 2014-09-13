//-----------------------------------------------------------------------
// <copyright file="Counter.cs" company="Joey Woodson">
//     Copyright © 2009 Joey Woodson. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PrimeNumbers
{
    /// <summary>
    /// The struct used to represent the counters in the <see cref="IntPrimeThread"/>, <see cref="LongPrimeThread"/>, and <see cref="BigIntPrimeThread"/> classes.
    /// </summary>
    public struct Counter
    {
        /// <summary>
        /// The current value of the counter.
        /// </summary>
        public uint Value;

        /// <summary>
        /// The number to add to the counter's value at each pass.
        /// </summary>
        public uint Stride;

        /// <summary>
        /// The prime number that the counter is checking divisibility for.
        /// </summary>
        public uint Prime;
    }
}