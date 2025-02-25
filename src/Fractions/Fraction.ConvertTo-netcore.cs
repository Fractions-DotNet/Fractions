#if NET7_0_OR_GREATER
using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Fractions;

public partial struct Fraction {
    /// <summary>
    ///     Converts the given Fraction to a Half precision floating point number.
    /// </summary>
    /// <param name="value">The Fraction to convert.</param>
    /// <returns>The converted Half precision floating point number.</returns>
    /// <exception cref="OverflowException">Thrown when the Fraction is too large to fit in a Half.</exception>
    public static explicit operator Half(Fraction value) {
        return (Half)value.ToDouble();
    }

    /// <summary>
    ///     Converts the given Fraction to a Complex floating point number.
    /// </summary>
    /// <param name="value">The Fraction to convert.</param>
    /// <returns>The converted Complex floating point number.</returns>
    public static explicit operator Complex(Fraction value) {
        return value.ToDouble();
    }

    /// <summary>
    ///     Converts the given Fraction to an Int128.
    /// </summary>
    /// <param name="value">The Fraction to convert.</param>
    /// <returns>The converted Int128.</returns>
    /// <exception cref="OverflowException">Thrown when the Fraction is too large to fit in an Int128.</exception>
    public static explicit operator Int128(Fraction value) {
        return (Int128)(BigInteger)value;
    }

