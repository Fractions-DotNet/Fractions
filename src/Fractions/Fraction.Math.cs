using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Fractions;

public readonly partial struct Fraction {
    /// <summary>
    /// Calculates the remainder of the division with the fraction's value and the supplied <paramref name="divisor"/> (% operator).
    /// </summary>
    /// <param name="divisor">Divisor</param>
    /// <returns>The remainder (left over)</returns>
    public Fraction Remainder(Fraction divisor) {
        var thisNumerator = Numerator;
        var thisDenominator = Denominator;
        var otherNumerator = divisor.Numerator;
        var otherDenominator = divisor.Denominator;

        if (otherNumerator.IsZero) {
            return NaN; // x / 0 == NaN
        }

        if (thisNumerator.IsZero) {
            return thisDenominator.IsZero
                ? NaN // 0 / NaN == NaN
                : Zero; // 0 / x == 0 (where x != 0)
        }

        if (thisDenominator.IsZero) {
            // a/0 (infinity)
            return NaN;
        }

        if (otherDenominator.IsZero) {
            // a % infinity
            return this;
        }

        if (_normalizationNotApplied || divisor._normalizationNotApplied) {
            if (thisDenominator == otherDenominator) {
                return new Fraction(true, thisNumerator % otherNumerator, thisDenominator);
            }

            var gcd = BigInteger.GreatestCommonDivisor(thisDenominator, otherDenominator);
            if (gcd.IsOne) {
                return new Fraction(true,
                    thisNumerator * otherDenominator % (otherNumerator * thisDenominator),
                    thisDenominator * otherDenominator);
            }

            if (gcd == thisDenominator) {
                return new Fraction(true, otherDenominator / gcd * thisNumerator % otherNumerator, otherDenominator);
            }

            if (gcd == otherDenominator) {
                return new Fraction(true, thisNumerator % (thisDenominator / gcd * otherNumerator), thisDenominator);
            }

            var thisMultiplier = thisDenominator / gcd;
            var otherMultiplier = otherDenominator / gcd;

            var leastCommonMultiple = thisMultiplier * otherDenominator;

            var a = thisNumerator * otherMultiplier;
            var b = otherNumerator * thisMultiplier;

            var remainder = a % b;

            return new Fraction(true, remainder, leastCommonMultiple);
        } else {
            if (thisDenominator == otherDenominator) {
                return GetReducedFraction(thisNumerator % otherNumerator, thisDenominator);
            }

            var gcd = BigInteger.GreatestCommonDivisor(thisDenominator, otherDenominator);
            if (gcd.IsOne) {
                return GetReducedFraction(
                    thisNumerator * otherDenominator % (otherNumerator * thisDenominator),
                    thisDenominator * otherDenominator);
            }

            if (gcd == thisDenominator) {
                return ReduceSigned(otherDenominator / gcd * thisNumerator % otherNumerator, otherDenominator);
            }

            if (gcd == otherDenominator) {
                return ReduceSigned(thisNumerator % (thisDenominator / gcd * otherNumerator), thisDenominator);
            }

            var thisMultiplier = thisDenominator / gcd;
            var otherMultiplier = otherDenominator / gcd;

            var leastCommonMultiple = thisMultiplier * otherDenominator;

            var a = thisNumerator * otherMultiplier;
            var b = otherNumerator * thisMultiplier;

            var remainder = a % b;

            return GetReducedFraction(remainder, leastCommonMultiple);
        }
    }

    /// <summary>
    /// Adds the fraction's value with <paramref name="summand"/>.
    /// </summary>
    /// <param name="summand">Summand</param>
    /// <returns>The result as summation.</returns>
    public Fraction Add(Fraction summand) {
        var thisNumerator = Numerator;
        if (thisNumerator.IsZero) {
            // `this` is either NaN (NaN + summand == NaN) or Zero (0 + summand == summand)
            return Denominator.IsZero ? this : summand;
        }

        var otherNumerator = summand.Numerator;
        if (otherNumerator.IsZero) {
            // summand is either NaN (this + NaN == NaN) or Zero (this + 0 == this)
            return summand.Denominator.IsZero ? summand : this;
        }

        // both fractions are non-zero numbers
        var thisDenominator = Denominator;
        var otherDenominator = summand.Denominator;

        if (thisDenominator.IsZero) {
            // `this` is (+/-) Infinity
            if (!otherDenominator.IsZero) {
                return this; // Inf + b = Inf
            }

            // adding infinities
            return (thisNumerator.Sign + otherNumerator.Sign) switch {
                2 => PositiveInfinity,
                -2 => NegativeInfinity,
                _ => NaN
            };
        }

        if (otherDenominator.IsZero) {
            return summand; // (+/-) Infinity
        }

        //  normalizing the signs
        if (_normalizationNotApplied && thisDenominator.Sign == -1) {
            thisDenominator = -thisDenominator;
            thisNumerator = -thisNumerator;
        }

        if (summand._normalizationNotApplied && otherDenominator.Sign == -1) {
            otherDenominator = -otherDenominator;
            otherNumerator = -otherNumerator;
        }

        // both values are non-zero
        if (_normalizationNotApplied || summand._normalizationNotApplied) {
            if (thisDenominator == otherDenominator) {
                return new Fraction(true, thisNumerator + otherNumerator, thisDenominator);
            }

            if (thisDenominator.IsOne) {
                return new Fraction(true, thisNumerator * otherDenominator + otherNumerator, otherDenominator);
            }

            if (otherDenominator.IsOne) {
                return new Fraction(true, thisNumerator + otherNumerator * thisDenominator, thisDenominator);
            }

            var gcd = BigInteger.GreatestCommonDivisor(thisDenominator, otherDenominator);
            if (gcd.IsOne) {
                return new Fraction(true,
                    thisNumerator * otherDenominator + otherNumerator * thisDenominator,
                    thisDenominator * otherDenominator);
            }

            if (gcd == thisDenominator) {
                return new Fraction(true, otherDenominator / gcd * thisNumerator + otherNumerator, otherDenominator);
            }

            if (gcd == otherDenominator) {
                return new Fraction(true, thisNumerator + thisDenominator / gcd * otherNumerator, thisDenominator);
            }

            var thisMultiplier = thisDenominator / gcd;
            var otherMultiplier = otherDenominator / gcd;

            var calculatedNumerator = thisNumerator * otherMultiplier + otherNumerator * thisMultiplier;
            var leastCommonMultiple = thisMultiplier * otherDenominator;

            return new Fraction(true, calculatedNumerator, leastCommonMultiple);
        } else {
            // both values are already normalized
            if (thisDenominator == otherDenominator) {
                return ReduceSigned(thisNumerator + otherNumerator, thisDenominator);
            }

            if (thisDenominator.IsOne) {
                return ReduceSigned(thisNumerator * otherDenominator + otherNumerator, otherDenominator);
            }

            if (otherDenominator.IsOne) {
                return ReduceSigned(thisNumerator + otherNumerator * thisDenominator, thisDenominator);
            }

            // note: using the GCD here is only useful for very large numbers, however the difference is significant
            // 1/2 + 1/6 => gcd = 2, d1=1, d2=3 => n=a*gcd*d2 + b*gcd*d1, d=gcd*d1*d2 
            var gcd = BigInteger.GreatestCommonDivisor(thisDenominator, otherDenominator);
            if (gcd.IsOne) {
                return ReduceSigned(
                    thisNumerator * otherDenominator + otherNumerator * thisDenominator,
                    thisDenominator * otherDenominator);
            }

            if (gcd == thisDenominator) {
                return ReduceSigned(otherDenominator / gcd * thisNumerator + otherNumerator, otherDenominator);
            }

            if (gcd == otherDenominator) {
                return ReduceSigned(thisNumerator + thisDenominator / gcd * otherNumerator, thisDenominator);
            }

            var thisMultiplier = thisDenominator / gcd;
            var otherMultiplier = otherDenominator / gcd;

            var calculatedNumerator = thisNumerator * otherMultiplier + otherNumerator * thisMultiplier;
            var leastCommonMultiple = thisMultiplier * otherDenominator;

            return ReduceSigned(calculatedNumerator, leastCommonMultiple);
        }
    }

    /// <summary>
    /// Subtracts the fraction's value (minuend) with <paramref name="subtrahend"/>.
    /// </summary>
    /// <param name="subtrahend">Subtrahend.</param>
    /// <returns>The result as difference.</returns>
    public Fraction Subtract(Fraction subtrahend) => Add(subtrahend.Negate());

    /// <summary>
    /// Negates the fraction. Has the same result as multiplying it by -1.
    /// </summary>
    /// <returns>The negated fraction.</returns>
    public Fraction Negate() => new(_normalizationNotApplied, -Numerator, Denominator);

    /// <inheritdoc cref="Negate"/>
    [Obsolete("The 'Invert' method is obsolete. Please use the the 'Negate' method or the negation operator '-value'.",
        error: false)]
    public Fraction Invert() => Negate();

    /// <summary>
    /// Multiply the fraction's value by <paramref name="factor"/>.
    /// </summary>
    /// <param name="factor">Factor</param>
    /// <returns>The result as product.</returns>
    public Fraction Multiply(Fraction factor) {
        // we want to skip the multiplications if we know the result is going to be 0/b or a/0
        var thisNumerator = Numerator;
        var thisDenominator = Denominator;
        if (thisDenominator.IsZero) {
            // `this` is NaN or Infinity
            return new Fraction(
                normalizationNotApplied: false,
                numerator: thisNumerator.Sign * factor.Numerator.Sign,
                denominator: BigInteger.Zero);
        }

        var otherNumerator = factor.Numerator;
        var otherDenominator = factor.Denominator;
        if (otherDenominator.IsZero) {
            // factor is NaN or Infinity
            return new Fraction(
                normalizationNotApplied: false,
                numerator: thisNumerator.Sign * otherNumerator.Sign,
                denominator: BigInteger.Zero);
        }

        if (thisNumerator.IsZero || otherNumerator.IsZero) {
            return Zero;
        }
        
        if (_normalizationNotApplied || factor._normalizationNotApplied) {
            return new Fraction(true,
                MultiplyTerms(ref thisNumerator, ref otherNumerator),
                MultiplyTerms(ref thisDenominator, ref otherDenominator));
        }

        return ReduceSigned(
            MultiplyTerms(ref thisNumerator, ref otherNumerator),
            MultiplyTerms(ref thisDenominator, ref otherDenominator));
    }

    /// <summary>
    /// Divides the fraction's value by <paramref name="divisor"/>.
    /// </summary>
    /// <param name="divisor">Divisor</param>
    /// <returns>The result as quotient.</returns>
    public Fraction Divide(Fraction divisor) {
        // we want to skip the multiplications if we know the result is going to be 0/b or a/0
        var otherNumerator = divisor.Numerator;
        var otherDenominator = divisor.Denominator;
        
        if (otherDenominator.IsZero) {
            // dividing by NaN or Infinity produces NaN or Zero
            return otherNumerator.IsZero || Denominator.IsZero ? NaN : Zero;
        }
        
        var thisNumerator = Numerator;
        var thisDenominator = Denominator;
        
        if (thisDenominator.IsZero) {
            // `this` is NaN or Infinity
            return thisNumerator.Sign switch {
                // +/- Infinity divided by a number
                1 => otherNumerator.Sign * otherDenominator.Sign < 0 ? NegativeInfinity : PositiveInfinity,
                -1 => otherNumerator.Sign * otherDenominator.Sign < 0 ? PositiveInfinity : NegativeInfinity,
                // NaN divided by a number
                _ => NaN
            };
        }

        if (_normalizationNotApplied || divisor._normalizationNotApplied) {
            if (thisNumerator.IsZero) {
                return otherNumerator.IsZero ?
                    NaN :
                    Zero;
            }

            if (otherNumerator.IsZero) {
                return new Fraction(false, thisNumerator.Sign * otherDenominator.Sign, BigInteger.Zero);
            }

            // all terms are non-negative: normalize the signs
            if (thisDenominator.Sign == -1) {
                thisNumerator = -thisNumerator;
                thisDenominator = -thisDenominator;
            }

            if (otherDenominator.Sign == -1) {
                otherNumerator = -otherNumerator;
                otherDenominator = -otherDenominator;
            }

            // adjust the signs of the numerator (optional)
            if (otherNumerator.Sign == -1) {
                thisNumerator = -thisNumerator;
                otherNumerator = -otherNumerator;
            }

            // attempt to reduce the denominators
            switch (thisDenominator.CompareTo(otherDenominator)) {
                case 1: {
                    if (otherDenominator.IsOne) {
                        // {123/10} / {456/1}
                        return new Fraction(true,
                            thisNumerator,
                            otherNumerator.IsOne
                                ? thisDenominator
                                : thisDenominator * otherNumerator);
                    }
            
                    var gcd = BigInteger.GreatestCommonDivisor(thisDenominator, otherDenominator);
                    if (gcd == otherDenominator) {
                        thisDenominator /= otherDenominator;
                        return new Fraction(true,
                            thisNumerator,
                            otherNumerator.IsOne
                                ? thisDenominator
                                : otherNumerator == BigInteger.MinusOne
                                    ? -thisDenominator
                                    : thisDenominator * otherNumerator);
                    }
            
                    if (!gcd.IsOne) {
                        thisDenominator /= gcd;
                        otherDenominator /= gcd;
                    }
                    
                    var numerator = thisNumerator.IsOne
                        ? otherDenominator
                        : thisNumerator == BigInteger.MinusOne
                            ? -otherDenominator
                            : thisNumerator * otherDenominator;

                    var denominator = otherNumerator.IsOne
                        ? thisDenominator
                        : thisDenominator * otherNumerator;

                    return new Fraction(true, numerator, denominator);
                }
                case -1: {
                    if (thisDenominator.IsOne) {
                        // {123/1} / {456/10}
                        return new Fraction(true,
                            thisNumerator.IsOne
                                ? otherDenominator
                                : thisNumerator == BigInteger.MinusOne
                                    ? -otherDenominator
                                    : thisNumerator * otherDenominator,
                            otherNumerator);
                    }
                    
                    var gcd = BigInteger.GreatestCommonDivisor(otherDenominator, thisDenominator);
                    if (gcd == thisDenominator) {
                        otherDenominator /= thisDenominator;
                        return new Fraction(true,
                            thisNumerator.IsOne
                                ? otherDenominator
                                : thisNumerator == BigInteger.MinusOne
                                    ? -otherDenominator
                                    : thisNumerator * otherDenominator,
                            otherNumerator);
                    }
                    
                    if (!gcd.IsOne) {
                        thisDenominator /= gcd;
                        otherDenominator /= gcd;
                    }
                    
                    var numerator = thisNumerator.IsOne
                        ? otherDenominator
                        : thisNumerator == BigInteger.MinusOne
                            ? -otherDenominator
                            : thisNumerator * otherDenominator;

                    var denominator = otherNumerator.IsOne
                        ? thisDenominator
                        : thisDenominator * otherNumerator;

                    return new Fraction(true, numerator, denominator);
                }
                default: {
                    return new Fraction(true, thisNumerator, otherNumerator);
                }
            }
        }

        switch (otherNumerator.Sign) {
            case 0:
                // divisor is 0
                return new Fraction(false, thisNumerator.Sign * otherDenominator.Sign, BigInteger.Zero);
            case -1:
                // the divisor is negative, correct signs
                otherNumerator = -otherNumerator;
                otherDenominator = -otherDenominator;
                break;
        }

        if (thisNumerator.IsZero) {
            return Zero;
        }

        return ReduceSigned(
            MultiplyTerms(ref thisNumerator, ref otherDenominator),
            MultiplyTerms(ref thisDenominator, ref otherNumerator));
    }

    /// <summary>
    ///     Returns this as reduced/simplified fraction. The fraction's sign will be normalized.
    /// </summary>
    /// <returns>A reduced and normalized fraction.</returns>
    public Fraction Reduce() => _normalizationNotApplied ? GetReducedFraction(Numerator, Denominator) : this;

    /// <summary>
    /// Gets the absolute value of a <see cref="Fraction"/> object.
    /// </summary>
    /// <returns>The absolute value.</returns>
    public Fraction Abs() => Abs(this);

    /// <summary>
    /// Gets the absolute value of a <see cref="Fraction"/> object.
    /// </summary>
    /// <param name="fraction">The fraction.</param>
    /// <returns>The absolute value.</returns>
    public static Fraction Abs(Fraction fraction) {
        return fraction._normalizationNotApplied
            ? new Fraction(true, BigInteger.Abs(fraction.Numerator), BigInteger.Abs(fraction.Denominator))
            : new Fraction(false, BigInteger.Abs(fraction.Numerator), fraction.Denominator);
    }

    /// <summary>
    /// Returns a reduced and normalized fraction.
    /// </summary>
    /// <param name="numerator">Numerator</param>
    /// <param name="denominator">Denominator</param>
    /// <returns>A reduced and normalized fraction</returns>
    public static Fraction GetReducedFraction(BigInteger numerator, BigInteger denominator) {
        if (denominator.IsZero) {
            return numerator.Sign switch {
                1 => PositiveInfinity,
                -1 => NegativeInfinity,
                _ => NaN
            };
        }

        if (numerator.IsZero) {
            return Zero;
        }

        if (denominator.Sign == -1) {
            // Denominator must not be negative after normalization
            numerator = -numerator;
            denominator = -denominator;
        }

        return ReduceSigned(numerator, denominator);
    }

    /// <summary>
    ///     Returns a fraction by reducing the numerator and denominator by their largest common divisor.
    /// </summary>
    /// <param name="numerator">The signed numerator</param>
    /// <param name="denominator">The denominator should be positive</param>
    /// <returns>A normalized fraction</returns>
    private static Fraction ReduceSigned(BigInteger numerator, BigInteger denominator) {
        if (numerator.IsOne || denominator.IsOne || numerator == BigInteger.MinusOne) {
            return new Fraction(false, numerator, denominator);
        }

        var gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);

        return gcd.IsOne
            ? new Fraction(false, numerator, denominator)
            : new Fraction(false, numerator / gcd, denominator / gcd);
    }

    /// <summary>
    /// Returns a fraction raised to the specified power.
    /// </summary>
    /// <param name="base">base to be raised to a power</param>
    /// <param name="exponent">A number that specifies a power (exponent)</param>
    /// <returns>The fraction <paramref name="base"/> raised to the power <paramref name="exponent"/>.</returns>
    public static Fraction Pow(Fraction @base, int exponent) {
        // Note: if the State is normalized, then it would remain normalized after exponentiation (2*2/3*3),
        // otherwise: it is better to reduce the fraction now (avoiding a larger allocation later on)
        Fraction fraction;
        switch (exponent) {
            case > 0:
                fraction = @base.Reduce();
                break;
            case < 0 when @base.IsZero:
                return PositiveInfinity;
            case < 0:
                exponent = -exponent;
                fraction = @base.Reciprocal().Reduce();
                break;
            default:
                // x^0 == 1
                return One;
        }

        return new Fraction(
            normalizationNotApplied: false,
            numerator: BigInteger.Pow(fraction.Numerator, exponent),
            denominator: BigInteger.Pow(fraction.Denominator, exponent));
    }

    internal static BigInteger PowerOfTen(int exponent) {
        if (exponent < PowersOfTen.Length) {
            return PowersOfTen[exponent];
        }

        var maxExponent = PowersOfTen.Length - 1;
        if (exponent > 3 * maxExponent) {
            return BigInteger.Pow(TEN, exponent); // the exponent is > 54
        }

        var maxPowerOfTen = PowersOfTen[maxExponent];
        var result = maxPowerOfTen;
        exponent -= maxExponent; // the exponent no more than 2 * maxExponent (36)
        if (exponent > maxExponent) {
            result *= maxPowerOfTen;
            exponent -= maxExponent; // the exponent is no more than maxExponent (18)
        }

        if (exponent == maxExponent) {
            return result * maxPowerOfTen;
        }

        return result * PowersOfTen[exponent];
    }

    /// <summary>
    /// Returns a fraction with the numerator and denominator exchanged.
    /// </summary>
    /// <returns>
    /// The fraction with the numerator and denominator exchanged.
    /// </returns>
    public Fraction Reciprocal() => Reciprocal(this);

    /// <summary>
    /// Returns a fraction with the numerator and denominator exchanged.
    /// </summary>
    /// <param name="fraction">The fraction.</param>
    /// <returns>
    /// The fraction with the numerator and denominator exchanged.
    /// </returns>
    public static Fraction Reciprocal(Fraction fraction) {
        if (fraction.IsInfinity) {
            // Note: we could technically support "positive" and "negative" zeros as non-normalized fractions
            return Zero;
        }

        return fraction is { _normalizationNotApplied: false, Numerator.Sign: -1 }
            ? new Fraction(false, -fraction.Denominator, -fraction.Numerator)
            : new Fraction(fraction._normalizationNotApplied, fraction.Denominator, fraction.Numerator);
    }

    internal static BigInteger MultiplyTerms(ref readonly BigInteger a, ref readonly BigInteger b) {
        if (a.IsOne) {
            return b;
        }

        return b.IsOne
            ? a
            : a * b;
    }
}
