#nullable enable
using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Fractions.Properties;

namespace Fractions.Formatter;

/// <summary>
///     Provides functionality to format the value of a Fraction object into a decimal string representation following the
///     standard numeric formats, as implemented by the double type.
/// </summary>
/// <remarks>
///     This class implements the <see cref="ICustomFormatter" /> interface and provides custom formatting for objects of
///     type <see cref="Fraction" />.
///     It supports a variety of format specifiers, including general, fixed-point, standard numeric, scientific, and
///     significant digits after radix formats.
/// </remarks>
/// <example>
///     Here is an example of how to use the `DecimalFractionFormatter`:
///     <code>
/// Fraction fraction = new Fraction(1, 2);
/// ICustomFormatter formatter = DecimalFractionFormatter.Instance;
/// string formatted = formatter.Format("f", fraction, CultureInfo.InvariantCulture);
/// Console.WriteLine(formatted);  // Outputs: "0.50"
/// </code>
/// </example>
/// <seealso cref="ICustomFormatter" />
/// <seealso cref="Fraction" />
public class DecimalNotationFormatter : ICustomFormatter {
    /// <summary>
    ///     <list type="bullet">
    ///         <item>
    ///             On .NET Framework and .NET Core up to .NET Core 2.0, the runtime selects the result with the greater least
    ///             significant digit (that is, using <see cref="MidpointRounding.AwayFromZero" />) for both the double and decimal rounding.
    ///         </item>
    ///         <item>
    ///             On .NET Core 2.1 and later, the EEE‑754 floating‑point (e.g. double) numbers appear to be formatted using some variant of the Ry¯u: Fast Float-to-String Conversion.
    ///         </item>
    ///     </list>   
    /// </summary>
    private const MidpointRounding DefaultMidpointRoundingMode =  MidpointRounding.AwayFromZero;

    /// <summary>
    ///     The default precision used for the general format specifier (G)
    /// </summary>
    public const int DefaultGeneralFormatPrecision =
#if NETCOREAPP2_0_OR_GREATER
        16;
#else
        15;
#endif

    /// <summary>
    ///     The default precision used for the exponential (scientific) format specifier (E)
    /// </summary>
    public const int DefaultScientificFormatPrecision = 6;

    /// <summary>
    ///     Gets the singleton instance of the DecimalFractionFormatter class.
    /// </summary>
    /// <value>
    ///     The singleton instance of the DecimalFractionFormatter class.
    /// </value>
    /// <remarks>
    ///     This instance can be used to format Fraction objects into decimal string representations.
    /// </remarks>
    public static DecimalNotationFormatter Instance { get; } = new();

    /// <summary>
    /// <inheritdoc cref="Format(string?,Fraction,System.IFormatProvider?)" path="/summary"/>
    /// </summary>
    /// <param name="format"><inheritdoc cref="Format(string?,Fraction,System.IFormatProvider?)" path="/param[@name='format']"/></param>
    /// <param name="value"><inheritdoc cref="Format(string?,Fraction,System.IFormatProvider?)" path="/param[@name='fraction']"/></param>
    /// <param name="formatProvider"><inheritdoc cref="Format(string?,Fraction,System.IFormatProvider?)" path="/param[@name='formatProvider']"/></param>
    /// <returns><inheritdoc cref="Format(string?,Fraction,System.IFormatProvider?)" path="/returns"/></returns>
    /// <exception cref="FormatException">If <paramref name="value"/> is not of type <see cref="Fraction"/>.</exception>
    /// <remarks><inheritdoc cref="Format(string?,Fraction,System.IFormatProvider?)" path="/remarks"/></remarks>
    public string Format(string? format, object? value, IFormatProvider? formatProvider) {
        if (value is null) {
            return string.Empty;
        }

        if (value is not Fraction fraction) {
            throw new FormatException(string.Format(Resources.TypeXnotSupported, value.GetType()));
        }

        return Format(format, fraction, formatProvider);
    }
    
