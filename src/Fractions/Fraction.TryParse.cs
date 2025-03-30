#nullable enable

#if !NET
using System.Linq;
#endif
using System;
using System.Globalization;
using System.Numerics;

namespace Fractions;

public readonly partial struct Fraction {
    /// <summary>
    ///     Attempts to parse a string into a fraction.
    /// </summary>
    /// <param name="value">
    ///     The input string to parse. The numerator and denominator must be separated by a '/' (slash) character.
    ///     For example, "3/4". If the string is not in this format, string is parsed using the
    ///     <see cref="NumberStyles.Number" /> style and the thread current culture.
    /// </param>
    /// <param name="fraction">
    ///     When this method returns, contains the parsed fraction if the operation was successful; otherwise,
    ///     it contains the default value of <see cref="Fraction"/>.
    /// </param>
    /// <returns>
    ///     <c>true</c> if the input string is well-formed and could be parsed into a fraction; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     Here are some examples of how to use the <see cref="TryParse(string, out Fraction)" /> method:
    ///     <example>
    ///         <code>
    /// Fraction.TryParse("3/4", out var fraction);
    /// </code>
    ///         This example parses the string "3/4" into a <see cref="Fraction" /> object with a numerator of 3 and a
    ///         denominator of 4.
    ///         <code>
    /// Fraction.TryParse("1.25", out var fraction);
    /// </code>
    ///         This example parses the string "1.25" into a <see cref="Fraction" /> object with a numerator of 5 and a
    ///         denominator of 4.
    ///         <code>
    /// Fraction.TryParse("1.23e-2", out var fraction);
    /// </code>
    ///         This example parses the string "1.23e-2" into a <see cref="Fraction" /> object with a numerator of 123 and
    ///         a denominator of 10000.
    ///     </example>
    /// </remarks>
    public static bool TryParse(string? value, out Fraction fraction) =>
        TryParse(
            value: value,
            numberStyles: NumberStyles.Number,
            formatProvider: null,
            normalize: true,
            fraction: out fraction);

    /// <summary>
    ///     Attempts to parse a string of characters into a fraction.
    /// </summary>
    /// <param name="value">
    ///     The input string to parse. The numerator and denominator must be separated by a '/' (slash) character.
    ///     For example, "3/4". If the string is not in this format, the parsing behavior is influenced by the
    ///     <paramref name="numberStyles" /> and <paramref name="formatProvider" /> parameters.
    /// </param>
    /// <param name="numberStyles">
    ///     A bitwise combination of number styles permitted in the input string. This is relevant when the string
    ///     is not in the numerator/denominator format. For instance, <see cref="NumberStyles.Float" /> allows decimal
    ///     points and scientific notation.
    /// </param>
    /// <param name="formatProvider">
    ///     An <see cref="IFormatProvider" /> that supplies culture-specific information used to parse the input string.
    ///     This is relevant when the string is not in the numerator/denominator format. For example,
    ///     <c>CultureInfo.GetCultureInfo("en-US")</c> for US English culture.
    /// </param>
    /// <param name="fraction">
    ///     When this method returns, contains the parsed fraction if the operation was successful; otherwise,
    ///     it contains the default value of <see cref="Fraction"/>.
    /// </param>
    /// <returns>
    ///     <c>true</c> if the input string is well-formed and could be parsed into a fraction; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     The <paramref name="numberStyles" /> parameter allows you to specify which number styles are allowed in the input
    ///     string while the <paramref name="formatProvider" /> parameter provides culture-specific formatting information.
    ///     <para>
    ///         Here are some examples of how to use the
    ///         <see cref="TryParse(string, NumberStyles, IFormatProvider, out Fraction)" /> method:
    ///         <example>
    ///             <code>
    /// Fraction.TryParse("3/4", NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out var fraction);
    /// </code>
    ///             This example parses the string "3/4" into a <see cref="Fraction" /> object with a numerator of 3 and a
    ///             denominator of 4.
    ///             <code>
    /// Fraction.TryParse("1.25", NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out var fraction);
    /// </code>
    ///             This example parses the string "1.25" into a <see cref="Fraction" /> object with a numerator of 5 and a
    ///             denominator of 4.
    ///             <code>
    /// Fraction.TryParse("1.23e-2", NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out var fraction);
    /// </code>
    ///             This example parses the string "1.23e-2" into a <see cref="Fraction" /> object with a numerator of 123 and
    ///             a denominator of 10000.
    ///         </example>
    ///     </para>
    /// </remarks>
    public static bool TryParse(string? value, NumberStyles numberStyles, IFormatProvider? formatProvider,
        out Fraction fraction) {
        return TryParse(value, numberStyles, formatProvider, true, out fraction);
    }

#if NET
    /// <summary>
    ///     Attempts to parse a string of characters into a fraction.
    /// </summary>
    /// <param name="value">
    ///     The input string to parse. The numerator and denominator must be separated by a '/' (slash) character.
    ///     For example, "3/4". If the string is not in this format, the parsing behavior is influenced by the
    ///     <paramref name="numberStyles" /> and <paramref name="formatProvider" /> parameters.
    /// </param>
    /// <param name="numberStyles">
    ///     A bitwise combination of number styles permitted in the input string. This is relevant when the string
    ///     is not in the numerator/denominator format. For instance, <see cref="NumberStyles.Float" /> allows decimal
    ///     points and scientific notation.
    /// </param>
    /// <param name="formatProvider">
    ///     An <see cref="IFormatProvider" /> that supplies culture-specific information used to parse the input string.
    ///     This is relevant when the string is not in the numerator/denominator format. For example,
    ///     <c>CultureInfo.GetCultureInfo("en-US")</c> for US English culture.
    /// </param>
    /// <param name="normalize">
    ///     A boolean value indicating whether the parsed fraction should be reduced to its simplest form.
    ///     For example, if true, "4/8" will be reduced to "1/2".
    /// </param>
    /// <param name="fraction">
    ///     When this method returns, contains the parsed fraction if the operation was successful; otherwise,
    ///     it contains the default value of <see cref="Fraction"/>.
    /// </param>
    /// <returns>
    ///     <c>true</c> if the input string is well-formed and could be parsed into a fraction; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     The <paramref name="numberStyles" /> parameter allows you to specify which number styles are allowed in the input
    ///     string while the <paramref name="formatProvider" /> parameter provides culture-specific formatting information.
    ///     <para>
    ///         Here are some examples of how to use the
    ///         <see cref="TryParse(ReadOnlySpan{char}, NumberStyles, IFormatProvider, bool, out Fraction)" /> method:
    ///         <example>
    ///             <code>
    /// Fraction.TryParse("3/4", NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), true, out Fraction fraction);
    ///         </code>
    ///             This example parses the string "3/4" into a <see cref="Fraction" /> object with a numerator of 3 and a
    ///             denominator of 4.
    ///             <code>
    /// Fraction.TryParse("1.25", NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), true, out Fraction fraction);
    ///         </code>
    ///             This example parses the string "1.25" into a <see cref="Fraction" /> object with a numerator of 5 and a
    ///             denominator of 4.
    ///             <code>
    /// Fraction.TryParse("1,234.56", NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), false, out Fraction fraction);
    ///         </code>
    ///             This example parses the string "1,234.56" into a <see cref="Fraction" /> object with a numerator of 12345
    ///             and a
    ///             denominator of 100.
    ///             <code>
    /// Fraction.TryParse("1.23e-2", NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), false, out Fraction fraction);
    ///         </code>
    ///             This example parses the string "1.23e-2" into a <see cref="Fraction" /> object with a numerator of 123 and
    ///             a
    ///             denominator of 10000.
    ///         </example>
    ///     </para>
    /// </remarks>
    public static bool TryParse(string? value, NumberStyles numberStyles, IFormatProvider? formatProvider,
        bool normalize, out Fraction fraction) {
        return TryParse(value.AsSpan(), numberStyles, formatProvider, normalize, out fraction);
    }

    /// <summary>
    ///     Attempts to parse a ReadOnlySpan of characters into a fraction.
    /// </summary>
    /// <param name="value">
    ///     The input string to parse. The numerator and denominator must be separated by a '/' (slash) character.
    ///     For example, "3/4". If the string is not in this format, the parsing behavior is influenced by the
    ///     <paramref name="numberStyles" /> and <paramref name="formatProvider" /> parameters.
    /// </param>
    /// <param name="numberStyles">
    ///     A bitwise combination of number styles permitted in the input string. This is relevant when the string
    ///     is not in the numerator/denominator format. For instance, <see cref="NumberStyles.Float" /> allows decimal
    ///     points and scientific notation.
    /// </param>
    /// <param name="formatProvider">
    ///     An <see cref="IFormatProvider" /> that supplies culture-specific information used to parse the input string.
    ///     This is relevant when the string is not in the numerator/denominator format. For example,
    ///     <c>CultureInfo.GetCultureInfo("en-US")</c> for US English culture.
    /// </param>
    /// <param name="fraction">
    ///     When this method returns, contains the parsed fraction if the operation was successful; otherwise,
    ///     it contains the default value of <see cref="Fraction"/>.
    /// </param>
    /// <returns>
    ///     <c>true</c> if the input string is well-formed and could be parsed into a fraction; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     The <paramref name="numberStyles" /> parameter allows you to specify which number styles are allowed in the input
    ///     string while the <paramref name="formatProvider" /> parameter provides culture-specific formatting information.
    ///     <para>
    ///         Here are some examples of how to use the
    ///         <see cref="TryParse(ReadOnlySpan{char}, NumberStyles, IFormatProvider, out Fraction)" /> method:
    ///         <example>
    ///             <code>
    /// Fraction.TryParse("3/4", NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out var fraction);
    /// </code>
    ///             This example parses the string "3/4" into a <see cref="Fraction" /> object with a numerator of 3 and a
    ///             denominator of 4.
    ///             <code>
    /// Fraction.TryParse("1.25", NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out var fraction);
    /// </code>
    ///             This example parses the string "1.25" into a <see cref="Fraction" /> object with a numerator of 5 and a
    ///             denominator of 4.
    ///             <code>
    /// Fraction.TryParse("1.23e-2", NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out var fraction);
    /// </code>
    ///             This example parses the string "1.23e-2" into a <see cref="Fraction" /> object with a numerator of 123 and
    ///             a denominator of 10000.
    ///         </example>
    ///     </para>
    /// </remarks>
    public static bool TryParse(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? formatProvider,
        out Fraction fraction) {
        return TryParse(value, numberStyles, formatProvider, true, out fraction);
    }

    /// <summary>
    ///     Attempts to parse a ReadOnlySpan of characters into a fraction.
    /// </summary>
    /// <param name="value">
    ///     The input string to parse. The numerator and denominator must be separated by a '/' (slash) character.
    ///     For example, "3/4". If the string is not in this format, the parsing behavior is influenced by the
    ///     <paramref name="numberStyles" /> and <paramref name="formatProvider" /> parameters.
    /// </param>
    /// <param name="numberStyles">
    ///     A bitwise combination of number styles permitted in the input string. This is relevant when the string
    ///     is not in the numerator/denominator format. For instance, <see cref="NumberStyles.Float" /> allows decimal
    ///     points and scientific notation.
    /// </param>
    /// <param name="formatProvider">
    ///     An <see cref="IFormatProvider" /> that supplies culture-specific information used to parse the input string.
    ///     This is relevant when the string is not in the numerator/denominator format. For example,
    ///     <c>CultureInfo.GetCultureInfo("en-US")</c> for US English culture.
    /// </param>
    /// <param name="normalize">
    ///     A boolean value indicating whether the parsed fraction should be reduced to its simplest form.
    ///     For example, if true, "4/8" will be reduced to "1/2".
    /// </param>
    /// <param name="fraction">
    ///     When this method returns, contains the parsed fraction if the operation was successful; otherwise,
    ///     it contains the default value of <see cref="Fraction"/>.
    /// </param>
    /// <returns>
    ///     <c>true</c> if the input string is well-formed and could be parsed into a fraction; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     The <paramref name="numberStyles" /> parameter allows you to specify which number styles are allowed in the input
    ///     string while the <paramref name="formatProvider" /> parameter provides culture-specific formatting information.
    ///     <para>
    ///         Here are some examples of how to use the
    ///         <see cref="TryParse(ReadOnlySpan{char}, NumberStyles, IFormatProvider, bool, out Fraction)" /> method:
    ///         <example>
    ///             <code>
    /// Fraction.TryParse("3/4", NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), true, out var fraction);
    ///         </code>
    ///             This example parses the string "3/4" into a <see cref="Fraction" /> object with a numerator of 3 and a
    ///             denominator of 4.
    ///             <code>
    /// Fraction.TryParse("1.25", NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), true, out var fraction);
    ///         </code>
    ///             This example parses the string "1.25" into a <see cref="Fraction" /> object with a numerator of 5 and a
    ///             denominator of 4.
    ///             <code>
    /// Fraction.TryParse("1,234.56", NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), false, out var fraction);
    ///         </code>
    ///             This example parses the string "1,234.56" into a <see cref="Fraction" /> object with a numerator of 12345
    ///             and a
    ///             denominator of 100.
    ///             <code>
    /// Fraction.TryParse("1.23e-2", NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), false, out var fraction);
    ///         </code>
    ///             This example parses the string "1.23e-2" into a <see cref="Fraction" /> object with a numerator of 123 and
    ///             a
    ///             denominator of 10000.
    ///         </example>
    ///     </para>
    /// </remarks>
    public static bool TryParse(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? formatProvider,
        bool normalize, out Fraction fraction) {
        if (value.IsEmpty) {
            return CannotParse(out fraction);
        }

        if (value.Length == 1) {
            if (!BigInteger.TryParse(value, numberStyles, formatProvider, out var singleDigit)) {
                var numberFormat = NumberFormatInfo.GetInstance(formatProvider);
                if (value.SequenceEqual(numberFormat.PositiveInfinitySymbol)) {
                    fraction = PositiveInfinity;
                    return true;
                }

                if (value.SequenceEqual(numberFormat.NegativeInfinitySymbol)) {
                    fraction = NegativeInfinity;
                    return true;
                }

                if (value.SequenceEqual(numberFormat.NaNSymbol)) {
                    fraction = NaN;
                    return true;
                }

                return CannotParse(out fraction);
            }

            fraction = new Fraction(singleDigit);
            return true;
        }

        var ranges = new Span<Range>(new Range[2]);
        var nbRangesFilled = value.Split(ranges, '/');

        if (nbRangesFilled == 2) {
            var numeratorValue = value[ranges[0]];
            var denominatorValue = value[ranges[1]];

            var withoutDecimalPoint = numberStyles & ~NumberStyles.AllowDecimalPoint;
            if (!BigInteger.TryParse(
                    numeratorValue,
                    withoutDecimalPoint,
                    formatProvider,
                    out var numerator)
                || !BigInteger.TryParse(
                    denominatorValue,
                    withoutDecimalPoint,
                    formatProvider,
                    out var denominator)) {
                return CannotParse(out fraction);
            }

            fraction = new Fraction(numerator, denominator, normalize);
            return true;
        }

        // parsing a number using to the selected NumberStyles: e.g. " $ 12345.1234321e-4- " should result in -1.23451234321 with NumberStyles.Any
        var numberFormatInfo = NumberFormatInfo.GetInstance(formatProvider);
        // check for any of the special symbols (these cannot be combined with anything else)
        if (value.SequenceEqual(numberFormatInfo.NaNSymbol.AsSpan())) {
            fraction = NaN;
            return true;
        }

        if (value.SequenceEqual(numberFormatInfo.PositiveInfinitySymbol.AsSpan())) {
            fraction = PositiveInfinity;
            return true;
        }

        if (value.SequenceEqual(numberFormatInfo.NegativeInfinitySymbol.AsSpan())) {
            fraction = NegativeInfinity;
            return true;
        }

        var currencyAllowed = (numberStyles & NumberStyles.AllowCurrencySymbol) != 0;
        // there "special" rules regarding the white-spaces after a leading currency symbol is detected
        var currencyDetected = false;
        ReadOnlySpan<char> currencySymbol;
        if (currencyAllowed) {
            currencySymbol = numberFormatInfo.CurrencySymbol.AsSpan();
            if (currencySymbol.IsEmpty) {
                currencyAllowed = false;
                numberStyles &= ~NumberStyles.AllowCurrencySymbol; // no currency symbol available
            }
        } else {
            currencySymbol = [];
        }

        // note: decimal.TryParse relies on the CurrencySymbol (when currency is detected)
        var decimalsAllowed = (numberStyles & NumberStyles.AllowDecimalPoint) != 0;
        var decimalSeparator = decimalsAllowed ? numberFormatInfo.NumberDecimalSeparator.AsSpan() : [];

        var startIndex = 0;
        var endIndex = value.Length;
        var isNegative = false;

        // examine the leading characters
        do {
            var character = value[startIndex];
            if (char.IsDigit(character)) {
                break;
            }

            if (char.IsWhiteSpace(character)) {
                if ((numberStyles & NumberStyles.AllowLeadingWhite) == 0) {
                    return CannotParse(out fraction);
                }

                startIndex++;
                continue;
            }

            if (character == '(') {
                if ((numberStyles & NumberStyles.AllowParentheses) == 0) {
                    return CannotParse(out fraction); // not allowed
                }

                if (startIndex == endIndex - 1) {
                    return CannotParse(out fraction); // not enough characters
                }

                startIndex++; // consume the current character
                isNegative = true; // the closing parenthesis will be validated in the backwards iteration

                if (currencyDetected) {
                    // any number of white-spaces are allowed following a leading currency symbol (but no other signs)
                    numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                      NumberStyles.AllowTrailingSign |
                                      NumberStyles.AllowHexSpecifier);
                } else if (currencyAllowed && value[startIndex..].StartsWith(currencySymbol)) {
                    // there can be no more currency symbols but there could be more white-spaces (we skip and continue) 
                    currencyDetected = true;
                    startIndex += currencySymbol.Length;
                    numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                      NumberStyles.AllowTrailingSign |
                                      NumberStyles.AllowCurrencySymbol |
                                      NumberStyles.AllowHexSpecifier);
                } else {
                    // if next character is a white space the format should be rejected
                    numberStyles &= ~(NumberStyles.AllowLeadingWhite |
                                      NumberStyles.AllowLeadingSign |
                                      NumberStyles.AllowTrailingSign |
                                      NumberStyles.AllowHexSpecifier);
                    break;
                }

                continue;
            }

            if ((numberStyles & NumberStyles.AllowLeadingSign) != 0) {
                if (numberFormatInfo.NegativeSign.AsSpan() is { IsEmpty: false } negativeSign && value[startIndex..].StartsWith(negativeSign)) {
                    isNegative = true;
                    startIndex += negativeSign.Length;
                    if (currencyDetected) {
                        // any number of white-spaces are allowed following a leading currency symbol (but no other signs)
                        numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                          NumberStyles.AllowTrailingSign |
                                          NumberStyles.AllowParentheses |
                                          NumberStyles.AllowHexSpecifier);
                    } else if (currencyAllowed && value[startIndex..].StartsWith(currencySymbol)) {
                        // there can be no more currency symbols but there could be more white-spaces (we skip and continue) 
                        currencyDetected = true;
                        startIndex += currencySymbol.Length;
                        numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                          NumberStyles.AllowTrailingSign |
                                          NumberStyles.AllowParentheses |
                                          NumberStyles.AllowCurrencySymbol |
                                          NumberStyles.AllowHexSpecifier);
                    } else {
                        // if next character is a white space the format should be rejected
                        numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                          NumberStyles.AllowTrailingSign |
                                          NumberStyles.AllowParentheses |
                                          NumberStyles.AllowLeadingWhite |
                                          NumberStyles.AllowHexSpecifier);
                        break;
                    }

                    continue;
                }

                if (numberFormatInfo.PositiveSign.AsSpan() is { IsEmpty: false } positiveSign && value[startIndex..].StartsWith(positiveSign)) {
                    isNegative = false;
                    startIndex += positiveSign.Length;
                    if (currencyDetected) {
                        // any number of white-spaces are allowed following a leading currency symbol (but no other signs)
                        numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                          NumberStyles.AllowTrailingSign |
                                          NumberStyles.AllowParentheses |
                                          NumberStyles.AllowHexSpecifier);
                    } else if (currencyAllowed && value[startIndex..].StartsWith(currencySymbol)) {
                        // there can be no more currency symbols but there could be more white-spaces (we skip and continue) 
                        currencyDetected = true;
                        startIndex += currencySymbol.Length;
                        numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                          NumberStyles.AllowTrailingSign |
                                          NumberStyles.AllowParentheses |
                                          NumberStyles.AllowCurrencySymbol |
                                          NumberStyles.AllowHexSpecifier);
                    } else {
                        // if next character is a white space the format should be rejected
                        numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                          NumberStyles.AllowTrailingSign |
                                          NumberStyles.AllowParentheses |
                                          NumberStyles.AllowLeadingWhite |
                                          NumberStyles.AllowHexSpecifier);
                        break;
                    }

                    continue;
                }
            }

            if (currencyAllowed && !currencyDetected && value[startIndex..].StartsWith(currencySymbol)) {
                // there can be no more currency symbols but there could be more white-spaces (we skip and continue)
                currencyDetected = true;
                numberStyles &= ~NumberStyles.AllowCurrencySymbol;
                startIndex += currencySymbol.Length;
                continue;
            }

            if (decimalsAllowed && value[startIndex..].StartsWith(decimalSeparator)) {
                break; // decimal string with no leading zeros
            }

            // this is either an expected hex string or an invalid format
            return (numberStyles & NumberStyles.AllowHexSpecifier) != 0
                ? TryParseInteger(value[startIndex..endIndex], numberStyles & ~NumberStyles.AllowTrailingSign,
                    formatProvider, isNegative, out fraction)
                : CannotParse(out fraction); // unexpected character
        } while (startIndex < endIndex);

        if (startIndex >= endIndex) {
            return CannotParse(out fraction);
        }

        if (isNegative) {
            numberStyles &= ~(NumberStyles.AllowLeadingWhite |
                              NumberStyles.AllowLeadingSign);
        } else {
            numberStyles &= ~(NumberStyles.AllowLeadingWhite |
                              NumberStyles.AllowLeadingSign |
                              NumberStyles.AllowParentheses);
        }

        // examine the trailing characters
        do {
            var character = value[endIndex - 1];
            if (char.IsDigit(character)) {
                break;
            }

            if (char.IsWhiteSpace(character)) {
                if ((numberStyles & NumberStyles.AllowTrailingWhite) == 0) {
                    return CannotParse(out fraction);
                }

                endIndex--;
                continue;
            }

            if (character == ')') {
                if ((numberStyles & NumberStyles.AllowParentheses) == 0) {
                    return CannotParse(out fraction); // not allowed
                }

                numberStyles &= ~(NumberStyles.AllowParentheses | NumberStyles.AllowCurrencySymbol);
                endIndex--;
                continue;
            }

            if ((numberStyles & NumberStyles.AllowTrailingSign) != 0) {
                if (numberFormatInfo.NegativeSign.AsSpan() is { IsEmpty: false } negativeSign && value[..endIndex].EndsWith(negativeSign)) {
                    isNegative = true;
                    numberStyles &= ~(NumberStyles.AllowTrailingSign | NumberStyles.AllowHexSpecifier);
                    endIndex -= negativeSign.Length;
                    continue;
                }

                if (numberFormatInfo.PositiveSign.AsSpan() is { IsEmpty: false } positiveSign && value[..endIndex].EndsWith(positiveSign)) {
                    isNegative = false;
                    numberStyles &= ~(NumberStyles.AllowTrailingSign | NumberStyles.AllowHexSpecifier);
                    endIndex -= positiveSign.Length;
                    continue;
                }
            }

            if (currencyAllowed && !currencyDetected && value[..endIndex].EndsWith(currencySymbol)) {
                // there can be no more currency symbols but there could be more white-spaces (we skip and continue)
                currencyDetected = true;
                numberStyles &= ~NumberStyles.AllowCurrencySymbol;
                endIndex -= currencySymbol.Length;
                continue;
            }

            if (decimalsAllowed && value[..endIndex].EndsWith(decimalSeparator)) {
                break;
            }

            // this is either an expected hex string or an invalid format
            return (numberStyles & NumberStyles.AllowHexSpecifier) != 0
                ? TryParseInteger(value[startIndex..endIndex], numberStyles & ~NumberStyles.AllowTrailingSign,
                    formatProvider, isNegative, out fraction)
                : CannotParse(out fraction); // unexpected character
        } while (startIndex < endIndex);

        if (startIndex >= endIndex) {
            return CannotParse(out fraction); // not enough characters
        }

        if (isNegative && (numberStyles & NumberStyles.AllowParentheses) != 0) {
            return CannotParse(out fraction); // failed to find a closing parentheses
        }

        numberStyles &= ~(NumberStyles.AllowTrailingWhite |
                          NumberStyles.AllowTrailingSign |
                          NumberStyles.AllowCurrencySymbol);
        // at this point value[startIndex, endIndex] should correspond to the number without the sign (or the format is invalid)
        var unsignedValue = value[startIndex..endIndex];

        if (unsignedValue.Length == 1) {
            // this can only be a single digit (integer)
            return TryParseInteger(unsignedValue, numberStyles, formatProvider, isNegative, out fraction);
        }

        if ((numberStyles & NumberStyles.AllowExponent) != 0) {
            return TryParseWithExponent(unsignedValue, numberStyles, numberFormatInfo, isNegative, normalize,
                out fraction);
        }

        return decimalsAllowed
            ? TryParseDecimalNumber(unsignedValue, numberStyles, numberFormatInfo, isNegative, normalize, out fraction)
            : TryParseInteger(unsignedValue, numberStyles, formatProvider, isNegative, out fraction);
    }

    private static bool TryParseWithExponent(ReadOnlySpan<char> value, NumberStyles parseNumberStyles,
        NumberFormatInfo numberFormatInfo, bool isNegative, bool reduceTerms,
        out Fraction fraction) {
        // 1. try to find the exponent character (extracting the left and right sides)
        parseNumberStyles &= ~NumberStyles.AllowExponent;
        var ranges = new Span<Range>(new Range[2]);
        var nbRangesFilled = value.SplitAny(ranges, ['e', 'E']);

        if (nbRangesFilled != 2) {
            // no exponent found
            return (parseNumberStyles & NumberStyles.AllowDecimalPoint) != 0
                ? TryParseDecimalNumber(value, parseNumberStyles, numberFormatInfo, isNegative, reduceTerms,
                    out fraction)
                : TryParseInteger(value, parseNumberStyles, numberFormatInfo, isNegative, out fraction);
        }

        // 2. try to parse the exponent (w.r.t. the scientific notation format)
        var exponentSpan = value[ranges[1]];
        if (!int.TryParse(exponentSpan, NumberStyles.AllowLeadingSign | NumberStyles.Integer, numberFormatInfo,
                out var exponent)) {
            return CannotParse(out fraction);
        }

        // 3. try to parse the coefficient (w.r.t. the decimal separator allowance)
        var coefficientSpan = value[ranges[0]];
        if (coefficientSpan.Length == 1 || (parseNumberStyles & NumberStyles.AllowDecimalPoint) == 0) {
            if (!TryParseInteger(coefficientSpan, parseNumberStyles, numberFormatInfo, isNegative, out fraction)) {
                return false;
            }
        } else {
            if (!TryParseDecimalNumber(coefficientSpan, parseNumberStyles, numberFormatInfo, isNegative, reduceTerms,
                    out fraction)) {
                return false;
            }
        }

        // 4. multiply the coefficient by 10 to the power of the exponent
        fraction *= Pow(TEN, exponent);
        return true;
    }

    private static bool TryParseDecimalNumber(ReadOnlySpan<char> value, NumberStyles parseNumberStyles,
        NumberFormatInfo numberFormatInfo, bool isNegative, bool reduceTerms,
        out Fraction fraction) {
        // 1. try to find the decimal separator (extracting the left and right sides)
        var ranges = new Span<Range>(new Range[2]);
        var nbRangesFilled = value.Split(ranges, numberFormatInfo.NumberDecimalSeparator);

        if (nbRangesFilled != 2) {
            return TryParseInteger(value, parseNumberStyles, numberFormatInfo, isNegative, out fraction);
        }

        var integerSpan = value[ranges[0]];
        var fractionalSpan = value[ranges[1]];

        // 2. validate the format of the string after the radix
        if (fractionalSpan.IsEmpty) {
            // after excluding the sign, the input was reduced to just an integer part (e.g. "1 234.")
            return TryParseInteger(integerSpan, parseNumberStyles, numberFormatInfo, isNegative, out fraction);
        }

        if ((parseNumberStyles & NumberStyles.AllowThousands) != 0) {
            if (fractionalSpan.Contains(numberFormatInfo.NumberGroupSeparator.AsSpan(), StringComparison.Ordinal)) {
                return
                    CannotParse(out fraction); // number group separator detected in the fractional part (e.g. "1.2 34")
            }
        }

        // 3. extract the value of the string corresponding to the number without the decimal separator: "0.123 " should return "0123"
        var integerString = string.Concat(integerSpan, fractionalSpan);
        if (!BigInteger.TryParse(integerString, parseNumberStyles, numberFormatInfo, out var numerator)) {
            return CannotParse(out fraction);
        }

        if (numerator.IsZero && reduceTerms) {
            fraction = Zero;
            return true;
        }

        if (isNegative) {
            numerator = -numerator;
        }

        // 4. construct the fractional part using the corresponding decimal power for the denominator
        var nbDecimals = fractionalSpan.Length;
        var denominator = PowerOfTen(nbDecimals);
        fraction = reduceTerms ? ReduceSigned(numerator, denominator) : new Fraction(true, numerator, denominator);
        return true;
    }

    private static bool TryParseInteger(ReadOnlySpan<char> value, NumberStyles parseNumberStyles,
        IFormatProvider? formatProvider, bool isNegative, out Fraction fraction) {
        if (!BigInteger.TryParse(value, parseNumberStyles, formatProvider, out var bigInteger)) {
            return CannotParse(out fraction);
        }

        fraction = new Fraction(isNegative ? -bigInteger : bigInteger);
        return true;
    }

