//-----------------------------------------------------------------------
// <copyright file="BigIntPrimeThread.cs" company="Joey Woodson">
//     Copyright © 2009 Joey Woodson. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PrimeNumbers
{
    /// <summary>
    /// Contains methods used by each thread to determine whether a <see cref="BigInt"/> is prime.
    /// </summary>
    public class BigIntPrimeThread
    {
        /// <summary>
        /// The <see cref="BigInt"/> to check for primality.
        /// </summary>
        private BigInt numberToCheck = new BigInt();
       
        /// <summary>
        /// The square root of <see cref="numberToCheck"/>.
        /// </summary>
        private BigInt squareRoot = new BigInt();
      
        /// <summary>
        /// The first number that <see cref="numberToCheck"/> is to be tested for divisibility by.
        /// </summary>
        private BigInt divisor = new BigInt();
    
        /// <summary>
        /// The number to advance <see cref="divisor"/> by after every divisibility test.
        /// </summary>
        private uint stride;
      
        /// <summary>
        /// The <see cref="PrimeTester"/> that provides the thread-shared property <see cref="PrimeTester.Result"/>.
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
        /// Initializes a new instance of the <see cref="BigIntPrimeThread"/> class.
        /// </summary>
        /// <param name="numberToCheck">The <see cref="BigInt"/> to test for primality.</param>
        /// <param name="squareRoot">The square root of <paramref name="numberToCheck"/>.</param>
        /// <param name="divisor">The first number that <paramref name="numberToCheck"/> is to be tested for divisibility by.</param>
        /// <param name="stride">The number to advance <paramref name="divisor"/> by after every divisibility test.</param>
        /// <param name="tester">The <see cref="PrimeTester"/> that provides the thread-shared property <see cref="PrimeTester.Result"/>.</param>
        public BigIntPrimeThread(ref BigInt numberToCheck, ref BigInt squareRoot, ref BigInt divisor, uint stride, PrimeTester tester)
        {
            this.stride = stride;
            this.tester = tester;
            NativeMethods.InitializeAndSet(ref this.numberToCheck, ref numberToCheck);
            NativeMethods.InitializeAndSet(ref this.squareRoot, ref squareRoot);
            NativeMethods.InitializeAndSet(ref this.divisor, ref divisor);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BigIntPrimeThread"/> class.
        /// </summary>
        /// <param name="numberToCheck">The <see cref="BigInt"/> to test for primality.</param>
        /// <param name="squareRoot">The square root of <paramref name="numberToCheck"/>.</param>
        /// <param name="divisor">The first number that <paramref name="numberToCheck"/> is to be tested for divisibility by.</param>
        /// <param name="stride">The number to advance <paramref name="divisor"/> by after every divisibility test.</param>
        /// <param name="tester">The <see cref="PrimeTester"/> that provides the thread-shared property <see cref="PrimeTester.Result"/>.</param>
        /// <param name="primes">A uint[] with the first n primes in it (n being the number of counters desired).</param>
        public BigIntPrimeThread(ref BigInt numberToCheck, ref BigInt squareRoot, ref BigInt divisor, uint stride, PrimeTester tester, uint[] primes)
        {
            this.stride = stride;
            this.tester = tester;
            NativeMethods.InitializeAndSet(ref this.numberToCheck, ref numberToCheck);
            NativeMethods.InitializeAndSet(ref this.squareRoot, ref squareRoot);
            NativeMethods.InitializeAndSet(ref this.divisor, ref divisor);
            if (primes != null)
            {
                this.numberOfCounters = primes.Length;
                this.counters = new Counter[this.numberOfCounters];
                BigInt temp = new BigInt();
                NativeMethods.Initialize(ref temp);
                for (int i = 0; i < this.numberOfCounters; i++)
                {
                    NativeMethods.Subtract(ref temp, ref divisor, stride);
                    this.counters[i].Prime = primes[i];
                    this.counters[i].Value = NativeMethods.ModFloor(ref temp, ref temp, primes[i]);
                    this.counters[i].Stride = stride % primes[i];
                }

                NativeMethods.Clear(ref temp);
            }
        }

        /// <summary>
        /// Tests <see cref="numberToCheck"/> for primality.
        /// </summary>
        public unsafe void TestPrime()
        {
            if (this.numberOfCounters == 0)
            {
                this.TestPrimeNoCounters();
            }
            else
            {
                BigInt localNumberToCheck = new BigInt();
                BigInt localSquareRoot = new BigInt();
                BigInt localDivisor = new BigInt();
                NativeMethods.InitializeAndSet(ref localNumberToCheck, ref this.numberToCheck);
                NativeMethods.InitializeAndSet(ref localSquareRoot, ref this.squareRoot);
                NativeMethods.InitializeAndSet(ref localDivisor, ref this.divisor);
                NativeMethods.Clear(ref this.numberToCheck);
                NativeMethods.Clear(ref this.squareRoot);
                NativeMethods.Clear(ref this.divisor);
                int localNumberOfCounters = this.numberOfCounters;
                uint localStride = this.stride;
                uint maximumDivisor = uint.MaxValue - localStride;
                Counter[] counterArray = (Counter[])this.counters.Clone();
                fixed (Counter* firstCounter = &counterArray[0], lastCounter = &counterArray[localNumberOfCounters - 1])
                {
                    bool counterTriggered;
                    Counter* c = firstCounter;

                    if (NativeMethods.Compare(ref localDivisor, maximumDivisor) <= 0)
                    {
                        uint uintDivisor = NativeMethods.ToUint(ref localDivisor);
                        while (uintDivisor < maximumDivisor)
                        {
                            if (this.tester.Result != 0)
                            {
                                NativeMethods.Clear(ref localNumberToCheck);
                                NativeMethods.Clear(ref localSquareRoot);
                                NativeMethods.Clear(ref localDivisor);
                                return;
                            }

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
                                if (NativeMethods.Compare(ref localSquareRoot, uintDivisor) < 0)
                                {
                                    NativeMethods.Clear(ref localNumberToCheck);
                                    NativeMethods.Clear(ref localSquareRoot);
                                    NativeMethods.Clear(ref localDivisor);
                                    return;
                                }

                                if (NativeMethods.Divisible(ref localNumberToCheck, uintDivisor) != 0)
                                {
                                    this.tester.Result = 1;
                                    BigInt temp = new BigInt();
                                    NativeMethods.InitializeAndSet(ref temp, uintDivisor);
                                    lock (this.tester)
                                    {
                                        this.tester.BigIntFactor = temp;
                                    }

                                    NativeMethods.Clear(ref temp);
                                    NativeMethods.Clear(ref localNumberToCheck);
                                    NativeMethods.Clear(ref localSquareRoot);
                                    NativeMethods.Clear(ref localDivisor);
                                    return;
                                }
                            }

                            uintDivisor += localStride;
                        }

                        NativeMethods.Set(ref localDivisor, uintDivisor);
                    }

                    while (true)
                    {
                        if (this.tester.Result != 0)
                        {
                            NativeMethods.Clear(ref localNumberToCheck);
                            NativeMethods.Clear(ref localSquareRoot);
                            NativeMethods.Clear(ref localDivisor);
                            return;
                        }

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
                            if (NativeMethods.Compare(ref localDivisor, ref localSquareRoot) > 0)
                            {
                                NativeMethods.Clear(ref localNumberToCheck);
                                NativeMethods.Clear(ref localSquareRoot);
                                NativeMethods.Clear(ref localDivisor);
                                return;
                            }

                            if (NativeMethods.Divisible(ref localNumberToCheck, ref localDivisor) != 0)
                            {
                                this.tester.Result = 1;
                                lock (this.tester)
                                {
                                    this.tester.BigIntFactor = localDivisor;
                                }

                                NativeMethods.Clear(ref localNumberToCheck);
                                NativeMethods.Clear(ref localSquareRoot);
                                NativeMethods.Clear(ref localDivisor);
                                return;
                            }
                        }

                        NativeMethods.Add(ref localDivisor, ref localDivisor, localStride);
                    }
                }
            }
        }

        /// <summary>
        /// Tests <see cref="numberToCheck"/> for primality with no counters.
        /// </summary>
        public void TestPrimeNoCounters()
        {
            BigInt localNumberToCheck = new BigInt();
            BigInt localSquareRoot = new BigInt();
            BigInt localDivisor = new BigInt();
            NativeMethods.InitializeAndSet(ref localNumberToCheck, ref this.numberToCheck);
            NativeMethods.InitializeAndSet(ref localSquareRoot, ref this.squareRoot);
            NativeMethods.InitializeAndSet(ref localDivisor, ref this.divisor);
            NativeMethods.Clear(ref this.numberToCheck);
            NativeMethods.Clear(ref this.squareRoot);
            NativeMethods.Clear(ref this.divisor);
            int localNumberOfCounters = this.numberOfCounters;
            uint localStride = this.stride;
            uint maximumDivisor = uint.MaxValue - localStride;
            if (NativeMethods.Compare(ref localDivisor, maximumDivisor) <= 0)
            {
                uint uintDivisor = NativeMethods.ToUint(ref localDivisor);
                while (uintDivisor < maximumDivisor)
                {
                    if (this.tester.Result != 0)
                    {
                        NativeMethods.Clear(ref localNumberToCheck);
                        NativeMethods.Clear(ref localSquareRoot);
                        NativeMethods.Clear(ref localDivisor);
                        return;
                    }

                    if (NativeMethods.Compare(ref localSquareRoot, uintDivisor) < 0)
                    {
                        NativeMethods.Clear(ref localNumberToCheck);
                        NativeMethods.Clear(ref localSquareRoot);
                        NativeMethods.Clear(ref localDivisor);
                        return;
                    }

                    if (NativeMethods.Divisible(ref localNumberToCheck, uintDivisor) != 0)
                    {
                        this.tester.Result = 1;
                        BigInt temp = new BigInt();
                        NativeMethods.InitializeAndSet(ref temp, uintDivisor);
                        lock (this.tester)
                        {
                            this.tester.BigIntFactor = temp;
                        }

                        NativeMethods.Clear(ref temp);
                        NativeMethods.Clear(ref localNumberToCheck);
                        NativeMethods.Clear(ref localSquareRoot);
                        NativeMethods.Clear(ref localDivisor);
                        return;
                    }

                    uintDivisor += localStride;
                }

                NativeMethods.Set(ref localDivisor, uintDivisor);
            }

            while (this.tester.Result == 0)
            {
                if (NativeMethods.Compare(ref localDivisor, ref localSquareRoot) > 0)
                {
                    NativeMethods.Clear(ref localNumberToCheck);
                    NativeMethods.Clear(ref localSquareRoot);
                    NativeMethods.Clear(ref localDivisor);
                    return;
                }

                if (NativeMethods.Divisible(ref localNumberToCheck, ref localDivisor) != 0)
                {
                    this.tester.Result = 1;
                    lock (this.tester)
                    {
                        this.tester.BigIntFactor = localDivisor;
                    }

                    NativeMethods.Clear(ref localNumberToCheck);
                    NativeMethods.Clear(ref localSquareRoot);
                    NativeMethods.Clear(ref localDivisor);
                    return;
                }

                NativeMethods.Add(ref localDivisor, ref localDivisor, localStride);
            }
        }
    }
}