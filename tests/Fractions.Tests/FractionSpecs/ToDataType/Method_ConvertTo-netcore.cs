#if NET
using System;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;

namespace Fractions.Tests.FractionSpecs.ToDataType;

// these are tests for the methods from Fraction.ConvertTo-netcore.cs (called via the INumber interface)

[TestFixture]
public class When_a_finite_positive_fraction_is_converted_to_a_number : NumberConversionSpec {
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
        // although there isn't any char.CreateChecked(..) method, all numeric types support this conversion as well
        CreateChecked<char, double>(97.9).Should().Be('a');
        CreateChecked<char, Fraction>(new Fraction(979, 10)).Should().Be('a');
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
        byte.CreateSaturating(fraction).Should().Be(1);
        sbyte.CreateSaturating(fraction).Should().Be(1);
        // although there isn't any char.CreateChecked(..) method, all numeric types support this conversion as well
        CreateSaturating<char, double>(97.9).Should().Be('a');
        CreateSaturating<char, Fraction>(new Fraction(979, 10)).Should().Be('a');
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
        // although there isn't any char.CreateChecked(..) method, all numeric types support this conversion as well
        CreateSaturating<char, double>(97.9).Should().Be('a');
        CreateSaturating<char, Fraction>(new Fraction(979, 10)).Should().Be('a');
    }
}

[TestFixture]
public class When_a_finite_negative_fraction_is_converted_to_a_number : NumberConversionSpec {
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
        // although there isn't any char.CreateChecked(..) method, all numeric types support this conversion as well
        Invoking(() => CreateChecked<char, double>(-1.5)).Should().Throw<OverflowException>();
        Invoking(() => CreateChecked<char, Fraction>(fraction)).Should().Throw<OverflowException>();
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
        // although there isn't any char.CreateSaturating(..) method, all numeric types support this conversion as well
        CreateSaturating<char, double>(-1.5).Should().Be(char.MinValue);
        CreateSaturating<char, Fraction>(fraction).Should().Be(char.MinValue);
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
        UInt128.CreateTruncating(fraction).Should().Be(UInt128.MaxValue);
        long.CreateTruncating(fraction).Should().Be(-1);
        ulong.CreateTruncating(fraction).Should().Be(ulong.MaxValue);
        int.CreateTruncating(fraction).Should().Be(-1);
        uint.CreateTruncating(fraction).Should().Be(uint.MaxValue);
        nint.CreateTruncating(fraction).Should().Be(-1);
        UIntPtr.CreateTruncating(fraction).Should().Be(UIntPtr.MaxValue);
        short.CreateTruncating(fraction).Should().Be(-1);
        ushort.CreateTruncating(fraction).Should().Be(ushort.MaxValue);
        byte.CreateTruncating(fraction).Should().Be(byte.MaxValue);
        sbyte.CreateTruncating(fraction).Should().Be(-1);
        // although there isn't any char.CreateTruncating(..) method, all numeric types support this conversion as well
        // note: for some reason the floating point types don't care implement an independent truncating method, instead they return the saturated result
        CreateTruncating<char, double>(-1.5).Should().Be(char.MinValue);
        CreateTruncating<char, int>(-1).Should().Be(char.MaxValue);
        CreateTruncating<char, Fraction>(fraction).Should().Be(char.MaxValue);
    }
}