#else
    /// <summary>
    ///     Attempts to parse a string of characters into a fraction.
    /// </summary>
    /// <param name="value">
    ///     The input string to parse. The numerator and denominator must be separated by a '/' (slash) character.
    ///     For example, "3/4". If the string is not in this format, the parsing behavior is influenced by the
    ///     <paramref name="numberStyles" /> and <paramref name="formatProvider" /> parameters.
    /// </param>
    /// <param name="numberStyles">
    ///     A bitwise combination of number styles permitted in the input string. This is relevant when the string
    ///     is not in the numerator/denominator format. For instance, <see cref="NumberStyles.Float" /> allows decimal
    ///     points and scientific notation.
    /// </param>
    /// <param name="formatProvider">
    ///     An <see cref="IFormatProvider" /> that supplies culture-specific information used to parse the input string.
    ///     This is relevant when the string is not in the numerator/denominator format. For example,
    ///     <c>CultureInfo.GetCultureInfo("en-US")</c> for US English culture.
    /// </param>
    /// <param name="normalize">
    ///     A boolean value indicating whether the parsed fraction should be reduced to its simplest form.
    ///     For example, if true, "4/8" will be reduced to "1/2".
    /// </param>
    /// <param name="fraction">
    ///     When this method returns, contains the parsed fraction if the operation was successful; otherwise,
    ///     it contains the default value of <see cref="Fraction"/>.
    /// </param>
    /// <returns>
    ///     <c>true</c> if the input string is well-formed and could be parsed into a fraction; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     The <paramref name="numberStyles" /> parameter allows you to specify which number styles are allowed in the input
    ///     string.
    ///     For example, <see cref="NumberStyles.Float" /> allows decimal points and scientific notation.
    ///     The <paramref name="formatProvider" /> parameter provides culture-specific formatting information.
    ///     For example, you can use <c>CultureInfo.GetCultureInfo("en-US")</c> for US English culture.
    ///     Here are some examples of how to use the <see cref="TryParse(string, NumberStyles, IFormatProvider, bool, out Fraction)" /> method:
    ///     <example>
    ///         <code>
    /// Fraction.TryParse("3/4", NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), true, out var fraction);
    /// </code>
    ///         This example parses the string "3/4" into a <see cref="Fraction" /> object with a numerator of 3 and a
    ///         denominator of 4.
    ///         <code>
    /// Fraction.TryParse("1.25", NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), true, out var fraction);
    /// </code>
    ///         This example parses the string "1.25" into a <see cref="Fraction" /> object with a numerator of 5 and a
    ///         denominator of 4.
    ///         <code>
    /// Fraction.TryParse("1.23e-2", NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), true, out var fraction);
    /// </code>
    ///         This example parses the string "1.23e-2" into a <see cref="Fraction" /> object with a numerator of 123 and a
    ///         denominator of 10000.
    ///     </example>
    /// </remarks>
    public static bool TryParse(string? value, NumberStyles numberStyles, IFormatProvider? formatProvider,
        bool normalize, out Fraction fraction) {
        if (string.IsNullOrWhiteSpace(value)) {
            return CannotParse(out fraction);
        }

        if (value!.Length == 1) {
            if (!BigInteger.TryParse(value, numberStyles, formatProvider, out var singleDigit)) {
                var numberFormat = NumberFormatInfo.GetInstance(formatProvider);
                if (value.SequenceEqual(numberFormat.PositiveInfinitySymbol)) {
                    fraction = PositiveInfinity;
                    return true;
                }

                if (value.SequenceEqual(numberFormat.NegativeInfinitySymbol)) {
                    fraction = NegativeInfinity;
                    return true;
                }

                if (value.SequenceEqual(numberFormat.NaNSymbol)) {
                    fraction = NaN;
                    return true;
                }

                return CannotParse(out fraction);
            }

            fraction = new Fraction(singleDigit);
            return true;
        }

        var components = value.Split('/');

        if (components.Length >= 2) {
            var numeratorString = components[0];
            var denominatorString = components[1];

            var withoutDecimalPoint = numberStyles & ~NumberStyles.AllowDecimalPoint;
            if (!BigInteger.TryParse(
                    numeratorString,
                    withoutDecimalPoint,
                    formatProvider,
                    out var numerator)
                || !BigInteger.TryParse(
                    denominatorString,
                    withoutDecimalPoint,
                    formatProvider,
                    out var denominator)) {
                return CannotParse(out fraction);
            }

            fraction = new Fraction(numerator, denominator, normalize);
            return true;
        }

        // parsing a number using to the selected NumberStyles: e.g. " $ 12345.1234321e-4- " should result in -1.23451234321 with NumberStyles.Any
        var numberFormatInfo = NumberFormatInfo.GetInstance(formatProvider);
        // check for any of the special symbols (these cannot be combined with anything else)
        if (value == numberFormatInfo.NaNSymbol) {
            fraction = NaN;
            return true;
        }

        if (value == numberFormatInfo.PositiveInfinitySymbol) {
            fraction = PositiveInfinity;
            return true;
        }

        if (value == numberFormatInfo.NegativeInfinitySymbol) {
            fraction = NegativeInfinity;
            return true;
        }

        var currencyAllowed = (numberStyles & NumberStyles.AllowCurrencySymbol) != 0;

        // there "special" rules regarding the white-spaces after a leading currency symbol is detected
        var currencyDetected = false;

        var currencySymbol = numberFormatInfo.CurrencySymbol;
        if (currencyAllowed && string.IsNullOrEmpty(currencySymbol)) {
            numberStyles &= ~NumberStyles.AllowCurrencySymbol; // no currency symbol available
            currencyAllowed = false;
        }

        // note: decimal.TryParse relies on the CurrencySymbol (when currency is detected)
        var decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
        var decimalsAllowed = (numberStyles & NumberStyles.AllowDecimalPoint) != 0;

        var startIndex = 0;
        var endIndex = value.Length;
        var isNegative = false;

        // examine the leading characters
        do {
            var character = value[startIndex];
            if (char.IsDigit(character)) {
                break;
            }

            if (char.IsWhiteSpace(character)) {
                if ((numberStyles & NumberStyles.AllowLeadingWhite) == 0) {
                    return CannotParse(out fraction);
                }

                startIndex++;
                continue;
            }

            if (character == '(') {
                if ((numberStyles & NumberStyles.AllowParentheses) == 0) {
                    return CannotParse(out fraction); // not allowed
                }

                if (startIndex == endIndex - 1) {
                    return CannotParse(out fraction); // not enough characters
                }

                startIndex++; // consume the current character
                isNegative = true; // the closing parenthesis will be validated in the backwards iteration

                if (currencyDetected) {
                    // any number of white-spaces are allowed following a leading currency symbol (but no other signs)
                    numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                      NumberStyles.AllowTrailingSign |
                                      NumberStyles.AllowHexSpecifier);
                } else if (currencyAllowed && startsWith(value, currencySymbol, startIndex)) {
                    // there can be no more currency symbols but there could be more white-spaces (we skip and continue) 
                    currencyDetected = true;
                    startIndex += currencySymbol.Length;
                    numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                      NumberStyles.AllowTrailingSign |
                                      NumberStyles.AllowCurrencySymbol |
                                      NumberStyles.AllowHexSpecifier);
                } else {
                    // if next character is a white space the format should be rejected
                    numberStyles &= ~(NumberStyles.AllowLeadingWhite |
                                      NumberStyles.AllowLeadingSign |
                                      NumberStyles.AllowTrailingSign |
                                      NumberStyles.AllowHexSpecifier);
                    break;
                }

                continue;
            }

            if ((numberStyles & NumberStyles.AllowLeadingSign) != 0) {
                if (numberFormatInfo.NegativeSign is { Length: > 0 } negativeSign &&
                    startsWith(value, negativeSign, startIndex)) {
                    isNegative = true;
                    startIndex += negativeSign.Length;
                    if (currencyDetected) {
                        // any number of white-spaces are allowed following a leading currency symbol (but no other signs)
                        numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                          NumberStyles.AllowTrailingSign |
                                          NumberStyles.AllowParentheses |
                                          NumberStyles.AllowHexSpecifier);
                    } else if (currencyAllowed && startsWith(value, currencySymbol, startIndex)) {
                        // there can be no more currency symbols but there could be more white-spaces (we skip and continue) 
                        currencyDetected = true;
                        startIndex += currencySymbol.Length;
                        numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                          NumberStyles.AllowTrailingSign |
                                          NumberStyles.AllowParentheses |
                                          NumberStyles.AllowCurrencySymbol |
                                          NumberStyles.AllowHexSpecifier);
                    } else {
                        // if next character is a white space the format should be rejected
                        numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                          NumberStyles.AllowTrailingSign |
                                          NumberStyles.AllowParentheses |
                                          NumberStyles.AllowLeadingWhite |
                                          NumberStyles.AllowHexSpecifier);
                        break;
                    }

                    continue;
                }

                if (numberFormatInfo.PositiveSign is { Length: > 0 } positiveSign &&
                    startsWith(value, positiveSign, startIndex)) {
                    isNegative = false;
                    startIndex += positiveSign.Length;
                    if (currencyDetected) {
                        // any number of white-spaces are allowed following a leading currency symbol (but no other signs)
                        numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                          NumberStyles.AllowTrailingSign |
                                          NumberStyles.AllowParentheses |
                                          NumberStyles.AllowHexSpecifier);
                    } else if (currencyAllowed && startsWith(value, currencySymbol, startIndex)) {
                        // there can be no more currency symbols but there could be more white-spaces (we skip and continue) 
                        currencyDetected = true;
                        startIndex += currencySymbol.Length;
                        numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                          NumberStyles.AllowTrailingSign |
                                          NumberStyles.AllowParentheses |
                                          NumberStyles.AllowCurrencySymbol |
                                          NumberStyles.AllowHexSpecifier);
                    } else {
                        // if next character is a white space the format should be rejected
                        numberStyles &= ~(NumberStyles.AllowLeadingSign |
                                          NumberStyles.AllowTrailingSign |
                                          NumberStyles.AllowParentheses |
                                          NumberStyles.AllowLeadingWhite |
                                          NumberStyles.AllowHexSpecifier);
                        break;
                    }

                    continue;
                }
            }

            if (currencyAllowed && !currencyDetected && startsWith(value, currencySymbol, startIndex)) {
                // there can be no more currency symbols but there could be more white-spaces (we skip and continue)
                currencyDetected = true;
                numberStyles &= ~NumberStyles.AllowCurrencySymbol;
                startIndex += currencySymbol.Length;
                continue;
            }

            if (decimalsAllowed && startsWith(value, decimalSeparator, startIndex)) {
                break; // decimal string with no leading zeros
            }

            // this is either an expected hex string or an invalid format
            return (numberStyles & NumberStyles.AllowHexSpecifier) != 0
                ? TryParseInteger(value, numberStyles & ~NumberStyles.AllowTrailingSign, formatProvider, startIndex,
                    endIndex, isNegative, out fraction)
                : CannotParse(out fraction); // unexpected character
        } while (startIndex < endIndex);

        if (startIndex >= endIndex) {
            return CannotParse(out fraction);
        }

        if (isNegative) {
            numberStyles &= ~(NumberStyles.AllowLeadingWhite |
                              NumberStyles.AllowLeadingSign);
        } else {
            numberStyles &= ~(NumberStyles.AllowLeadingWhite |
                              NumberStyles.AllowLeadingSign |
                              NumberStyles.AllowParentheses);
        }

        // examine the trailing characters
        do {
            var character = value[endIndex - 1];
            if (char.IsDigit(character)) {
                break;
            }

            if (char.IsWhiteSpace(character)) {
                if ((numberStyles & NumberStyles.AllowTrailingWhite) == 0) {
                    return CannotParse(out fraction);
                }

                endIndex--;
                continue;
            }

            if (character == ')') {
                if ((numberStyles & NumberStyles.AllowParentheses) == 0) {
                    return CannotParse(out fraction); // not allowed
                }

                numberStyles &= ~(NumberStyles.AllowParentheses | NumberStyles.AllowCurrencySymbol);
                endIndex--;
                continue;
            }

            if ((numberStyles & NumberStyles.AllowTrailingSign) != 0) {
                if (numberFormatInfo.NegativeSign is { Length: > 0 } negativeSign &&
                    endsWith(value, negativeSign, endIndex)) {
                    isNegative = true;
                    numberStyles &= ~(NumberStyles.AllowTrailingSign | NumberStyles.AllowHexSpecifier);
                    endIndex -= negativeSign.Length;
                    continue;
                }

                if (numberFormatInfo.PositiveSign is { Length: > 0 } positiveSign &&
                    endsWith(value, positiveSign, endIndex)) {
                    isNegative = false;
                    numberStyles &= ~(NumberStyles.AllowTrailingSign | NumberStyles.AllowHexSpecifier);
                    endIndex -= positiveSign.Length;
                    continue;
                }
            }

            if (currencyAllowed && !currencyDetected && endsWith(value, currencySymbol, endIndex)) {
                // there can be no more currency symbols but there could be more white-spaces (we skip and continue)
                currencyDetected = true;
                numberStyles &= ~NumberStyles.AllowCurrencySymbol;
                endIndex -= currencySymbol.Length;
                continue;
            }

            if (decimalsAllowed && endsWith(value, decimalSeparator, endIndex)) {
                break;
            }

            // this is either an expected hex string or an invalid format
            return (numberStyles & NumberStyles.AllowHexSpecifier) != 0
                ? TryParseInteger(value, numberStyles & ~NumberStyles.AllowTrailingSign, formatProvider, startIndex,
                    endIndex, isNegative, out fraction)
                : CannotParse(out fraction); // unexpected character
        } while (startIndex < endIndex);

        if (startIndex >= endIndex) {
            return CannotParse(out fraction); // not enough characters
        }

        if (isNegative && (numberStyles & NumberStyles.AllowParentheses) != 0) {
            return CannotParse(out fraction); // failed to find a closing parentheses
        }

        numberStyles &= ~(NumberStyles.AllowTrailingWhite |
                          NumberStyles.AllowTrailingSign |
                          NumberStyles.AllowCurrencySymbol);
        // at this point value[startIndex, endIndex] should correspond to the number without the sign (or the format is invalid)

        if (startIndex == endIndex - 1) {
            // this can only be a single digit (integer)
            return TryParseInteger(value, numberStyles, formatProvider, startIndex, endIndex, isNegative, out fraction);
        }

        if ((numberStyles & NumberStyles.AllowExponent) != 0) {
            return TryParseWithExponent(value, numberStyles, numberFormatInfo, startIndex, endIndex, isNegative,
                normalize, out fraction);
        }

        return decimalsAllowed
            ? TryParseDecimalNumber(value, numberStyles, numberFormatInfo, startIndex, endIndex, isNegative, normalize,
                out fraction)
            : TryParseInteger(value, numberStyles, formatProvider, startIndex, endIndex, isNegative, out fraction);

        static bool startsWith(string fractionString, string testString, int startIndex) {
            if (fractionString.Length - startIndex < testString.Length) {
                return false;
            }

            return !testString.Where((t, i) => fractionString[i + startIndex] != t).Any();
        }

        static bool endsWith(string fractionString, string testString, int endIndex) {
            return endIndex >= testString.Length && startsWith(fractionString, testString, endIndex - testString.Length);
        }
    }

    private static bool TryParseWithExponent(string valueString, NumberStyles parseNumberStyles,
        NumberFormatInfo numberFormatInfo,
        int startIndex, int endIndex, bool isNegative, bool reduceTerms, out Fraction fraction) {
        // 1. try to find the exponent character index
        parseNumberStyles &= ~NumberStyles.AllowExponent;
        var exponentIndex = valueString.IndexOfAny(['e', 'E'], startIndex + 1, endIndex - startIndex - 1);
        if (exponentIndex == -1) {
            // no exponent found
            return (parseNumberStyles & NumberStyles.AllowDecimalPoint) != 0
                ? TryParseDecimalNumber(valueString, parseNumberStyles, numberFormatInfo, startIndex, endIndex,
                    isNegative, reduceTerms, out fraction)
                : TryParseInteger(valueString, parseNumberStyles, numberFormatInfo, startIndex, endIndex, isNegative,
                    out fraction);
        }

        // 2. try to parse the exponent (w.r.t. the scientific notation format)
        var exponentString = valueString.Substring(exponentIndex + 1, endIndex - exponentIndex - 1);
        if (!int.TryParse(exponentString, NumberStyles.AllowLeadingSign | NumberStyles.Integer, numberFormatInfo,
                out var exponent)) {
            return CannotParse(out fraction);
        }

        // 3. try to parse the coefficient (w.r.t. the decimal separator allowance)
        if (startIndex == endIndex - 1 || (parseNumberStyles & NumberStyles.AllowDecimalPoint) == 0) {
            if (!TryParseInteger(valueString, parseNumberStyles, numberFormatInfo, startIndex, exponentIndex,
                    isNegative, out fraction)) {
                return false;
            }
        } else {
            if (!TryParseDecimalNumber(valueString, parseNumberStyles, numberFormatInfo, startIndex, exponentIndex,
                    isNegative, reduceTerms, out fraction)) {
                return false;
            }
        }

        // 4. multiply the coefficient by 10 to the power of the exponent
        fraction *= Pow(TEN, exponent);
        return true;
    }

    private static bool TryParseDecimalNumber(string valueString, NumberStyles parseNumberStyles,
        NumberFormatInfo numberFormatInfo,
        int startIndex, int endIndex, bool isNegative, bool reduceTerms, out Fraction fraction) {
        // 1. find the position of the decimal separator (if any)
        var decimalSeparatorIndex = valueString.IndexOf(numberFormatInfo.NumberDecimalSeparator, startIndex,
            endIndex - startIndex, StringComparison.Ordinal);
        if (decimalSeparatorIndex == -1) {
            return TryParseInteger(valueString, parseNumberStyles, numberFormatInfo, startIndex, endIndex, isNegative,
                out fraction);
        }

        // 2. validate the format of the string after the radix
        var decimalSeparatorLength = numberFormatInfo.NumberDecimalSeparator.Length;
        if (startIndex + decimalSeparatorLength == endIndex) {
            // after excluding the sign, the input was reduced to just the decimal separator (with nothing on either sides)
            return CannotParse(out fraction);
        }

        if ((parseNumberStyles & NumberStyles.AllowThousands) != 0) {
            if (valueString.IndexOf(numberFormatInfo.NumberGroupSeparator,
                    decimalSeparatorIndex + decimalSeparatorLength, StringComparison.Ordinal) != -1) {
                return CannotParse(out fraction);
            }
        }

        // 3. extract the value of the string corresponding to the number without the decimal separator: " 0.123 " should return "0123"
        var integerString = string.Concat(
            valueString.Substring(startIndex, decimalSeparatorIndex - startIndex),
            valueString.Substring(decimalSeparatorIndex + decimalSeparatorLength,
                endIndex - decimalSeparatorIndex - decimalSeparatorLength));

        if (!BigInteger.TryParse(integerString, parseNumberStyles, numberFormatInfo, out var numerator)) {
            return CannotParse(out fraction);
        }

        if (numerator.IsZero && reduceTerms) {
            fraction = Zero;
            return true;
        }

        if (isNegative) {
            numerator = -numerator;
        }

        var nbDecimals = endIndex - decimalSeparatorIndex - decimalSeparatorLength;
        if (nbDecimals == 0) {
            fraction = new Fraction(numerator);
            return true;
        }

        // 4. construct the fractional part using the corresponding decimal power for the denominator
        var denominator = PowerOfTen(nbDecimals);
        fraction = reduceTerms ? ReduceSigned(numerator, denominator) : new Fraction(true, numerator, denominator);
        return true;
    }

    private static bool TryParseInteger(string valueString, NumberStyles parseNumberStyles,
        IFormatProvider? formatProvider,
        int startIndex, int endIndex, bool isNegative, out Fraction fraction) {
        if (!BigInteger.TryParse(valueString.Substring(startIndex, endIndex - startIndex), parseNumberStyles,
                formatProvider, out var bigInteger)) {
            return CannotParse(out fraction);
        }

        fraction = new Fraction(isNegative ? -bigInteger : bigInteger);
        return true;
    }

#endif

    /// <summary>
    ///     Sets the out parameter to the default value of Fraction and returns false.
    ///     This method is used when the parsing of a string to a Fraction fails.
    /// </summary>
    /// <param name="fraction">The Fraction object that will be set to its default value.</param>
    /// <returns>Always returns false.</returns>
    private static bool CannotParse(out Fraction fraction) {
        fraction = default;
        return false;
    }
}
