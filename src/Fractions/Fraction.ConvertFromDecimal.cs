using System.Numerics;
using System;
#if NET
using System.Buffers.Binary;
#endif

namespace Fractions;

public readonly partial struct Fraction {
    /// <summary>
    /// <inheritdoc cref="FromDecimal(decimal, bool)" path="/summary"/>
    /// </summary>
    /// <param name="value"><inheritdoc cref="FromDecimal(decimal, bool)" path="/param[@name='value']"/></param>
    /// <returns>A fraction reduced to the lowest common denominator.</returns>
    public static Fraction FromDecimal(decimal value) {
        return FromDecimal(value, reduceTerms: true);
    }

    /// <summary>
    ///     Converts a decimal value in a fraction. The value will not be rounded.
    /// </summary>
    /// <param name="value">A decimal value.</param>
    /// <param name="reduceTerms">
    ///     Indicates whether the terms of the fraction should be reduced by their greatest common
    ///     denominator.
    /// </param>
    /// <returns>A fraction.</returns>
    public static Fraction FromDecimal(decimal value, bool reduceTerms) {
        if (reduceTerms) {
            switch (value) {
                case decimal.Zero:
                    return Zero;
                case decimal.One:
                    return One;
                case decimal.MinusOne:
                    return MinusOne;
            }
        }
#if NET
        Span<int> bits = stackalloc int[4];
        decimal.GetBits(value, bits);
        Span<byte> buffer = stackalloc byte[16];
        // Assume BitConverter.IsLittleEndian = true
        BinaryPrimitives.WriteInt32LittleEndian(buffer[..4], bits[0]);
        BinaryPrimitives.WriteInt32LittleEndian(buffer.Slice(4, 4), bits[1]);
        BinaryPrimitives.WriteInt32LittleEndian(buffer.Slice(8, 4), bits[2]);
        BinaryPrimitives.WriteInt32LittleEndian(buffer.Slice(12, 4), bits[3]);
        var exp = buffer[14];
        var positiveSign = (buffer[15] & 0x80) == 0;
        // Pass false to the isBigEndian parameter
        var numerator = new BigInteger(buffer[..13], isUnsigned: false, isBigEndian: false);
#else
        var bits = decimal.GetBits(value);
        var low = BitConverter.GetBytes(bits[0]);
        var middle = BitConverter.GetBytes(bits[1]);
        var high = BitConverter.GetBytes(bits[2]);
        var scale = BitConverter.GetBytes(bits[3]);

        var exp = scale[2];
        var positiveSign = (scale[3] & 0x80) == 0;

        // value = 0x00,high,middle,low / 10^exp
        var numerator = new BigInteger([
            low[0], low[1], low[2], low[3],
            middle[0], middle[1], middle[2], middle[3],
            high[0], high[1], high[2], high[3],
            0x00
        ]);
#endif

        if (!positiveSign) {
            numerator = -numerator;
        }

        var denominator = PowerOfTen(exp);

        return reduceTerms ? ReduceSigned(numerator, denominator) : new Fraction(true, numerator, denominator);
    }
}
