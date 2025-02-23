using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using Fractions.TypeConverters;

namespace Fractions;

/// <summary>
/// A mathematical fraction. A rational number written as a/b (a is the numerator and b the denominator). 
/// The data type is not capable to store NaN (not a number) or infinite.
/// </summary>
[TypeConverter(typeof(FractionTypeConverter))]
[StructLayout(LayoutKind.Sequential)]
[DebuggerTypeProxy(typeof(FractionDebugView))]
public readonly partial struct Fraction :
#if NET
    INumber<Fraction>, ISignedNumber<Fraction>
#else
    IEquatable<Fraction>, IComparable, IComparable<Fraction>, IFormattable 
#endif

{
#pragma warning disable IDE1006
    internal static readonly BigInteger TEN = new(10);

    internal static readonly BigInteger[] PowersOfTen = [
        BigInteger.One, // 10^0 = 1
        new(10), // 10^1 = 10
        new(100), // 10^2 = 100
        new(1000), // 10^3 = 1,000
        new(10000), // 10^4 = 10,000
        new(100000), // 10^5 = 100,000
        new(1000000), // 10^6 = 1,000,000
        new(10000000), // 10^7 = 10,000,000
        new(100000000), // 10^8 = 100,000,000
        new(1000000000), // 10^9 = 1,000,000,000
        new(10000000000), // 10^10 = 10,000,000,000
        new(100000000000), // 10^11 = 100,000,000,000
        new(1000000000000), // 10^12 = 1,000,000,000,000
        new(10000000000000), // 10^13 = 10,000,000,000,000
        new(100000000000000), // 10^14 = 100,000,000,000,000
        new(1000000000000000), // 10^15 = 1,000,000,000,000,000
        new(10000000000000000), // 10^16 = 10,000,000,000,000,000
        new(100000000000000000), // 10^17 = 100,000,000,000,000,000
        new(1000000000000000000) // 10^18 = 1,000,000,000,000,000,000
    ];

#pragma warning restore IDE1006

    private readonly BigInteger? _denominator;

    /// <summary>
    /// No normalization was performed when creating the fraction.
    /// </summary>
    /// <remarks>
    /// If the value is <c>true</c>, we don't know whether it is a real or improper fraction.
    /// The numerator and denominator do not necessarily have to be reduced to the lowest common divisor.
    /// The signs may not be normalized. NaN and/or Infinity may not be normalized.
    /// </remarks>
    private readonly bool _normalizationNotApplied;

    /// <summary>
    /// A fraction representing the positive infinity.
    /// </summary>
    public static Fraction PositiveInfinity { get; } = new(false, BigInteger.One, BigInteger.Zero);

    /// <summary>
    /// A fraction representing the negative infinity.
    /// </summary>
    public static Fraction NegativeInfinity { get; } = new(false, BigInteger.MinusOne, BigInteger.Zero);

    /// <summary>
    /// A fraction representing the result of dividing zero by zero.
    /// </summary>
    public static Fraction NaN { get; } = new(false, BigInteger.Zero, BigInteger.Zero);

    /// <summary>
    ///     A fraction with the reduced/simplified value of 0.
    /// </summary>
    public static Fraction Zero { get; } = new(BigInteger.Zero);

    /// <summary>
    ///     A fraction with the reduced/simplified value of 1.
    /// </summary>
    public static Fraction One { get; } = new(BigInteger.One);

    /// <summary>
    ///     A fraction with the reduced/simplified value of 2.
    /// </summary>
    public static Fraction Two { get; } = new(new BigInteger(2));

    /// <summary>
    ///     A fraction with the reduced/simplified value of -1.
    /// </summary>
    public static Fraction MinusOne { get; } = new(BigInteger.MinusOne);

    /// <summary>
    /// The numerator.
    /// </summary>
    public BigInteger Numerator { get; }

    /// <summary>
    /// The denominator
    /// </summary>
    public BigInteger Denominator => _denominator.GetValueOrDefault(BigInteger.One);

    /// <summary>
    /// <c>true</c> if the fraction represents a valid number or <c>false</c> otherwise.
    /// </summary>
    public bool IsNaN => Denominator.IsZero && Numerator.IsZero;

    /// <summary>
    /// <c>true</c> if the fraction evaluates to positive or negative infinity or <c>false</c> otherwise.
    /// </summary>
    public bool IsInfinity => Denominator.IsZero && !Numerator.IsZero;

    /// <summary>
    /// <c>true</c> if the fraction evaluates to positive infinity or <c>false</c> otherwise.
    /// </summary>
    public bool IsPositiveInfinity => Denominator.IsZero && Numerator.Sign == 1;

    /// <summary>
    /// <c>true</c> if the fraction evaluates to negative infinity or <c>false</c> otherwise.
    /// </summary>
    public bool IsNegativeInfinity => Denominator.IsZero && Numerator.Sign == -1;

    /// <summary>
    ///     <c>true</c> if the value is greater than zero or <c>false</c> otherwise.
    /// </summary>
    public bool IsPositive => Numerator.Sign switch {
        0 => false,
        1 => Denominator.Sign >= 0,
        _ => Denominator.Sign < 0
    };

    /// <summary>
    /// <c>true</c> if the value is lesser than zero or <c>false</c> otherwise.
    /// </summary>
    public bool IsNegative => Numerator.Sign switch {
        0 => false,
        -1 => Denominator.Sign >= 0,
        _ => Denominator.Sign < 0
    };

    /// <summary>
    /// <c>true</c> if the fraction represents the value 0 or <c>false</c> otherwise.
    /// </summary>
    public bool IsZero => Numerator.IsZero && !Denominator.IsZero;

    /// <summary>
    /// The fraction's state.
    /// </summary>
    public FractionState State => _normalizationNotApplied ? FractionState.Unknown : FractionState.IsNormalized;


    #region INumber definitions

#if NET7_0_OR_GREATER
    static int INumberBase<Fraction>.Radix => 10;
    static Fraction ISignedNumber<Fraction>.NegativeOne => MinusOne;
    static Fraction IAdditiveIdentity<Fraction, Fraction>.AdditiveIdentity => Zero;
    static Fraction IMultiplicativeIdentity<Fraction, Fraction>.MultiplicativeIdentity => One;

    /// <summary>Determines if a value represents zero or a positive real number.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="value" /> represents (positive) zero or a positive real number;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    static bool INumberBase<Fraction>.IsPositive(Fraction value) {
        return value.IsPositive;
    }

    /// <summary>Determines if a value represents a negative real number.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="value" /> represents negative zero or a negative real number; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    static bool INumberBase<Fraction>.IsNegative(Fraction value) {
        return value.IsNegative;
    }

    /// <summary>Determines if a value is zero.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="value" /> is zero; otherwise, <see langword="false" />.
    /// </returns>
    static bool INumberBase<Fraction>.IsZero(Fraction value) {
        return value.IsZero;
    }

    static bool INumberBase<Fraction>.IsNormal(Fraction value) {
        return IsFinite(value);
    }

    static bool INumberBase<Fraction>.IsRealNumber(Fraction value) {
        return !value.IsNaN;
    }

    static bool INumberBase<Fraction>.IsComplexNumber(Fraction value) {
        return false;
    }

    static bool INumberBase<Fraction>.IsImaginaryNumber(Fraction value) {
        return false;
    }

    static bool INumberBase<Fraction>.IsSubnormal(Fraction value) {
        return false;
    }

    /// <summary>Determines if a value is infinite.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="value" /> is infinite; otherwise, <see langword="false" />.
    /// </returns>
    static bool INumberBase<Fraction>.IsInfinity(Fraction value) {
        return value.IsInfinity;
    }

    /// <summary>Determines if a value is positive infinity.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="value" /> is positive infinity; otherwise, <see langword="false" />.
    /// </returns>
    static bool INumberBase<Fraction>.IsPositiveInfinity(Fraction value) {
        return value.IsPositiveInfinity;
    }

    /// <summary>Determines if a value is negative infinity.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="value" /> is negative infinity; otherwise, <see langword="false" />.
    /// </returns>
    static bool INumberBase<Fraction>.IsNegativeInfinity(Fraction value) {
        return value.IsNegativeInfinity;
    }

    /// <summary>Determines if a value is NaN.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="value" /> is NaN; otherwise, <see langword="false" />.
    /// </returns>
    static bool INumberBase<Fraction>.IsNaN(Fraction value) {
        return value.IsNaN;
    }
