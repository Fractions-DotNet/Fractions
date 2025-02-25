#if NET
using System;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;

namespace Fractions.Tests.FractionSpecs.ToDataType;

[TestFixture]
public class When_using_a_checked_conversion_from_a_number : NumberConversionSpec {
    [Test]
    public void CreateChecked_with_a_real_finite_number_should_return_the_converted_fraction() {
        CreateChecked(1.5).Should().Be(new Fraction(3, 2));
        CreateChecked(new Complex(1.5, 0)).Should().Be(new Fraction(3, 2));
        CreateChecked(1.5m).Should().Be(new Fraction(3, 2));
        CreateChecked((Half)1.5).Should().Be(new Fraction(3, 2));
        CreateChecked(BigInteger.One).Should().Be(Fraction.One);
        CreateChecked(Int128.One).Should().Be(Fraction.One);
        CreateChecked(UInt128.One).Should().Be(Fraction.One);
        CreateChecked(1L).Should().Be(Fraction.One);
        CreateChecked(1UL).Should().Be(Fraction.One);
        CreateChecked(1).Should().Be(Fraction.One);
        CreateChecked(1U).Should().Be(Fraction.One);
        CreateChecked((nint)1).Should().Be(Fraction.One);
        CreateChecked((UIntPtr)1).Should().Be(Fraction.One);
        CreateChecked((short)1).Should().Be(Fraction.One);
        CreateChecked((ushort)1).Should().Be(Fraction.One);
        CreateChecked((byte)1).Should().Be(Fraction.One);
        CreateChecked((sbyte)1).Should().Be(Fraction.One);
        CreateChecked('a').Should().Be(new Fraction(97));
    }

    [Test]
    public void CreateChecked_with_a_NaN_return_NaN() {
        CreateChecked(double.NaN).Should().Be(Fraction.NaN);
        CreateChecked(float.NaN).Should().Be(Fraction.NaN);
        CreateChecked(Half.NaN).Should().Be(Fraction.NaN);
        CreateChecked(Complex.NaN).Should().Be(Fraction.NaN);
    }

    [Test]
    public void CreateChecked_with_a_real_PositiveInfinity_should_return_PositiveInfinity() {
        CreateChecked(double.PositiveInfinity).Should().Be(Fraction.PositiveInfinity);
        CreateChecked(float.PositiveInfinity).Should().Be(Fraction.PositiveInfinity);
        CreateChecked(Half.PositiveInfinity).Should().Be(Fraction.PositiveInfinity);
        CreateChecked(new Complex(double.PositiveInfinity, 0)).Should().Be(Fraction.PositiveInfinity);
    }

    [Test]
    public void CreateChecked_with_a_real_NegativeInfinity_should_return_NegativeInfinity() {
        CreateChecked(double.NegativeInfinity).Should().Be(Fraction.NegativeInfinity);
        CreateChecked(float.NegativeInfinity).Should().Be(Fraction.NegativeInfinity);
        CreateChecked(Half.NegativeInfinity).Should().Be(Fraction.NegativeInfinity);
        CreateChecked(new Complex(double.NegativeInfinity, 0)).Should().Be(Fraction.NegativeInfinity);
    }

    [Test]
    public void CreateChecked_with_a_ComplexNumber_having_an_imaginary_part_should_return_NaN() {
        CreateChecked(Complex.ImaginaryOne).Should().Be(Fraction.NaN);
        CreateChecked(Complex.Infinity).Should().Be(Fraction.NaN);
    }

    [Test]
    public void CreateChecked_with_an_unsupported_type_throws_NotSupportedException() {
        Invoking(() => CreateChecked(FakeNumber.Zero)).Should().Throw<NotSupportedException>();
    }

    private static Fraction CreateChecked<TOther>(TOther number) where TOther : INumberBase<TOther> {
        return CreateChecked<Fraction, TOther>(number);
    }
}

