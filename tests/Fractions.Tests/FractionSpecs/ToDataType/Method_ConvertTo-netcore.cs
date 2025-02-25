#if NET
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.ToDataType;

// these are tests for the methods from Fraction.ConvertTo-netcore.cs (called via the INumber interface)

[TestFixture]
public class When_a_finite_positive_fraction_is_converted_to_a_number : Spec {
    [Test]
    public void CreateChecked_should_return_the_expected_value() {
        var fraction = new Fraction(3, 2);

        double.CreateChecked(fraction).Should().Be(1.5);
        Complex.CreateChecked(fraction).Should().Be(new Complex(1.5, 0));
        decimal.CreateChecked(fraction).Should().Be(1.5m);
        float.CreateChecked(fraction).Should().Be(1.5f);
        Half.CreateChecked(fraction).Should().Be((Half)1.5);
        BigInteger.CreateChecked(fraction).Should().Be(1);
        Int128.CreateChecked(fraction).Should().Be(1);
        UInt128.CreateChecked(fraction).Should().Be(1);
        long.CreateChecked(fraction).Should().Be(1);
        ulong.CreateChecked(fraction).Should().Be(1);
        int.CreateChecked(fraction).Should().Be(1);
        uint.CreateChecked(fraction).Should().Be(1);
        nint.CreateChecked(fraction).Should().Be(1);
        UIntPtr.CreateChecked(fraction).Should().Be(1);
        short.CreateChecked(fraction).Should().Be(1);
        ushort.CreateChecked(fraction).Should().Be(1);
        byte.CreateChecked(fraction).Should().Be(1);
        sbyte.CreateChecked(fraction).Should().Be(1);
    }

    [Test]
    public void CreateSaturating_should_return_the_expected_value() {
        var fraction = new Fraction(3, 2);

        double.CreateSaturating(fraction).Should().Be(1.5);
        Complex.CreateSaturating(fraction).Should().Be(new Complex(1.5, 0));
        decimal.CreateSaturating(fraction).Should().Be(1.5m);
        float.CreateSaturating(fraction).Should().Be(1.5f);
        Half.CreateSaturating(fraction).Should().Be((Half)1.5);
        BigInteger.CreateSaturating(fraction).Should().Be(1);
        Int128.CreateSaturating(fraction).Should().Be(1);
        UInt128.CreateSaturating(fraction).Should().Be(1);
        long.CreateSaturating(fraction).Should().Be(1);
        ulong.CreateSaturating(fraction).Should().Be(1);
        int.CreateSaturating(fraction).Should().Be(1);
        uint.CreateSaturating(fraction).Should().Be(1);
        nint.CreateSaturating(fraction).Should().Be(1);
        UIntPtr.CreateSaturating(fraction).Should().Be(1);
        short.CreateSaturating(fraction).Should().Be(1);
        ushort.CreateSaturating(fraction).Should().Be(1);
        byte.CreateTruncating(fraction).Should().Be(1);
        sbyte.CreateSaturating(fraction).Should().Be(1);
    }

    [Test]
    public void CreateTruncating_should_return_the_expected_value() {
        var fraction = new Fraction(3, 2);

        double.CreateTruncating(fraction).Should().Be(1.5);
        Complex.CreateTruncating(fraction).Should().Be(new Complex(1.5, 0));
        decimal.CreateTruncating(fraction).Should().Be(1.5m);
        float.CreateTruncating(fraction).Should().Be(1.5f);
        Half.CreateTruncating(fraction).Should().Be((Half)1.5);
        BigInteger.CreateTruncating(fraction).Should().Be(1);
        Int128.CreateTruncating(fraction).Should().Be(1);
        UInt128.CreateTruncating(fraction).Should().Be(1);
        long.CreateTruncating(fraction).Should().Be(1);
        ulong.CreateTruncating(fraction).Should().Be(1);
        int.CreateTruncating(fraction).Should().Be(1);
        uint.CreateTruncating(fraction).Should().Be(1);
        nint.CreateTruncating(fraction).Should().Be(1);
        UIntPtr.CreateTruncating(fraction).Should().Be(1);
        short.CreateTruncating(fraction).Should().Be(1);
        ushort.CreateTruncating(fraction).Should().Be(1);
        byte.CreateTruncating(fraction).Should().Be(1);
        sbyte.CreateTruncating(fraction).Should().Be(1);
    }
}

