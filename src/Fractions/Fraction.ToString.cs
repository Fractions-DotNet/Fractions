#nullable enable

using System;
using System.Globalization;
using Fractions.Formatter;

namespace Fractions;

public readonly partial struct Fraction {
    /// <summary>
    /// Returns the fraction in default format "G".
    /// </summary>
    /// <returns>Either numerator/denominator, <see cref="NumberFormatInfo.NaNSymbol"/>, <see cref="NumberFormatInfo.PositiveInfinitySymbol"/>, <see cref="NumberFormatInfo.NegativeInfinitySymbol"/> or just the numerator if the faction is normalized.</returns>
    /// <remarks><i>Note: This method will always return numerator/denominator if the fraction is not normalized.</i></remarks>
    public override string ToString() => ToString("G", CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the fraction in default format "G". The returning value is formatted using the specified <paramref name="formatProvider"/>.
    /// </summary>
    /// <param name="formatProvider"><inheritdoc cref="IFormatProvider" path="/summary"/></param>
    /// <returns>Either numerator/denominator, <see cref="NumberFormatInfo.NaNSymbol"/>, <see cref="NumberFormatInfo.PositiveInfinitySymbol"/>, <see cref="NumberFormatInfo.NegativeInfinitySymbol"/> or just the numerator if the faction is normalized.</returns>
    /// <remarks><i>Note: This method will always return numerator/denominator if the fraction is not normalized.</i></remarks>
    public string ToString(IFormatProvider? formatProvider) => ToString("G", formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format"><inheritdoc cref="ToString(string,IFormatProvider)" path="/param[@name='format']" /></param>
    /// <returns><inheritdoc cref="ToString(string,IFormatProvider)" path="/returns" /></returns>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    /// <summary>
    /// Formats the value of the current instance using the specified format. The numbers are however culture invariant.
    /// </summary>
    /// <returns>
    /// The value of the current instance in the specified format.
    /// </returns>
    /// <param name="format">The format to use. 
    /// <list type="table">
    /// <listheader><term>symbol</term><description>description</description></listheader>
    /// <item><term>G</term><description>General format: numerator/denominator, or <b>if the fraction is normalized</b> <see cref="NumberFormatInfo.NaNSymbol"/>, <see cref="NumberFormatInfo.PositiveInfinitySymbol"/>, <see cref="NumberFormatInfo.NegativeInfinitySymbol"/> or just the numerator if the denominator is 1.</description></item>
    /// <item><term>n</term><description>Numerator</description></item>
    /// <item><term>d</term><description>Denominator</description></item>
    /// <item><term>z</term><description>The fraction as integer</description></item>
    /// <item><term>r</term><description>The positive remainder of all digits after the decimal point using the format: numerator/denominator or <see cref="string.Empty"/> if the fraction is a valid integer without digits after the decimal point.</description></item>
    /// <item><term>m</term><description>The fraction as mixed number e.g. "2 1/3" instead of "7/3"</description></item>
    /// </list>
    /// -or- A null reference (Nothing in Visual Basic) to use the default format defined for the type of the <see cref="T:System.IFormattable"/> implementation. </param>
    /// <param name="formatProvider">The provider to use to format the value. -or- A null reference (Nothing in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system.</param>
    /// <filterpriority>2</filterpriority>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        formatProvider?.GetFormat(GetType()) is ICustomFormatter formatter
            ? formatter.Format(format, this, formatProvider)
            : DefaultFractionFormatter.Instance.Format(format, this, formatProvider);

#if NET
    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        string formattedValue;
        if (provider?.GetFormat(typeof(Fraction)) is ICustomFormatter formatter)
        {
            formattedValue = formatter.Format(format.ToString(), this, provider);
        }
        else
        {
            // TODO this overload should be pushed to the DefaultFractionFormatter and DecimalNotationFormatter
            formattedValue = DefaultFractionFormatter.Instance.Format(format.ToString(), this, provider);
        }

        if (formattedValue.Length > destination.Length || string.IsNullOrEmpty(formattedValue))
        {
            charsWritten = 0;
            return false;
        }

        formattedValue.CopyTo(destination);
        charsWritten = formattedValue.Length;
        return true;
    }
#endif
}
