﻿using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.Reciprocal;

[TestFixture]
public class When_a_natural_number_is_reciprocated : Spec {
    private Fraction _result1;
    private Fraction _result2;
    private Fraction _result3;

    public override void Act() {
        _result1 = Fraction.One.Reciprocal();
        _result2 = new Fraction(2).Reciprocal();
        _result3 = new Fraction(17).Reciprocal();
    }

    [Test]
    public void Should_be_1_over_the_number() {
        _result1.Should().Be(Fraction.One);
        _result2.Should().Be(new Fraction(1, 2));
        _result3.Should().Be(new Fraction(1, 17));
    }
}

[TestFixture]
public class When_an_negative_normalized_fraction_is_reciprocated : Spec {
    private Fraction _result;

    public override void Act() => _result = new Fraction(-2, 3, true).Reciprocal();

    [Test]
    public void Should_still_be_improper() {
        _result.Should().Be(new Fraction(-3, 2, true));
    }
}

[TestFixture]
public class When_an_improper_fraction_is_reciprocated : Spec {
    private Fraction _result;

    public override void Act() => _result = new Fraction(4, 8, false).Reciprocal();

    [Test]
    public void Should_still_be_improper() {
        _result.Should().Be(new Fraction(8, 4, false));
    }
}

[TestFixture]
public class When_the_equivalent_of_minus_1_10_is_checked : Spec {
    private bool _result;

    public override void Act() =>
        _result = new Fraction(-1, 10).Reciprocal().IsEquivalentTo(new Fraction(-10));

    [Test]
    public void Should_the_result_be_minus_10() => _result.Should().BeTrue();
}
