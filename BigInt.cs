//-----------------------------------------------------------------------
// <copyright file="BigInt.cs" company="Joey Woodson">
//     Copyright © 2009 Joey Woodson. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PrimeNumbers
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// A GMP mpz_t big integer
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct BigInt
    {
        /// <summary>
        /// The number of limbs currently allocated.
        /// </summary>
        private int allocated;

        /// <summary>
        /// The number of limbs.
        /// </summary>
        private int size;

        /// <summary>
        /// A pointer to a little-endian array of limbs.
        /// </summary>
        private IntPtr pointer;

        /// <summary>
        /// Determines whether this and <paramref name="obj"/> are equal.
        /// </summary>
        /// <param name="obj">The object to compare this to.</param>
        /// <returns>Whether this and <paramref name="obj"/> are equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is BigInt)
            {
                return ((BigInt)obj).ToHexString() == this.ToHexString();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Converts this to a string in base 10 (decimal).
        /// </summary>
        /// <returns>A base-10 string representation of this.</returns>
        public override string ToString()
        {
            StringBuilder temp = new StringBuilder((int)NativeMethods.SizeInBase(ref this, 10) + 2);
            NativeMethods.ToString(temp, 10, ref this);
            return temp.ToString();
        }

        /// <summary>
        /// Gets a unique number representing this (namely, the least significant bits of this as an int with the same sign as this).
        /// </summary>
        /// <returns>A unique number representing this.</returns>
        public override int GetHashCode()
        {
            return NativeMethods.ToInt(ref this);
        }

        /// <summary>
        /// Converts this to a string in base 16 (hexadecimal).
        /// </summary>
        /// <returns>A base-16 string representation of this.</returns>
        public string ToHexString()
        {
            StringBuilder temp = new StringBuilder((int)NativeMethods.SizeInBase(ref this, 16) + 2);
            NativeMethods.ToString(temp, 16, ref this);
            return temp.ToString();
        }
    }
}