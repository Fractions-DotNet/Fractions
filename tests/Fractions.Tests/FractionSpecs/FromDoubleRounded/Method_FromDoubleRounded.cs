using System;
using System.Collections.Generic;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.FromDoubleRounded;

[TestFixture]
// German: Wenn ein Bruch anhand einer NaN double Zahl erzeugt wird
public class When_a_fraction_is_created_based_on_a_NaN_double : Spec {
    private Fraction _result;

    public override void SetUp() {
        _result = Fraction.FromDoubleRounded(double.NaN);
    }

    [Test]
    public void The_result_should_be_NaN() {
        _result.IsNaN.Should().BeTrue();
    }
}

[TestFixture]
// German: Wenn ein Bruch anhand einer positiv unendlichen double Zahl erzeugt wird
public class When_a_fraction_is_created_based_on_a_positive_infinite_double : Spec {
    private Fraction _result;

    public override void SetUp() {
        _result = Fraction.FromDoubleRounded(double.PositiveInfinity);
    }

    [Test]
    public void The_result_should_be_PositiveInfinity() {
        _result.Should().Be(Fraction.PositiveInfinity);
    }
}

[TestFixture]
// German: Wenn ein Bruch anhand einer negativ unendlichen double Zahl erzeugt wird
public class When_a_fraction_is_created_based_on_a_negative_infinite_double : Spec {
    private Fraction _result;

    public override void SetUp() {
        _result = Fraction.FromDoubleRounded(double.NegativeInfinity);
    }

    [Test]
    public void The_result_should_be_NegativeInfinity() {
        _result.Should().Be(Fraction.NegativeInfinity);
    }
}

[TestFixture]
// German: Wenn ein Bruch mit einer 0 double Zahl erzeugt wird
public class When_a_fraction_is_created_with_a_0_double : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        _fraction = Fraction.FromDoubleRounded(0);
    }

    [Test]
    // German: Soll der Wert danach 0 sein
    public void The_value_should_then_be_0() {
        _fraction.Should().Be(Fraction.Zero);
    }
}

[TestFixture]
// German: Wenn ein Bruch mit einer minus 1 double Zahl erzeugt wird
public class When_a_fraction_is_created_with_a_minus_1_double : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        _fraction = Fraction.FromDoubleRounded(-1);
    }

    [Test]
    // German: Soll der Wert danach minus 1 sein
    public void The_value_should_then_be_minus_1() {
        _fraction.Should().Be(new Fraction(-1, 1));
    }
}

[TestFixture]
// German: Wenn ein Bruch mit einer 1 double Zahl erzeugt wird
public class When_a_fraction_is_created_with_a_1_double : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        _fraction = Fraction.FromDoubleRounded(1);
    }

    [Test]
    // German: Soll der Wert danach 1 sein
    public void The_value_should_then_be_1() {
        _fraction.Should().Be(new Fraction(1, 1));
    }
}

[TestFixture]
// German: Wenn ein Bruch mit einer 1 Komma 345 double Zahl erzeugt wird
public class When_a_fraction_is_created_with_a_1_point_345_double : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        _fraction = Fraction.FromDoubleRounded(1.345);
    }

    [Test]
    // German: Soll der Bruch danach einen Zähler von 269 haben
    public void The_fraction_should_then_have_a_numerator_of_269() {
        _fraction.Numerator.Should().Be(new BigInteger(269));
    }

    [Test]
    // German: Soll der Bruch danach einen Nenner von 200 haben
    public void The_fraction_should_then_have_a_denominator_of_200() {
        _fraction.Denominator.Should().Be(new BigInteger(200));
    }
}

