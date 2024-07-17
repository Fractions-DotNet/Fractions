using System;
using System.Numerics;

namespace Fractions;

public readonly partial struct Fraction {
    private static readonly BigInteger MIN_DECIMAL = new(decimal.MinValue);
    private static readonly BigInteger MAX_DECIMAL = new(decimal.MaxValue);
    private static readonly BigInteger MIN_DOUBLE = new(double.MinValue);
    private static readonly BigInteger MAX_DOUBLE = new(double.MaxValue);

    /// <summary>
    ///     Converts the fraction to a 32-bit signed integer.
    /// </summary>
    /// <returns>The result of the integer division of the numerator by the denominator.</returns>
    /// <exception cref="DivideByZeroException">Thrown when the denominator is zero - i.e. the value is NaN or Infinity.</exception>
    /// <exception cref="OverflowException">
    ///     Thrown when the result of the division is outside the range of a 32-bit signed integer.
    /// </exception>
    public int ToInt32() {
        return (int)ToBigInteger();
    }

    /// <summary>
    ///     Converts the fraction to a 64-bit signed integer.
    /// </summary>
    /// <returns>The result of the integer division of the numerator by the denominator.</returns>
    /// <exception cref="DivideByZeroException">Thrown when the denominator is zero - i.e. the value is NaN or Infinity.</exception>
    /// <exception cref="OverflowException">
    ///     Thrown when the result of the division is outside the range of a 64-bit signed integer.
    /// </exception>
    public long ToInt64() {
        return (long)ToBigInteger();
    }

    /// <summary>
    ///     Converts the fraction to a 32-bit unsigned integer.
    /// </summary>
    /// <returns>The result of the integer division of the numerator by the denominator.</returns>
    /// <exception cref="DivideByZeroException">Thrown when the denominator is zero - i.e. the value is NaN or Infinity.</exception>
    /// <exception cref="OverflowException">
    ///     Thrown when the result of the division is outside the range of a 32-bit unsigned integer.
    /// </exception>
    [CLSCompliant(false)]
    public uint ToUInt32() {
        return (uint)ToBigInteger();
    }

    /// <summary>
    ///     Converts the fraction to a 64-bit unsigned integer.
    /// </summary>
    /// <returns>The result of the integer division of the numerator by the denominator.</returns>
    /// <exception cref="DivideByZeroException">Thrown when the denominator is zero - i.e. the value is NaN or Infinity.</exception>
    /// <exception cref="OverflowException">
    ///     Thrown when the result of the division is outside the range of a 64-bit unsigned integer.
    /// </exception>
    [CLSCompliant(false)]
    public ulong ToUInt64() {
        return (ulong)ToBigInteger();
    }

    /// <summary>
    ///     Converts the fraction to a BigInteger.
    /// </summary>
    /// <returns>The result of the integer division of the numerator by the denominator.</returns>
    /// <exception cref="DivideByZeroException">Thrown when the denominator is zero - i.e. the value is NaN or Infinity.</exception>
    public BigInteger ToBigInteger() {
        var denominator = Denominator;
        return denominator.IsOne ? Numerator : Numerator / denominator;
    }

    /// <summary>
    ///     Converts the fraction to a decimal value.
    /// </summary>
    /// <returns>
    ///     The fraction represented as a decimal. If the number exceeds decimal precision, the extra decimals are lost
    ///     due to rounding.
    /// </returns>
    /// <exception cref="DivideByZeroException">Thrown when the denominator is zero - i.e. the value is NaN or Infinity.</exception>
    /// <exception cref="OverflowException">
    ///     Thrown when the number represented by this fraction is outside the decimal range.
    /// </exception>
    public decimal ToDecimal() {
        var numerator = Numerator;
        var denominator = Denominator;
        switch (denominator.Sign) {
            case 0: {
                throw new DivideByZeroException();
            }
            case -1: {
                numerator = -numerator;
                denominator = -denominator;
                break;
            }
        }

        if (numerator.IsZero) {
            return decimal.Zero;
        }

        if (denominator.IsOne) {
            return (decimal)numerator;
        }

        if (numerator > MAX_DECIMAL) {
            if (denominator > MAX_DECIMAL) {
                // both terms need to be rounded
                numerator = (numerator * MAX_DECIMAL / denominator);
                denominator = MAX_DECIMAL;
                if (numerator <= MAX_DECIMAL) {
                    // both terms are now within range
                    return (decimal)numerator / (decimal)denominator;
                }
            }

            var withoutDecimalPlaces = (decimal) BigInteger.DivRem(numerator, denominator, out var remainder);
            return remainder.IsZero ? withoutDecimalPlaces : withoutDecimalPlaces + (decimal)remainder / (decimal)denominator;
        }

        if (numerator < MIN_DECIMAL) {
            if (denominator > MAX_DECIMAL) {
                // both terms need to be rounded
                numerator = (numerator * MAX_DECIMAL / denominator);
                denominator = MAX_DECIMAL;
                if (numerator >= MIN_DECIMAL) {
                    // both terms are now within range
                    return (decimal)numerator / (decimal)denominator;
                }
            }

            var withoutDecimalPlaces = (decimal)BigInteger.DivRem(numerator, denominator, out var remainder);
            return remainder.IsZero ? withoutDecimalPlaces : withoutDecimalPlaces + (decimal)remainder / (decimal)denominator;
        }

        if (denominator > MAX_DECIMAL) {
            // since both terms are non-zero and the numerator is smaller (in magnitude) to the denominator
            // the resulting number would be in the range [-1,1] (exclusive)
            // we want to flip the operation: x = a/b -> 1/x = b/a
            var decimalPart = BigInteger.DivRem(denominator, numerator, out var remainder);
            if (decimalPart < MIN_DECIMAL || decimalPart > MAX_DECIMAL) {
                return decimal.Zero;
            }

            return remainder.IsZero ? 1m / (decimal)decimalPart : 1m / ((decimal)decimalPart + (decimal)remainder / (decimal)numerator);
        }

        return (decimal)numerator / (decimal)denominator;
    }

    /// <summary>
    ///     Converts the fraction to a decimal number. If the fraction cannot be represented as a decimal due to its
    ///     size, the method will return the closest possible decimal representation (either decimal.MaxValue or
    ///     decimal.MinValue).
    /// </summary>
    /// <returns>
    ///     The decimal representation of the fraction. If the fraction is too large to represent as a decimal, returns
    ///     decimal.MaxValue. If the fraction is too small to represent as a decimal, returns decimal.MinValue.
    /// </returns>
    /// <remarks>If the fraction is NaN (the numerator and the denominator are both zero), the method will return decimal.Zero.</remarks>
    public decimal ToDecimalSaturating() {
        var numerator = Numerator;
        var denominator = Denominator;
        switch (denominator.Sign) {
            case 0: {
                return numerator.Sign switch {
                    1 => decimal.MaxValue,
                    -1 => decimal.MinValue,
                    _ => decimal.Zero
                };
            }
            case -1: {
                numerator = -numerator;
                denominator = -denominator;
                break;
            }
        }

        if (numerator.IsZero) {
            return decimal.Zero;
        }

        if (denominator.IsOne) {
            if (numerator > MAX_DECIMAL) {
                return decimal.MaxValue;
            }

            if (numerator < MIN_DECIMAL) {
                return decimal.MinValue;
            }

            return (decimal)numerator;
        }

        if (numerator > MAX_DECIMAL) {
            if (denominator > MAX_DECIMAL) {
                // both terms need to be rounded
                numerator = numerator * MAX_DECIMAL / denominator;
                denominator = MAX_DECIMAL;
                if (numerator <= MAX_DECIMAL) {
                    // both terms are now within range
                    return (decimal)numerator / (decimal)denominator;
                }
            }

            var withoutDecimalPlaces = BigInteger.DivRem(numerator, denominator, out var remainder);
            if (withoutDecimalPlaces > MAX_DECIMAL) {
                return decimal.MaxValue;
            }

            return remainder.IsZero ? (decimal)withoutDecimalPlaces : (decimal)withoutDecimalPlaces + (decimal)remainder / (decimal)denominator;
        }

        if (numerator < MIN_DECIMAL) {
            if (denominator > MAX_DECIMAL) {
                // both terms need to be rounded
                numerator = numerator * MAX_DECIMAL / denominator;
                denominator = MAX_DECIMAL;
                if (numerator >= MIN_DECIMAL) {
                    // both terms are now within range
                    return (decimal)numerator / (decimal)denominator;
                }
            }

            var withoutDecimalPlaces = BigInteger.DivRem(numerator, denominator, out var remainder);
            if (withoutDecimalPlaces < MIN_DECIMAL) {
                return decimal.MinValue;
            }

            return remainder.IsZero ? (decimal)withoutDecimalPlaces : (decimal)withoutDecimalPlaces + (decimal)remainder / (decimal)denominator;
        }

        if (denominator > MAX_DECIMAL) {
            // since both terms are non-zero and the numerator is smaller (in magnitude) to the denominator
            // the resulting number would be in the range [-1,1] (exclusive)
            // we want to flip the operation: x = a/b -> 1/x = b/a
            var decimalPart = BigInteger.DivRem(denominator, numerator, out var remainder);
            if (decimalPart < MIN_DECIMAL || decimalPart > MAX_DECIMAL) {
                return decimal.Zero;
            }

            return remainder.IsZero ? 1m / (decimal)decimalPart : 1m / ((decimal)decimalPart + (decimal)remainder / (decimal)numerator);
        }

        return (decimal)numerator / (decimal)denominator;
    }
    
    /// <summary>
    ///     Converts the fraction to a double precision floating point number.
    /// </summary>
    /// <returns>
    ///     The fraction represented as a double. If the number exceeds the precision of a double, the extra decimals are
    ///     lost due to rounding.
    /// </returns>
    /// <remarks>
    ///     If the denominator is zero, the result is NaN for a zero numerator and positive or negative infinity for a non-zero
    ///     numerator.
    /// </remarks>
    public double ToDouble() {
        var numerator = Numerator;
        var denominator = Denominator;

        switch (denominator.Sign) {
            case 0: {
                return numerator.Sign switch {
                    1 => double.PositiveInfinity,
                    -1 => double.NegativeInfinity,
                    _ => double.NaN
                };
            }
            case -1: {
                numerator = -numerator;
                denominator = -denominator;
                break;
            }
        }

        if (numerator.IsZero) {
            return 0;
        }
        
        if (denominator.IsOne) {
            return (double)numerator;
        }

        var convertedNumerator = (double)numerator;
        if (double.IsPositiveInfinity(convertedNumerator)) {
            if (denominator > MAX_DOUBLE) {
                // both terms need to be rounded
                numerator = numerator * MAX_DOUBLE / denominator;
                denominator = MAX_DOUBLE;
                if (numerator <= MAX_DOUBLE) {
                    // both terms are now within range
                    return (double)numerator / (double)denominator;
                }
            }

            var withoutDecimalPlaces = (double)BigInteger.DivRem(numerator, denominator, out var remainder);
            if (double.IsPositiveInfinity(withoutDecimalPlaces)) {
                return double.PositiveInfinity;
            }

            return remainder.IsZero ? withoutDecimalPlaces : withoutDecimalPlaces + (double)remainder / (double)denominator;
        }

        if (double.IsNegativeInfinity(convertedNumerator)) {
            if (denominator > MAX_DOUBLE) {
                // both terms need to be rounded
                numerator = numerator * MAX_DOUBLE / denominator;
                denominator = MAX_DOUBLE;
                if (numerator >= MIN_DOUBLE) {
                    // both terms are now within range
                    return (double)numerator / (double)denominator;
                }
            }

            var withoutDecimalPlaces = (double)BigInteger.DivRem(numerator, denominator, out var remainder);
            if (double.IsNegativeInfinity(withoutDecimalPlaces)) {
                return double.NegativeInfinity;
            }

            return remainder.IsZero ? withoutDecimalPlaces : withoutDecimalPlaces + (double)remainder / (double)denominator;
        }
        
        var convertedDenominator = (double)denominator;
        if (double.IsPositiveInfinity(convertedDenominator)) {
            // since both terms are non-zero and the numerator is smaller (in magnitude) to the denominator
            // the resulting number would be in the range [-1,1] (exclusive)
            // we want to flip the operation: x = a/b -> 1/x = b/a
            var decimalPart = (double) BigInteger.DivRem(denominator, numerator, out var remainder);
            if (double.IsInfinity(decimalPart)) {
                return 0;
            }

            return remainder.IsZero ? 1 / decimalPart : 1 / (decimalPart + (double)remainder / (double)numerator);
        }
        
        return convertedNumerator / convertedDenominator;
    }
}
