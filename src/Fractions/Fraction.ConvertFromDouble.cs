using System;
using System.Numerics;
using Fractions.Extensions;

namespace Fractions;

public readonly partial struct Fraction {
    /// <summary>
    /// <inheritdoc cref="FromDouble(double, bool)" path="/summary"/>
    /// </summary>
    /// <param name="value"><inheritdoc cref="FromDouble(double, bool)" path="/param[@name='value']"/></param>
    /// <returns>
    ///     A fraction that corresponds to the floating-point binary representation of the value,
    ///     with its terms reduced by their greatest common denominator.
    /// </returns>
    /// <remarks><inheritdoc cref="FromDouble(double, bool)" path="/remarks"/></remarks>
    public static Fraction FromDouble(double value) {
        return FromDouble(value, true);
    }

    /// <summary>
    ///     Converts a floating point value to a fraction. Due to the fact that no rounding is applied to the input, values
    ///     such as 0.2 or 0.3, which do not have an exact representation as a <see cref="double" />, would result in
    ///     very large values in the numerator and denominator.
    /// </summary>
    /// <param name="value">A floating point value.</param>
    /// <param name="reduceTerms">
    ///     Indicates whether the terms of the fraction should be reduced by their greatest common
    ///     denominator.
    /// </param>
    /// <returns>A fraction corresponding to the binary floating-point representation of the value</returns>
    /// <remarks>
    ///     The <see cref="double" /> data type in C# uses a binary floating-point representation, which can't accurately
    ///     represent all
    ///     decimal fractions. When you convert a <see cref="double" /> to a <see cref="Fraction" /> using this method, the
    ///     resulting fraction is an
    ///     exact representation of the <see cref="double" /> value, not the decimal number that the <see cref="double" /> is
    ///     intended to approximate.
    ///     This is why you can end up with large numerators and denominators.
    ///     <code>
    /// var value = Fraction.FromDouble(0.1);
    /// Console.WriteLine(value);  // Outputs "3602879701896397/36028797018963968"
    /// </code>
    ///     The output fraction is an exact representation of the <see cref="double" /> value 0.1, which is actually slightly
    ///     more than 0.1
    ///     due to the limitations of binary floating-point representation.
    ///     <para>
    ///         Additionally, as the <see cref="double" /> value approaches the limits of its precision,
    ///         `Fraction.FromDouble(value).ToDouble() == value` might not hold true. This is because the numerator and
    ///         denominator
    ///         of the <see cref="Fraction" /> are both very large numbers. When these numbers are converted to
    ///         <see cref="double" /> for the division
    ///         operation in the <see cref="ToDouble" /> method, they can exceed the precision limit of the
    ///         <see cref="double" /> type, resulting in
    ///         a loss of precision.
    ///     </para>
    ///     <code>
    /// var value = Fraction.FromDouble(double.Epsilon);
    /// Console.WriteLine(value.ToDouble() == double.Epsilon);  // Outputs "False"
    /// </code>
    ///     For more information, visit the
    ///     <see href="https://github.com/danm-de/Fractions?tab=readme-ov-file#creation-from-double-without-rounding">
    ///         official GitHub repository
    ///         page.
    ///     </see>
    /// </remarks>
    public static Fraction FromDouble(double value, bool reduceTerms) {
        // No rounding here! It will convert the actual number that is stored as double! 
        // See https://csharpindepth.com/Articles/FloatingPoint
#pragma warning disable IDE1006
        const ulong SIGN_BIT = 0x8000000000000000;
        const ulong EXPONENT_BITS = 0x7FF0000000000000;
        const ulong MANTISSA = 0x000FFFFFFFFFFFFF;
        const ulong MANTISSA_DIVISOR = 0x0010000000000000;
        const ulong K = 1023;
#pragma warning restore IDE1006

        // value = (-1 * sign)   *   (1 + 2^(-1) + 2^(-2) .. + 2^(-52))   *   2^(exponent-K)
        var valueBits = unchecked((ulong)BitConverter.DoubleToInt64Bits(value));

        if (valueBits == 0) {
            // See IEEE 754
            return Zero;
        }

        var isNegative = (valueBits & SIGN_BIT) == SIGN_BIT;
        var mantissaBits = valueBits & MANTISSA;

        // (exponent-K)
        var exponentBits = valueBits & EXPONENT_BITS;

        if (exponentBits == EXPONENT_BITS) {
            // NaN or Infinity
            if (mantissaBits != 0) {
                return NaN;
            }

            // Infinity
            return isNegative
                ? NegativeInfinity
                : PositiveInfinity;
        }

        var exponent = (int)((exponentBits >> 52) - K);

        // construct the fraction from the mantissa bits and the exponent
        // (1 + 2^(-1) + 2^(-2) .. + 2^(-52))
        if (reduceTerms) {
            var mantissa = ReduceSigned(mantissaBits + MANTISSA_DIVISOR, MANTISSA_DIVISOR);

            var factorSign = isNegative ? BigInteger.MinusOne : BigInteger.One;
            // 2^exponent
            var factor = exponent < 0
                ? ReduceSigned(factorSign, BigInteger.One << -exponent)
                : new Fraction(factorSign << exponent);

            return mantissa * factor;
        } else {
            var mantissa = new Fraction(true, mantissaBits + MANTISSA_DIVISOR, MANTISSA_DIVISOR);

            var factorSign = isNegative ? BigInteger.MinusOne : BigInteger.One;
            // 2^exponent
            var factor = exponent < 0
                ? new Fraction(true, factorSign, BigInteger.One << -exponent)
                : new Fraction(factorSign << exponent);

            return mantissa * factor;
        }
    }

    /// <summary>
    /// <inheritdoc cref="FromDoubleRounded(double, bool)" path="/summary"/>
    /// </summary>
    /// <param name="value"><inheritdoc cref="FromDoubleRounded(double, bool)" path="/param[@name='value']"/></param>
    /// <returns>
    ///     A fraction that approximates the input value, rounded to the nearest rational number and
    ///     with its terms reduced by their greatest common denominator.
    ///     If converted back to double, it would produce the same value.
    /// </returns>
    /// <remarks><inheritdoc cref="FromDoubleRounded(double, bool)" path="/remarks"/></remarks>
    public static Fraction FromDoubleRounded(double value) {
        return FromDoubleRounded(value, reduceTerms: true);
    }

    /// <summary>
    ///     Converts a floating point value to a fraction by rounding to the nearest rational number.
    ///     This method is designed to avoid large numbers in the numerator and denominator.
    /// </summary>
    /// <param name="value">A floating point value.</param>
    /// <param name="reduceTerms">
    ///     Indicates whether the terms of the fraction should be reduced by their greatest common
    ///     denominator.
    /// </param>
    /// <returns>
    ///     A fraction that approximates the input value, rounded to the nearest rational number. If converted back to
    ///     double, it would produce the same value.
    /// </returns>
    /// <remarks>
    ///     This method is the fastest among the three methods for converting a double to a fraction. However, it shouldn't be
    ///     used for strict comparisons with other fractions due to the heuristic approach it uses.
    ///     This approach is not guaranteed to produce the exact rational representation of the input. This is demonstrated in
    ///     the following example:
    ///     <code>
    ///     var doubleValue = 1055.05585262;
    ///     var roundedValue = Fraction.FromDoubleRounded(doubleValue);      // returns {4085925351/3872710} which is 1055.0558526199999483565771772222
    ///     var literalValue = Fraction.FromDoubleRounded(doubleValue, 15);  // returns {52752792631/50000000} which is 1055.05585262 exactly
    ///     Console.WriteLine(roundedValue.CompareTo(literalValue); // Outputs "-1" which stands for "smaller than"
    ///     Console.WriteLine(roundedValue.ToDouble() == doubleValue); // Outputs "true" as the actual difference is smaller than the precision of the doubles
    ///     </code>
    ///     For more information, visit the
    ///     <see
    ///         href="https://github.com/danm-de/Fractions?tab=readme-ov-file#creation-from-double-with-maximum-number-of-significant-digits">
    ///         official GitHub repository
    ///         page.
    ///     </see>
    /// </remarks>
    public static Fraction FromDoubleRounded(double value, bool reduceTerms) {
        if (double.IsPositiveInfinity(value)) {
            return PositiveInfinity;
        }

        if (double.IsNegativeInfinity(value)) {
            return NegativeInfinity;
        }

        if (double.IsNaN(value)) {
            return NaN;
        }

        if (value == 0d) {
            return Zero;
        }

        // Inspired from Syed Mehroz Alam <smehrozalam@yahoo.com> http://www.geocities.ws/smehrozalam/source/fractioncs.txt
        // .. who got it from http://ebookbrowse.com/confrac-pdf-d13212190 
        // or ftp://89.25.159.69/knm/ksiazki/reszta/homepage.smc.edu.kennedy_john/CONFRAC.pdf
        var sign = Math.Sign(value);
        var absoluteValue = Math.Abs(value);

        var numerator = new BigInteger(absoluteValue);
        var denominator = 1.0;
        var remainingDigits = absoluteValue;
        var previousDenominator = 0.0;
        var breakCounter = 0;

        while (MathExt.RemainingDigitsAfterTheDecimalPoint(remainingDigits)
               && Math.Abs(absoluteValue - (double)numerator / denominator) > double.Epsilon) {
            remainingDigits = 1.0 / (remainingDigits - Math.Floor(remainingDigits));

            if (double.IsInfinity(remainingDigits)) {
                // remaining value is near "0"
                break;
            }

            var tmp = denominator;

            denominator = Math.Floor(remainingDigits) * denominator + previousDenominator;
            numerator = new BigInteger(absoluteValue * denominator + 0.5);

            previousDenominator = tmp;

            // See http://www.ozgrid.com/forum/archive/index.php/t-22530.html
            if (++breakCounter > 594) {
                break;
            }
        }

        if (sign < 0) {
            numerator = -numerator;
        }

        return reduceTerms
            ? ReduceSigned(numerator, new BigInteger(denominator))
            : new Fraction(true, numerator, new BigInteger(denominator));
    }

    /// <summary>
    /// <inheritdoc cref="FromDoubleRounded(double, int, bool)" path="/summary"/>
    /// </summary>
    /// <param name="value"><inheritdoc cref="FromDoubleRounded(double, int, bool)" path="/param[@name='value']"/></param>
    /// <param name="significantDigits"><inheritdoc cref="FromDoubleRounded(double, int, bool)" path="/param[@name='significantDigits']"/></param>
    /// <returns>A Fraction representing the rounded floating point value with its terms reduced by their greatest common denominator.</returns>
    /// <remarks><inheritdoc cref="FromDoubleRounded(double, int, bool)" path="/remarks"/></remarks>
    public static Fraction FromDoubleRounded(double value, int significantDigits) {
        return FromDoubleRounded(value, significantDigits, reduceTerms: true);
    }

    /// <summary>
    ///     Converts a floating point value to a Fraction by rounding to the nearest rational number with a specified number of
    ///     significant digits.
    ///     <para>
    ///         This method is designed to avoid large numbers in the numerator and denominator while also making the resulting
    ///         <see cref="Fraction" /> safe to
    ///         use in comparison operations with other fractions.
    ///     </para>
    /// </summary>
    /// <param name="value">The floating point value to convert.</param>
    /// <param name="significantDigits">The maximum number of significant digits to consider when rounding the value.</param>
    /// <param name="reduceTerms">
    ///     Indicates whether the terms of the fraction should be reduced by their greatest common
    ///     denominator.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the number of significant digits is less than 1 or greater than 17.
    /// </exception>
    /// <returns>A Fraction representing the rounded floating point value.</returns>
    /// <remarks>
    ///     The double data type stores its values as 64-bit floating point numbers in accordance with the
    ///     <see href="https://en.wikipedia.org/wiki/IEEE_754">
    ///         IEC 60559:1989 (IEEE
    ///         754)
    ///     </see>
    ///     standard for binary
    ///     <see href="https://en.wikipedia.org/wiki/Floating-point_arithmetic">floating-point arithmetic</see>.
    ///     However, the double data type cannot precisely store some binary fractions. For instance, 1/10, which is accurately
    ///     represented by .1 as a decimal fraction, is represented by .0001100110011... as a binary fraction, with the pattern
    ///     0011 repeating indefinitely.
    ///     In such cases, the floating-point value provides an approximate representation of the number.
    ///     <para>
    ///         This method can be used to avoid large numbers in the numerator and denominator while also making it safe to
    ///         use in comparison operations with other fractions.
    ///         <code>
    /// Fraction value = Fraction.FromDoubleRounded(0.1, 15); // returns {1/10}, which is exactly 0.1 
    /// </code>
    ///     </para>
    ///     <para>
    ///         If you care only about minimizing the size of the numerator/denominator, and do not expect to use the
    ///         fraction in any strict comparison operations, then creating an approximated fraction using the
    ///         <see cref="FromDoubleRounded(double, bool)" /> overload should offer much better performance.
    ///     </para>
    ///     For more information, visit the
    ///     <see
    ///         href="https://github.com/danm-de/Fractions?tab=readme-ov-file#creation-from-double-with-rounding-to-a-close-approximation">
    ///         official GitHub repository
    ///         page.
    ///     </see>
    /// </remarks>
    public static Fraction FromDoubleRounded(double value, int significantDigits, bool reduceTerms) {
        if (significantDigits is < 1 or > 17) {
            throw new ArgumentOutOfRangeException(nameof(significantDigits), significantDigits, Properties.Resources.SignificantDigitsOutOfRange);
        }
        
        switch (value) {
            case 0d:
                return Zero;
            case double.NaN:
                return NaN;
            case double.PositiveInfinity:
                return PositiveInfinity;
            case double.NegativeInfinity:
                return NegativeInfinity;
        }

        // Determine the number of decimal places to keep
        var magnitude = Math.Floor(Math.Log10(Math.Abs(value)));
        if (magnitude > significantDigits) {
            var digitsToKeep = new BigInteger(value / Math.Pow(10, magnitude - significantDigits));
            return digitsToKeep * PowerOfTen((int)magnitude - significantDigits);
        }

        // "decimal" values
        var truncatedValue = Math.Truncate(value);
        var integerPart = new BigInteger(truncatedValue);

        var decimalPlaces = Math.Min(-(int)magnitude + significantDigits - 1, 308);
        var scaleFactor = Math.Pow(10, decimalPlaces);
        // Get the fractional part
        var fractionalPart = (long)Math.Round((value - truncatedValue) * scaleFactor);
        if (fractionalPart == 0) // rounded to integer
        {
            return new Fraction(integerPart);
        }
        
        // reduce the insignificant trailing zeros from the fractional part before converting it to BigInteger
        while (fractionalPart % 10 == 0) {
            fractionalPart /= 10;
            decimalPlaces--;
        }

        var denominator = PowerOfTen(decimalPlaces);
        var numerator = integerPart.IsZero? fractionalPart : integerPart * denominator + fractionalPart;
        return reduceTerms ? ReduceSigned(numerator, denominator) : new Fraction(true, numerator, denominator);
    }
}