[TestFixture]
public class When_a_finite_negative_fraction_is_converted_to_a_number : Spec {
    [Test]
    public void CreateChecked_should_return_the_expected_value() {
        var fraction = new Fraction(-3, 2);

        double.CreateChecked(fraction).Should().Be(-1.5);
        Complex.CreateChecked(fraction).Should().Be(new Complex(-1.5, 0));
        decimal.CreateChecked(fraction).Should().Be(-1.5m);
        float.CreateChecked(fraction).Should().Be(-1.5f);
        Half.CreateChecked(fraction).Should().Be((Half)(-1.5));
        BigInteger.CreateChecked(fraction).Should().Be(-1);
        Int128.CreateChecked(fraction).Should().Be(-1);
        Invoking(() => UInt128.CreateChecked(fraction)).Should().Throw<OverflowException>();
        long.CreateChecked(fraction).Should().Be(-1);
        Invoking(() => ulong.CreateChecked(fraction)).Should().Throw<OverflowException>();
        int.CreateChecked(fraction).Should().Be(-1);
        Invoking(() => uint.CreateChecked(fraction)).Should().Throw<OverflowException>();
        nint.CreateChecked(fraction).Should().Be(-1);
        Invoking(() => UIntPtr.CreateChecked(fraction)).Should().Throw<OverflowException>();
        short.CreateChecked(fraction).Should().Be(-1);
        Invoking(() => ushort.CreateChecked(fraction)).Should().Throw<OverflowException>();
        Invoking(() => byte.CreateChecked(fraction)).Should().Throw<OverflowException>();
        sbyte.CreateChecked(fraction).Should().Be(-1);
    }

    [Test]
    public void CreateSaturating_should_return_the_expected_value() {
        var fraction = new Fraction(-3, 2);

        double.CreateSaturating(fraction).Should().Be(-1.5);
        Complex.CreateSaturating(fraction).Should().Be(new Complex(-1.5, 0));
        decimal.CreateSaturating(fraction).Should().Be(-1.5m);
        float.CreateSaturating(fraction).Should().Be(-1.5f);
        Half.CreateSaturating(fraction).Should().Be((Half)(-1.5));
        BigInteger.CreateSaturating(fraction).Should().Be(-1);
        Int128.CreateSaturating(fraction).Should().Be(-1);
        UInt128.CreateSaturating(fraction).Should().Be(0);
        long.CreateSaturating(fraction).Should().Be(-1);
        ulong.CreateSaturating(fraction).Should().Be(0);
        int.CreateSaturating(fraction).Should().Be(-1);
        uint.CreateSaturating(fraction).Should().Be(0);
        nint.CreateSaturating(fraction).Should().Be(-1);
        UIntPtr.CreateSaturating(fraction).Should().Be(0);
        short.CreateSaturating(fraction).Should().Be(-1);
        ushort.CreateSaturating(fraction).Should().Be(0);
        byte.CreateSaturating(fraction).Should().Be(0);
        sbyte.CreateSaturating(fraction).Should().Be(-1);
    }