[TestFixture]
// German: Wenn ein Bruch mit double MaxValue erzeugt wird
public class When_a_fraction_is_created_with_double_MaxValue : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        _fraction = Fraction.FromDoubleRounded(double.MaxValue);
    }

    [Test]
    // German: Soll der Bruch danach einen Zähler von double MaxValue haben
    public void The_fraction_should_then_have_a_numerator_of_double_MaxValue() {
        _fraction.Numerator.Should().Be(new BigInteger(double.MaxValue));
    }

    [Test]
    // German: Soll der Bruch danach einen Nenner von 1 haben
    public void The_fraction_should_then_have_a_denominator_of_1() {
        _fraction.Denominator.Should().Be(new BigInteger(1));
    }
}

[TestFixture]
// German: Wenn ein Bruch mit double MinValue erzeugt wird
public class When_a_fraction_is_created_with_double_MinValue : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        _fraction = Fraction.FromDoubleRounded(double.MinValue);
    }

    [Test]
    // German: Soll der Bruch danach einen Zähler von minus double MaxValue haben
    public void The_fraction_should_then_have_a_numerator_of_minus_double_MaxValue() {
        _fraction.Numerator.Should().Be(BigInteger.Negate(new BigInteger(double.MaxValue)));
    }

    [Test]
    // German: Soll der Bruch danach einen Nenner von 1 haben
    public void The_fraction_should_then_have_a_denominator_of_1() {
        _fraction.Denominator.Should().Be(new BigInteger(1));
    }
}

[TestFixture]
// German: Wenn ein Bruch mit PI erzeugt wird
public class When_a_fraction_is_created_with_PI : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        _fraction = Fraction.FromDoubleRounded(Math.PI);
    }

    [Test]
    // German: Soll der Bruch danach einen Zähler von 245850922 haben
    public void The_fraction_should_then_have_a_numerator_of_245850922() {
        _fraction.Numerator.Should().Be(245850922);
    }

    [Test]
    // German: Soll der Bruch danach einen Nenner von 78256779 haben
    public void The_fraction_should_then_have_a_denominator_of_78256779() {
        _fraction.Denominator.Should().Be(78256779);
    }
}

[TestFixture]
// German: Wenn ein Bruch mit 1 Drittel erzeugt wird
public class When_a_fraction_is_created_with_1_third : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        _fraction = Fraction.FromDoubleRounded(1.0 / 3.0);
    }

    [Test]
    // German: Soll der Bruch danach einen Zähler von 1 haben
    public void The_fraction_should_then_have_a_numerator_of_1() {
        _fraction.Numerator.Should().Be(1);
    }

    [Test]
    // German: Soll der Bruch danach einen Nenner von 3 haben
    public void The_fraction_should_then_have_a_denominator_of_3() {
        _fraction.Denominator.Should().Be(3);
    }
}

[TestFixture]
public class When_a_fractions_is_created_by_rounding_a_double_without_precision : Spec {
    private const double DoubleValue = 1055.05585262;
    private const decimal LiteralValue = 1055.05585262m; // the "true/literal" decimal representation

    private Fraction _fraction;

    public override void Act() {
        _fraction = Fraction.FromDoubleRounded(DoubleValue);
    }

    [Test]
    public void The_actual_fraction_may_differ_from_the_literal_value() {
        LiteralValue.Should().Be((decimal)DoubleValue).And.NotBe(_fraction.ToDecimal());
    }

    [Test]
    public void The_actual_fraction_is_an_approximation_of_the_literal_value_as_a_decimal() {
        _fraction.ToDecimal().Should().BeApproximately(LiteralValue, 8);
    }
}

[TestFixture]
public class When_a_fraction_is_created_using_very_small_double_values : Spec {
    private static IEnumerable<TestCaseData> TestCases {
        get {
            yield return new TestCaseData(double.Epsilon, true)
                .SetName("double.Epsilon")
                .Returns(Fraction.Zero);
            
            yield return new TestCaseData(double.Epsilon, false)
                .SetName("double.Epsilon")
                .Returns(Fraction.Zero);
            
            yield return new TestCaseData(-double.Epsilon, false)
                .SetName("double.Epsilon")
                .Returns(Fraction.Zero);

            yield return new TestCaseData(double.Epsilon * 2, true)
                .SetName("double.Epsilon*2")
                .Returns(Fraction.Zero);

            // https://github.com/danm-de/Fractions/issues/83
            yield return new TestCaseData(1.0 / (double.MaxValue - 100), true)
                .SetName("1.0 / (double.MaxValue - 100)")
                .Returns(Fraction.Zero);

            // (1.b51 b50 ... b0)bin * 2^(exp - 1023)
            yield return new TestCaseData(double.Epsilon * Math.Pow(2,51), true)
                .SetName("double.Epsilon*2^51")
                .Returns(new Fraction(1, BigInteger.Pow(2, 1023)));
        }
    }