[TestFixture]
public class When_converting_NaN_to_a_number : NumberConversionSpec {
    [Test]
    public void CreateChecked_should_return_NaN_or_throw_an_exception() {
        double.CreateChecked(Fraction.NaN).Should().Be(double.NaN);
        Complex.CreateChecked(Fraction.NaN).Should().Be(new Complex(double.NaN, 0));
        Invoking(() => decimal.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        float.CreateChecked(Fraction.NaN).Should().Be(float.NaN);
        Half.CreateChecked(Fraction.NaN).Should().Be(Half.NaN);
        Invoking(() => BigInteger.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => Int128.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => UInt128.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => long.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => ulong.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => int.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => int.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => uint.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => nint.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => nint.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => UIntPtr.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => short.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => ushort.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => byte.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        Invoking(() => sbyte.CreateChecked(Fraction.NaN)).Should().Throw<OverflowException>();
        // although there isn't any char.CreateChecked(..) method, all numeric types support this conversion as well
        Invoking(() => CreateChecked<char, double>(double.NaN)).Should().Throw<OverflowException>();
        Invoking(() => CreateChecked<char, Fraction>(Fraction.NaN)).Should().Throw<OverflowException>();
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
        sbyte.CreateSaturating(Fraction.NaN).Should().Be(0);
        // although there isn't any char.CreateSaturating(..) method, all numeric types support this conversion as well
        CreateSaturating<char, double>(double.NaN).Should().Be('\0');
        CreateSaturating<char, Fraction>(Fraction.NaN).Should().Be('\0');
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
        // although there isn't any char.CreateTruncating(..) method, all numeric types support this conversion as well
        CreateTruncating<char, double>(double.NaN).Should().Be('\0');
        CreateTruncating<char, Fraction>(Fraction.NaN).Should().Be('\0');
    }
}

[TestFixture]
public class When_converting_PositiveInfinity_to_a_number : NumberConversionSpec {
    [Test]
    public void CreateChecked_should_return_PositiveInfinity_or_throw_a_OverflowException() {
        double.CreateChecked(Fraction.PositiveInfinity).Should().Be(double.PositiveInfinity);
        Complex.CreateChecked(Fraction.PositiveInfinity).Should().Be(new Complex(double.PositiveInfinity, 0));
        Invoking(() => decimal.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        float.CreateChecked(Fraction.PositiveInfinity).Should().Be(float.PositiveInfinity);
        Half.CreateChecked(Fraction.PositiveInfinity).Should().Be(Half.PositiveInfinity);
        Invoking(() => BigInteger.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => Int128.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => UInt128.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => long.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => ulong.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => int.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => int.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => uint.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => nint.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => nint.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => UIntPtr.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => short.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => ushort.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => byte.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => sbyte.CreateChecked(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
        // although there isn't any char.CreateChecked(..) method, all numeric types support this conversion as well
        Invoking(() => CreateChecked<char, double>(double.PositiveInfinity)).Should().Throw<OverflowException>();
        Invoking(() => CreateChecked<char, Fraction>(Fraction.PositiveInfinity)).Should().Throw<OverflowException>();
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
        // although there isn't any char.CreateSaturating(..) method, all numeric types support this conversion as well
        CreateSaturating<char, double>(double.PositiveInfinity).Should().Be(char.MaxValue);
        CreateSaturating<char, Fraction>(Fraction.PositiveInfinity).Should().Be(char.MaxValue);
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
        // although there isn't any char.CreateTruncating(..) method, all numeric types support this conversion as well
        CreateTruncating<char, double>(double.PositiveInfinity).Should().Be(char.MaxValue);
        CreateTruncating<char, Fraction>(Fraction.PositiveInfinity).Should().Be(char.MaxValue);
    }
}

[TestFixture]
public class When_converting_NegativeInfinity_to_a_number : NumberConversionSpec {
    [Test]
    public void CreateChecked_should_return_NegativeInfinity_or_throw_a_OverflowException() {
        double.CreateChecked(Fraction.NegativeInfinity).Should().Be(double.NegativeInfinity);
        Complex.CreateChecked(Fraction.NegativeInfinity).Should().Be(new Complex(double.NegativeInfinity, 0));
        Invoking(() => decimal.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        float.CreateChecked(Fraction.NegativeInfinity).Should().Be(float.NegativeInfinity);
        Half.CreateChecked(Fraction.NegativeInfinity).Should().Be(Half.NegativeInfinity);
        Invoking(() => BigInteger.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => Int128.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => UInt128.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => long.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => ulong.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => int.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => int.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => uint.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => nint.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => nint.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => UIntPtr.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => short.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => ushort.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => byte.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => sbyte.CreateChecked(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
        // although there isn't any char.CreateChecked(..) method, all numeric types support this conversion as well
        Invoking(() => CreateChecked<char, double>(double.NegativeInfinity)).Should().Throw<OverflowException>();
        Invoking(() => CreateChecked<char, Fraction>(Fraction.NegativeInfinity)).Should().Throw<OverflowException>();
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
        // although there isn't any char.CreateSaturating(..) method, all numeric types support this conversion as well
        CreateSaturating<char, double>(double.NegativeInfinity).Should().Be(char.MinValue);
        CreateSaturating<char, Fraction>(Fraction.NegativeInfinity).Should().Be(char.MinValue);
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
        // although there isn't any char.CreateTruncating(..) method, all numeric types support this conversion as well
        CreateTruncating<char, double>(double.NegativeInfinity).Should().Be(char.MinValue);
        CreateTruncating<char, Fraction>(Fraction.NegativeInfinity).Should().Be(char.MinValue);
    }
}

[TestFixture]
public class When_converting_to_an_unsupported_type : NumberConversionSpec {
    [Test]
    public void CreateChecked_should_throw_NotSupportedException() {
        Assert.Throws<NotSupportedException>(() => CreateChecked<FakeNumber, Fraction>(Fraction.One));
    }

    [Test]
    public void CreateSaturating_should_throw_NotSupportedException() {
        Assert.Throws<NotSupportedException>(() => CreateSaturating<FakeNumber, Fraction>(Fraction.One));
    }

    [Test]
    public void CreateTruncating_should_throw_NotSupportedException() {
        Assert.Throws<NotSupportedException>(() => CreateTruncating<FakeNumber, Fraction>(Fraction.One));
    }
}

#endif