[TestFixture]
public class When_using_a_saturating_conversion_from_a_number : NumberConversionSpec {
    [Test]
    public void CreateSaturating_with_a_real_finite_number_should_return_the_converted_fraction() {
        CreateSaturating(1.5).Should().Be(new Fraction(3, 2));
        CreateSaturating(new Complex(1.5, 0)).Should().Be(new Fraction(3, 2));
        CreateSaturating(1.5m).Should().Be(new Fraction(3, 2));
        CreateSaturating((Half)1.5).Should().Be(new Fraction(3, 2));
        CreateSaturating(BigInteger.One).Should().Be(Fraction.One);
        CreateSaturating(Int128.One).Should().Be(Fraction.One);
        CreateSaturating(UInt128.One).Should().Be(Fraction.One);
        CreateSaturating(1L).Should().Be(Fraction.One);
        CreateSaturating(1UL).Should().Be(Fraction.One);
        CreateSaturating(1).Should().Be(Fraction.One);
        CreateSaturating(1U).Should().Be(Fraction.One);
        CreateSaturating((nint)1).Should().Be(Fraction.One);
        CreateSaturating((UIntPtr)1).Should().Be(Fraction.One);
        CreateSaturating((short)1).Should().Be(Fraction.One);
        CreateSaturating((ushort)1).Should().Be(Fraction.One);
        CreateSaturating((byte)1).Should().Be(Fraction.One);
        CreateSaturating((sbyte)1).Should().Be(Fraction.One);
        CreateSaturating('a').Should().Be(new Fraction(97));
    }

    [Test]
    public void CreateSaturating_with_a_NaN_return_NaN() {
        CreateSaturating(double.NaN).Should().Be(Fraction.NaN);
        CreateSaturating(float.NaN).Should().Be(Fraction.NaN);
        CreateSaturating(Half.NaN).Should().Be(Fraction.NaN);
        CreateSaturating(Complex.NaN).Should().Be(Fraction.NaN);
    }

    [Test]
    public void CreateSaturating_with_a_real_PositiveInfinity_should_return_PositiveInfinity() {
        CreateSaturating(double.PositiveInfinity).Should().Be(Fraction.PositiveInfinity);
        CreateSaturating(float.PositiveInfinity).Should().Be(Fraction.PositiveInfinity);
        CreateSaturating(Half.PositiveInfinity).Should().Be(Fraction.PositiveInfinity);
        CreateSaturating(new Complex(double.PositiveInfinity, 0)).Should().Be(Fraction.PositiveInfinity);
    }

    [Test]
    public void CreateSaturating_with_a_real_NegativeInfinity_should_return_NegativeInfinity() {
        CreateSaturating(double.NegativeInfinity).Should().Be(Fraction.NegativeInfinity);
        CreateSaturating(float.NegativeInfinity).Should().Be(Fraction.NegativeInfinity);
        CreateSaturating(Half.NegativeInfinity).Should().Be(Fraction.NegativeInfinity);
        CreateSaturating(new Complex(double.NegativeInfinity, 0)).Should().Be(Fraction.NegativeInfinity);
    }

    [Test]
    public void CreateSaturating_with_a_ComplexNumber_having_an_imaginary_part_should_return_NaN() {
        // converts as Complex -> double -> Fraction
        CreateSaturating<double, Complex>(Complex.ImaginaryOne).Should().Be(0);
        CreateSaturating(Complex.ImaginaryOne).Should().Be(Fraction.Zero);

        CreateSaturating<double, Complex>(Complex.Infinity).Should().Be(double.PositiveInfinity);
        CreateSaturating(Complex.Infinity).Should().Be(Fraction.PositiveInfinity);
    }

    [Test]
    public void CreateSaturating_with_an_unsupported_type_throws_NotSupportedException() {
        Invoking(() => CreateSaturating(FakeNumber.Zero)).Should().Throw<NotSupportedException>();
    }

    private static Fraction CreateSaturating<TOther>(TOther number) where TOther : INumberBase<TOther> {
        return CreateSaturating<Fraction, TOther>(number);
    }
}

