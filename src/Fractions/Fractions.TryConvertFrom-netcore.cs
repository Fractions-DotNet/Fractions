#if NET
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Fractions;

public partial struct Fraction {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool INumberBase<Fraction>.TryConvertFromChecked<TOther>(TOther value, out Fraction result) {
        if (TryConvertFrom(value, out result)) {
            return true;
        }

        if (typeof(TOther) == typeof(Complex)) {
            // Complex numbers with an imaginary part can't be represented as a "real number"
            // so we will convert it to NaN for the floating-point types,
            // since that's what Sqrt(-1) (which is `new Complex(0, 1)`) results in.
            result = FromDouble(double.CreateChecked(value));
            return true;
        }

        result = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool INumberBase<Fraction>.TryConvertFromSaturating<TOther>(TOther value, out Fraction result) {
        if (TryConvertFrom(value, out result)) {
            return true;
        }

        if (typeof(TOther) == typeof(Complex)) {
            result = FromDouble(double.CreateSaturating(value));
            return true;
        }

        result = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool INumberBase<Fraction>.TryConvertFromTruncating<TOther>(TOther value, out Fraction result) {
        if (TryConvertFrom(value, out result)) {
            return true;
        }

        if (typeof(TOther) == typeof(Complex)) {
            result = FromDouble(double.CreateTruncating(value));
            return true;
        }

        result = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryConvertFrom<TOther>(TOther value, out Fraction result) where TOther : INumberBase<TOther> {
        if (typeof(TOther) == typeof(decimal)) {
            var num = (decimal)(object)value;
            result = num;
            return true;
        }

        if (typeof(TOther) == typeof(double)) {
            var num = (double)(object)value;
            result = FromDouble(num);
            return true;
        }

        if (typeof(TOther) == typeof(float)) {
            var actualValue = (float)(object)value;
            result = FromDouble(actualValue);
            return true;
        }

        if (typeof(TOther) == typeof(Half)) {
            var actualValue = (Half)(object)value;
            result = FromDouble((double)actualValue);
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger)) {
            var num = (BigInteger)(object)value;
            result = num;
            return true;
        }

        if (typeof(TOther) == typeof(Int128)) {
            var num = (Int128)(object)value;
            result = (BigInteger)num;
            return true;
        }

        if (typeof(TOther) == typeof(UInt128)) {
            var num = (UInt128)(object)value;
            result = (BigInteger)num;
            return true;
        }

        if (typeof(TOther) == typeof(long)) {
            var num = (long)(object)value;
            result = num;
            return true;
        }

        if (typeof(TOther) == typeof(ulong)) {
            var num = (ulong)(object)value;
            result = num;
            return true;
        }

        if (typeof(TOther) == typeof(int)) {
            long num = (int)(object)value;
            result = num;
            return true;
        }

        if (typeof(TOther) == typeof(uint)) {
            var num = (uint)(object)value;
            result = num;
            return true;
        }

        if (typeof(TOther) == typeof(nint)) {
            var num = (nint)(object)value;
            result = num;
            return true;
        }

        if (typeof(TOther) == typeof(UIntPtr)) {
            var num = (UIntPtr)(object)value;
            result = num;
            return true;
        }

        if (typeof(TOther) == typeof(short)) {
            var num = (short)(object)value;
            result = (BigInteger)num;
            return true;
        }

        if (typeof(TOther) == typeof(ushort)) {
            var num = (ushort)(object)value;
            result = (BigInteger)num;
            return true;
        }

        if (typeof(TOther) == typeof(char)) {
            var ch = (char)(object)value;
            result = (BigInteger)ch;
            return true;
        }

        if (typeof(TOther) == typeof(byte)) {
            var num = (byte)(object)value;
            result = (BigInteger)num;
            return true;
        }

        if (typeof(TOther) == typeof(sbyte)) {
            var num = (sbyte)(object)value;
            result = (BigInteger)num;
            return true;
        }

        result = default;
        return false;
    }
}

#endif