    /// <summary>
    ///     Converts the given Fraction to an UInt128.
    /// </summary>
    /// <param name="value">The Fraction to convert.</param>
    /// <returns>The converted UInt128.</returns>
    /// <exception cref="OverflowException">Thrown when the Fraction is too large to fit in an UInt128.</exception>
    [CLSCompliant(false)]
    public static explicit operator UInt128(Fraction value) {
        return (UInt128)(BigInteger)value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool INumberBase<Fraction>.TryConvertToChecked<TOther>(Fraction value, [MaybeNullWhen(false)] out TOther result) {
        if (typeof(TOther) == typeof(decimal)) {
            var convertedValue = value.ToDecimal();
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(double)) {
            var convertedValue = value.ToDouble();
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(Complex)) {
            Complex convertedValue = value.ToDouble();
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(float)) {
            var convertedValue = (float)value.ToDouble();
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(Half)) {
            var convertedValue = (Half)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger)) {
            var convertedValue = (BigInteger)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(Int128)) {
            var convertedValue = (Int128)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(UInt128)) {
            var convertedValue = (UInt128)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(long)) {
            var convertedValue = (long)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(ulong)) {
            var convertedValue = (ulong)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(int)) {
            var convertedValue = (int)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(uint)) {
            var convertedValue = (uint)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(nint)) {
            var convertedValue = (nint)(BigInteger)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(UIntPtr)) {
            var convertedValue = (UIntPtr)(BigInteger)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(short)) {
            var convertedValue = (short)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(ushort)) {
            var convertedValue = (ushort)(BigInteger)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(byte)) {
            var convertedValue = (byte)(BigInteger)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(sbyte)) {
            var convertedValue = (sbyte)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        result = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool INumberBase<Fraction>.TryConvertToSaturating<TOther>(Fraction value, [MaybeNullWhen(false)] out TOther result) {
        if (typeof(TOther) == typeof(decimal)) {
            var convertedValue = value.ToDecimalSaturating();
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(double)) {
            var convertedValue = value.ToDouble();
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(Complex)) {
            Complex convertedValue = value.ToDouble();
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(float)) {
            var convertedValue = (float)value.ToDouble();
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(Half)) {
            var convertedValue = (Half)value;
            result = (TOther)(object)convertedValue;
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger)) {
            if (IsFinite(value)) {
                result = (TOther)(object)(BigInteger)value;
            } else if (value.Numerator.IsZero) {
                result = default;
            } else {
                throw new OverflowException();
            }

            return true;
        }

        if (typeof(TOther) == typeof(Int128)) {
            return convertFromBigInteger<Int128>(value, out result);
        }

        if (typeof(TOther) == typeof(UInt128)) {
            return convertFromBigInteger<UInt128>(value, out result);
        }

        if (typeof(TOther) == typeof(long)) {
            return convertFromBigInteger<long>(value, out result);
        }

        if (typeof(TOther) == typeof(ulong)) {
            return convertFromBigInteger<ulong>(value, out result);
        }

        if (typeof(TOther) == typeof(int)) {
            return convertFromBigInteger<int>(value, out result);
        }

        if (typeof(TOther) == typeof(uint)) {
            return convertFromBigInteger<uint>(value, out result);
        }

        if (typeof(TOther) == typeof(nint)) {
            return convertFromBigInteger<nint>(value, out result);
        }

        if (typeof(TOther) == typeof(UIntPtr)) {
            return convertFromBigInteger<UIntPtr>(value, out result);
        }

        if (typeof(TOther) == typeof(short)) {
            return convertFromBigInteger<short>(value, out result);
        }

        if (typeof(TOther) == typeof(ushort)) {
            return convertFromBigInteger<ushort>(value, out result);
        }

        if (typeof(TOther) == typeof(byte)) {
            return convertFromBigInteger<byte>(value, out result);
        }

        if (typeof(TOther) == typeof(sbyte)) {
            return convertFromBigInteger<sbyte>(value, out result);
        }

        result = default;
        return false;

        static bool convertFromBigInteger<TNumber>(Fraction value, out TOther convertedValue) where TNumber : IMinMaxValue<TNumber> {
            convertedValue = IsFinite(value)
                ? TOther.CreateSaturating((BigInteger)value)
                : value.Numerator.Sign switch {
                    1 => (TOther)(object)TNumber.MaxValue,
                    -1 => (TOther)(object)TNumber.MinValue,
                    _ => default
                };

            return true;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool INumberBase<Fraction>.TryConvertToTruncating<TOther>(Fraction value, [MaybeNullWhen(false)] out TOther result) {
        if (typeof(TOther) == typeof(decimal)) {
            var num = value.ToDecimalSaturating();
            result = (TOther)(object)num;
            return true;
        }

        if (typeof(TOther) == typeof(double)) {
            var num = value.ToDouble();
            result = (TOther)(object)num;
            return true;
        }

        if (typeof(TOther) == typeof(Complex)) {
            Complex num = value.ToDouble();
            result = (TOther)(object)num;
            return true;
        }

        if (typeof(TOther) == typeof(float)) {
            var num = (float)value.ToDouble();
            result = (TOther)(object)num;
            return true;
        }

        if (typeof(TOther) == typeof(Half)) {
            var half = (Half)value;
            result = (TOther)(object)half;
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger)) {
            if (IsFinite(value)) {
                result = (TOther)(object)(BigInteger)value;
            } else if (value.Numerator.IsZero) {
                result = default;
            } else {
                throw new OverflowException();
            }

            return true;
        }

        if (typeof(TOther) == typeof(Int128)) {
            return convertFromBigInteger<Int128>(value, out result);
        }

        if (typeof(TOther) == typeof(UInt128)) {
            return convertFromBigInteger<UInt128>(value, out result);
        }

        if (typeof(TOther) == typeof(long)) {
            return convertFromBigInteger<long>(value, out result);
        }

        if (typeof(TOther) == typeof(ulong)) {
            return convertFromBigInteger<ulong>(value, out result);
        }

        if (typeof(TOther) == typeof(int)) {
            return convertFromBigInteger<int>(value, out result);
        }

        if (typeof(TOther) == typeof(uint)) {
            return convertFromBigInteger<uint>(value, out result);
        }

        if (typeof(TOther) == typeof(nint)) {
            return convertFromBigInteger<nint>(value, out result);
        }

        if (typeof(TOther) == typeof(UIntPtr)) {
            return convertFromBigInteger<UIntPtr>(value, out result);
        }

        if (typeof(TOther) == typeof(short)) {
            return convertFromBigInteger<short>(value, out result);
        }

        if (typeof(TOther) == typeof(ushort)) {
            return convertFromBigInteger<ushort>(value, out result);
        }

        if (typeof(TOther) == typeof(byte)) {
            return convertFromBigInteger<byte>(value, out result);
        }

        if (typeof(TOther) == typeof(sbyte)) {
            return convertFromBigInteger<sbyte>(value, out result);
        }

        result = default;
        return false;

        static bool convertFromBigInteger<TNumber>(Fraction value, out TOther convertedValue) where TNumber : IMinMaxValue<TNumber> {
            convertedValue = IsFinite(value)
                ? TOther.CreateTruncating((BigInteger)value)
                : value.Numerator.Sign switch {
                    1 => (TOther)(object)TNumber.MaxValue,
                    -1 => (TOther)(object)TNumber.MinValue,
                    _ => default
                };

            return true;
        }
    }
}

#endif
