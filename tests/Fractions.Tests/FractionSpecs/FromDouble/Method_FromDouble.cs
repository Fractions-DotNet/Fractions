using System;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.FromDouble;

[TestFixture]
// German: Wenn ein Bruch anhand einer NaN double Zahl erzeugt wird
public class When_a_fraction_is_created_from_a_NaN_double : Spec {
    private Fraction _result;

    public override void SetUp() {
        base.SetUp();
        _result = Fraction.FromDouble(double.NaN);
    }

    [Test]
    public void The_result_should_be_NaN() {
        _result.IsNaN.Should().BeTrue();
    }
}

[TestFixture]
// German: Wenn ein Bruch anhand einer positiv unendlichen double Zahl erzeugt wird
public class When_a_fraction_is_created_from_a_positive_infinite_double : Spec {
    private Fraction _result;

    public override void SetUp() {
        base.SetUp();
        _result = Fraction.FromDouble(double.PositiveInfinity);
    }

    [Test]
    public void The_result_should_be_PositiveInfinity() {
        _result.Should().Be(Fraction.PositiveInfinity);
    }
}

[TestFixture]
// German: Wenn ein Bruch anhand einer negativ unendlichen double Zahl erzeugt wird
public class When_a_fraction_is_created_from_a_negative_infinite_double : Spec {
    private Fraction _result;

    public override void SetUp() {
        base.SetUp();
        _result = Fraction.FromDouble(double.NegativeInfinity);
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
        base.SetUp();
        _fraction = Fraction.FromDouble(0.0);
    }

    [Test]
    // German: Soll der Wert danach 0 sein
    public void The_value_after_should_be_0() {
        _fraction.Should().Be(Fraction.Zero);
    }
}

[TestFixture]
// German: Wenn ein Bruch mit einer minus 1 double Zahl erzeugt wird
public class When_a_fraction_is_created_with_a_minus_1_double : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        base.SetUp();
        _fraction = Fraction.FromDouble(-1.0);
    }

    [Test]
    // German: Soll der Wert danach minus 1 sein
    public void The_value_after_should_be_minus_1() {
        _fraction.Should().Be(new Fraction(-1, 1));
    }
}

[TestFixture]
// German: Wenn ein Bruch mit einer 1 double Zahl erzeugt wird
public class When_a_fraction_is_created_with_a_1_double : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        base.SetUp();
        _fraction = Fraction.FromDouble(1);
    }

    [Test]
    // German: Soll der Wert danach 1 sein
    public void The_value_after_should_be_1() {
        _fraction.Should().Be(new Fraction(1, 1));
    }
}

[TestFixture]
// German: Wenn ein Bruch mit einer 1 Komma 345 double Zahl erzeugt wird
public class When_a_fraction_is_created_with_a_1_point_345_double : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        base.SetUp();
        _fraction = Fraction.FromDouble(1.345);
    }

    [Test]
    // German: Soll der auf 15 Nachkommastellen gerundete Wert der Zahl entsprechen
    public void The_value_rounded_to_15_decimal_places_should_match_the_number() {
        var rounded = Math.Round(_fraction.ToDouble(), 15, MidpointRounding.ToEven);
        rounded.Should().Be(1.345);
    }
}

[TestFixture]
// German: Wenn ein Bruch mit einer 0 Komma 3125 double Zahl erzeugt wird
public class When_a_fraction_is_created_with_a_0_point_3125_double : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        base.SetUp();
        _fraction = Fraction.FromDouble(0.3125);
    }

    [Test]
    // German: Soll der Bruch danach einen Zähler von 5 haben
    public void The_fraction_should_have_a_numerator_of_5() {
        _fraction.Numerator.Should().Be(new BigInteger(5));
    }

    [Test]
    // German: Soll der Bruch danach einen Nenner von 16 haben
    public void The_fraction_should_have_a_denominator_of_16() {
        _fraction.Denominator.Should().Be(new BigInteger(16));
    }
}

[TestFixture]
// German: Wenn ein Bruch mit einer minus 0 Komma 3125 double Zahl erzeugt wird
public class When_a_fraction_is_created_with_a_minus_0_point_3125_double : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        base.SetUp();
        _fraction = Fraction.FromDouble(-0.3125);
    }

    [Test]
    // German: Soll der Bruch danach einen Zähler von minus 5 haben
    public void The_fraction_should_have_a_numerator_of_minus_5() {
        _fraction.Numerator.Should().Be(new BigInteger(-5));
    }

    [Test]
    // German: Soll der Bruch danach einen Nenner von 16 haben
    public void The_fraction_should_have_a_denominator_of_16() {
        _fraction.Denominator.Should().Be(new BigInteger(16));
    }
}