[TestFixture]
public class When_using_a_truncating_conversion_from_a_number : NumberConversionSpec {
    [Test]
    public void CreateTruncating_with_a_real_finite_number_should_return_the_converted_fraction() {
        CreateTruncating(1.5).Should().Be(new Fraction(3, 2));
        CreateTruncating(new Complex(1.5, 0)).Should().Be(new Fraction(3, 2));
        CreateTruncating(1.5m).Should().Be(new Fraction(3, 2));
        CreateTruncating((Half)1.5).Should().Be(new Fraction(3, 2));
        CreateTruncating(BigInteger.One).Should().Be(Fraction.One);
        CreateTruncating(Int128.One).Should().Be(Fraction.One);
        CreateTruncating(UInt128.One).Should().Be(Fraction.One);
        CreateTruncating(1L).Should().Be(Fraction.One);
        CreateTruncating(1UL).Should().Be(Fraction.One);
        CreateTruncating(1).Should().Be(Fraction.One);
        CreateTruncating(1U).Should().Be(Fraction.One);
        CreateTruncating((nint)1).Should().Be(Fraction.One);
        CreateTruncating((UIntPtr)1).Should().Be(Fraction.One);
        CreateTruncating((short)1).Should().Be(Fraction.One);
        CreateTruncating((ushort)1).Should().Be(Fraction.One);
        CreateTruncating((byte)1).Should().Be(Fraction.One);
        CreateTruncating((sbyte)1).Should().Be(Fraction.One);
        CreateTruncating('a').Should().Be(new Fraction(97));
    }

    [Test]
    public void CreateTruncating_with_a_NaN_return_NaN() {
        CreateTruncating(double.NaN).Should().Be(Fraction.NaN);
        CreateTruncating(float.NaN).Should().Be(Fraction.NaN);
        CreateTruncating(Half.NaN).Should().Be(Fraction.NaN);
        CreateTruncating(Complex.NaN).Should().Be(Fraction.NaN);
    }

    [Test]
    public void CreateTruncating_with_a_real_PositiveInfinity_should_return_PositiveInfinity() {
        CreateTruncating(double.PositiveInfinity).Should().Be(Fraction.PositiveInfinity);
        CreateTruncating(float.PositiveInfinity).Should().Be(Fraction.PositiveInfinity);
        CreateTruncating(Half.PositiveInfinity).Should().Be(Fraction.PositiveInfinity);
        CreateTruncating(new Complex(double.PositiveInfinity, 0)).Should().Be(Fraction.PositiveInfinity);
    }

    [Test]
    public void CreateTruncating_with_a_real_NegativeInfinity_should_return_NegativeInfinity() {
        CreateTruncating(double.NegativeInfinity).Should().Be(Fraction.NegativeInfinity);
        CreateTruncating(float.NegativeInfinity).Should().Be(Fraction.NegativeInfinity);
        CreateTruncating(Half.NegativeInfinity).Should().Be(Fraction.NegativeInfinity);
        CreateTruncating(new Complex(double.NegativeInfinity, 0)).Should().Be(Fraction.NegativeInfinity);
    }

    [Test]
    public void CreateTruncating_with_a_ComplexNumber_having_an_imaginary_part_should_return_NaN() {
        // converts as Complex -> double -> Fraction
        CreateTruncating<double, Complex>(Complex.ImaginaryOne).Should().Be(0);
        CreateTruncating(Complex.ImaginaryOne).Should().Be(Fraction.Zero);

        CreateTruncating<double, Complex>(Complex.Infinity).Should().Be(double.PositiveInfinity);
        CreateTruncating(Complex.Infinity).Should().Be(Fraction.PositiveInfinity);
    }

    [Test]
    public void CreateTruncating_with_an_unsupported_type_throws_NotSupportedException() {
        Invoking(() => CreateTruncating(FakeNumber.Zero)).Should().Throw<NotSupportedException>();
    }

    private static Fraction CreateTruncating<TOther>(TOther number) where TOther : INumberBase<TOther> {
        return CreateTruncating<Fraction, TOther>(number);
    }
}
#endif