    /// <summary>
    ///     Formats the value of the specified Fraction as a string using the specified format.
    /// </summary>
    /// <param name="format">A standard or custom numeric format string.</param>
    /// <param name="fraction">The Fraction object to be formatted.</param>
    /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
    /// <returns>
    ///     The string representation of the value of the Fraction object as specified by the format and formatProvider
    ///     parameters.
    /// </returns>
    /// <remarks>
    ///     This method supports the following format strings:
    ///     <list type="bullet">
    ///         <item>
    ///             <description>
    ///                 <see
    ///                     href="https://docs.microsoft.com/dotnet/standard/base-types/standard-numeric-format-strings#the-general-g-format-specifier">
    ///                     'G'
    ///                     or 'g'
    ///                 </see>
    ///                 : General format. Example: 400/3 formatted with 'G2' gives "1.3E+02".
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see
    ///                     href="https://docs.microsoft.com/dotnet/standard/base-types/standard-numeric-format-strings#the-fixed-point-f-format-specifier">
    ///                     'F'
    ///                     or 'f'
    ///                 </see>
    ///                 : Fixed-point format. Example: 12345/10 formatted with 'F2' gives "1234.50".
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see
    ///                     href="https://docs.microsoft.com/dotnet/standard/base-types/standard-numeric-format-strings#the-number-n-format-specifier">
    ///                     'N'
    ///                     or 'n'
    ///                 </see>
    ///                 : Standard Numeric format. Example: 1234567/1000 formatted with 'N2' gives "1,234.57".
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see
    ///                     href="https://docs.microsoft.com/dotnet/standard/base-types/standard-numeric-format-strings#the-exponential-e-format-specifier">
    ///                     'E'
    ///                     or 'e'
    ///                 </see>
    ///                 : Scientific format. Example: 1234567/1000 formatted with 'E2' gives "1.23E+003".
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see
    ///                     href="https://docs.microsoft.com/dotnet/standard/base-types/standard-numeric-format-strings#the-percent-p-format-specifier">
    ///                     'P'
    ///                     or 'p'
    ///                 </see>
    ///                 : Percent format. Example: 2/3 formatted with 'P2' gives "66.67 %".
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see
    ///                     href="https://docs.microsoft.com/dotnet/standard/base-types/standard-numeric-format-strings#the-currency-c-format-specifier">
    ///                     'C'
    ///                     or 'c'
    ///                 </see>
    ///                 : Currency format. Example: 1234567/1000 formatted with 'C2' gives "$1,234.57".
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see
    ///                     href="https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#RFormatString">
    ///                     'R'
    ///                     or 'r'
    ///                 </see>
    ///                 : Round-trip format. Example: 1234567/1000 formatted with 'R' gives "1234.567".
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 <see
    ///                     href="https://github.com/danm-de/Fractions?tab=readme-ov-file#significant-digits-after-radix-format">
    ///                     'S'
    ///                     or 's'
    ///                 </see>
    ///                 : Significant Digits After Radix format. Example: 400/3 formatted with 'S2' gives
    ///                 "133.33".
    ///             </description>
    ///         </item>
    ///     </list>
    ///     Note: The 'R' format and custom formats do not support precision specifiers and are handed over to the `double`
    ///     type for formatting, which may result in a loss of precision.
    ///     For more information about the formatter, see the
    ///     <see href="https://github.com/danm-de/Fractions?tab=readme-ov-file#decimalnotationformatter">
    ///         DecimalNotationFormatter
    ///         section
    ///     </see>
    ///     in the GitHub README.
    /// </remarks>
    public static string Format(string? format, Fraction fraction, IFormatProvider? formatProvider) {
        formatProvider ??= CultureInfo.CurrentCulture;
        var numberFormatInfo = (NumberFormatInfo)formatProvider.GetFormat(typeof(NumberFormatInfo))!;

        if (fraction.IsPositiveInfinity) {
            return numberFormatInfo.PositiveInfinitySymbol;
        }

        if (fraction.IsNegativeInfinity) {
            return numberFormatInfo.NegativeInfinitySymbol;
        }

        if (fraction.IsNaN) {
            return numberFormatInfo.NaNSymbol;
        }

        if (string.IsNullOrEmpty(format)) {
            return FormatGeneral(fraction, "G", numberFormatInfo);
        }

        var formatCharacter = format![0];
        return formatCharacter switch {
            'G' or 'g' => FormatGeneral(fraction, format, numberFormatInfo),
            'F' or 'f' => FormatWithFixedPointFormat(fraction, format, numberFormatInfo),
            'N' or 'n' => FormatWithStandardNumericFormat(fraction, format, numberFormatInfo),
            'E' or 'e' => FormatWithScientificFormat(fraction, format, numberFormatInfo),
            'P' or 'p' => FormatWithPercentFormat(fraction, format, numberFormatInfo),
            'C' or 'c' => FormatWithCurrencyFormat(fraction, format, numberFormatInfo),
            'S' or 's' => FormatWithSignificantDigitsAfterRadix(fraction, format, numberFormatInfo),
            _ => // 'R', 'r' and the custom formats are handed over to the double (possible loss of precision) 
                fraction.ToDouble().ToString(format, formatProvider)
        };
    }

    private static bool TryGetPrecisionDigits(string format, int defaultPrecision, out int maxNbDecimals) {
        if (format.Length == 1) {
            // The number of digits is not specified, use default precision.
            maxNbDecimals = defaultPrecision;
            return true;
        }

#if NET
        if (int.TryParse(format.AsSpan(1), out maxNbDecimals)) {
            return true;
        }
#else
        if (int.TryParse(format.Substring(1), out maxNbDecimals)) {
            return true;
        }
#endif

        // Seems to be some kind of custom format we do not understand, fallback to default precision.
        maxNbDecimals = defaultPrecision;
        return false;
    }

    /// <summary>
    ///     The fixed-point ("F") format specifier converts a number to a string of the form "-ddd.ddd…" where each "d"
    ///     indicates a digit (0-9). The string starts with a minus sign if the number is negative.
    /// </summary>
    /// <remarks>
    ///     The precision specifier indicates the desired number of decimal places. If the precision specifier is omitted, the
    ///     current <see cref="NumberFormatInfo.NumberDecimalDigits" /> property supplies the numeric precision.
    /// </remarks>
    private static string FormatWithFixedPointFormat(Fraction fraction, string format, NumberFormatInfo formatProvider) {
        if (fraction.Numerator.IsZero) {
            return 0.ToString(format, formatProvider);
        }

        if (fraction.Denominator.IsOne) {
            return fraction.Numerator.ToString(format, formatProvider)!;
        }

        if (!TryGetPrecisionDigits(format, formatProvider.NumberDecimalDigits, out var maxNbDecimalsAfterRadix)) {
            // not a valid "F" format: assuming a custom format
            return fraction.ToDouble().ToString(format, formatProvider);
        }

        if (maxNbDecimalsAfterRadix == 0) {
            return Round(fraction.Numerator, fraction.Denominator).ToString(format, formatProvider)!;
        }

        var roundedFraction = Round(fraction, maxNbDecimalsAfterRadix);
        bool isPositive;
        switch (roundedFraction.Numerator.Sign)
        {
            case 0:
                return 0.ToString(format, formatProvider);
            case 1:
                isPositive = true;
                break;
            default:
                isPositive = false;
                roundedFraction = -roundedFraction;
                break;
        }

        var sb = new StringBuilder(12 + maxNbDecimalsAfterRadix);
        if (!isPositive) {
            sb.Append(formatProvider.NegativeSign);
        }

        return AppendDecimals(sb, roundedFraction, formatProvider, maxNbDecimalsAfterRadix).ToString();
    }

