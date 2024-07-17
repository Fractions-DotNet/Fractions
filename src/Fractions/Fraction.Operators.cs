using System;
using System.Numerics;

namespace Fractions;

public readonly partial struct Fraction {
#pragma warning disable 1591
    // NaN == NaN: False -> see https://learn.microsoft.com/en-us/dotnet/api/system.double.nan?view=net-8.0#system-double-nan
    public static bool operator ==(Fraction left, Fraction right) => left.Equals(right) && !(left.IsNaN && right.IsNaN);

    // NaN != NaN : True -> see https://learn.microsoft.com/en-us/dotnet/api/system.double.nan?view=net-8.0#system-double-nan
    public static bool operator !=(Fraction left, Fraction right) => !left.Equals(right) || (left.IsNaN && right.IsNaN);
    
    public static Fraction operator +(Fraction a, Fraction b) => a.Add(b);
    
    public static Fraction operator ++(Fraction a) => a.Add(One);
    
    public static Fraction operator +(Fraction a) => a;

    public static Fraction operator -(Fraction a, Fraction b) => a.Subtract(b);

    public static Fraction operator --(Fraction a) => a.Add(MinusOne);
    
    public static Fraction operator -(Fraction a) => a.Negate();

    public static Fraction operator *(Fraction a, Fraction b) => a.Multiply(b);

    public static Fraction operator /(Fraction a, Fraction b) => a.Divide(b);

    public static Fraction operator %(Fraction a, Fraction b) => a.Remainder(b);

    public static bool operator <(Fraction a, Fraction b) => !(a.IsNaN || b.IsNaN) && a.CompareTo(b) < 0;

    public static bool operator >(Fraction a, Fraction b) => !(a.IsNaN || b.IsNaN) && a.CompareTo(b) > 0;

    public static bool operator <=(Fraction a, Fraction b) => !(a.IsNaN || b.IsNaN) && a.CompareTo(b) <= 0;

    public static bool operator >=(Fraction a, Fraction b) => !(a.IsNaN || b.IsNaN) && a.CompareTo(b) >= 0;

    public static implicit operator Fraction(int value) => new(value);

    public static implicit operator Fraction(long value) => new(value);

    [CLSCompliant(false)]
    public static implicit operator Fraction(uint value) => new(value);

    [CLSCompliant(false)]
    public static implicit operator Fraction(ulong value) => new(value);

    public static implicit operator Fraction(BigInteger value) => new(value);

    public static explicit operator Fraction(double value) => new(value);

    public static implicit operator Fraction(decimal value) => new(value);

    public static explicit operator Fraction(string value) => FromString(value);

    public static explicit operator int(Fraction fraction) => fraction.ToInt32();

    public static explicit operator long(Fraction fraction) => fraction.ToInt64();

    [CLSCompliant(false)]
    public static explicit operator uint(Fraction fraction) => fraction.ToUInt32();

    [CLSCompliant(false)]
    public static explicit operator ulong(Fraction fraction) => fraction.ToUInt64();

    public static explicit operator decimal(Fraction fraction) => fraction.ToDecimal();

    public static explicit operator double(Fraction fraction) => fraction.ToDouble();

    public static explicit operator BigInteger(Fraction fraction) => fraction.ToBigInteger();
#pragma warning restore 1591
}