    [Test, TestCaseSource(nameof(TestCases))]
    public Fraction Should_it_return_the_expected_rounded_fraction(double value, bool reduceTerms) =>
        Fraction.FromDoubleRounded(value, reduceTerms);
}

[TestFixture]
public class When_a_fractions_is_created_by_rounding_a_double_with_maximum_precision : Spec {
    private const double DoubleValue = 1055.05585262;
    private const decimal LiteralValue = 1055.05585262m; // the "true/literal" decimal representation

    // anything that's in the range [minRequiredPrecision, maxExpectedPrecision] should work
    private const int MaxSignificantDigits = 15;

    private Fraction _fraction;

    public override void Act() {
        _fraction = Fraction.FromDoubleRounded(DoubleValue, MaxSignificantDigits);
    }

    [Test]
    public void The_actual_fraction_matches_the_literal_value() {
        LiteralValue.Should().Be((decimal)DoubleValue).And.Be(_fraction.ToDecimal());
    }
}

[TestFixture]
public class When_a_fractions_is_created_by_rounding_a_double_with_reasonable_number_of_significant_digits : Spec {
    // anything that's in the range [minRequiredPrecision, maxExpectedPrecision] should work 
    private const int ReasonableNumberOfSignificantDigits = 15;

    private static IEnumerable<TestCaseData> TestCases { get; } = [
        new TestCaseData(0.0, ReasonableNumberOfSignificantDigits).Returns(Fraction.Zero),
        new TestCaseData(1.0, ReasonableNumberOfSignificantDigits).Returns(Fraction.One),
        new TestCaseData(10.0, ReasonableNumberOfSignificantDigits).Returns(new Fraction(10m)),
        new TestCaseData(-10.0, ReasonableNumberOfSignificantDigits).Returns(new Fraction(-10m)),
        new TestCaseData(0.1, ReasonableNumberOfSignificantDigits).Returns(new Fraction(0.1m)),
        new TestCaseData(-0.1, ReasonableNumberOfSignificantDigits).Returns(new Fraction(-0.1m)),
        new TestCaseData(1055.05585262, 17).Returns(new Fraction(1055.05585262m)),
        new TestCaseData(-1055.05585262, 17).Returns(new Fraction(-1055.05585262m))
    ];


    [Test]
    [TestCaseSource(nameof(TestCases))]
    public Fraction The_fraction_corresponds_to_the_rounded_decimal(double value, int significantDigits) {
        return Fraction.FromDoubleRounded(value, significantDigits);
    }
}

[TestFixture]
public class When_creating_a_fraction_by_rounding_a_double_with_more_than_17_significant_digits : Spec {
    private const double DoubleValue = 1055.05585262;

    [Test]
    public void ArgumentOutOfRangeException_should_be_thrown() {
        Invoking(() => Fraction.FromDoubleRounded(DoubleValue, 18)).Should().Throw<ArgumentOutOfRangeException>();
    }
}

[TestFixture]
public class When_creating_a_fraction_by_rounding_a_double_with_less_than_1_significant_digits : Spec {
    private const double DoubleValue = 1055.05585262;

    [Test]
    public void ArgumentOutOfRangeException_should_be_thrown() {
        Invoking(() => Fraction.FromDoubleRounded(DoubleValue, 0)).Should().Throw<ArgumentOutOfRangeException>();
    }
}