    /// <summary>
    ///     The numeric ("N") format specifier converts a number to a string of the form "-d,ddd,ddd.ddd…", where "-"
    ///     indicates
    ///     a negative number symbol if required, "d" indicates a digit (0-9), "," indicates a group separator, and "."
    ///     indicates a decimal point symbol.
    /// </summary>
    /// <remarks>
    ///     The precision specifier indicates the desired number of decimal places. If the precision specifier is omitted, the
    ///     current <see cref="NumberFormatInfo.NumberDecimalDigits" /> property supplies the numeric precision.
    /// </remarks>
    private static string FormatWithStandardNumericFormat(Fraction fraction, string format, NumberFormatInfo formatProvider) {
        if (fraction.Numerator.IsZero) {
            return 0.ToString(format, formatProvider);
        }

        if (fraction.Denominator.IsOne) {
            return fraction.Numerator.ToString(format, formatProvider)!;
        }
        
        if (!TryGetPrecisionDigits(format, formatProvider.NumberDecimalDigits, out var maxNbDecimals)) {
            // not a valid "N" format: assuming a custom format
            return fraction.ToDouble().ToString(format, formatProvider);
        }

        if (maxNbDecimals == 0) {
            var roundedValue = Round(fraction.Numerator, fraction.Denominator);
            return roundedValue.ToString(format, formatProvider)!;
        }

        var roundedFraction = Round(fraction, maxNbDecimals);
        bool isPositive;
        switch (roundedFraction.Numerator.Sign)
        {
            case 0:
                return 0.ToString(format, formatProvider);
            case 1:
                isPositive = true;
                break;
            default:
                isPositive = false;
                roundedFraction = -roundedFraction;
                break;
        }
        
        var sb = AppendDecimals(new StringBuilder(6 + maxNbDecimals), roundedFraction, formatProvider, maxNbDecimals, "N0");
        return isPositive
            ? sb.ToString()
            : withNegativeSign(sb, formatProvider.NegativeSign, formatProvider.NumberNegativePattern);

        static string withNegativeSign(StringBuilder sb, string negativeSignSymbol, int pattern) =>
            pattern switch {
                0 => // (n)
                    sb.Insert(0, '(').Append(')').ToString(),
                1 => // -n
                    sb.Insert(0, negativeSignSymbol).ToString(),
                2 => // - n
                    sb.Insert(0, negativeSignSymbol + ' ').ToString(),
                3 => // n-
                    sb.Append(negativeSignSymbol).ToString(),
                4 => // n -
                    sb.Append(' ').Append(negativeSignSymbol).ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(pattern))
            };
    }

    /// <summary>
    ///     The percent ("P") format specifier multiplies a number by 100 and converts it to a string that represents a
    ///     percentage.
    /// </summary>
    /// <remarks>
    ///     The precision specifier indicates the desired number of decimal places. If the precision specifier is omitted,
    ///     the default numeric precision supplied by the current <see cref="NumberFormatInfo.PercentDecimalDigits" /> property
    ///     is used.
    /// </remarks>
    private static string FormatWithPercentFormat(Fraction fraction, string format, NumberFormatInfo formatProvider) {
        if (fraction.Numerator.IsZero) {
            return 0.ToString(format, formatProvider);
        }

        if (fraction.Denominator.IsOne) {
            return fraction.Numerator.ToString(format, formatProvider)!;
        }
        
        if (!TryGetPrecisionDigits(format, formatProvider.PercentDecimalDigits, out var maxNbDecimals)) {
            // not a valid "P" format: assuming a custom format
            return fraction.ToDouble().ToString(format, formatProvider);
        }

        StringBuilder sb;
        bool isPositive;
        if (maxNbDecimals == 0) {
            var roundedValue = Round(fraction.Numerator * 100, fraction.Denominator);
            switch (roundedValue.Sign)
            {
                case 0:
                    return 0.ToString(format, formatProvider);
                case 1:
                    isPositive = true;
                    break;
                default:
                    isPositive = false;
                    roundedValue = -roundedValue;
                    break;
            }

            var percentFormatInfo = new NumberFormatInfo {
                NumberDecimalSeparator = formatProvider.PercentDecimalSeparator,
                NumberGroupSeparator = formatProvider.PercentGroupSeparator,
                NumberGroupSizes = formatProvider.PercentGroupSizes,
                NativeDigits = formatProvider.NativeDigits,
                DigitSubstitution = formatProvider.DigitSubstitution
            };
            sb = new StringBuilder(roundedValue.ToString("N0", percentFormatInfo));
        } else {
            var roundedFraction = Round(fraction * 100, maxNbDecimals);
            switch (roundedFraction.Numerator.Sign)
            {
                case 0:
                    return 0.ToString(format, formatProvider);
                case 1:
                    isPositive = true;
                    break;
                default:
                    isPositive = false;
                    roundedFraction = -roundedFraction;
                    break;
            }

            var percentFormatInfo = new NumberFormatInfo {
                NumberDecimalSeparator = formatProvider.PercentDecimalSeparator,
                NumberGroupSeparator = formatProvider.PercentGroupSeparator,
                NumberGroupSizes = formatProvider.PercentGroupSizes,
                NativeDigits = formatProvider.NativeDigits,
                DigitSubstitution = formatProvider.DigitSubstitution
            };
            sb = AppendDecimals(new StringBuilder(8 + maxNbDecimals), roundedFraction, percentFormatInfo, maxNbDecimals, "N0");
        }

        return isPositive
            ? withPositiveSign(sb, formatProvider.PercentSymbol, formatProvider.PercentPositivePattern)
            : withNegativeSign(sb, formatProvider.PercentSymbol, formatProvider.NegativeSign,
                formatProvider.PercentNegativePattern);

        static string withPositiveSign(StringBuilder sb, string percentSymbol, int pattern) =>
            pattern switch {
                0 => // n %
                    sb.Append(' ').Append(percentSymbol).ToString(),
                1 => // n%
                    sb.Append(percentSymbol).ToString(),
                2 => // %n
                    sb.Insert(0, percentSymbol).ToString(),
                3 => // % n
                    sb.Insert(0, percentSymbol + ' ').ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(pattern))
            };

        static string withNegativeSign(StringBuilder sb, string percentSymbol, string negativeSignSymbol, int pattern) =>
            pattern switch {
                0 => // -n %
                    sb.Insert(0, negativeSignSymbol).Append(' ').Append(percentSymbol).ToString(),
                1 => // -n%
                    sb.Insert(0, negativeSignSymbol).Append(percentSymbol).ToString(),
                2 => // -%n
                    sb.Insert(0, negativeSignSymbol + percentSymbol).ToString(),
                3 => // %-n
                    sb.Insert(0, percentSymbol + negativeSignSymbol).ToString(),
                4 => // %n-
                    sb.Insert(0, percentSymbol).Append(negativeSignSymbol).ToString(),
                5 => // n-%
                    sb.Append(negativeSignSymbol).Append(percentSymbol).ToString(),
                6 => // n%-
                    sb.Append(percentSymbol).Append(negativeSignSymbol).ToString(),
                7 => // -% n
                    sb.Insert(0, negativeSignSymbol + percentSymbol + ' ').ToString(),
                8 => // n %-
                    sb.Append(' ').Append(percentSymbol).Append(negativeSignSymbol).ToString(),
                9 => // % n-
                    sb.Insert(0, percentSymbol + ' ').Append(negativeSignSymbol).ToString(),
                10 => // % -n
                    sb.Insert(0, percentSymbol + ' ').Insert(2, negativeSignSymbol).ToString(),
                11 => // n- %
                    sb.Append(negativeSignSymbol).Append(' ').Append(percentSymbol).ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(pattern))
            };
    }


    /// <summary>
    ///     The "C" (or currency) format specifier converts a number to a string that represents a currency amount. The
    ///     precision specifier indicates the desired number of decimal places in the result string. If the precision specifier
    ///     is omitted, the default precision is defined by the <see cref="NumberFormatInfo.CurrencyDecimalDigits" /> property.
    /// </summary>
    /// <remarks>
    ///     If the value to be formatted has more than the specified or default number of decimal places, the fractional
    ///     value is rounded in the result string. If the value to the right of the number of specified decimal places is 5 or
    ///     greater, the last digit in the result string is rounded away from zero.
    /// </remarks>
    private static string FormatWithCurrencyFormat(Fraction fraction, string format, NumberFormatInfo formatProvider) {
        if (fraction.Numerator.IsZero) {
            return 0.ToString(format, formatProvider);
        }

        if (fraction.Denominator.IsOne) {
            return fraction.Numerator.ToString(format, formatProvider)!;
        }

        if (!TryGetPrecisionDigits(format, formatProvider.CurrencyDecimalDigits, out var maxNbDecimals)) {
            // not a valid "C" format: assuming a custom format
            return fraction.ToDouble().ToString(format, formatProvider);
        }

        if (maxNbDecimals == 0) {
            var roundedValue = Round(fraction.Numerator, fraction.Denominator);
            return roundedValue.ToString(format, formatProvider)!;
        }

        var roundedFraction = Round(fraction, maxNbDecimals);
        
        bool isPositive;
        switch (roundedFraction.Numerator.Sign)
        {
            case 0:
                return 0.ToString(format, formatProvider);
            case 1:
                isPositive = true;
                break;
            default:
                isPositive = false;
                roundedFraction = -roundedFraction;
                break;
        }

        var currencyFormatInfo = new NumberFormatInfo {
            NumberDecimalSeparator = formatProvider.CurrencyDecimalSeparator,
            NumberGroupSeparator = formatProvider.CurrencyGroupSeparator,
            NumberGroupSizes = formatProvider.CurrencyGroupSizes,
            NativeDigits = formatProvider.NativeDigits,
            DigitSubstitution = formatProvider.DigitSubstitution
        };

        var sb = AppendDecimals(new StringBuilder(8 + maxNbDecimals), roundedFraction, currencyFormatInfo, maxNbDecimals, "N0");
        return isPositive
            ? withPositiveSign(sb, formatProvider.CurrencySymbol, formatProvider.CurrencyPositivePattern)
            : withNegativeSign(sb, formatProvider.CurrencySymbol, formatProvider.NegativeSign,
                formatProvider.CurrencyNegativePattern);

        static string withPositiveSign(StringBuilder sb, string currencySymbol, int pattern) =>
            pattern switch {
                0 => // $n
                    sb.Insert(0, currencySymbol).ToString(),
                1 => // n$
                    sb.Append(currencySymbol).ToString(),
                2 => // $ n
                    sb.Insert(0, currencySymbol + ' ').ToString(),
                3 => // n $
                    sb.Append(' ').Append(currencySymbol).ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(pattern))
            };

        static string withNegativeSign(StringBuilder sb, string currencySymbol, string negativeSignSymbol,
            int pattern) =>
            pattern switch {
                0 => // ($n)
                    sb.Insert(0, '(').Insert(1, currencySymbol).Append(')').ToString(),
                1 => // -$n
                    sb.Insert(0, negativeSignSymbol + currencySymbol).ToString(),
                2 => // $-n
                    sb.Insert(0, currencySymbol + negativeSignSymbol).ToString(),
                3 => // $n-
                    sb.Insert(0, currencySymbol).Append(negativeSignSymbol).ToString(),
                4 => // (n$)
                    sb.Insert(0, '(').Append(currencySymbol).Append(')').ToString(),
                5 => // -n$
                    sb.Insert(0, negativeSignSymbol).Append(currencySymbol).ToString(),
                6 => // n-$
                    sb.Append(negativeSignSymbol).Append(currencySymbol).ToString(),
                7 => // n$-
                    sb.Append(currencySymbol).Append(negativeSignSymbol).ToString(),
                8 => // -n $
                    sb.Insert(0, negativeSignSymbol).Append(' ').Append(currencySymbol).ToString(),
                9 => // -$ n
                    sb.Insert(0, negativeSignSymbol + currencySymbol + ' ').ToString(),
                10 => // n $-
                    sb.Append(' ').Append(currencySymbol).Append(negativeSignSymbol).ToString(),
                11 => // $ n-
                    sb.Insert(0, currencySymbol + ' ').Append(negativeSignSymbol).ToString(),
                12 => // $ -n
                    sb.Insert(0, currencySymbol + ' ' + negativeSignSymbol).ToString(),
                13 => // n- $
                    sb.Append(negativeSignSymbol).Append(' ').Append(currencySymbol).ToString(),
                14 => // ($ n)
                    sb.Insert(0, '(').Insert(1, currencySymbol + ' ').Append(')').ToString(),
                15 => // (n $)
                    sb.Insert(0, '(').Append(' ').Append(currencySymbol).Append(')').ToString(),
#if NETSTANDARD
                _ => // On .NET Core 3.1 and earlier versions, this exception is thrown if the value is greater than 15. (https://learn.microsoft.com/en-us/dotnet/api/system.globalization.numberformatinfo.currencynegativepattern?view=net-9.0)
                    throw new ArgumentOutOfRangeException(nameof(pattern))
#else
                16 => 
                    sb.Insert(0, currencySymbol + negativeSignSymbol + ' ').ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(pattern))
