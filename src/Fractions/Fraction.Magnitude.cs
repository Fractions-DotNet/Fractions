namespace Fractions;

public partial struct Fraction {
    /// <summary>
    ///     Returns the <see cref="Fraction" /> with the maximum magnitude from the two provided values.
    /// </summary>
    /// <param name="x">The first <see cref="Fraction" /> to compare.</param>
    /// <param name="y">The second <see cref="Fraction" /> to compare.</param>
    /// <returns>The <see cref="Fraction" /> with the maximum magnitude.</returns>
    public static Fraction MaxMagnitude(Fraction x, Fraction y) {
        if (x.IsNaN) {
            return x;
        }
        
        if (y.IsNaN) {
            return y;
        }

        var comparison = x.Abs().CompareTo(y.Abs());
        return comparison switch {
            1 => x,
            -1 => y,
            _ => x.IsNegative ? y : x
        };
    }

    /// <summary>
    ///     Compares two values to compute which has the greater magnitude and returning the other value if an input is
    ///     <c>NaN</c>.
    /// </summary>
    /// <param name="x">The value to compare with <paramref name="y" />.</param>
    /// <param name="y">The value to compare with <paramref name="x" />.</param>
    /// <returns>
    ///     <paramref name="x" /> if it is greater than <paramref name="y" />; otherwise, <paramref name="y" />.
    /// </returns>
    public static Fraction MaxMagnitudeNumber(Fraction x, Fraction y) {
        if (x.IsNaN) {
            return y;
        }

        if (y.IsNaN) {
            return x;
        }
        
        var comparison = x.Abs().CompareTo(y.Abs());
        return comparison switch {
            1 => x,
            -1 => y,
            _ => x.IsNegative ? y : x
        };
    }

    /// <summary>
    ///     Returns the Fraction with the smallest magnitude from the two provided Fractions.
    /// </summary>
    /// <param name="x">The first Fraction to compare.</param>
    /// <param name="y">The second Fraction to compare.</param>
    /// <returns>The Fraction with the smallest magnitude.</returns>
    public static Fraction MinMagnitude(Fraction x, Fraction y) {
        var comparison = x.Abs().CompareTo(y.Abs());
        return comparison switch {
            -1 => x,
            1 => y,
            _ => x.IsNegative ? x : y
        };
    }

    /// <summary>
    ///     Compares two values to compute which has the lesser magnitude and returning the other value if an input is
    ///     <c>NaN</c>.
    /// </summary>
    /// <param name="x">The value to compare with <paramref name="y" />.</param>
    /// <param name="y">The value to compare with <paramref name="x" />.</param>
    /// <returns>
    ///     <paramref name="x" /> if it is less than <paramref name="y" />; otherwise, <paramref name="y" />.
    /// </returns>
    public static Fraction MinMagnitudeNumber(Fraction x, Fraction y) {
        return x.IsNaN ? y : y.IsNaN ? x : MinMagnitude(x, y);
    }
}