#endif

    /// <summary>
    ///     Determines whether the given Fraction is in canonical form.
    /// </summary>
    /// <param name="value">The Fraction to check.</param>
    /// <returns>True if the Fraction is in canonical form, otherwise false.</returns>
    /// <remarks>
    ///     A Fraction is considered to be in canonical form if its denominator is one,
    ///     or if its numerator and denominator are coprime numbers (their greatest common divisor is one).
    /// </remarks>
    public static bool IsCanonical(Fraction value) {
        var numerator = value.Numerator;
        var denominator = value.Denominator;
        if (denominator.IsOne) {
            return true;
        }

        if (numerator.IsZero) {
            return denominator.IsZero; // if we want to consider NaN as "canonical"
        }

        if (denominator.IsZero) {
            return numerator.IsOne || numerator == BigInteger.MinusOne;
        }

        return BigInteger.GreatestCommonDivisor(numerator, denominator).IsOne;
    }

    /// <summary>Determines if a value represents an even integral number.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="value" /> is an even integer; otherwise, <see langword="false" />.
    /// </returns>
    public static bool IsEvenInteger(Fraction value) {
        var fraction = value.Reduce();
        return fraction.Denominator.IsOne && fraction.Numerator.IsEven;
    }

    /// <summary>Determines if a value is finite.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="value" /> is finite; otherwise, <see langword="false" />.
    /// </returns>
    public static bool IsFinite(Fraction value) {
        return !value.Denominator.IsZero;
    }

    /// <summary>Determines if a value represents an integral number.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="value" /> is an integer; otherwise, <see langword="false" />.
    /// </returns>
    public static bool IsInteger(Fraction value) {
        var denominator = BigInteger.Abs(value.Denominator);
        if (denominator.IsOne) {
            return true;
        }

        if (denominator.IsZero) {
            return false;
        }

        var numerator = BigInteger.Abs(value.Numerator);
        if (numerator.IsZero) {
            return true;
        }
        
        return numerator >= denominator && (numerator % denominator).IsZero;
    }

    /// <summary>Determines if a value represents an odd integral number.</summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="value" /> is an odd integer; otherwise, <see langword="false" />.
    /// </returns>
    public static bool IsOddInteger(Fraction value) {
        var fraction = value.Reduce();
        return fraction.Denominator.IsOne && !fraction.Numerator.IsEven;
    }

    #endregion
}