#endif
            };
    }

    /// <summary>
    ///     Exponential format specifier (E)
    ///     The exponential ("E") format specifier converts a number to a string of the form "-d.ddd…E+ddd" or "-d.ddd…e+ddd",
    ///     where each "d" indicates a digit (0-9). The string starts with a minus sign if the number is negative. Exactly one
    ///     digit always precedes the decimal point.
    /// </summary>
    /// <remarks>
    ///     The precision specifier indicates the desired number of digits after the decimal point. If the precision specifier
    ///     is omitted, a default of six digits after the decimal point is used.
    ///     The case of the format specifier indicates whether to prefix the exponent with an "E" or an "e". The exponent
    ///     always consists of a plus or minus sign and a minimum of three digits. The exponent is padded with zeros to meet
    ///     this minimum, if required.
    /// </remarks>
    private static string FormatWithScientificFormat(Fraction fraction, string format, NumberFormatInfo formatProvider) {
        if (fraction.Numerator.IsZero) {
            return 0.ToString(format, formatProvider);
        }

        if (fraction.Denominator.IsOne) {
            return fraction.Numerator.ToString(format, formatProvider)!;
        }
        
        if (!TryGetPrecisionDigits(format, DefaultScientificFormatPrecision, out var maxNbDecimals)) {
            // not a valid "E" format: assuming a custom format
            return fraction.ToDouble().ToString(format, formatProvider);
        }
        
        var sb = new StringBuilder(DefaultScientificFormatPrecision + maxNbDecimals);

        if (fraction.IsNegative) {
            sb.Append(formatProvider.NegativeSign);
            fraction = fraction.Abs();
        }

        var exponent = GetExponentPower(fraction.Numerator, fraction.Denominator, out var exponentTerm);
        var mantissa = exponent switch {
            0 => Round(fraction, maxNbDecimals),
            > 0 => Round(fraction / exponentTerm, maxNbDecimals),
            _ => Round(fraction * exponentTerm, maxNbDecimals)
        };

        if (mantissa.Denominator.IsOne) {
            sb.Append(mantissa.Numerator.ToString($"F{maxNbDecimals}", formatProvider));
        } else {
            AppendDecimals(sb, mantissa, formatProvider, maxNbDecimals);
        }

        var symbol = format[0];

        return exponent >= 0
            ? sb.Append(symbol)
                .Append(formatProvider.PositiveSign)
                .Append(exponent.ToString("D3", formatProvider)).ToString()
            : sb.Append(symbol)
                .Append(exponent.ToString("D3", formatProvider)).ToString();
    }

    /// <summary>
    ///     The general ("G") format specifier converts a number to the more compact of either fixed-point or scientific
    ///     notation, depending on the type of the number and whether a precision specifier is present.
    /// </summary>
    /// <remarks>
    ///     The precision specifier
    ///     defines the maximum number of significant digits that can appear in the result string. If the precision specifier
    ///     is omitted or zero, the type of the number determines the default precision.
    /// </remarks>
    private static string FormatGeneral(Fraction fraction, string format, NumberFormatInfo formatProvider) {
        if (fraction.Numerator.IsZero) {
            return 0.ToString(format, formatProvider);
        }

        if (!TryGetPrecisionDigits(format, DefaultGeneralFormatPrecision, out var maxNbDecimals)) {
            // not a valid "G" format: assuming a custom format
            return fraction.ToDouble().ToString(format, formatProvider);
        }

        if (maxNbDecimals == 0) {
            maxNbDecimals = DefaultGeneralFormatPrecision;
        }

        var sb = new StringBuilder(3 + maxNbDecimals);

        if (fraction.IsNegative) {
            sb.Append(formatProvider.NegativeSign);
            fraction = fraction.Abs();
        }

        var exponent = GetExponentPower(fraction.Numerator, fraction.Denominator, out var exponentTerm);

        if (exponent == maxNbDecimals - 1) {
            // integral result: both 123400 (1.234e5) and 123400.01 (1.234001e+005) result in "123400" with the "G6" format
            return sb.Append(Round(fraction.Numerator, fraction.Denominator)).ToString();
        }

        if (exponent > maxNbDecimals - 1) {
            // we are required to shorten down a number of the form 123400 (1.234E+05)
            if (maxNbDecimals == 1) {
                sb.Append(Round(fraction.Numerator, fraction.Denominator * exponentTerm));
            } else {
                var mantissa = Round(new Fraction(fraction.Numerator, fraction.Denominator * exponentTerm, false), maxNbDecimals - 1);
                AppendSignificantDecimals(sb, mantissa, formatProvider, maxNbDecimals - 1);
            }
            
            return AppendExponentWithSignificantDigits(sb, exponent, formatProvider, format[0] is 'g' ? 'e' : 'E')
                .ToString();
        }

        if (exponent <= -5) {
            // the largest value would have the form: 1.23e-5 (0.000123)
            var mantissa = Round(fraction * exponentTerm, maxNbDecimals - 1);
            AppendSignificantDecimals(sb, mantissa, formatProvider, maxNbDecimals - 1);
            return AppendExponentWithSignificantDigits(sb, exponent, formatProvider, format[0] is 'g' ? 'e' : 'E')
                .ToString();
        }
        
        // the smallest value would have the form: 1.23e-4 (0.00123)
        var roundedDecimal = Round(fraction, maxNbDecimals - exponent - 1);
        return AppendSignificantDecimals(sb, roundedDecimal, formatProvider, maxNbDecimals - exponent - 1)
            .ToString();
    }

    /// <summary>
    ///     Formats the given fraction with a specified number of significant digits after the radix point.
    /// </summary>
    /// <param name="fraction">The fraction to format.</param>
    /// <param name="format">
    ///     The format string to use. The format string should specify the maximum number of digits after the
    ///     radix point.
    /// </param>
    /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
    /// <returns>
    ///     A string representation of the fraction, formatted with the specified number of significant digits after the
    ///     radix point.
    /// </returns>
    /// <remarks>
    ///     The method formats the fraction based on the absolute value of the fraction:
    ///     <list type="bullet">
    ///         <item>
    ///             For values greater than 1e5 (e.g., 1230000), the fraction is formatted as a number with an exponent (e.g.,
    ///             1.23e6).
    ///         </item>
    ///         <item>
    ///             For values less than or equal to 1e-4 (e.g., 0.000123), the fraction is formatted as a number with an
    ///             exponent (e.g., 1.23e-4).
    ///         </item>
    ///         <item>
    ///             For values between 1e-3 and 1e5 (e.g., 0.00123 to 123000), the fraction is formatted as a decimal number.
    ///         </item>
    ///     </list>
    /// </remarks>
    private static string FormatWithSignificantDigitsAfterRadix(Fraction fraction, string format,
        NumberFormatInfo formatProvider) {
        if (fraction.Numerator.IsZero) {
            return 0.ToString(formatProvider);
        }

        const string quotientFormat = "N0";
        
        if (!TryGetPrecisionDigits(format, 2, out var maxDigitsAfterRadix)) {
            // not a valid "S" format: assuming a custom format
            return fraction.ToDouble().ToString(format, formatProvider);
        }
        
        var sb = new StringBuilder(3 + maxDigitsAfterRadix);

        if (fraction.IsNegative) {
            sb.Append(formatProvider.NegativeSign);
            fraction = fraction.Abs();
        }

        var exponent = GetExponentPower(fraction.Numerator, fraction.Denominator, out var exponentTerm);
        Fraction mantissa;

        switch (exponent) {
            case > 5:
                // the smallest value would have the form: 1.23e6 (1230000)
                // mantissa = Round(fraction / exponentTerm, maxDigitsAfterRadix);
                mantissa = Round(new Fraction(fraction.Numerator, fraction.Denominator * exponentTerm, false), maxDigitsAfterRadix);
                AppendSignificantDecimals(sb, mantissa, formatProvider, maxDigitsAfterRadix, quotientFormat);
                return AppendExponentWithSignificantDigits(sb, exponent, formatProvider, format[0] is 's' ? 'e' : 'E')
                    .ToString();
            case <= -4:
                // the largest value would have the form: 1.23e-4 (0.000123)
                // mantissa = Round(fraction * exponentTerm, maxDigitsAfterRadix);
                mantissa = Round(new Fraction(fraction.Numerator * exponentTerm, fraction.Denominator, false), maxDigitsAfterRadix);
                AppendSignificantDecimals(sb, mantissa, formatProvider, maxDigitsAfterRadix, quotientFormat);
                return AppendExponentWithSignificantDigits(sb, exponent, formatProvider, format[0] is 's' ? 'e' : 'E')
                    .ToString();
            case < 0:
                // the smallest value would have the form: 1.23e-3 (0.00123)
                var leadingZeroes = -exponent;
                maxDigitsAfterRadix += leadingZeroes - 1;
                mantissa = Round(fraction, maxDigitsAfterRadix);
                return AppendSignificantDecimals(sb, mantissa, formatProvider, maxDigitsAfterRadix, quotientFormat)
                    .ToString();
            default:
                // the largest value would have the form: 1.23e5 (123000)
                mantissa = Round(fraction, maxDigitsAfterRadix);
                return AppendSignificantDecimals(sb, mantissa, formatProvider, maxDigitsAfterRadix, quotientFormat)
                    .ToString();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static BigInteger PreviousPowerOfTen(BigInteger powerOfTen, int exponent) =>
        exponent <= Fraction.PowersOfTen.Length
            ? Fraction.PowersOfTen[exponent - 1]
            : powerOfTen / Fraction.TEN;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static BigInteger NextPowerOfTen(BigInteger powerOfTen, int exponent) =>
        exponent + 1 < Fraction.PowersOfTen.Length
            ? Fraction.PowersOfTen[exponent + 1]
            : powerOfTen * Fraction.TEN;

    /// <summary>
    ///     Appends the decimal representation of a fraction to the specified <see cref="StringBuilder" />.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder" /> to which the decimal representation will be appended.</param>
    /// <param name="decimalFraction">The <see cref="Fraction" /> to be represented in decimal form.</param>
    /// <param name="formatProvider">The <see cref="NumberFormatInfo" /> that provides culture-specific formatting information.</param>
    /// <param name="nbDecimals">The maximum number of decimal places to include in the representation.</param>
    /// <param name="quotientFormat">
    ///     An optional format string for formatting the quotient part of the fraction.
    ///     Defaults to <c>"F0"</c>.
    /// </param>
    /// <returns>The <see cref="StringBuilder" /> with the appended decimal representation.</returns>
    /// <remarks>
    ///     This method calculates the decimal representation of the given fraction by dividing the numerator
    ///     by the denominator and appending the result to the <paramref name="sb" />, padding with "0" to ensure the specified
    ///     number of decimal places is respected.
    ///     <para>
    ///         The <paramref name="decimalFraction" /> must represent a value with a denominator that is a power of 10,
    ///         equal to the specified <paramref name="nbDecimals" />.
    ///     </para>
    /// </remarks>
    private static StringBuilder AppendDecimals(StringBuilder sb, Fraction decimalFraction, NumberFormatInfo formatProvider,
        int nbDecimals, string quotientFormat = "F0") {
        var numerator = decimalFraction.Numerator;
        var denominator = decimalFraction.Denominator;
        var quotient = BigInteger.DivRem(numerator, denominator, out var remainder);

        sb.Append(quotient.ToString(quotientFormat, formatProvider)).Append(formatProvider.NumberDecimalSeparator);

        remainder = BigInteger.Abs(remainder);

        var decimalsAdded = 0;
        while (!remainder.IsZero && decimalsAdded++ < nbDecimals - 1) {
            denominator = PreviousPowerOfTen(denominator, nbDecimals - decimalsAdded + 1);
            var digit = (char)('0' + (int)BigInteger.DivRem(remainder, denominator, out remainder));
            sb.Append(digit);
        }

        if (remainder.IsZero) {
            sb.Append('0', nbDecimals - decimalsAdded);
        } else {
            sb.Append(remainder.ToString(formatProvider));
        }

        return sb;
    }

    /// <summary>
    ///     Appends the significant decimal digits of a fractional number to the specified <see cref="StringBuilder" />.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder" /> to which the significant decimal digits will be appended.</param>
    /// <param name="decimalFraction">
    ///     The fractional number whose significant decimal digits are to be appended.
    ///     The denominator of this fraction must be a power of 10, corresponding to the <paramref name="maxNbDecimals" />.
    /// </param>
    /// <param name="formatProvider">The <see cref="NumberFormatInfo" /> that provides culture-specific formatting information.</param>
    /// <param name="maxNbDecimals">The maximum number of decimal digits to append.</param>
    /// <param name="quotientFormat">
    ///     An optional format string that specifies the format of the quotient. Defaults to "F0".
    /// </param>
    /// <returns>
    ///     The <see cref="StringBuilder" /> instance with the appended significant decimal digits.
    /// </returns>
    /// <remarks>
    ///     This method calculates and appends the significant decimal digits of the given fractional number.
    ///     It ensures that the number of appended decimal digits does not exceed the specified maximum.
    ///     <para>
    ///         The <paramref name="decimalFraction" /> must represent a value with a denominator that is a power of 10,
    ///         equal to the specified <paramref name="maxNbDecimals" />.
    ///     </para>
    /// </remarks>
    private static StringBuilder AppendSignificantDecimals(StringBuilder sb, Fraction decimalFraction, NumberFormatInfo formatProvider,
        int maxNbDecimals, string quotientFormat = "F0") {
        var numerator = decimalFraction.Numerator;
        var denominator = decimalFraction.Denominator;
        var quotient = BigInteger.DivRem(numerator, denominator, out var remainder);

        sb.Append(quotient.ToString(quotientFormat, formatProvider));

        if (remainder.IsZero) {
            return sb;
        }

        sb.Append(formatProvider.NumberDecimalSeparator);

        var decimalsRemaining = maxNbDecimals;
        do {
            denominator = PreviousPowerOfTen(denominator, decimalsRemaining);
            var digit = (char)('0' + (int)BigInteger.DivRem(remainder, denominator, out remainder));
            sb.Append(digit);
            decimalsRemaining--;
        } while (!remainder.IsZero && decimalsRemaining > 0);

        return sb;
    }

    private static StringBuilder AppendExponentWithSignificantDigits(StringBuilder sb, int exponent,
        NumberFormatInfo formatProvider, char exponentSymbol) =>
        exponent switch {
            <= -1000 => sb.Append(exponentSymbol).Append(exponent.ToString(formatProvider)),
            <= 0 => sb.Append(exponentSymbol).Append(exponent.ToString("D2", formatProvider)),
            < 100 => sb.Append(exponentSymbol).Append(formatProvider.PositiveSign)
                .Append(exponent.ToString("D2", formatProvider)),
            < 1000 => sb.Append(exponentSymbol).Append(formatProvider.PositiveSign)
                .Append(exponent.ToString("D3", formatProvider)),
            _ => sb.Append(exponentSymbol).Append(formatProvider.PositiveSign)
                .Append(exponent.ToString(formatProvider))
        };

    #region Rounding functions (with the default-per-framework rounding mode)

    /// <summary>
    ///     Rounds the given Fraction to a specified number of digits using a specified rounding strategy.
    /// </summary>
    /// <param name="x">The Fraction to be rounded.</param>
    /// <param name="nbDigits">The number of decimal places in the return value.</param>
    /// <param name="midpointRounding">
    ///     One of the enumeration values that specifies which rounding strategy to use. If not
    ///     provided, the default rounding strategy is used. The default rounding strategy is determined by the target
    ///     framework's default string rounding mode.
    /// </param>
    /// <returns>
    ///     A new Fraction that is the nearest number to 'x' with the specified number of digits, rounded as specified by
    ///     'midpointRounding'.
    /// </returns>
    private static Fraction Round(Fraction x, int nbDigits,
        MidpointRounding midpointRounding = DefaultMidpointRoundingMode) =>
        Fraction.Round(x, nbDigits, midpointRounding, false);

    /// <summary>
    ///     Rounds the given Fraction to the specified precision using the specified rounding strategy.
    /// </summary>
    /// <param name="numerator">The numerator of the fraction to be rounded.</param>
    /// <param name="denominator">The denominator of the fraction to be rounded.</param>
    /// <param name="midpointRounding">
    ///     One of the enumeration values that specifies which rounding strategy to use. If not
    ///     provided, the default rounding strategy is used. The default rounding strategy is determined by the target
    ///     framework's default string rounding mode.
    /// </param>
    /// <returns>The number rounded to using the <paramref name="midpointRounding" /> rounding strategy.</returns>
    private static BigInteger Round(BigInteger numerator, BigInteger denominator,
        MidpointRounding midpointRounding = DefaultMidpointRoundingMode) =>
        Fraction.RoundToBigInteger(new Fraction(numerator, denominator, false), midpointRounding);

    #endregion

    private static readonly double Log10Of2 = Math.Log10(2);

    /// <summary>
    ///     Calculates the exponent power for the given fraction terms.
    /// </summary>
    /// <param name="numerator">The numerator of the fraction.</param>
    /// <param name="denominator">The denominator of the fraction.</param>
    /// <param name="powerOfTen">
    ///     Output parameter that returns the power of ten that corresponds to the calculated exponent
    ///     power.
    /// </param>
    /// <returns>The exponent power for the given fraction.</returns>
    /// <remarks>It is expected that both terms have the same sign (i.e. the Fraction is positive)</remarks>
    private static int GetExponentPower(BigInteger numerator, BigInteger denominator, out BigInteger powerOfTen) {
        if (denominator.Sign == -1) {
            // normalize the signs of the terms
            numerator = -numerator;
            denominator = -denominator;
        }

        // Preconditions: numerator > 0, denominator > 0.
#if NET
        var numLen = numerator.GetBitLength();
        var denLen = denominator.GetBitLength();
#else
        var numLen = getBitLength(numerator);
        var denLen = getBitLength(denominator);
#endif
        // If the two numbers have equal bit-length, then their ratio is in [1,2) if numerator >= denominator,
        // or in (0,1) if numerator < denominator. In these cases the scientific exponent is fixed.
        if (numLen == denLen) {
            if (numerator >= denominator) {
                powerOfTen = BigInteger.One; // 10^0
                return 0;
            }

            // When numerator < denominator, ratio < 1, and normalization requires multiplying by 10 once.
            powerOfTen = Fraction.TEN; // 10^1, leading to an exponent of -1.
            return -1;
        }

        if (numLen > denLen) {
            // Case: number >= 1, so the scientific exponent is positive.
            // The adjusted candidate uses (diffBits - 1) because an L-bit number is at least 2^(L-1).
            var diffBits = numLen - denLen;
            var exponent = (int)Math.Floor((diffBits - 1) * Log10Of2) + 1;
            powerOfTen = Fraction.PowerOfTen(exponent);

            // Adjustment: if our candidate powerOfTen is too high,
            // then the quotient doesn't reach that many digits.
            if (numerator >= denominator * powerOfTen) {
                return exponent;
            }

            powerOfTen = PreviousPowerOfTen(powerOfTen, exponent);
            return exponent - 1;
        } else {
            // Case: number < 1, so the scientific exponent is negative.
            // We need the smallest k such that numerator * 10^k >= denominator.
            var diffBits = denLen - numLen;
            var exponent = (int)Math.Ceiling(diffBits * Log10Of2) - 1;
            powerOfTen = Fraction.PowerOfTen(exponent);

            // First, check if one fewer factor of Ten would suffice.
            if (numerator * powerOfTen >= denominator) {
                return -exponent;
            }

            // Select the next guess as 10^exponent
            powerOfTen = NextPowerOfTen(powerOfTen, exponent++);
            
            // Then, check if our candidate is too low.
            if (numerator * powerOfTen < denominator) {
                powerOfTen = NextPowerOfTen(powerOfTen, exponent++);
            }

            // For numbers < 1, the scientific exponent is -k.
            return -exponent;
        }

#if NETSTANDARD
        static long getBitLength(BigInteger value) =>
            // Using BigInteger.Log(value, 2) avoids creating a byte array.
            // Note: For value > 0 this returns a double that we floor and add 1.
            (long)Math.Floor(BigInteger.Log(value, 2)) + 1;
#endif
    }
}