    [Test]
    public void CreateTruncating_should_return_the_expected_value() {
        var fraction = new Fraction(-3, 2);

        double.CreateTruncating(fraction).Should().Be(-1.5);
        Complex.CreateTruncating(fraction).Should().Be(new Complex(-1.5, 0));
        decimal.CreateTruncating(fraction).Should().Be(-1.5m);
        float.CreateTruncating(fraction).Should().Be(-1.5f);
        Half.CreateTruncating(fraction).Should().Be((Half)(-1.5));
        BigInteger.CreateTruncating(fraction).Should().Be(-1);
        Int128.CreateTruncating(fraction).Should().Be(-1);
        UInt128.CreateTruncating(fraction).Should().Be(new UInt128(18446744073709551615, 18446744073709551615));
        long.CreateTruncating(fraction).Should().Be(-1);
        ulong.CreateTruncating(fraction).Should().Be(18446744073709551615UL);
        int.CreateTruncating(fraction).Should().Be(-1);
        uint.CreateTruncating(fraction).Should().Be(4294967295u);
        nint.CreateTruncating(fraction).Should().Be(-1);
        UIntPtr.CreateTruncating(fraction).Should().Be(new UIntPtr(18446744073709551615));
        short.CreateTruncating(fraction).Should().Be(-1);
        ushort.CreateTruncating(fraction).Should().Be(65535);
        byte.CreateTruncating(fraction).Should().Be(255);
        sbyte.CreateTruncating(fraction).Should().Be(-1);
    }
}