[TestFixture]
public class When_a_fractions_is_created_by_rounding_a_double_with_less_than_the_actual_significant_digits : Spec {
    private static IEnumerable<TestCaseData> TestCases { get; } = [
        new TestCaseData(1055.05585262, 1).Returns(new Fraction(1000m)),
        new TestCaseData(-1055.05585262, 1).Returns(new Fraction(-1000m)),
        new TestCaseData(1055.05585262, 2).Returns(new Fraction(1050m)),
        new TestCaseData(-1055.05585262, 2).Returns(new Fraction(-1050m)),
        new TestCaseData(1055.05585262, 3).Returns(new Fraction(1055m)),
        new TestCaseData(-1055.05585262, 3).Returns(new Fraction(-1055m)),
        new TestCaseData(1055.05585262, 4).Returns(new Fraction(1055m)),
        new TestCaseData(-1055.05585262, 4).Returns(new Fraction(-1055m)),
        new TestCaseData(1055.05585262, 5).Returns(new Fraction(1055.1m)),
        new TestCaseData(-1055.05585262, 5).Returns(new Fraction(-1055.1m)),
        new TestCaseData(1055.05585262, 6).Returns(new Fraction(1055.06m)),
        new TestCaseData(-1055.05585262, 6).Returns(new Fraction(-1055.06m)),
        new TestCaseData(1055.05585262, 7).Returns(new Fraction(1055.056m)),
        new TestCaseData(-1055.05585262, 7).Returns(new Fraction(-1055.056m)),
        new TestCaseData(1055.05585262, 8).Returns(new Fraction(1055.0559m)),
        new TestCaseData(-1055.05585262, 8).Returns(new Fraction(-1055.0559m)),
        new TestCaseData(1055.05585262, 9).Returns(new Fraction(1055.05585m)),
        new TestCaseData(-1055.05585262, 9).Returns(new Fraction(-1055.05585m)),
        new TestCaseData(1055.05585262, 10).Returns(new Fraction(1055.055853m)),
        new TestCaseData(-1055.05585262, 10).Returns(new Fraction(-1055.055853m)),
        new TestCaseData(1055.05585262, 11).Returns(new Fraction(1055.0558526m)),
        new TestCaseData(-1055.05585262, 11).Returns(new Fraction(-1055.0558526m)),
        new TestCaseData(1055.05585262, 12).Returns(new Fraction(1055.05585262m)),
        new TestCaseData(-1055.05585262, 12).Returns(new Fraction(-1055.05585262m)),
        new TestCaseData(1055.05585262, 13).Returns(new Fraction(1055.05585262m)),
        new TestCaseData(-1055.05585262, 13).Returns(new Fraction(-1055.05585262m))
    ];


    [Test]
    [TestCaseSource(nameof(TestCases))]
    public Fraction The_fraction_is_rounded_to_the_specified_precision(double value, int significantDigits) {
        return Fraction.FromDoubleRounded(value, significantDigits);
    }
}

[TestFixture]
public class When_a_fraction_is_created_by_rounding_NaN_with_any_number_of_significant_digits {
    [Test]
    public void The_result_should_be_NaN() {
        Fraction.FromDoubleRounded(double.NaN, 15).IsNaN.Should().BeTrue();
    }
}

[TestFixture]
public class When_a_fraction_is_created_by_rounding_PositiveInfinity_with_any_number_of_significant_digits {
    [Test]
    public void The_result_should_be_PositiveInfinity() {
        Fraction.FromDoubleRounded(double.PositiveInfinity, 15).IsPositiveInfinity.Should().BeTrue();
    }
}

[TestFixture]
public class When_a_fraction_is_created_by_rounding_NegativeInfinity_with_any_number_of_significant_digits {
    [Test]
    public void The_result_should_be_NegativeInfinity() {
        Fraction.FromDoubleRounded(double.NegativeInfinity, 15).IsNegativeInfinity.Should().BeTrue();
    }
}
