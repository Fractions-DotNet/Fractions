using System;
using System.Numerics;
using Fractions.Properties;

namespace Fractions;

public readonly partial struct Fraction {
    /// <summary>
    /// Rounds the given Fraction to the specified precision using <see cref="MidpointRounding.ToEven"/> rounding strategy.
    /// </summary>
    /// <param name="fraction">The Fraction to be rounded.</param>
    /// <param name="decimals">The number of significant decimal places (precision) in the return value.</param>
    /// <returns>The number that <paramref name="fraction" /> is rounded to using the <see cref="MidpointRounding.ToEven"/> rounding strategy and with a precision of <paramref name="decimals" />. If the precision of <paramref name="fraction" /> is less than <paramref name="decimals" />, <paramref name="fraction" /> is returned unchanged.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="decimals"/> is less than 0</exception>
    public static Fraction Round(Fraction fraction, int decimals) =>
        Round(fraction, decimals, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds the given Fraction to the specified precision using the specified rounding strategy.
    /// </summary>
    /// <param name="fraction">The Fraction to be rounded.</param>
    /// <param name="decimals">The number of significant decimal places (precision) in the return value.</param>
    /// <param name="mode">Specifies the strategy that mathematical rounding methods should use to round a number.</param>
    /// <returns>The number that <paramref name="fraction" /> is rounded to using the <paramref name="mode" /> rounding strategy and with a precision of <paramref name="decimals" />. If the precision of <paramref name="fraction" /> is less than <paramref name="decimals" />, <paramref name="fraction" /> is returned unchanged.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="decimals"/> is less than 0</exception>
    /// <remarks>The resulting Fraction would be automatically reduced to it's lowest terms.</remarks>
    public static Fraction Round(Fraction fraction, int decimals, MidpointRounding mode) {
        return Round(fraction, decimals, mode, true);
    }

    /// <summary>
    /// Rounds the given Fraction to the specified precision using the specified rounding strategy.
    /// </summary>
    /// <param name="fraction">The Fraction to be rounded.</param>
    /// <param name="decimals">The number of significant decimal places (precision) in the return value.</param>
    /// <param name="mode">Specifies the strategy that mathematical rounding methods should use to round a number.</param>
    /// <param name="normalize">A boolean flag indicating whether to reduce the resulting Fraction after rounding. If set to true, the Fraction is reduced if possible; otherwise, the Fraction is left as is.</param>
    /// <returns>The number that <paramref name="fraction" /> is rounded to using the <paramref name="mode" /> rounding strategy and with a precision of <paramref name="decimals" />. If the precision of <paramref name="fraction" /> is less than <paramref name="decimals" />, <paramref name="fraction" /> is returned unchanged.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="decimals"/> is less than 0</exception>
    public static Fraction Round(Fraction fraction, int decimals, MidpointRounding mode, bool normalize) {
#if NET
        ArgumentOutOfRangeException.ThrowIfNegative(decimals);
#else
        if (decimals < 0) {
            throw new ArgumentOutOfRangeException(nameof(decimals));
        }
#endif
        if (normalize) {
            var numerator = fraction.Numerator;
            if (numerator.IsZero) {
                return fraction.Reduce();
            }

            var denominator = fraction.Denominator;
            if (denominator.IsOne || denominator.IsZero) {
                return fraction.Reduce();
            }

            var factor = PowerOfTen(decimals);
            var roundedNumerator = RoundToBigInteger(numerator * factor, denominator, mode);
            return ReduceSigned(roundedNumerator, factor);
        } else {
            var numerator = fraction.Numerator;
            if (numerator.IsZero) {
                return fraction;
            }

            var denominator = fraction.Denominator;
            if (denominator.IsOne || denominator.IsZero) {
                return fraction;
            }
            
            var factor = PowerOfTen(decimals);
            var roundedNumerator = RoundToBigInteger(numerator * factor, denominator, mode);
            return new Fraction(true, roundedNumerator, factor);
        }
    }

    /// <summary>
    /// Rounds the given Fraction to the specified precision using <see cref="MidpointRounding.ToEven"/> rounding strategy.
    /// </summary>
    /// <param name="fraction">The Fraction to be rounded.</param>
    /// <returns>The number as <see cref="BigInteger"/> that <paramref name="fraction" /> is rounded to using the <see cref="MidpointRounding.ToEven"/> rounding strategy.</returns>
    public static BigInteger RoundToBigInteger(Fraction fraction) =>
        RoundToBigInteger(fraction, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds the given Fraction to the specified precision using the specified rounding strategy.
    /// </summary>
    /// <param name="fraction">The Fraction to be rounded.</param>
    /// <param name="mode">Specifies the strategy that mathematical rounding methods should use to round a number.</param>
    /// <returns>The number as <see cref="BigInteger"/> that <paramref name="fraction" /> is rounded to using the <paramref name="mode" /> rounding strategy.</returns>
    public static BigInteger RoundToBigInteger(Fraction fraction, MidpointRounding mode) =>
        RoundToBigInteger(fraction.Numerator, fraction.Denominator, mode);

    /// <summary>
    /// Rounds the given Fraction to the specified precision using the specified rounding strategy.
    /// </summary>
    /// <param name="numerator">The numerator of the fraction to be rounded.</param>
    /// <param name="denominator">The denominator of the fraction to be rounded.</param>
    /// <param name="mode">Specifies the strategy that mathematical rounding methods should use to round a number.</param>
    /// <returns>The number rounded to using the <paramref name="mode" /> rounding strategy.</returns>
    /// <exception cref="OverflowException">
    ///     Thrown when the denominator is zero, meaning the value is NaN or infinity.
    /// </exception>
    private static BigInteger RoundToBigInteger(BigInteger numerator, BigInteger denominator, MidpointRounding mode) {
        if (denominator.IsZero) {
            throw new OverflowException(Resources.ValueMustNotNanOrInfinity);
        }

        if (numerator.IsZero || denominator.IsOne) {
            return numerator;
        }

        return mode switch {
            MidpointRounding.AwayFromZero => roundAwayFromZero(numerator, denominator),
            MidpointRounding.ToEven => roundToEven(numerator, denominator),
#if NET
            MidpointRounding.ToZero => BigInteger.Divide(numerator, denominator),
            MidpointRounding.ToPositiveInfinity => roundToPositiveInfinity(numerator, denominator),
            MidpointRounding.ToNegativeInfinity => roundToNegativeInfinity(numerator, denominator),
#endif
            _ => throw new ArgumentOutOfRangeException(nameof(mode))
        };

        static BigInteger roundAwayFromZero(BigInteger numerator, BigInteger denominator) {
            var halfDenominator = denominator >> 1;
            return numerator.Sign == denominator.Sign
                ? BigInteger.Divide(numerator + halfDenominator, denominator)
                : BigInteger.Divide(numerator - halfDenominator, denominator);
        }

        static BigInteger roundToEven(BigInteger numerator, BigInteger denominator) {
            var quotient = BigInteger.DivRem(numerator, denominator, out var remainder);
            if (remainder.IsZero) {
                return quotient;
            }

            if (denominator.Sign == 1) {
                if (numerator.Sign == 1) {
                    // Both values are positive
                    var midpoint = remainder << 1;
                    if (midpoint > denominator || (midpoint == denominator && !quotient.IsEven)) {
                        return quotient + BigInteger.One;
                    }
                } else {
                    // For negative values
                    var midpoint = -remainder << 1;
                    if (midpoint > denominator || (midpoint == denominator && !quotient.IsEven)) {
                        return quotient - BigInteger.One;
                    }
                }
            } else {
                if (numerator.Sign == -1) {
                    // Both values are positive
                    var midpoint = remainder << 1;
                    if (midpoint < denominator || (midpoint == denominator && !quotient.IsEven)) {
                        return quotient + BigInteger.One;
                    }
                } else {
                    // For negative values
                    var midpoint = -remainder << 1;
                    if (midpoint < denominator || (midpoint == denominator && !quotient.IsEven)) {
                        return quotient - BigInteger.One;
                    }
                }
            }

            return quotient;
        }
#if NET
        static BigInteger roundToPositiveInfinity(BigInteger numerator, BigInteger denominator) {
            var quotient = BigInteger.DivRem(numerator, denominator, out var remainder);
            return remainder.Sign == denominator.Sign ? quotient + BigInteger.One : quotient;
        }

        static BigInteger roundToNegativeInfinity(BigInteger numerator, BigInteger denominator) {
            var quotient = BigInteger.DivRem(numerator, denominator, out var remainder);
            return remainder.Sign == -denominator.Sign ? quotient - BigInteger.One : quotient;
        }
#endif
    }
}
