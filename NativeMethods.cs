//-----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Joey Woodson">
//     Copyright © 2009 Joey Woodson. All rights reserved.
//     Some documentation taken from the GMP manual, version 4.3.0.
//     (GMP manual copyright 1991-2009 Free Software Foundation, Inc.)
// </copyright>
// <note>
//     Numbers such as 5.1 are GMP Manual (version 4.3.0) sections.
//     Many of these are untested, although all of them should work.
//     These are named according to .NET conventions.
//     For the (identical) GMP-named methods, see the commented-out class mpz.
// </note>
//-----------------------------------------------------------------------

namespace PrimeNumbers
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    /// <summary>
    /// Contains methods for working with <see cref="BigInt"/>s.
    /// </summary>
    public static class NativeMethods
    {
        #region 5.1 Initialization Functions

        /// <summary>
        /// Initializes <paramref name="integer"/>, and set its value to 0.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be initialized.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_init", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Initialize(ref BigInt integer);

        /// <summary>
        /// Initializes <paramref name="integer"/>, with space for <paramref name="bits"/> bits, and set its value to 0.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be initialized.</param>
        /// <param name="bits">The number of bits.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_init2", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Initialize(ref BigInt integer, uint bits);

        /// <summary>
        /// Frees the space occupied by <paramref name="integer"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be cleared.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_clear", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Clear(ref BigInt integer);

        /// <summary>
        /// Changes the space allocated for <paramref name="integer"/> to <paramref name="bits"/> bits.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> whose capacity is to be changed.</param>
        /// <param name="bits">The number of bits.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_realloc2", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Reallocate(ref BigInt integer, uint bits);

        #endregion

        #region 5.2 Assignment Functions

        /// <summary>
        /// Sets the value of <paramref name="integer"/> from <paramref name="number"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be set.</param>
        /// <param name="number">The new value of <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_set", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Set(ref BigInt integer, ref BigInt number);

        /// <summary>
        /// Sets the value of <paramref name="integer"/> from <paramref name="number"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be set.</param>
        /// <param name="number">The new value of <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_set_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Set(ref BigInt integer, uint number);

        /// <summary>
        /// Sets the value of <paramref name="integer"/> from <paramref name="number"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be set.</param>
        /// <param name="number">The new value of <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_set_si", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Set(ref BigInt integer, int number);

        /// <summary>
        /// Sets the value of <paramref name="integer"/> from <paramref name="number"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be set.</param>
        /// <param name="number">The new value of <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_set_d", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Set(ref BigInt integer, double number);

        /// <summary>
        /// Sets the value of <paramref name="integer"/> from <paramref name="number"/>, a string in base <paramref name="numberBase"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be set.</param>
        /// <param name="number">The new value of <paramref name="integer"/>.</param>
        /// <param name="numberBase">The base of <paramref name="number"/>.</param>
        /// <returns>0 if the entire string is a valid number in base <paramref name="numberBase"/>, otherwise 1.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_set_str", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false), SuppressUnmanagedCodeSecurity]
        public static extern int Set(ref BigInt integer, [MarshalAs(UnmanagedType.LPStr)]string number, int numberBase);

        /// <summary>
        /// Swaps the values of <paramref name="integer1"/> and <paramref name="integer2"/> efficiently.
        /// </summary>
        /// <param name="integer1">The <see cref="BigInt"/> to be swapped with <paramref name="integer2"/>.</param>
        /// <param name="integer2">The <see cref="BigInt"/> to be swapped with <paramref name="integer1"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_swap", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Swap(ref BigInt integer1, ref BigInt integer2);

        #endregion

        #region 5.3 Combined Initialization and Assignment Functions

        /// <summary>
        /// Initializes <paramref name="integer"/> and sets the initial numeric value from <paramref name="number"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be initialized and set.</param>
        /// <param name="number">The new value of <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_init_set", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void InitializeAndSet(ref BigInt integer, ref BigInt number);

        /// <summary>
        /// Initializes <paramref name="integer"/> and sets the initial numeric value from <paramref name="number"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be initialized and set.</param>
        /// <param name="number">The new value of <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_init_set_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void InitializeAndSet(ref BigInt integer, uint number);

        /// <summary>
        /// Initializes <paramref name="integer"/> and sets the initial numeric value from <paramref name="number"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be initialized and set.</param>
        /// <param name="number">The new value of <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_init_set_si", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void InitializeAndSet(ref BigInt integer, int number);

        /// <summary>
        /// Initializes <paramref name="integer"/> and sets the initial numeric value from <paramref name="number"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be initialized and set.</param>
        /// <param name="number">The new value of <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_init_set_d", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void InitializeAndSet(ref BigInt integer, double number);

        /// <summary>
        /// Initializes <paramref name="integer"/> and sets the initial numeric value from <paramref name="number"/>, a string in base <paramref name="numberBase"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be initialized and set.</param>
        /// <param name="number">The new value of <paramref name="integer"/>.</param>
        /// <param name="numberBase">The base of <paramref name="number"/>.</param>
        /// <returns>0 if the entire string is a valid number in base <paramref name="numberBase"/>, otherwise 1.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_init_set_str", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false), SuppressUnmanagedCodeSecurity]
        public static extern int InitializeAndSet(ref BigInt integer, [MarshalAs(UnmanagedType.LPStr)]string number, int numberBase);

        #endregion

        #region 5.4 Conversion Functions

        /// <summary>
        /// Converts <paramref name="integer"/> to a uint.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be converted to a uint.</param>
        /// <returns>The value of <paramref name="integer"/> as a uint.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_get_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint ToUint(ref BigInt integer);

        /// <summary>
        /// Converts <paramref name="integer"/> to an int.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be converted to an int.</param>
        /// <returns>
        /// The value of <paramref name="integer"/> if <paramref name="integer"/> fits into an int, otherwise
        /// the least significant part of <paramref name="integer"/>, with the same sign as <paramref name="integer"/>.
        /// </returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_get_si", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int ToInt(ref BigInt integer);

        /// <summary>
        /// Converts <paramref name="integer"/> to a double, truncating (rounding towards zero) if necessary.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be converted to a double.</param>
        /// <returns>The value of <paramref name="integer"/> as a double.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_get_d", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern double ToDouble(ref BigInt integer);

        /// <summary>
        /// Converts <paramref name="integer"/> to a double, truncating (rounding towards zero) if necessary, and returning the exponent seperately.
        /// </summary>
        /// <param name="exp">A uint to store the exponent in.</param>
        /// <param name="integer">The <see cref="BigInt"/> to be converted to a double.</param>
        /// <returns>The base of the exponent, greater than or equal to 0.5 and less than 1.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_get_d_2exp", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern double ToDouble(ref uint exp, ref BigInt integer);

        /// <summary>
        /// Converts <paramref name="integer"/> to a string of digits in base <paramref name="numberBase"/>.
        /// </summary>
        /// <param name="stringToSet">A StringBuilder to store the return string in.</param>
        /// <param name="numberBase">The base that <paramref name="integer"/> is to be converted into.</param>
        /// <param name="integer">The <see cref="BigInt"/> to be converted to a string.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_get_str", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false), SuppressUnmanagedCodeSecurity]
        public static extern void ToString([MarshalAs(UnmanagedType.LPStr)]StringBuilder stringToSet, int numberBase, ref BigInt integer);

        #endregion

        #region 5.5 Arithmetic Functions

        /// <summary>
        /// Sets <paramref name="sum"/> to <paramref name="addend1"/> + <paramref name="addend2"/>.
        /// </summary>
        /// <param name="sum">The <see cref="BigInt"/> used to store the sum.</param>
        /// <param name="addend1">The first addend.</param>
        /// <param name="addend2">The second addend.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_add", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Add(ref BigInt sum, ref BigInt addend1, ref BigInt addend2);

        /// <summary>
        /// Sets <paramref name="sum"/> to <paramref name="addend1"/> + <paramref name="addend2"/>.
        /// </summary>
        /// <param name="sum">The <see cref="BigInt"/> used to store the sum.</param>
        /// <param name="addend1">The first addend.</param>
        /// <param name="addend2">The second addend.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_add_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Add(ref BigInt sum, ref BigInt addend1, uint addend2);

        /// <summary>
        /// Sets <paramref name="difference"/> to <paramref name="minuend"/> - <paramref name="subtrahend"/>.
        /// </summary>
        /// <param name="difference">The <see cref="BigInt"/> used to store the difference.</param>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_sub", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Subtract(ref BigInt difference, ref BigInt minuend, ref BigInt subtrahend);

        /// <summary>
        /// Sets <paramref name="difference"/> to <paramref name="minuend"/> - <paramref name="subtrahend"/>.
        /// </summary>
        /// <param name="difference">The <see cref="BigInt"/> used to store the difference.</param>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_sub_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Subtract(ref BigInt difference, ref BigInt minuend, uint subtrahend);

        /// <summary>
        /// Sets <paramref name="difference"/> to <paramref name="minuend"/> - <paramref name="subtrahend"/>.
        /// </summary>
        /// <param name="difference">The <see cref="BigInt"/> used to store the difference.</param>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_ui_sub", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Subtract(ref BigInt difference, uint minuend, ref BigInt subtrahend);

        /// <summary>
        /// Sets <paramref name="product"/> to <paramref name="factor1"/> * <paramref name="factor2"/>.
        /// </summary>
        /// <param name="product">The <see cref="BigInt"/> used to store the product.</param>
        /// <param name="factor1">The first factor.</param>
        /// <param name="factor2">The second factor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_mul", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Multiply(ref BigInt product, ref BigInt factor1, ref BigInt factor2);

        /// <summary>
        /// Sets <paramref name="product"/> to <paramref name="factor1"/> * <paramref name="factor2"/>.
        /// </summary>
        /// <param name="product">The <see cref="BigInt"/> used to store the product.</param>
        /// <param name="factor1">The first factor.</param>
        /// <param name="factor2">The second factor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_mul_si", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Multiply(ref BigInt product, ref BigInt factor1, int factor2);

        /// <summary>
        /// Sets <paramref name="product"/> to <paramref name="factor1"/> * <paramref name="factor2"/>.
        /// </summary>
        /// <param name="product">The <see cref="BigInt"/> used to store the product.</param>
        /// <param name="factor1">The first factor.</param>
        /// <param name="factor2">The second factor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_mul_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Multiply(ref BigInt product, ref BigInt factor1, uint factor2);

        /// <summary>
        /// Sets <paramref name="integer"/> to <paramref name="integer"/> + (<paramref name="addend"/> * <paramref name="factor"/>).
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be added to.</param>
        /// <param name="addend">The first factor of the number to be added to <paramref name="integer"/>.</param>
        /// <param name="factor">The second factor of the number to be added to <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_addmul", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void AddAndMultiply(ref BigInt integer, ref BigInt addend, ref BigInt factor);

        /// <summary>
        /// Sets <paramref name="integer"/> to <paramref name="integer"/> + (<paramref name="addend"/> * <paramref name="factor"/>).
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be added to.</param>
        /// <param name="addend">The first factor of the number to be added to <paramref name="integer"/>.</param>
        /// <param name="factor">The second factor of the number to be added to <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_addmul_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void AddAndMultiply(ref BigInt integer, ref BigInt addend, uint factor);

        /// <summary>
        /// Sets <paramref name="integer"/> to <paramref name="integer"/> - (<paramref name="subtrahend"/> * <paramref name="factor"/>).
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be subtracted from.</param>
        /// <param name="subtrahend">The first factor of the number to be subtracted from <paramref name="integer"/>.</param>
        /// <param name="factor">The second factor of the number to be subtracted from <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_submul", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void SubtractAndMultiply(ref BigInt integer, ref BigInt subtrahend, ref BigInt factor);

        /// <summary>
        /// Sets <paramref name="integer"/> to <paramref name="integer"/> - (<paramref name="subtrahend"/> * <paramref name="factor"/>).
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> to be subtracted from.</param>
        /// <param name="subtrahend">The first factor of the number to be subtracted from <paramref name="integer"/>.</param>
        /// <param name="factor">The second factor of the number to be subtracted from <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_submul_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void SubtractAndMultiply(ref BigInt integer, ref BigInt subtrahend, uint factor);

        /// <summary>
        /// Sets <paramref name="result"/> to <paramref name="number"/> * 2^<paramref name="bits"/> (a left shift by <paramref name="bits"/> bits).
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number">The <see cref="BigInt"/> to be left-shifted.</param>
        /// <param name="bits">The number of bits to shift <paramref name="number"/> by.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_mul_2exp", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void LeftShift(ref BigInt result, ref BigInt number, uint bits);

        /// <summary>
        /// Sets <paramref name="result"/> to -<paramref name="number"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> to be set.</param>
        /// <param name="number">The <see cref="BigInt"/> to be negated.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_neg", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Neg(ref BigInt result, ref BigInt number);

        /// <summary>
        /// Sets <paramref name="result"/> to the absolute value of <paramref name="number"/>
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> to be set.</param>
        /// <param name="number">The <see cref="BigInt"/> to be read from.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_abs", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Abs(ref BigInt result, ref BigInt number);

        #endregion

        #region 5.6 Division Functions

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a quotient <paramref name="quotient"/>.
        /// The quotient is rounded up towards positive infinity.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cdiv_q", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void DivideCeil(ref BigInt quotient, ref BigInt dividend, ref BigInt divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a remainder <paramref name="remainder"/>.
        /// The remainder has the opposite sign to the divisor.
        /// </summary>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cdiv_r", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void ModCeil(ref BigInt remainder, ref BigInt dividend, ref BigInt divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a quotient <paramref name="quotient"/> and a remainder <paramref name="remainder"/>.
        /// The quotient is rounded up towards positive infinity, and the remainder has the opposite sign to the divisor.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="remainder">The remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cdiv_qr", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void DivideCeil(ref BigInt quotient, ref BigInt remainder, ref BigInt dividend, ref BigInt divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a quotient <paramref name="quotient"/>.
        /// The quotient is rounded up towards positive infinity.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The remainder.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cdiv_q_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint DivideCeil(ref BigInt quotient, ref BigInt dividend, uint divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a remainder <paramref name="remainder"/>.
        /// The remainder has the opposite sign to the divisor.
        /// </summary>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The remainder.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cdiv_r_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint ModCeil(ref BigInt remainder, ref BigInt dividend, uint divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a quotient <paramref name="quotient"/> and a remainder <paramref name="remainder"/>.
        /// The quotient is rounded up towards positive infinity, and the remainder has the opposite sign to the divisor.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The remainder.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cdiv_qr_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint DivideCeil(ref BigInt quotient, ref BigInt remainder, ref BigInt dividend, uint divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by 2^<paramref name="bits"/>, forming a quotient <paramref name="quotient"/> (a right shift by <paramref name="bits"/> bits).
        /// The quotient is rounded up towards positive infinity.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="dividend">The <see cref="BigInt"/> to be right-shifted.</param>
        /// <param name="bits">The number of bits to shift <paramref name="dividend"/> by.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cdiv_q_2exp", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void RightShiftCeil(ref BigInt quotient, ref BigInt dividend, uint bits);

        /// <summary>
        /// Divides <paramref name="dividend"/> by 2^<paramref name="bits"/>, forming a remainder <paramref name="remainder"/> (a <paramref name="bits"/>-bit bit mask).
        /// The remainder has the opposite sign to the divisor.
        /// </summary>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The <see cref="BigInt"/> to be bit-masked.</param>
        /// <param name="bits">The number of bits to use in the bit mask of <paramref name="dividend"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cdiv_r_2exp", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void BitMaskCeil(ref BigInt remainder, ref BigInt dividend, uint bits);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a quotient <paramref name="quotient"/>.
        /// The quotient is rounded down towards negative infinity.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_fdiv_q", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void DivideFloor(ref BigInt quotient, ref BigInt dividend, ref BigInt divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a remainder <paramref name="remainder"/>.
        /// The remainder has the same sign as the divisor.
        /// </summary>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_fdiv_r", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void ModFloor(ref BigInt remainder, ref BigInt dividend, ref BigInt divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a quotient <paramref name="quotient"/> and a remainder <paramref name="remainder"/>.
        /// The quotient is rounded down towards negative infinity, and the remainder has the same sign as the divisor.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_fdiv_qr", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void DivideFloor(ref BigInt quotient, ref BigInt remainder, ref BigInt dividend, ref BigInt divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a quotient <paramref name="quotient"/>.
        /// The quotient is rounded down towards negative infinity.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The remainder.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_fdiv_q_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint DivideFloor(ref BigInt quotient, ref BigInt dividend, uint divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a remainder <paramref name="remainder"/>.
        /// The remainder has the same sign as the divisor.
        /// </summary>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The remainder.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_fdiv_r_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint ModFloor(ref BigInt remainder, ref BigInt dividend, uint divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a quotient <paramref name="quotient"/> and a remainder <paramref name="remainder"/>.
        /// The quotient is rounded down towards negative infinity, and the remainder has the same sign as the divisor.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The remainder.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_fdiv_qr_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint DivideFloor(ref BigInt quotient, ref BigInt remainder, ref BigInt dividend, uint divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by 2^<paramref name="bits"/>, forming a quotient <paramref name="quotient"/> (a right shift by <paramref name="bits"/> bits).
        /// The quotient is rounded down towards negative infinity.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="dividend">The <see cref="BigInt"/> to be right-shifted.</param>
        /// <param name="bits">The number of bits to shift <paramref name="dividend"/> by.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_fdiv_q_2exp", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void RightShiftFloor(ref BigInt quotient, ref BigInt dividend, uint bits);

        /// <summary>
        /// Divides <paramref name="dividend"/> by 2^<paramref name="bits"/>, forming a remainder <paramref name="remainder"/> (a <paramref name="bits"/>-bit bit mask).
        /// The remainder has the same sign as the divisor.
        /// </summary>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The <see cref="BigInt"/> to be bit-masked.</param>
        /// <param name="bits">The number of bits to use in the bit mask of <paramref name="dividend"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_fdiv_r_2exp", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void BitMaskFloor(ref BigInt remainder, ref BigInt dividend, uint bits);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a quotient <paramref name="quotient"/>.
        /// The quotient is rounded towards zero.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_tdiv_q", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void DivideTrunc(ref BigInt quotient, ref BigInt dividend, ref BigInt divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a remainder <paramref name="remainder"/>.
        /// The remainder has the same sign as the dividend.
        /// </summary>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_tdiv_r", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void ModTrunc(ref BigInt remainder, ref BigInt dividend, ref BigInt divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a quotient <paramref name="quotient"/> and a remainder <paramref name="remainder"/>.
        /// The quotient is rounded towards zero, and the remainder has the same sign as the dividend.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_tdiv_qr", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void DivideTrunc(ref BigInt quotient, ref BigInt remainder, ref BigInt dividend, ref BigInt divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a quotient <paramref name="quotient"/>.
        /// The quotient is rounded towards zero.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The remainder.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_tdiv_q_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint DivideTrunc(ref BigInt quotient, ref BigInt dividend, uint divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a remainder <paramref name="remainder"/>.
        /// The remainder has the same sign as the dividend.
        /// </summary>
        /// <param name="remainder"><see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The remainder.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_tdiv_r_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint ModTrunc(ref BigInt remainder, ref BigInt dividend, uint divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, forming a quotient <paramref name="quotient"/> and a remainder <paramref name="remainder"/>.
        /// The quotient is rounded towards zero, and the remainder has the same sign as the dividend.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The remainder.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_tdiv_qr_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint DivideTrunc(ref BigInt quotient, ref BigInt remainder, ref BigInt dividend, uint divisor);

        /// <summary>
        /// Divides <paramref name="dividend"/> by 2^<paramref name="bits"/>, forming a quotient <paramref name="quotient"/> (a right shift by <paramref name="bits"/> bits).
        /// The quotient is rounded towards zero.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="dividend">The <see cref="BigInt"/> to be right-shifted.</param>
        /// <param name="bits">The number of bits to shift <paramref name="dividend"/> by.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_tdiv_q_2exp", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void RightShiftTrunc(ref BigInt quotient, ref BigInt dividend, uint bits);

        /// <summary>
        /// Divides <paramref name="dividend"/> by 2^<paramref name="bits"/>, forming a remainder <paramref name="remainder"/> (a <paramref name="bits"/>-bit bit mask).
        /// The remainder has the same sign as the dividend.
        /// </summary>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The <see cref="BigInt"/> to be bit-masked.</param>
        /// <param name="bits">The number of bits to use in the bit mask of <paramref name="dividend"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_tdiv_r_2exp", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void BitMaskTrunc(ref BigInt remainder, ref BigInt dividend, uint bits);

        /// <summary>
        /// Sets <paramref name="remainder"/> to <paramref name="dividend"/> mod <paramref name="divisor"/>.
        /// </summary>
        /// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_mod", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Mod(ref BigInt remainder, ref BigInt dividend, ref BigInt divisor);

        /////// <summary>
        /////// Sets <paramref name="remainder"/> to <paramref name="dividend"/> mod <paramref name="divisor"/>.
        /////// </summary>
        /////// <param name="remainder">The <see cref="BigInt"/> used to store the remainder.</param>
        /////// <param name="dividend">The dividend.</param>
        /////// <param name="divisor">The divisor.</param>
        /////// <returns>The remainder.</returns>
        ////[DllImport("libgmp-3", EntryPoint = "__gmpz_mod_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity, Obsolete("Currently does not work. Use ModFloor(ref remainder, ref dividend, divisor) instead", true)]
        ////public static extern uint Mod(ref BigInt remainder, ref BigInt dividend, uint divisor);

        /// <summary>
        /// Sets <paramref name="quotient"/> to <paramref name="dividend"/> / <paramref name="divisor"/>.
        /// This function produces a correct result only when it is known in advance that <paramref name="divisor"/> divides <paramref name="dividend"/> exactly.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_divexact", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void DivideExact(ref BigInt quotient, ref BigInt dividend, ref BigInt divisor);

        /// <summary>
        /// Sets <paramref name="quotient"/> to <paramref name="dividend"/> / <paramref name="divisor"/>.
        /// This function produces a correct result only when it is known in advance that <paramref name="divisor"/> divides <paramref name="dividend"/> exactly.
        /// </summary>
        /// <param name="quotient">The <see cref="BigInt"/> used to store the quotient.</param>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_divexact_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void DivideExact(ref BigInt quotient, ref BigInt dividend, uint divisor);

        /// <summary>
        /// Tests <paramref name="dividend"/> for divisibility by <paramref name="divisor"/>.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>Non-zero if <paramref name="dividend"/> is exactly divisible by <paramref name="divisor"/>.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_divisible_p", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Divisible(ref BigInt dividend, ref BigInt divisor);

        /// <summary>
        /// Tests <paramref name="dividend"/> for divisibility by <paramref name="divisor"/>.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>Non-zero if <paramref name="dividend"/> is exactly divisible by <paramref name="divisor"/>.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_divisible_ui_p", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Divisible(ref BigInt dividend, uint divisor);

        /// <summary>
        /// Tests <paramref name="dividend"/> for divisibility by 2^<paramref name="bits"/>.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="bits">The number of bits.</param>
        /// <returns>Non-zero if <paramref name="dividend"/> is exactly divisible by 2^<paramref name="bits"/>.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_divisible_2exp_p", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int DivisibleExp(ref BigInt dividend, uint bits);

        /// <summary>
        /// Tests <paramref name="dividend1"/> for congruency to <paramref name="dividend2"/> modulo <paramref name="divisor"/>.
        /// </summary>
        /// <param name="dividend1">The first dividend.</param>
        /// <param name="dividend2">The second dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>Non-zero if <paramref name="dividend1"/> is congruent to <paramref name="dividend2"/> modulo <paramref name="divisor"/>.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_congruent_p", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Congruent(ref BigInt dividend1, ref BigInt dividend2, ref BigInt divisor);

        /// <summary>
        /// Tests <paramref name="dividend1"/> for congruency to <paramref name="dividend2"/> modulo <paramref name="divisor"/>.
        /// </summary>
        /// <param name="dividend1">The first dividend.</param>
        /// <param name="dividend2">The second dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>Non-zero if <paramref name="dividend1"/> is congruent to <paramref name="dividend2"/> modulo <paramref name="divisor"/>.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_congruent_ui_p", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Congruent(ref BigInt dividend1, uint dividend2, uint divisor);

        /// <summary>
        /// Tests <paramref name="dividend1"/> for congruency to <paramref name="dividend2"/> modulo 2^<paramref name="bits"/>.
        /// </summary>
        /// <param name="dividend1">The first dividend.</param>
        /// <param name="dividend2">The second dividened.</param>
        /// <param name="bits">The number of bits.</param>
        /// <returns>Non-zero if <paramref name="dividend1"/> is congruent to <paramref name="dividend2"/> modulo 2^<paramref name="bits"/>.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_congruent_2exp_p", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int CongruentExp(ref BigInt dividend1, ref BigInt dividend2, uint bits);

        #endregion

        #region 5.7 Exponentiation Functions

        /// <summary>
        /// Sets <paramref name="result"/> to <paramref name="baseNumber"/>^<paramref name="exponent"/> mod <paramref name="mod"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="baseNumber">The base of the exponent.</param>
        /// <param name="exponent">The exponent.</param>
        /// <param name="mod">The modulus.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_powm", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void PowMod(ref BigInt result, ref BigInt baseNumber, ref BigInt exponent, ref BigInt mod);

        /// <summary>
        /// Sets <paramref name="result"/> to <paramref name="baseNumber"/>^<paramref name="exponent"/> mod <paramref name="mod"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="baseNumber">The base of the exponent.</param>
        /// <param name="exponent">The exponent.</param>
        /// <param name="mod">The modulus.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_powm_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void PowMod(ref BigInt result, ref BigInt baseNumber, uint exponent, ref BigInt mod);

        /// <summary>
        /// Sets <paramref name="result"/> to <paramref name="baseNumber"/>^<paramref name="exponent"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="baseNumber">The base of the exponent.</param>
        /// <param name="exponent">The exponent.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_pow_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Pow(ref BigInt result, ref BigInt baseNumber, uint exponent);

        /// <summary>
        /// Sets <paramref name="result"/> to <paramref name="baseNumber"/>^<paramref name="exponent"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="baseNumber">The base of the exponent.</param>
        /// <param name="exponent">The exponent.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_ui_pow_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Pow(ref BigInt result, uint baseNumber, uint exponent);

        #endregion

        #region 5.8 Root Extraction Functions

        /// <summary>
        /// Sets <paramref name="result"/> to the truncated integer part of the <paramref name="index"/>th root of <paramref name="baseNumber"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="baseNumber">The <see cref="BigInt"/> whose root is to be taken.</param>
        /// <param name="index">The index of the root to be taken.</param>
        /// <returns>Non-zero if the computation was exact (if <paramref name="result"/> is <paramref name="baseNumber"/> to the <paramref name="index"/>th power.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_root", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Root(ref BigInt result, ref BigInt baseNumber, uint index);

        /// <summary>
        /// Sets <paramref name="root"/> to the truncated integer part of the <paramref name="index"/>th root of <paramref name="baseNumber"/>.
        /// Sets <paramref name="remainder"/> to the remainder, (<paramref name="index"/> - <paramref name="baseNumber"/>^<paramref name="index"/>).
        /// </summary>
        /// <param name="root">The <see cref="BigInt"/> used to store the root.</param>
        /// <param name="remainder">The remainder, (<paramref name="index"/> - <paramref name="baseNumber"/>^<paramref name="index"/>).</param>
        /// <param name="baseNumber">The <see cref="BigInt"/> whose root is to be taken.</param>
        /// <param name="index">The index of the root to be taken.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_rootrem", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void RootRemainder(ref BigInt root, ref BigInt remainder, ref BigInt baseNumber, uint index);

        /// <summary>
        /// Sets <paramref name="result"/> to the truncated integer part of the square root of <paramref name="baseNumber"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="baseNumber">The <see cref="BigInt"/> whose square root is to be taken.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_sqrt", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Sqrt(ref BigInt result, ref BigInt baseNumber);

        /// <summary>
        /// Sets <paramref name="root"/> to the truncated integer part of the square root of <paramref name="baseNumber"/>.
        /// Sets <paramref name="remainder"/> to the remainder, (<paramref name="baseNumber"/> - <paramref name="root"/>^2).
        /// </summary>
        /// <param name="root">The <see cref="BigInt"/> used to store the square root.</param>
        /// <param name="remainder">The remainder, (<paramref name="baseNumber"/> - <paramref name="root"/>^2).</param>
        /// <param name="baseNumber">The <see cref="BigInt"/> whose square root is to be taken.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_sqrtrem", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void SqrtRemainder(ref BigInt root, ref BigInt remainder, ref BigInt baseNumber);

        /// <summary>
        /// Tests whether <paramref name="number"/> is a perfect power (if there exist integers a and b, with b > 1, such that <paramref name="number"/> = a^b).
        /// </summary>
        /// <param name="number">The <see cref="BigInt"/> to be tested.</param>
        /// <returns>Non-zero if <paramref name="number"/> is a perfect power.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_perfect_power_p", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int PerfectPower(ref BigInt number);

        /// <summary>
        /// Tests whether <paramref name="number"/> is a perfect square (if the square root of <paramref name="number"/> is an integer).
        /// </summary>
        /// <param name="number">The <see cref="BigInt"/> to be tested.</param>
        /// <returns>Non-zero if <paramref name="number"/> is a perfect square.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_perfect_square_p", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int PerfectSquare(ref BigInt number);

        #endregion

        #region 5.9 Number Theoretic Functions

        /// <summary>
        /// Determines whether <paramref name="number"/> is prime.
        /// </summary>
        /// <param name="number">The <see cref="BigInt"/> to be tested for primality.</param>
        /// <param name="repititions">The number of Miller-Rabin tests to be done on <paramref name="number"/>.</param>
        /// <returns>
        /// 2 if <paramref name="number"/> is definitely prime, 1 if <paramref name="number"/> is probably prime (without being certain),
        /// and 0 if <paramref name="number"/> is definitely composite.
        /// </returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_probab_prime_p", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int ProbablePrime(ref BigInt number, int repititions);

        /// <summary>
        /// Sets <paramref name="result"/> to the next prime greater than <paramref name="number"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number">The <see cref="BigInt"/> that serves as the input.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_nextprime", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void NextPrime(ref BigInt result, ref BigInt number);

        /// <summary>
        /// Sets <paramref name="result"/> to the greatest common divisor of <paramref name="number1"/> and <paramref name="number2"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_gcd", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Gcd(ref BigInt result, ref BigInt number1, ref BigInt number2);

        /// <summary>
        /// Computes the greatest common divisor of <paramref name="number1"/> and <paramref name="number2"/>.
        /// If <paramref name="result"/> is not null, stores the result there.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        /// <returns>The result.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_gcd_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint Gcd(ref BigInt result, ref BigInt number1, uint number2);

        /// <summary>
        /// Sets <paramref name="gcd"/> to the greatest common divisor of <paramref name="number1"/> and <paramref name="number2"/>, 
        /// and in addition sets <paramref name="coefficient1"/> and <paramref name="coefficient2"/> to coefficients satisfying
        /// <paramref name="coefficient1"/>*<paramref name="number1"/> + <paramref name="coefficient2"/>*<paramref name="number2"/> = <paramref name="gcd"/>.
        /// </summary>
        /// <param name="gcd">The <see cref="BigInt"/> used to store the greatest common divisor.</param>
        /// <param name="coefficient1">The <see cref="BigInt"/> used to store the first coefficient.</param>
        /// <param name="coefficient2">The <see cref="BigInt"/> used to store the second. coefficient.</param>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_gcdext", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void GcdExt(ref BigInt gcd, ref BigInt coefficient1, ref BigInt coefficient2, ref BigInt number1, ref BigInt number2);

        /// <summary>
        /// Sets <paramref name="result"/> to the least common multiple of <paramref name="number1"/> and <paramref name="number2"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_lcm", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Lcm(ref BigInt result, ref BigInt number1, ref BigInt number2);

        /// <summary>
        /// Sets <paramref name="result"/> to the least common multiple of <paramref name="number1"/> and <paramref name="number2"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_lcm_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Lcm(ref BigInt result, ref BigInt number1, uint number2);

        /// <summary>
        /// Computes the inverse of <paramref name="number"/> modulo <paramref name="modulus"/> and puts the result in <paramref name="result"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number">The input whose modulus is taken.</param>
        /// <param name="modulus">The input used as the modulus.</param>
        /// <returns>Non-zero if the inverse exists, zero if the inverse doesn't exist.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_invert", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Inv(ref BigInt result, ref BigInt number, ref BigInt modulus);

        /// <summary>
        /// Calculates the Jacobi symbol (<paramref name="numerator"/>/<paramref name="denominator"/>).
        /// </summary>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <returns>The Jacobi symbol.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_jacobi", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Jacobi(ref BigInt numerator, ref BigInt denominator);

        /// <summary>
        /// Calculates the Legendre symbol (<paramref name="numerator"/>/<paramref name="denominator"/>).
        /// </summary>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <returns>The Legendre symbol.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_legendre", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Legendre(ref BigInt numerator, ref BigInt denominator);

        /////// <summary>
        /////// Calculates the Jacobi symbol (<paramref name="numerator"/>/<paramref name="denominator"/>) with the Kronecker extension
        /////// (<paramref name="numerator"/>/2) = (2/<paramref name="numerator"/>) when <paramref name="numerator"/> odd, or
        /////// (<paramref name="numerator"/>/2) = 0 when <paramref name="numerator"/> even.
        /////// </summary>
        /////// <param name="numerator">The numerator.</param>
        /////// <param name="denominator">The denominator.</param>
        /////// <returns>The Jacobi symbol with Kronecker extension.</returns>
        ////[DllImport("libgmp-3", EntryPoint = "__gmpz_kronecker", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity, Obsolete("Currently does not work. Use another overload of Kronecker() instead", true)]
        ////public static extern int Kronecker(ref BigInt numerator, ref BigInt denominator);

        /// <summary>
        /// Calculates the Jacobi symbol (<paramref name="numerator"/>/<paramref name="denominator"/>) with the Kronecker extension
        /// (<paramref name="numerator"/>/2) = (2/<paramref name="numerator"/>) when <paramref name="numerator"/> odd, or
        /// (<paramref name="numerator"/>/2) = 0 when <paramref name="numerator"/> even.
        /// </summary>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <returns>The Jacobi symbol with Kronecker extension.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_kronecker_si", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Kronecker(ref BigInt numerator, int denominator);

        /// <summary>
        /// Calculates the Jacobi symbol (<paramref name="numerator"/>/<paramref name="denominator"/>) with the Kronecker extension
        /// (<paramref name="numerator"/>/2) = (2/<paramref name="numerator"/>) when <paramref name="numerator"/> odd, or
        /// (<paramref name="numerator"/>/2) = 0 when <paramref name="numerator"/> even.
        /// </summary>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <returns>The Jacobi symbol with Kronecker extension.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_kronecker_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Kronecker(ref BigInt numerator, uint denominator);

        /// <summary>
        /// Calculates the Jacobi symbol (<paramref name="numerator"/>/<paramref name="denominator"/>) with the Kronecker extension
        /// (<paramref name="numerator"/>/2) = (2/<paramref name="numerator"/>) when <paramref name="numerator"/> odd, or
        /// (<paramref name="numerator"/>/2) = 0 when <paramref name="numerator"/> even.
        /// </summary>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <returns>The Jacobi symbol with Kronecker extension.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_si_kronecker", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Kronecker(int numerator, ref BigInt denominator);

        /// <summary>
        /// Calculates the Jacobi symbol (<paramref name="numerator"/>/<paramref name="denominator"/>) with the Kronecker extension
        /// (<paramref name="numerator"/>/2) = (2/<paramref name="numerator"/>) when <paramref name="numerator"/> odd, or
        /// (<paramref name="numerator"/>/2) = 0 when <paramref name="numerator"/> even.
        /// </summary>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <returns>The Jacobi symbol with Kronecker extension.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_ui_kronecker", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Kronecker(uint numerator, ref BigInt denominator);

        /// <summary>
        /// Removes all occurrences of the factor <paramref name="factor"/> from <paramref name="number"/> and stores the result in <paramref name="result"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number">The number whose factors are to be removed.</param>
        /// <param name="factor">The factor to be removed from <paramref name="number"/>.</param>
        /// <returns>The number of occurrences that were removed.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_remove", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint Remove(ref BigInt result, ref BigInt number, ref BigInt factor);

        /// <summary>
        /// Sets <paramref name="result"/> to <paramref name="number"/>!, the factorial of <paramref name="number"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number">The number whose factorial is to be taken.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_fac_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Factorial(ref BigInt result, uint number);

        /// <summary>
        /// Computes the binomial coefficient (<paramref name="number1"/> <paramref name="number2"/>) and stores the result in <paramref name="result"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number1">The first input.</param>
        /// <param name="number2">The second input.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_bin_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Binomial(ref BigInt result, ref BigInt number1, uint number2);

        /// <summary>
        /// Computes the binomial coefficient (<paramref name="number1"/> <paramref name="number2"/>) and stores the result in <paramref name="result"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number1">The first input.</param>
        /// <param name="number2">The second input.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_bin_uiui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Binomial(ref BigInt result, uint number1, uint number2);

        /// <summary>
        /// Sets <paramref name="fibonacciNumber"/> to the <paramref name="index"/>'th Fibonacci number.
        /// </summary>
        /// <param name="fibonacciNumber">The <see cref="BigInt"/> used to store the <paramref name="index"/>'th Fibonacci number.</param>
        /// <param name="index">The index of the desired Fibonacci number.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_fib_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Fibonacci(ref BigInt fibonacciNumber, uint index);

        /// <summary>
        /// Sets <paramref name="fibonacciNumber"/> to the <paramref name="index"/>'th Fibonacci number, and 
        /// <paramref name="previousFibonacciNumber"/> to the (<paramref name="index"/> - 1)'th Fibonacci number.
        /// </summary>
        /// <param name="fibonacciNumber">The <see cref="BigInt"/> used to store the <paramref name="index"/>'th Fibonacci number.</param>
        /// <param name="previousFibonacciNumber">The <see cref="BigInt"/> used to store the (<paramref name="index"/> - 1)'th Fibonacci number.</param>
        /// <param name="index">The index of the desired Fibonacci number.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_fib2_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Fibonacci(ref BigInt fibonacciNumber, ref BigInt previousFibonacciNumber, uint index);

        /// <summary>
        /// Sets <paramref name="lucasNumber"/> to the <paramref name="index"/>'th Lucas number.
        /// </summary>
        /// <param name="lucasNumber">The <see cref="BigInt"/> used to store the <paramref name="index"/>'th Lucas number.</param>
        /// <param name="index">The index of the desired Lucas number.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_lucnum_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Lucas(ref BigInt lucasNumber, uint index);

        /// <summary>
        /// Sets <paramref name="lucasNumber"/> to the <paramref name="index"/>'th Lucas number, and 
        /// <paramref name="previousLucasNumber"/> to the (<paramref name="index"/> - 1)'th Lucas number.
        /// </summary>
        /// <param name="lucasNumber">The <see cref="BigInt"/> used to store the <paramref name="index"/>'th Lucas number.</param>
        /// <param name="previousLucasNumber">The <see cref="BigInt"/> used to store the (<paramref name="index"/> - 1)'th Lucas number.</param>
        /// <param name="index">The index of the desired Lucas number.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_lucnum2_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Lucas(ref BigInt lucasNumber, ref BigInt previousLucasNumber, uint index);

        #endregion

        #region 5.10 Comparison Functions

        /// <summary>
        /// Compares <paramref name="number1"/> and <paramref name="number2"/>.
        /// </summary>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        /// <returns>
        /// A positive value if <paramref name="number1"/> is greater than <paramref name="number2"/>, zero if <paramref name="number1"/> is equal to 
        /// <paramref name="number2"/>, or a negative value if <paramref name="number1"/> is less than <paramref name="number2"/>.
        /// </returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cmp", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Compare(ref BigInt number1, ref BigInt number2);

        /// <summary>
        /// Compares <paramref name="number1"/> and <paramref name="number2"/>.
        /// </summary>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        /// <returns>
        /// A positive value if <paramref name="number1"/> is greater than <paramref name="number2"/>, zero if <paramref name="number1"/> is equal to 
        /// <paramref name="number2"/>, or a negative value if <paramref name="number1"/> is less than <paramref name="number2"/>.
        /// </returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cmp_d", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Compare(ref BigInt number1, double number2);

        /// <summary>
        /// Compares <paramref name="number1"/> and <paramref name="number2"/>.
        /// </summary>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        /// <returns>
        /// A positive value if <paramref name="number1"/> is greater than <paramref name="number2"/>, zero if <paramref name="number1"/> is equal to 
        /// <paramref name="number2"/>, or a negative value if <paramref name="number1"/> is less than <paramref name="number2"/>.
        /// </returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cmp_si", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Compare(ref BigInt number1, int number2);

        /// <summary>
        /// Compares <paramref name="number1"/> and <paramref name="number2"/>.
        /// </summary>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        /// <returns>
        /// A positive value if <paramref name="number1"/> is greater than <paramref name="number2"/>, zero if <paramref name="number1"/> is equal to 
        /// <paramref name="number2"/>, or a negative value if <paramref name="number1"/> is less than <paramref name="number2"/>.
        /// </returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cmp_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int Compare(ref BigInt number1, uint number2);

        /// <summary>
        /// Compares the absolute values of <paramref name="number1"/> and <paramref name="number2"/>.
        /// </summary>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        /// <returns>
        /// A positive value if |<paramref name="number1"/>| is greater than |<paramref name="number2"/>|, zero if |<paramref name="number1"/>| is equal to
        /// |<paramref name="number2"/>|, or a negative value if |<paramref name="number1"/>| is greater than |<paramref name="number2"/>|.
        /// </returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cmpabs", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int CompareAbs(ref BigInt number1, ref BigInt number2);

        /// <summary>
        /// Compares the absolute values of <paramref name="number1"/> and <paramref name="number2"/>.
        /// </summary>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        /// <returns>
        /// A positive value if |<paramref name="number1"/>| is greater than |<paramref name="number2"/>|, zero if |<paramref name="number1"/>| is equal to
        /// |<paramref name="number2"/>|, or a negative value if |<paramref name="number1"/>| is greater than |<paramref name="number2"/>|.
        /// </returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cmpabs_d", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int CompareAbs(ref BigInt number1, double number2);

        /// <summary>
        /// Compares the absolute values of <paramref name="number1"/> and <paramref name="number2"/>.
        /// </summary>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        /// <returns>
        /// A positive value if |<paramref name="number1"/>| is greater than |<paramref name="number2"/>|, zero if |<paramref name="number1"/>| is equal to
        /// |<paramref name="number2"/>|, or a negative value if |<paramref name="number1"/>| is greater than |<paramref name="number2"/>|.
        /// </returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_cmpabs_ui", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern int CompareAbs(ref BigInt number1, uint number2);

        /////// <summary>
        /////// Determines the sign of <paramref name="number"/>.
        /////// </summary>
        /////// <param name="number">The <see cref="BigInt"/> whose sign is to be determined.</param>
        /////// <returns>+1 if <paramref name="number"/> is greater than 0, 0 if <paramref name="number"/> is equal to 0, and -1 if <paramref name="number"/> is less than 0.</returns>
        ////[DllImport("libgmp-3", EntryPoint = "__gmpz_sgn", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity, Obsolete("Currently does not work. Use Compare(ref number, 0) instead", true)]
        ////public static extern int Sign(ref BigInt number);

        #endregion

        #region 5.11 Logical and Bit Manipulation Functions

        /// <summary>
        /// Sets <paramref name="result"/> to <paramref name="number1"/> bitwise-and <paramref name="number2"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_and", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void And(ref BigInt result, ref BigInt number1, ref BigInt number2);

        /// <summary>
        /// Sets <paramref name="result"/> to <paramref name="number1"/> bitwise inclusive-or <paramref name="number2"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_ior", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Or(ref BigInt result, ref BigInt number1, ref BigInt number2);

        /// <summary>
        /// Sets <paramref name="result"/> to <paramref name="number1"/> bitwise exclusive-or <paramref name="number2"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_xor", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Xor(ref BigInt result, ref BigInt number1, ref BigInt number2);

        /// <summary>
        /// Sets <paramref name="result"/> to the one's complement of <paramref name="number"/>.
        /// </summary>
        /// <param name="result">The <see cref="BigInt"/> used to store the result.</param>
        /// <param name="number">The <see cref="BigInt"/> whose complement is to be found.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_com", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void Complement(ref BigInt result, ref BigInt number);

        /// <summary>
        /// Finds the population count (the number of 1 bits in the binary representation) of <paramref name="number"/>.
        /// </summary>
        /// <param name="number">The <see cref="BigInt"/> whose population count is to be found.</param>
        /// <returns>
        /// If <paramref name="number"/> is greater than or equal to 0, the population count of <paramref name="number"/>;
        /// if <paramref name="number"/> is less than 0, uint.MaxValue, the largest possible uint, since the number of 1s is infinite.
        /// </returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_popcount", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint PopulationCount(ref BigInt number);

        /// <summary>
        /// Finds the hamming distance between <paramref name="number1"/> and <paramref name="number2"/> (the number of bit
        /// positions where <paramref name="number1"/> and <paramref name="number2"/> have different bit values).
        /// </summary>
        /// <param name="number1">The first of the two inputs.</param>
        /// <param name="number2">The second of the two inputs.</param>
        /// <returns>
        /// If <paramref name="number1"/> and <paramref name="number2"/> are both greater than or equal to 0 or both less than 0, the hamming distance between the two operands;
        /// if one operand is greater than or equal to 0 and the other is less than 0, uint.MaxValue, the largest possible uint, since the number of bits different is infinite.
        /// </returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_hamdist", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint HammingDistance(ref BigInt number1, ref BigInt number2);

        /// <summary>
        /// Scans <paramref name="number"/>, starting from bit <paramref name="startingBit"/>, towards more significant bits, until the first 0 bit is found.
        /// </summary>
        /// <param name="number">The <see cref="BigInt"/> to be scanned.</param>
        /// <param name="startingBit">The bit to start scanning at.</param>
        /// <returns>The index of the found bit; if no bit is found, uint.MaxValue.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_scan0", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint Scan0(ref BigInt number, uint startingBit);

        /// <summary>
        /// Scans <paramref name="number"/>, starting from bit <paramref name="startingBit"/>, towards more significant bits, until the first 1 bit is found.
        /// </summary>
        /// <param name="number">The <see cref="BigInt"/> to be scanned.</param>
        /// <param name="startingBit">The bit to start scanning at.</param>
        /// <returns>The index of the found bit; if no bit is found, uint.MaxValue.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_scan1", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint Scan1(ref BigInt number, uint startingBit);

        /// <summary>
        /// Sets bit <paramref name="bitIndex"/> in <paramref name="integer"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> whose bit is to be set.</param>
        /// <param name="bitIndex">The bit to set in <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_setbit", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void SetBit(ref BigInt integer, uint bitIndex);

        /// <summary>
        /// Clears bit <paramref name="bitIndex"/> in <paramref name="integer"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> whose bit is to be cleared.</param>
        /// <param name="bitIndex">The bit to clear in <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_clrbit", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void ClearBit(ref BigInt integer, uint bitIndex);

        /// <summary>
        /// Complements bit <paramref name="bitIndex"/> in <paramref name="integer"/>.
        /// </summary>
        /// <param name="integer">The <see cref="BigInt"/> whose bit is to be complemented.</param>
        /// <param name="bitIndex">The bit to complement in <paramref name="integer"/>.</param>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_combit", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern void ComplementBit(ref BigInt integer, uint bitIndex);

        /// <summary>
        /// Tests bit <paramref name="bitIndex"/> in <paramref name="number"/>.
        /// </summary>
        /// <param name="number">The <see cref="BigInt"/> whose bit is to be tested.</param>
        /// <param name="bitIndex">The bit to test in <paramref name="number"/>.</param>
        /// <returns>0 or 1, according to the value of  bit <paramref name="bitIndex"/> in <paramref name="number"/>.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_tstbit", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint TestBit(ref BigInt number, uint bitIndex);

        #endregion

        #region 5.12-5.14 Not Implemented
        // 5.12 Input and Output Functions
        // 5.13 Random Number Functions
        // 5.14 Integer Import and Export
        #endregion

        #region 5.15 Special Functions

        /// <summary>
        /// Returns the size of <paramref name="number"/> measured in the number of digits in base <paramref name="numberBase"/>.
        /// </summary>
        /// <param name="number">The <see cref="BigInt"/> to check the size of.</param>
        /// <param name="numberBase">The base to check the size of <paramref name="number"/> in.</param>
        /// <returns>The size of <paramref name="number"/> in base <paramref name="numberBase"/>.</returns>
        [DllImport("libgmp-3", EntryPoint = "__gmpz_sizeinbase", CharSet = CharSet.Ansi, ExactSpelling = true), SuppressUnmanagedCodeSecurity]
        public static extern uint SizeInBase(ref BigInt number, int numberBase);

        #endregion

        #region 5.16 Not Implemented
        // 5.16 Special Functions
        #endregion
    }
}
