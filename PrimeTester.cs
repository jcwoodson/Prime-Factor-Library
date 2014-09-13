//-----------------------------------------------------------------------
// <copyright file="PrimeTester.cs" company="Joey Woodson">
//     Copyright © 2009 Joey Woodson. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PrimeNumbers
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains methods for testing the primality of numbers.
    /// </summary>
    public class PrimeTester
    {
        /// <summary>
        /// Stores the factor calculated by a <see cref="BigIntPrimeThread"/>.
        /// </summary>
        private BigInt bigIntFactor = new BigInt();

        /// <summary>
        /// A <see cref="PrimeLister"/> that lists primes for the counters.
        /// </summary>
        private PrimeLister lister = new PrimeLister();

        /// <summary>
        /// Gets or sets the factor calculated by an <see cref="IntPrimeThread"/> or a <see cref="LongPrimeThread"/>.
        /// </summary>
        public uint Factor { get; set; }

        /// <summary>
        /// Gets or sets a number that, when set by a <see cref="BigIntPrimeThread"/>, signals other <see cref="BigIntPrimeThread"/>s to stop running.
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// Sets <see cref="bigIntFactor"/>.
        /// </summary>
        public BigInt BigIntFactor
        {
            set { NativeMethods.Set(ref this.bigIntFactor, ref value); }
        }

        /// <summary>
        /// Tests a uint for primality.
        /// </summary>
        /// <param name="numberToCheck">The uint to check for primality.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, otherwise a factor of <paramref name="numberToCheck"/>.</returns>
        public uint IsPrime(uint numberToCheck)
        {
            return this.IsPrime(numberToCheck, Environment.ProcessorCount, 1, 0);
        }

        /// <summary>
        /// Tests a uint for primality.
        /// </summary>
        /// <param name="numberToCheck">The uint to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, otherwise a factor of <paramref name="numberToCheck"/>.</returns>
        public uint IsPrime(uint numberToCheck, int numberOfThreads)
        {
            return this.IsPrime(numberToCheck, numberOfThreads, 1, 0);
        }

        /// <summary>
        /// Tests a uint for primality.
        /// </summary>
        /// <param name="numberToCheck">The uint to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, otherwise a factor of <paramref name="numberToCheck"/>.</returns>
        public uint IsPrime(uint numberToCheck, int numberOfThreads, int actionsPerThread)
        {
            return this.IsPrime(numberToCheck, numberOfThreads, actionsPerThread, 0);
        }

        /// <summary>
        /// Tests a uint for primality.
        /// </summary>
        /// <param name="numberToCheck">The uint to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <param name="numberOfCounters">The number of counters to use when performing the test.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, otherwise a factor of <paramref name="numberToCheck"/>.</returns>
        public uint IsPrime(uint numberToCheck, int numberOfThreads, int actionsPerThread, uint numberOfCounters)
        {
            if (numberToCheck % 2 == 0)
            {
                return 2;
            }
            else
            {
                return this.IsPrime(numberToCheck, 3, numberOfThreads, actionsPerThread, numberOfCounters);
            }
        }

        /// <summary>
        /// Tests a ulong for primality.
        /// </summary>
        /// <param name="numberToCheck">The ulong to check for primality.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, otherwise a factor of <paramref name="numberToCheck"/>.</returns>
        public uint IsPrime(ulong numberToCheck)
        {
            return this.IsPrime(numberToCheck, Environment.ProcessorCount, 1, 0);
        }

        /// <summary>
        /// Tests a ulong for primality.
        /// </summary>
        /// <param name="numberToCheck">The ulong to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, otherwise a factor of <paramref name="numberToCheck"/>.</returns>
        public uint IsPrime(ulong numberToCheck, int numberOfThreads)
        {
            return this.IsPrime(numberToCheck, numberOfThreads, 1, 0);
        }

        /// <summary>
        /// Tests a ulong for primality.
        /// </summary>
        /// <param name="numberToCheck">The ulong to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, otherwise a factor of <paramref name="numberToCheck"/>.</returns>
        public uint IsPrime(ulong numberToCheck, int numberOfThreads, int actionsPerThread)
        {
            return this.IsPrime(numberToCheck, numberOfThreads, actionsPerThread, 0);
        }

        /// <summary>
        /// Tests a ulong for primality.
        /// </summary>
        /// <param name="numberToCheck">The ulong to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <param name="numberOfCounters">The number of counters to use when performing the test.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, otherwise a factor of <paramref name="numberToCheck"/>.</returns>
        public uint IsPrime(ulong numberToCheck, int numberOfThreads, int actionsPerThread, uint numberOfCounters)
        {
            if (numberToCheck % 2 == 0)
            {
                return 2;
            }
            else
            {
                return this.IsPrime(numberToCheck, 3, numberOfThreads, actionsPerThread, numberOfCounters);
            }
        }

        /// <summary>
        /// Tests a <see cref="BigInt"/> for primality.
        /// </summary>
        /// <param name="result">A <see cref="BigInt"/> used to store a factor of <paramref name="numberToCheck"/>.</param>
        /// <param name="numberToCheck">The <see cref="BigInt"/> to check for primality.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, 1 if it is composite.</returns>
        public int IsPrime(ref BigInt result, ref BigInt numberToCheck)
        {
            return this.IsPrime(ref result, ref numberToCheck, Environment.ProcessorCount, 1, 0, true);
        }

        /// <summary>
        /// Tests a <see cref="BigInt"/> for primality.
        /// </summary>
        /// <param name="result">A <see cref="BigInt"/> used to store a factor of <paramref name="numberToCheck"/>.</param>
        /// <param name="numberToCheck">The <see cref="BigInt"/> to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, 1 if it is composite.</returns>
        public int IsPrime(ref BigInt result, ref BigInt numberToCheck, int numberOfThreads)
        {
            return this.IsPrime(ref result, ref numberToCheck, numberOfThreads, 1, 0, true);
        }

        /// <summary>
        /// Tests a <see cref="BigInt"/> for primality.
        /// </summary>
        /// <param name="result">A <see cref="BigInt"/> used to store a factor of <paramref name="numberToCheck"/>.</param>
        /// <param name="numberToCheck">The <see cref="BigInt"/> to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, 1 if it is composite.</returns>
        public int IsPrime(ref BigInt result, ref BigInt numberToCheck, int numberOfThreads, int actionsPerThread)
        {
            return this.IsPrime(ref result, ref numberToCheck, numberOfThreads, actionsPerThread, 0, true);
        }

        /// <summary>
        /// Tests a <see cref="BigInt"/> for primality.
        /// </summary>
        /// <param name="result">A <see cref="BigInt"/> used to store a factor of <paramref name="numberToCheck"/>.</param>
        /// <param name="numberToCheck">The <see cref="BigInt"/> to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <param name="numberOfCounters">The number of counters to use when performing the test.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, 1 if it is composite.</returns>
        public int IsPrime(ref BigInt result, ref BigInt numberToCheck, int numberOfThreads, int actionsPerThread, uint numberOfCounters)
        {
            return this.IsPrime(ref result, ref numberToCheck, numberOfThreads, actionsPerThread, numberOfCounters, true);
        }

        /// <summary>
        /// Tests a <see cref="BigInt"/> for primality.
        /// </summary>
        /// <param name="result">A <see cref="BigInt"/> used to store a factor of <paramref name="numberToCheck"/>.</param>
        /// <param name="numberToCheck">The <see cref="BigInt"/> to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <param name="numberOfCounters">The number of counters to use when performing the test.</param>
        /// <param name="pseudoprimeFirst">Whether to perform a Miller-Rabin pseudoprimality test before starting deterministic testing.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, 1 if it is composite.</returns>
        public int IsPrime(ref BigInt result, ref BigInt numberToCheck, int numberOfThreads, int actionsPerThread, uint numberOfCounters, bool pseudoprimeFirst)
        {
            if (NativeMethods.Divisible(ref numberToCheck, 2) != 0)
            {
                NativeMethods.InitializeAndSet(ref result, 2);
                return 1;
            }
            else
            {
                BigInt three = new BigInt();
                NativeMethods.InitializeAndSet(ref three, 3);
                return this.IsPrime(ref result, ref numberToCheck, ref three, numberOfThreads, actionsPerThread, numberOfCounters, pseudoprimeFirst);
            }
        }

        /// <summary>
        /// Tests a number for primality.
        /// </summary>
        /// <param name="numberToCheck">The number, represented as a base-10 string, to check for primality.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, non-zero if it is composite.</returns>
        public string IsPrime(string numberToCheck)
        {
            return this.IsPrime(numberToCheck, Environment.ProcessorCount, 1, 0, true);
        }

        /// <summary>
        /// Tests a number for primality.
        /// </summary>
        /// <param name="numberToCheck">The number, represented as a base-10 string, to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, non-zero if it is composite.</returns>
        public string IsPrime(string numberToCheck, int numberOfThreads)
        {
            return this.IsPrime(numberToCheck, numberOfThreads, 1, 0, true);
        }

        /// <summary>
        /// Tests a number for primality.
        /// </summary>
        /// <param name="numberToCheck">The number, represented as a base-10 string, to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, non-zero if it is composite.</returns>
        public string IsPrime(string numberToCheck, int numberOfThreads, int actionsPerThread)
        {
            return this.IsPrime(numberToCheck, numberOfThreads, actionsPerThread, 0, true);
        }

        /// <summary>
        /// Tests a number for primality.
        /// </summary>
        /// <param name="numberToCheck">The number, represented as a base-10 string, to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <param name="numberOfCounters">The number of counters to use when performing the test.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, non-zero if it is composite.</returns>
        public string IsPrime(string numberToCheck, int numberOfThreads, int actionsPerThread, uint numberOfCounters)
        {
            return this.IsPrime(numberToCheck, numberOfThreads, actionsPerThread, numberOfCounters, true);
        }

        /// <summary>
        /// Tests a number for primality.
        /// </summary>
        /// <param name="numberToCheck">The number, represented as a base-10 string, to check for primality.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <param name="numberOfCounters">The number of counters to use when performing the test.</param>
        /// <param name="pseudoprimeFirst">Whether to perform a Miller-Rabin pseudoprimality test before starting deterministic testing.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, non-zero if it is composite.</returns>
        /// <remarks>If pseudoPrimeFirst is false, a facotr of the number will be returned if the number is composite.</remarks>
        public string IsPrime(string numberToCheck, int numberOfThreads, int actionsPerThread, uint numberOfCounters, bool pseudoprimeFirst)
        {
            BigInt bigintNumberToCheck = new BigInt();
            BigInt result = new BigInt();
            if (NativeMethods.InitializeAndSet(ref bigintNumberToCheck, numberToCheck, 10) == 1)
            {
                throw new FormatException("The string that was provided is not a proper base-10 integer.");
            }

            NativeMethods.Initialize(ref result);
            if (this.IsPrime(ref result, ref bigintNumberToCheck, numberOfThreads, actionsPerThread, numberOfCounters, pseudoprimeFirst) == 0)
            {
                return "0";
            }
            else
            {
                return result.ToString();
            }
        }

        /// <summary>
        /// Tests a uint for primality.
        /// </summary>
        /// <param name="numberToCheck">The uint to check for primality.</param>
        /// <param name="start">The first divisor to use in the primality check. This should usually be set to 3.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <param name="numberOfCounters">The number of counters to use when performing the test.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, otherwise a factor of <paramref name="numberToCheck"/>.</returns>
        private uint IsPrime(uint numberToCheck, uint start, int numberOfThreads, int actionsPerThread, uint numberOfCounters)
        {
            if (numberOfThreads > Environment.ProcessorCount)
            {
                numberOfThreads = Environment.ProcessorCount;
            }

            if (numberOfThreads == 1)
            {
                IntPrimeThread thread;
                this.Factor = 0;
                uint squareRoot = (uint)Math.Sqrt((double)numberToCheck);
                if (numberOfCounters == 0)
                {
                    thread = new IntPrimeThread(numberToCheck, squareRoot, start, 1, this);
                }
                else
                {
                    uint[] primes = this.lister.ListPrimes(numberOfCounters, true);
                    thread = new IntPrimeThread(numberToCheck, squareRoot, start, 1, this, primes);
                }

                thread.TestPrime();
                return this.Factor;
            }
            else
            {
                this.Factor = 0;
                ParallelOptions options = new ParallelOptions();
                options.MaxDegreeOfParallelism = numberOfThreads;
                uint squareRoot = (uint)Math.Sqrt((double)numberToCheck);
                Action[] actions = new Action[numberOfThreads * actionsPerThread];
                IntPrimeThread[] threads = new IntPrimeThread[actions.Length];
                if (numberOfCounters == 0)
                {
                    for (uint i = 0; i < actions.Length; i++)
                    {
                        threads[i] = new IntPrimeThread(numberToCheck, squareRoot, (i * 2) + start, (uint)actions.Length * 2, this);
                        actions[i] = threads[i].TestPrime;
                    }
                }
                else
                {
                    uint[] primes = this.lister.ListPrimes(numberOfCounters, true);
                    for (uint i = 0; i < actions.Length; i++)
                    {
                        threads[i] = new IntPrimeThread(numberToCheck, squareRoot, (i * 2) + start, (uint)actions.Length * 2, this, primes);
                        actions[i] = threads[i].TestPrime;
                    }
                }

                Parallel.Invoke(options, actions);
                return this.Factor;
            }
        }

        /// <summary>
        /// Tests a ulong for primality.
        /// </summary>
        /// <param name="numberToCheck">The ulong to check for primality.</param>
        /// <param name="start">The first divisor to use in the primality check. This should usually be set to 3.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <param name="numberOfCounters">The number of counters to use when performing the test.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, otherwise a factor of <paramref name="numberToCheck"/>.</returns>
        private uint IsPrime(ulong numberToCheck, uint start, int numberOfThreads, int actionsPerThread, uint numberOfCounters)
        {
            if (numberToCheck <= uint.MaxValue)
            {
                return this.IsPrime((uint)numberToCheck, start, numberOfThreads, actionsPerThread, numberOfCounters);
            }
            else
            {
                if (numberOfThreads > Environment.ProcessorCount)
                {
                    numberOfThreads = Environment.ProcessorCount;
                }

                if (numberOfThreads == 1)
                {
                    this.Factor = 0;
                    uint squareRoot = (uint)Math.Sqrt((double)numberToCheck);
                    LongPrimeThread thread;
                    if (numberOfCounters == 0)
                    {
                        thread = new LongPrimeThread(numberToCheck, squareRoot, start, 1, this);
                    }
                    else
                    {
                        uint[] primes = this.lister.ListPrimes(numberOfCounters, true);
                        thread = new LongPrimeThread(numberToCheck, squareRoot, start, 1, this, primes);
                    }

                    thread.TestPrime();
                    return this.Factor;
                }
                else
                {
                    this.Factor = 0;
                    ParallelOptions options = new ParallelOptions();
                    options.MaxDegreeOfParallelism = numberOfThreads;
                    uint squareRoot = (uint)Math.Sqrt((double)numberToCheck);
                    Action[] actions = new Action[numberOfThreads * actionsPerThread];
                    LongPrimeThread[] threads = new LongPrimeThread[actions.Length];
                    if (numberOfCounters == 0)
                    {
                        for (uint i = 0; i < actions.Length; i++)
                        {
                            threads[i] = new LongPrimeThread(numberToCheck, squareRoot, (i * 2) + start, (uint)actions.Length * 2, this);
                            actions[i] = threads[i].TestPrime;
                        }
                    }
                    else
                    {
                        uint[] primes = this.lister.ListPrimes(numberOfCounters, true);
                        for (uint i = 0; i < actions.Length; i++)
                        {
                            threads[i] = new LongPrimeThread(numberToCheck, squareRoot, (i * 2) + start, (uint)actions.Length * 2, this, primes);
                            actions[i] = threads[i].TestPrime;
                        }
                    }

                    Parallel.Invoke(options, actions);
                    return this.Factor;
                }
            }
        }

        /// <summary>
        /// Tests a <see cref="BigInt"/> for primality.
        /// </summary>
        /// <param name="result">A <see cref="BigInt"/> used to store a factor of <paramref name="numberToCheck"/>.</param>
        /// <param name="numberToCheck">The uint to check for primality.</param>
        /// <param name="start">The first divisor to use in the primality check. This should usually be set to 3.</param>
        /// <param name="numberOfThreads">The number of threads to use in the test.</param>
        /// <param name="actionsPerThread">The number of actions to use per thread. This should usually be set to 1.</param>
        /// <param name="numberOfCounters">The number of counters to use when performing the test.</param>
        /// <param name="pseudoprimeFirst">Whether to perform a Miller-Rabin pseudoprimality test before starting deterministic testing.</param>
        /// <returns>0 if <paramref name="numberToCheck"/> is prime, 1 if it is composite.</returns>
        private int IsPrime(ref BigInt result, ref BigInt numberToCheck, ref BigInt start, int numberOfThreads, int actionsPerThread, uint numberOfCounters, bool pseudoprimeFirst)
        {
            BigInt ulongMaxValue = new BigInt();
            if (NativeMethods.InitializeAndSet(ref ulongMaxValue, ulong.MaxValue.ToString("g", CultureInfo.InvariantCulture), 10) == 1)
            {
                throw new ArithmeticException("A method in this program has caused an error. Please report this error to the program developer.");
            }

            if (pseudoprimeFirst == true)
            {
                switch (NativeMethods.ProbablePrime(ref numberToCheck, 8))
                {
                    case 2:
                        NativeMethods.InitializeAndSet(ref result, 0);
                        return 0;
                    case 0:
                        NativeMethods.InitializeAndSet(ref result, 1);
                        return 1;
                    case 1:
                        break;
                }
            }

            if (NativeMethods.Compare(ref numberToCheck, ref ulongMaxValue) <= 0)
            {
                NativeMethods.Clear(ref ulongMaxValue);
                NativeMethods.InitializeAndSet(ref result, this.IsPrime(Convert.ToUInt64(numberToCheck.ToString(), CultureInfo.CurrentCulture), NativeMethods.ToUint(ref start), numberOfThreads, actionsPerThread, numberOfCounters));
                if (NativeMethods.ToUint(ref result) == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                NativeMethods.Clear(ref ulongMaxValue);

                if (numberOfThreads > Environment.ProcessorCount)
                {
                    numberOfThreads = Environment.ProcessorCount;
                }

                if (numberOfThreads == 1)
                {
                    this.Result = 0;
                    NativeMethods.InitializeAndSet(ref this.bigIntFactor, 0);
                    BigInt squareRoot = new BigInt();
                    NativeMethods.Initialize(ref squareRoot);
                    NativeMethods.Sqrt(ref squareRoot, ref numberToCheck);
                    BigIntPrimeThread thread;
                    if (numberOfCounters == 0)
                    {
                        thread = new BigIntPrimeThread(ref numberToCheck, ref squareRoot, ref start, 1, this);
                    }
                    else
                    {
                        uint[] primes = this.lister.ListPrimes(numberOfCounters, true);
                        thread = new BigIntPrimeThread(ref numberToCheck, ref squareRoot, ref start, 1, this, primes);
                    }

                    thread.TestPrime();
                    NativeMethods.InitializeAndSet(ref result, ref this.bigIntFactor);
                    NativeMethods.Clear(ref squareRoot);
                    NativeMethods.Clear(ref this.bigIntFactor);
                    return this.Result;
                }
                else
                {
                    this.Result = 0;
                    NativeMethods.InitializeAndSet(ref this.bigIntFactor, 0);
                    ParallelOptions options = new ParallelOptions();
                    options.MaxDegreeOfParallelism = numberOfThreads;
                    BigInt squareRoot = new BigInt();
                    NativeMethods.Initialize(ref squareRoot);
                    NativeMethods.Sqrt(ref squareRoot, ref numberToCheck);
                    Action[] actions = new Action[numberOfThreads * actionsPerThread];
                    BigIntPrimeThread[] threads = new BigIntPrimeThread[actions.Length];
                    BigInt startNumber = new BigInt();
                    NativeMethods.Initialize(ref startNumber);

                    if (numberOfCounters == 0)
                    {
                        for (uint i = 0; i < actions.Length; i++)
                        {
                            NativeMethods.Add(ref startNumber, ref start, i * 2);
                            threads[i] = new BigIntPrimeThread(ref numberToCheck, ref squareRoot, ref startNumber, (uint)actions.Length * 2, this);
                            actions[i] = threads[i].TestPrime;
                        }
                    }
                    else
                    {
                        uint[] primes = this.lister.ListPrimes(numberOfCounters, true);
                        for (uint i = 0; i < actions.Length; i++)
                        {
                            NativeMethods.Add(ref startNumber, ref start, i * 2);
                            threads[i] = new BigIntPrimeThread(ref numberToCheck, ref squareRoot, ref startNumber, (uint)actions.Length * 2, this, primes);
                            actions[i] = threads[i].TestPrime;
                        }
                    }

                    Parallel.Invoke(options, actions);
                    NativeMethods.Set(ref result, ref this.bigIntFactor);
                    NativeMethods.Clear(ref squareRoot);
                    NativeMethods.Clear(ref this.bigIntFactor);
                    NativeMethods.Clear(ref startNumber);
                    return this.Result;
                }
            }
        }
    }
}