//-----------------------------------------------------------------------
// <copyright file="IntPrimeThread.cs" company="Joey Woodson">
//     Copyright © 2009 Joey Woodson. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PrimeNumbers
{
    /// <summary>
    /// Contains methods used by each thread to determine whether a uint is prime.
    /// </summary>
    public class IntPrimeThread
    {
        /// <summary>
        /// The uint to check for primality.
        /// </summary>
        private uint numberToCheck;

        /// <summary>
        /// The square root of <see cref="numberToCheck"/>.
        /// </summary>
        private uint squareRoot;

        /// <summary>
        /// The first number that <see cref="numberToCheck"/> is to be tested for divisibility by.
        /// </summary>
        private uint divisor;

        /// <summary>
        /// The number to advance <see cref="divisor"/> by after every divisibility test.
        /// </summary>
        private uint stride;

        /// <summary>
        /// The <see cref="PrimeTester"/> that provides the thread-shared property <see cref="PrimeTester.Factor"/>.
        /// </summary>
        private PrimeTester tester;

        /// <summary>
        /// The int representing the number of counters to be used.
        /// </summary>
        private int numberOfCounters;

        /// <summary>
        /// The counters to be used in testing <see cref="numberToCheck"/> for primality.
        /// </summary>
        private Counter[] counters;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntPrimeThread"/> class.
        /// </summary>
        /// <param name="numberToCheck">The uint to test for primality.</param>
        /// <param name="squareRoot">The square root of <paramref name="numberToCheck"/>.</param>
        /// <param name="divisor">The first number that <paramref name="numberToCheck"/> is to be tested for divisibility by.</param>
        /// <param name="stride">The number to advance <paramref name="divisor"/> by after every divisibility test.</param>
        /// <param name="tester">The <see cref="PrimeTester"/> that provides the thread-shared property <see cref="PrimeTester.Factor"/>.</param>
        public IntPrimeThread(uint numberToCheck, uint squareRoot, uint divisor, uint stride, PrimeTester tester)
        {
            this.numberToCheck = numberToCheck;
            this.squareRoot = squareRoot;
            this.divisor = divisor;
            this.stride = stride;
            this.tester = tester;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntPrimeThread"/> class.
        /// </summary>
        /// <param name="numberToCheck">The uint to test for primality.</param>
        /// <param name="squareRoot">The square root of <paramref name="numberToCheck"/>.</param>
        /// <param name="divisor">The first number that <paramref name="numberToCheck"/> is to be tested for divisibility by.</param>
        /// <param name="stride">The number to advance <paramref name="divisor"/> by after every divisibility test.</param>
        /// <param name="tester">The <see cref="PrimeTester"/> that provides the thread-shared property <see cref="PrimeTester.Factor"/>.</param>
        /// <param name="primes">A uint[] with the first n primes in it (n being the number of counters desired).</param>
        public IntPrimeThread(uint numberToCheck, uint squareRoot, uint divisor, uint stride, PrimeTester tester, uint[] primes)
        {
            this.numberToCheck = numberToCheck;
            this.squareRoot = squareRoot;
            this.divisor = divisor;
            this.stride = stride;
            this.tester = tester;
            if (primes != null)
            {
                this.numberOfCounters = primes.Length;
                this.counters = new Counter[this.numberOfCounters];
                for (int i = 0; i < this.numberOfCounters; i++)
                {
                    this.counters[i].Prime = primes[i];
                    this.counters[i].Value = (divisor - stride) % primes[i];
                    this.counters[i].Stride = stride % primes[i];
                }
            }
        }

        /// <summary>
        /// Tests <see cref="numberToCheck"/> for primality.
        /// </summary>
        public unsafe void TestPrime()
        {
            int localNumberOfCounters = this.numberOfCounters;
            uint localStride = this.stride;
            uint localDivisor = this.divisor;
            uint localSquareRoot = this.squareRoot;
            uint localNumberToCheck = this.numberToCheck;
            if (localNumberOfCounters == 0)
            {
                while (this.tester.Factor == 0)
                {
                    if (localDivisor > localSquareRoot)
                    {
                        return;
                    }

                    if (localNumberToCheck % localDivisor == 0)
                    {
                        this.tester.Factor = localDivisor;
                        return;
                    }

                    localDivisor += localStride;
                }
            }
            else
            {
                Counter[] counterArray = (Counter[])this.counters.Clone();
                fixed (Counter* firstCounter = &counterArray[0], lastCounter = &counterArray[localNumberOfCounters - 1])
                {
                    bool counterTriggered;
                    Counter* c = firstCounter;

                    while (this.tester.Factor == 0)
                    {
                        counterTriggered = false;
                        c = firstCounter;
                        while (c <= lastCounter)
                        {
                            c->Value += c->Stride;
                            if (c->Value >= c->Prime)
                            {
                                c->Value -= c->Prime;
                                if (c->Value == 0)
                                {
                                    counterTriggered = true;
                                }
                            }

                            c++;
                        }

                        if (!counterTriggered)
                        {
                            if (localDivisor > localSquareRoot)
                            {
                                return;
                            }

                            if (localNumberToCheck / localDivisor * localDivisor == localNumberToCheck)
                            {
                                this.tester.Factor = localDivisor;
                                return;
                            }
                        }

                        localDivisor += localStride;
                    }
                }
            }
        }
    }
}