[TestFixture]
// German: Wenn ein Bruch mit double MaxValue erzeugt wird
public class When_a_fraction_is_created_with_double_MaxValue : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        base.SetUp();
        _fraction = Fraction.FromDouble(double.MaxValue);
    }

    [Test]
    // German: Soll der Bruch danach einen Zähler von double MaxValue haben
    public void The_fraction_should_have_a_numerator_of_double_MaxValue() {
        _fraction.Numerator.Should().Be(new BigInteger(double.MaxValue));
    }

    [Test]
    // German: Soll der Bruch danach einen Nenner von 1 haben
    public void The_fraction_should_have_a_denominator_of_1() {
        _fraction.Denominator.Should().Be(new BigInteger(1));
    }
}

[TestFixture]
// German: Wenn ein Bruch mit double MinValue erzeugt wird
public class When_a_fraction_is_created_with_double_MinValue : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        base.SetUp();
        _fraction = Fraction.FromDouble(double.MinValue);
    }

    [Test]
    // German: Soll der Bruch danach einen Zähler von minus double MaxValue haben
    public void The_fraction_should_have_a_numerator_of_minus_double_MaxValue() {
        _fraction.Numerator.Should().Be(BigInteger.Negate(new BigInteger(double.MaxValue)));
    }

    [Test]
    // German: Soll der Bruch danach einen Nenner von 1 haben
    public void The_fraction_should_have_a_denominator_of_1() {
        _fraction.Denominator.Should().Be(new BigInteger(1));
    }
}

[TestFixture]
// German: Wenn ein Bruch mit PI erzeugt wird
public class When_a_fraction_is_created_with_PI : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        base.SetUp();
        _fraction = Fraction.FromDouble(Math.PI);
    }

    [Test]
    // German: Soll der Bruch zurück in Double umgewandelt mit PI gleich sein
    public void When_converted_back_to_double_the_fraction_should_equal_PI() {
        _fraction.ToDouble().Should().Be(Math.PI);
    }
}

[TestFixture]
// German: Wenn ein Bruch von 0 komma 1 ohne Rundung erzeugt wird
public class When_a_fraction_is_created_from_0_point_1_without_rounding : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        base.SetUp();
        _fraction = Fraction.FromDouble(0.1);
        Console.WriteLine(_fraction.ToString());
    }

    [Test]
    // German: Soll das Ergebnis 3602879701896397 durch 36028797018963968 sein
    public void The_result_should_be_3602879701896397_over_36028797018963968() {
        _fraction.Should().Be(new Fraction(3602879701896397L, 36028797018963968L));
    }
}

[TestFixture]
// German: Wenn ein Bruch von 0 komma 1 mit Rundung erzeugt wird
public class When_a_fraction_is_created_from_0_point_1_with_rounding : Spec {
    private Fraction _fraction;

    public override void SetUp() {
        base.SetUp();
        _fraction = Fraction.FromDoubleRounded(0.1);
        Console.WriteLine(_fraction.ToString());
    }

    [Test]
    // German: Soll das Ergebnis 1 durch 10 sein
    public void The_result_should_be_1_over_10() {
        _fraction.Should().Be(new Fraction(1, 10));
    }
}

[TestFixture]
// German: Wenn ein Bruch mit 1 Drittel erzeugt wird
public class When_a_fraction_is_created_with_1_third : Spec {
    private Fraction _fraction;
    private const double ONE_THIRD = 1.0 / 3.0;

    public override void SetUp() {
        base.SetUp();
        _fraction = Fraction.FromDouble(ONE_THIRD);
    }

    [Test]
    // German: Soll der auf 15 Nachkommastellen gerundete Wert der Zahl entsprechen
    public void The_value_rounded_to_15_decimal_places_should_match_the_number() {
        var rounded = Math.Round(_fraction.ToDouble(), 15, MidpointRounding.ToEven);
        rounded.Should().Be(0.333333333333333);
    }
}

[TestFixture]
public class When_a_fraction_is_created_from_double : Spec {
    private const double VerySmallValue = 1.0 / (double.MaxValue - 100);
    private Fraction _fraction;

    public override void Act() =>
        _fraction = Fraction.FromDouble(VerySmallValue);

    [Test]
    public void ToDouble_does_not_return_the_exactly_the_same_value() =>
        _fraction.ToDouble().Should().NotBe(VerySmallValue);

    [Test]
    public void ToDouble_returns_an_approximation() {
        var approximateValue = _fraction.ToDouble(); // this is no longer returning 0 

        Math.Abs(approximateValue)
            .Should()
            .BeLessThanOrEqualTo(2.5 * VerySmallValue, "The last digit loses accuracy");
    }
}