[TestFixture]
public class When_converting_NaN_ToNumber : Spec {
    [Test]
    public void CreateChecked_should_return_NaN_or_throw_an_exception() {
        double.CreateChecked(Fraction.NaN).Should().Be(double.NaN);
        Complex.CreateChecked(Fraction.NaN).Should().Be(new Complex(double.NaN, 0));
        Invoking(() => decimal.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        float.CreateChecked(Fraction.NaN).Should().Be(float.NaN);
        Half.CreateChecked(Fraction.NaN).Should().Be(Half.NaN);
        Invoking(() => BigInteger.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => Int128.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => UInt128.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => long.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => ulong.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => int.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => int.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => uint.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => nint.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => nint.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => UIntPtr.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => short.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => ushort.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => byte.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
        Invoking(() => sbyte.CreateChecked(Fraction.NaN)).Should().Throw<DivideByZeroException>();
    }

    [Test]
    public void CreateSaturating_should_return_NaN_or_Zero() {
        double.CreateSaturating(Fraction.NaN).Should().Be(double.NaN);
        Complex.CreateSaturating(Fraction.NaN).Should().Be(new Complex(double.NaN, 0));
        decimal.CreateSaturating(Fraction.NaN).Should().Be(decimal.Zero);
        float.CreateSaturating(Fraction.NaN).Should().Be(float.NaN);
        Half.CreateSaturating(Fraction.NaN).Should().Be(Half.NaN);
        BigInteger.CreateSaturating(Fraction.NaN).Should().Be(BigInteger.Zero);
        Int128.CreateSaturating(Fraction.NaN).Should().Be(Int128.Zero);
        UInt128.CreateSaturating(Fraction.NaN).Should().Be(UInt128.Zero);
        long.CreateSaturating(Fraction.NaN).Should().Be(0);
        ulong.CreateSaturating(Fraction.NaN).Should().Be(0);
        int.CreateSaturating(Fraction.NaN).Should().Be(0);
        uint.CreateSaturating(Fraction.NaN).Should().Be(0);
        nint.CreateSaturating(Fraction.NaN).Should().Be(nint.Zero);
        UIntPtr.CreateSaturating(Fraction.NaN).Should().Be(UIntPtr.Zero);
        short.CreateSaturating(Fraction.NaN).Should().Be(0);
        ushort.CreateSaturating(Fraction.NaN).Should().Be(0);
        byte.CreateSaturating(Fraction.NaN).Should().Be(0);
        sbyte.CreateSaturating(Fraction.NaN).Should().Be(0);
    }

    [Test]
    public void CreateTruncating_should_return_NaN_or_Zero() {
        double.CreateTruncating(Fraction.NaN).Should().Be(double.NaN);
        Complex.CreateTruncating(Fraction.NaN).Should().Be(new Complex(double.NaN, 0));
        decimal.CreateTruncating(Fraction.NaN).Should().Be(decimal.Zero);
        float.CreateTruncating(Fraction.NaN).Should().Be(float.NaN);
        Half.CreateTruncating(Fraction.NaN).Should().Be(Half.NaN);
        BigInteger.CreateTruncating(Fraction.NaN).Should().Be(BigInteger.Zero);
        Int128.CreateTruncating(Fraction.NaN).Should().Be(Int128.Zero);
        UInt128.CreateTruncating(Fraction.NaN).Should().Be(UInt128.Zero);
        long.CreateTruncating(Fraction.NaN).Should().Be(0);
        ulong.CreateTruncating(Fraction.NaN).Should().Be(0);
        int.CreateTruncating(Fraction.NaN).Should().Be(0);
        uint.CreateTruncating(Fraction.NaN).Should().Be(0);
        nint.CreateTruncating(Fraction.NaN).Should().Be(0);
        UIntPtr.CreateTruncating(Fraction.NaN).Should().Be(0);
        short.CreateTruncating(Fraction.NaN).Should().Be(0);
        ushort.CreateTruncating(Fraction.NaN).Should().Be(0);
        byte.CreateTruncating(Fraction.NaN).Should().Be(0);
        sbyte.CreateTruncating(Fraction.NaN).Should().Be(0);
    }
}

[TestFixture]
public class When_converting_PositiveInfinity_ToNumber : Spec {
    [Test]
    public void CreateChecked_should_return_PositiveInfinity_or_throw_a_DivideByZeroException() {
        double.CreateChecked(Fraction.PositiveInfinity).Should().Be(double.PositiveInfinity);
        Complex.CreateChecked(Fraction.PositiveInfinity).Should().Be(new Complex(double.PositiveInfinity, 0));
        Invoking(() => decimal.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        float.CreateChecked(Fraction.PositiveInfinity).Should().Be(float.PositiveInfinity);
        Half.CreateChecked(Fraction.PositiveInfinity).Should().Be(Half.PositiveInfinity);
        Invoking(() => BigInteger.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => Int128.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => UInt128.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => long.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => ulong.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => int.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => int.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => uint.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => nint.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => nint.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => UIntPtr.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => short.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => ushort.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => byte.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => sbyte.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<DivideByZeroException>();
    }

    [Test]
    public void CreateSaturating_BigInteger_should_throw_OverflowException() {
        Invoking(() => BigInteger.CreateSaturating(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
    }

    [Test]
    public void CreateSaturating_should_return_MaxValue_or_PositiveInfinity() {
        double.CreateSaturating(Fraction.PositiveInfinity).Should().Be(double.PositiveInfinity);
        Complex.CreateSaturating(Fraction.PositiveInfinity).Should().Be(new Complex(double.PositiveInfinity, 0));
        decimal.CreateSaturating(Fraction.PositiveInfinity).Should().Be(decimal.MaxValue);
        float.CreateSaturating(Fraction.PositiveInfinity).Should().Be(float.PositiveInfinity);
        Half.CreateSaturating(Fraction.PositiveInfinity).Should().Be(Half.PositiveInfinity);
        Int128.CreateSaturating(Fraction.PositiveInfinity).Should().Be(Int128.MaxValue);
        UInt128.CreateSaturating(Fraction.PositiveInfinity).Should().Be(UInt128.MaxValue);
        long.CreateSaturating(Fraction.PositiveInfinity).Should().Be(long.MaxValue);
        ulong.CreateSaturating(Fraction.PositiveInfinity).Should().Be(ulong.MaxValue);
        int.CreateSaturating(Fraction.PositiveInfinity).Should().Be(int.MaxValue);
        uint.CreateSaturating(Fraction.PositiveInfinity).Should().Be(uint.MaxValue);
        nint.CreateSaturating(Fraction.PositiveInfinity).Should().Be(nint.MaxValue);
        UIntPtr.CreateSaturating(Fraction.PositiveInfinity).Should().Be(UIntPtr.MaxValue);
        short.CreateSaturating(Fraction.PositiveInfinity).Should().Be(short.MaxValue);
        ushort.CreateSaturating(Fraction.PositiveInfinity).Should().Be(ushort.MaxValue);
        byte.CreateSaturating(Fraction.PositiveInfinity).Should().Be(byte.MaxValue);
        sbyte.CreateSaturating(Fraction.PositiveInfinity).Should().Be(sbyte.MaxValue);
    }

    [Test]
    public void CreateTruncating_BigInteger_should_throw_OverflowException() {
        Invoking(() => BigInteger.CreateTruncating(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
    }

    [Test]
    public void CreateTruncating_should_return_MaxValue_or_PositiveInfinity() {
        double.CreateTruncating(Fraction.PositiveInfinity).Should().Be(double.PositiveInfinity);
        Complex.CreateTruncating(Fraction.PositiveInfinity).Should().Be(new Complex(double.PositiveInfinity, 0));
        decimal.CreateTruncating(Fraction.PositiveInfinity).Should().Be(decimal.MaxValue);
        float.CreateTruncating(Fraction.PositiveInfinity).Should().Be(float.PositiveInfinity);
        Half.CreateTruncating(Fraction.PositiveInfinity).Should().Be(Half.PositiveInfinity);
        Int128.CreateTruncating(Fraction.PositiveInfinity).Should().Be(Int128.MaxValue);
        UInt128.CreateTruncating(Fraction.PositiveInfinity).Should().Be(UInt128.MaxValue);
        long.CreateTruncating(Fraction.PositiveInfinity).Should().Be(long.MaxValue);
        ulong.CreateTruncating(Fraction.PositiveInfinity).Should().Be(ulong.MaxValue);
        int.CreateTruncating(Fraction.PositiveInfinity).Should().Be(int.MaxValue);
        uint.CreateTruncating(Fraction.PositiveInfinity).Should().Be(uint.MaxValue);
        nint.CreateTruncating(Fraction.PositiveInfinity).Should().Be(nint.MaxValue);
        UIntPtr.CreateTruncating(Fraction.PositiveInfinity).Should().Be(UIntPtr.MaxValue);
        short.CreateTruncating(Fraction.PositiveInfinity).Should().Be(short.MaxValue);
        ushort.CreateTruncating(Fraction.PositiveInfinity).Should().Be(ushort.MaxValue);
        byte.CreateTruncating(Fraction.PositiveInfinity).Should().Be(byte.MaxValue);
        sbyte.CreateTruncating(Fraction.PositiveInfinity).Should().Be(sbyte.MaxValue);
    }
}

[TestFixture]
public class When_converting_NegativeInfinity_ToNumber : Spec {
    [Test]
    public void CreateChecked_should_return_NegativeInfinity_or_throw_a_DivideByZeroException() {
        double.CreateChecked(Fraction.NegativeInfinity).Should().Be(double.NegativeInfinity);
        Complex.CreateChecked(Fraction.NegativeInfinity).Should().Be(new Complex(double.NegativeInfinity, 0));
        Invoking(() => decimal.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        float.CreateChecked(Fraction.NegativeInfinity).Should().Be(float.NegativeInfinity);
        Half.CreateChecked(Fraction.NegativeInfinity).Should().Be(Half.NegativeInfinity);
        Invoking(() => BigInteger.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => Int128.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => UInt128.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => long.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => ulong.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => int.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => int.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => uint.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => nint.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => nint.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => UIntPtr.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => short.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => ushort.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => byte.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
        Invoking(() => sbyte.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<DivideByZeroException>();
    }

    [Test]
    public void CreateSaturating_BigInteger_should_throw_OverflowException() {
        Invoking(() => BigInteger.CreateSaturating(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
    }

    [Test]
    public void CreateSaturating_should_return_MinValue_or_NegativeInfinity() {
        double.CreateSaturating(Fraction.NegativeInfinity).Should().Be(double.NegativeInfinity);
        Complex.CreateSaturating(Fraction.NegativeInfinity).Should().Be(new Complex(double.NegativeInfinity, 0));
        decimal.CreateSaturating(Fraction.NegativeInfinity).Should().Be(decimal.MinValue);
        float.CreateSaturating(Fraction.NegativeInfinity).Should().Be(float.NegativeInfinity);
        Half.CreateSaturating(Fraction.NegativeInfinity).Should().Be(Half.NegativeInfinity);
        Int128.CreateSaturating(Fraction.NegativeInfinity).Should().Be(Int128.MinValue);
        UInt128.CreateSaturating(Fraction.NegativeInfinity).Should().Be(UInt128.MinValue);
        long.CreateSaturating(Fraction.NegativeInfinity).Should().Be(long.MinValue);
        ulong.CreateSaturating(Fraction.NegativeInfinity).Should().Be(ulong.MinValue);
        int.CreateSaturating(Fraction.NegativeInfinity).Should().Be(int.MinValue);
        uint.CreateSaturating(Fraction.NegativeInfinity).Should().Be(uint.MinValue);
        nint.CreateSaturating(Fraction.NegativeInfinity).Should().Be(nint.MinValue);
        UIntPtr.CreateSaturating(Fraction.NegativeInfinity).Should().Be(UIntPtr.MinValue);
        short.CreateSaturating(Fraction.NegativeInfinity).Should().Be(short.MinValue);
        ushort.CreateSaturating(Fraction.NegativeInfinity).Should().Be(ushort.MinValue);
        byte.CreateSaturating(Fraction.NegativeInfinity).Should().Be(byte.MinValue);
        sbyte.CreateSaturating(Fraction.NegativeInfinity).Should().Be(sbyte.MinValue);
    }

    [Test]
    public void CreateTruncating_BigInteger_should_throw_OverflowException() {
        Invoking(() => BigInteger.CreateTruncating(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
    }

    [Test]
    public void CreateTruncating_should_return_MinValue_or_NegativeInfinity() {
        double.CreateTruncating(Fraction.NegativeInfinity).Should().Be(double.NegativeInfinity);
        Complex.CreateTruncating(Fraction.NegativeInfinity).Should().Be(new Complex(double.NegativeInfinity, 0));
        decimal.CreateTruncating(Fraction.NegativeInfinity).Should().Be(decimal.MinValue);
        float.CreateTruncating(Fraction.NegativeInfinity).Should().Be(float.NegativeInfinity);
        Half.CreateTruncating(Fraction.NegativeInfinity).Should().Be(Half.NegativeInfinity);
        Int128.CreateTruncating(Fraction.NegativeInfinity).Should().Be(Int128.MinValue);
        UInt128.CreateTruncating(Fraction.NegativeInfinity).Should().Be(UInt128.MinValue);
        long.CreateTruncating(Fraction.NegativeInfinity).Should().Be(long.MinValue);
        ulong.CreateTruncating(Fraction.NegativeInfinity).Should().Be(ulong.MinValue);
        int.CreateTruncating(Fraction.NegativeInfinity).Should().Be(int.MinValue);
        uint.CreateTruncating(Fraction.NegativeInfinity).Should().Be(uint.MinValue);
        nint.CreateTruncating(Fraction.NegativeInfinity).Should().Be(nint.MinValue);
        UIntPtr.CreateTruncating(Fraction.NegativeInfinity).Should().Be(UIntPtr.MinValue);
        short.CreateTruncating(Fraction.NegativeInfinity).Should().Be(short.MinValue);
        ushort.CreateTruncating(Fraction.NegativeInfinity).Should().Be(ushort.MinValue);
        byte.CreateTruncating(Fraction.NegativeInfinity).Should().Be(byte.MinValue);
        sbyte.CreateTruncating(Fraction.NegativeInfinity).Should().Be(sbyte.MinValue);
    }
}

[TestFixture]
public class When_trying_to_convert_to_an_unsupported_type {
    [Test]
    public void TryConvertToChecked_should_return_false() {
        FakeNumber.TryConvertToFakeNumberChecked(Fraction.Zero, out var result).Should().BeFalse();
        result.Should().BeNull();
    }

    [Test]
    public void TryConvertToSaturating_should_return_false() {
        FakeNumber.TryConvertToFakeNumberSaturating(Fraction.Zero, out var result).Should().BeFalse();
        result.Should().BeNull();
    }

    [Test]
    public void TryConvertToTruncating_should_return_false() {
        FakeNumber.TryConvertToFakeNumberTruncating(Fraction.Zero, out var result).Should().BeFalse();
        result.Should().BeNull();
    }

    /// <summary>
    ///     Represents a fake number implementation for testing purposes.
    /// </summary>
    /// <remarks>
    ///     This class is used internally within the test suite to simulate a number type that implements the
    ///     <see cref="System.Numerics.INumberBase{T}" /> interface.
    ///     It provides functionality to test conversions and operations involving custom number types.
    /// </remarks>
#pragma warning disable CA1067, CS0660, CS8632
    private class FakeNumber : INumberBase<FakeNumber> {
        public static bool TryConvertToFakeNumberChecked<TOther>(TOther value, out FakeNumber? result) where TOther : INumberBase<TOther> {
            // normally this would be called from a public CreateChecked method, where the type would first try a TryConvertFromChecked before calling TOther
            // when the result is false, a NotSupportedException is typically thrown (see decimal.CreateChecked(..))
            return TOther.TryConvertToChecked(value, out result);
        }

        public static bool TryConvertToFakeNumberSaturating<TOther>(TOther value, out FakeNumber? result) where TOther : INumberBase<TOther> {
            // normally this would be called from a public CreateSaturating method, where the type would first try a TryConvertFromSaturating before calling TOther
            // when the result is false, a NotSupportedException is typically thrown (see decimal.CreateSaturating(..))
            return TOther.TryConvertToSaturating(value, out result);
        }

        public static bool TryConvertToFakeNumberTruncating<TOther>(TOther value, out FakeNumber? result) where TOther : INumberBase<TOther> {
            // normally this would be called from a public CreateTruncating method, where the type would first try a TryConvertFromTruncating before calling TOther
            // when the result is false, a NotSupportedException is typically thrown (see decimal.CreateTruncating(..))
            return TOther.TryConvertToTruncating(value, out result);
        }

        #region Other interface members (not implemented)

        static bool INumberBase<FakeNumber>.TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out FakeNumber result) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out FakeNumber result) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out FakeNumber result) {
            throw new NotImplementedException();
        }

        bool IEquatable<FakeNumber>.Equals(FakeNumber? other) {
            throw new NotImplementedException();
        }

        string IFormattable.ToString(string? format, IFormatProvider? formatProvider) {
            throw new NotImplementedException();
        }

        bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider) {
            throw new NotImplementedException();
        }

        static FakeNumber IParsable<FakeNumber>.Parse(string s, IFormatProvider? provider) {
            throw new NotImplementedException();
        }

        static bool IParsable<FakeNumber>.TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out FakeNumber result) {
            throw new NotImplementedException();
        }

        static FakeNumber ISpanParsable<FakeNumber>.Parse(ReadOnlySpan<char> s, IFormatProvider? provider) {
            throw new NotImplementedException();
        }

        static bool ISpanParsable<FakeNumber>.TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out FakeNumber result) {
            throw new NotImplementedException();
        }


        static FakeNumber IAdditionOperators<FakeNumber, FakeNumber, FakeNumber>.operator +(FakeNumber left, FakeNumber right) {
            throw new NotImplementedException();
        }

        static FakeNumber IAdditiveIdentity<FakeNumber, FakeNumber>.AdditiveIdentity { get; } = new();

        static FakeNumber IDecrementOperators<FakeNumber>.operator --(FakeNumber value) {
            throw new NotImplementedException();
        }

        static FakeNumber IDivisionOperators<FakeNumber, FakeNumber, FakeNumber>.operator /(FakeNumber left, FakeNumber right) {
            throw new NotImplementedException();
        }

        static bool IEqualityOperators<FakeNumber, FakeNumber, bool>.operator ==(FakeNumber? left, FakeNumber? right) {
            throw new NotImplementedException();
        }

        static bool IEqualityOperators<FakeNumber, FakeNumber, bool>.operator !=(FakeNumber? left, FakeNumber? right) {
            throw new NotImplementedException();
        }

        static FakeNumber IIncrementOperators<FakeNumber>.operator ++(FakeNumber value) {
            throw new NotImplementedException();
        }

        static FakeNumber IMultiplicativeIdentity<FakeNumber, FakeNumber>.MultiplicativeIdentity { get; } = new();

        static FakeNumber IMultiplyOperators<FakeNumber, FakeNumber, FakeNumber>.operator *(FakeNumber left, FakeNumber right) {
            throw new NotImplementedException();
        }

        static FakeNumber ISubtractionOperators<FakeNumber, FakeNumber, FakeNumber>.operator -(FakeNumber left, FakeNumber right) {
            throw new NotImplementedException();
        }

        static FakeNumber IUnaryNegationOperators<FakeNumber, FakeNumber>.operator -(FakeNumber value) {
            throw new NotImplementedException();
        }

        static FakeNumber IUnaryPlusOperators<FakeNumber, FakeNumber>.operator +(FakeNumber value) {
            throw new NotImplementedException();
        }

        static FakeNumber INumberBase<FakeNumber>.Abs(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsCanonical(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsComplexNumber(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsEvenInteger(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsFinite(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsImaginaryNumber(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsInfinity(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsInteger(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsNaN(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsNegative(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsNegativeInfinity(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsNormal(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsOddInteger(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsPositive(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsPositiveInfinity(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsRealNumber(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsSubnormal(FakeNumber value) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.IsZero(FakeNumber value) {
            throw new NotImplementedException();
        }

        static FakeNumber INumberBase<FakeNumber>.MaxMagnitude(FakeNumber x, FakeNumber y) {
            throw new NotImplementedException();
        }

        static FakeNumber INumberBase<FakeNumber>.MaxMagnitudeNumber(FakeNumber x, FakeNumber y) {
            throw new NotImplementedException();
        }

        static FakeNumber INumberBase<FakeNumber>.MinMagnitude(FakeNumber x, FakeNumber y) {
            throw new NotImplementedException();
        }

        static FakeNumber INumberBase<FakeNumber>.MinMagnitudeNumber(FakeNumber x, FakeNumber y) {
            throw new NotImplementedException();
        }

        static FakeNumber INumberBase<FakeNumber>.Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider) {
            throw new NotImplementedException();
        }

        static FakeNumber INumberBase<FakeNumber>.Parse(string s, NumberStyles style, IFormatProvider? provider) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.TryConvertToChecked<TOther>(FakeNumber value, [MaybeNullWhen(false)] out TOther result) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.TryConvertToSaturating<TOther>(FakeNumber value, [MaybeNullWhen(false)] out TOther result) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.TryConvertToTruncating<TOther>(FakeNumber value, [MaybeNullWhen(false)] out TOther result) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FakeNumber result) {
            throw new NotImplementedException();
        }

        static bool INumberBase<FakeNumber>.TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out FakeNumber result) {
            throw new NotImplementedException();
        }

        static FakeNumber INumberBase<FakeNumber>.One { get; } = new();
        static int INumberBase<FakeNumber>.Radix { get; } = 10;
        static FakeNumber INumberBase<FakeNumber>.Zero { get; } = new();

        #endregion
    }
#pragma warning restore CS0660, CA1067, CS8632
}


#endif
