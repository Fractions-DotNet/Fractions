# Fractions

## Introduction

This package provides the `Fraction` type, used for representing rational numbers. It offers a comprehensive set of features for:

- Creating fractions from various data types (integers, decimals, strings, etc.)
- Performing common mathematical operations like addition, subtraction, multiplication, division, remainder, absolute value, and more.
- Rounding fractions to a specified precision.
- Converting fractions to different data types (decimals, strings, etc.).
- Formatting fractions for output using various notations (general, mixed number, decimal notation, etc.).


## Creation

You can implicitly cast `int`, `uint`, `long`, `ulong`, `decimal` or `BigInteger` to `Fraction`:

```csharp
Fraction a = 3;    // int
Fraction b = 4L;   // long
Fraction c = 3.3m; // decimal
Fraction d = new BigInteger(3);
// ..
```

> [!Note]
> For compatibility reasons, the `Fraction` that is produced by these operators is automatically [reduced to its lowest terms](#working-with-non-normalized-fractions), 
which may have a significant performance impact.
> If the performance is a concern, you should consider using one of the constructors that supports specifying the whether the terms should be reduced or not.

You can explicitly cast `double` to `Fraction`, however doing so directly has [some important caveats](#creation-from-double-without-rounding) that you should be aware of:

```csharp
var a = (Fraction)3.3;  // returns {3715469692580659/1125899906842624} which is 3.299999999999999822364316059975
```

You can explicitly cast from `Fraction` to any supported data type (`int`, `uint`, `long`, `ulong`, `BigInteger`, `decimal`, `double`). However, be aware that an `OverflowException` will be thrown, if the target data type's boundary values are exceeded.

### Constructors

There a three types of constructors available:

- `new Fraction (<value>)` for `int`, `uint`, `long`, `ulong`, `BigInteger`, `decimal` and _`double` [(without rounding)](#creation-from-double-without-rounding)_.
- `new Fraction (<numerator>, <denominator>)` using `BigInteger` for numerator and denominator.
- `new Fraction (<numerator>, <denominator>, <reduce>)` using `BigInteger` for numerator and denominator, as well as a `bool` specifying whether the resulting fraction should be normalized (reduced).

> [!IMPORTANT]
> Please refer to the [Working with non-normalized fractions](#working-with-non-normalized-fractions) section for more information about the possible side effects when working with non-reduced fractions.

### Static creation methods
> [!Note]
> All methods that were present in version 7.*, continue to return a `Fraction` that is automatically [reduced to its lowest terms](#working-with-non-normalized-fractions).
> Starting from version 8.0.0 these methods are now supplemented by an overload that adds an additional `boolean` parameter, specifying whether the terms should be reduced.

- `Fraction.FromDecimal(decimal)` : same as the [implicit constructor](#creation).
- `Fraction.FromDecimal(decimal, bool)` : the `boolean` parameter specifies whether the returned `Fraction` should be [reduced](#working-with-non-normalized-fractions).
- `Fraction.FromDouble(double)` : same as the [explicit constructor](#creation), the `Fraction` is constructed [*without rounding*](#creation-from-double-without-rounding).
- `Fraction.FromDoubleRounded(double)` : the resulting `Fraction` would represent a [_close approximation_](#creation-from-double-with-rounding-to-a-close-approximation) of the input.
- `Fraction.FromDoubleRounded(double, int)` : the value is rounded to the specified [number of significant digits](#creation-from-double-with-maximum-number-of-significant-digits).
- `Fraction.FromDoubleRounded(double, int, bool)` : the value is rounded to the specified [number of significant digits](#creation-from-double-with-maximum-number-of-significant-digits), with the `boolean` parameter specifying whether the returned `Fraction` should be [reduced](#working-with-non-normalized-fractions).

> [!IMPORTANT]
> Starting from version 8.0.0, the `FromDouble(..)` overloads no longer throw an `ArgumentException` when passed `double.NaN`, `double.PositiveInfinity` or `double.NegativeInfinity`.
> These values are instead represented as `0/0`, `+1/0` or `-1/0`.  
> For more information see the section about [working with `NaN` and `Infinity`](#working-with-nan-and-infinity).

### Creation from `double` without rounding

The `double` data type in C# uses a binary floating-point representation, which complies with the [IEC 60559:1989 (IEEE 754)](https://en.wikipedia.org/wiki/IEEE_754) standard for [binary floating-point arithmetic](https://en.wikipedia.org/wiki/Floating-point_arithmetic). This representation can't accurately represent all decimal fractions. For example, the decimal fraction _0.1_ is represented as the repeating binary fraction _.0001100110011..._. As a result, a `double` value can only provide an approximate representation of the decimal number it's intended to represent.

#### Large values in the numerator / denominator

When you convert a `double` to a `Fraction` using the `Fraction.FromDouble` method, the resulting fraction is an exact representation of the `double` value, not the decimal number that the `double` is intended to approximate. This is why you can end up with large numerators and denominators. 

```csharp
var value = Fraction.FromDouble(0.1);
Console.WriteLine(value); // Ouputs "3602879701896397/36028797018963968" which is 0.10000000000000000555111512312578
```

The output fraction is an exact representation of the `double` value 0.1, which is actually slightly more than 0.1 due to the limitations of binary floating-point representation.

#### Comparing fractions created with double precision

Using a `Fraction` that was created using this method for strict Equality/Comparison should be avoided. For example:

```csharp
var fraction1 = Fraction.FromDouble(0.1);
var fraction2 = new Fraction(1, 10);
Console.WriteLine(fraction1 == fraction2); // Outputs "False"
```

If you need to compare a `Fraction` created from `double` with others fractions you should either do so by using a tolerance or consider constructing the `Fraction` by [specifying the maximum number of significant digits.](#creation-from-double-with-maximum-number-of-significant-digits)

#### Possible rounding errors near the limits of the double precision

When a `double` value is very close to the limits of its precision, `Fraction.FromDouble(value).ToDouble() == value` might not hold true. This is because the numerator and denominator of the `Fraction` are both very large numbers. When these numbers are converted to `double` for the division operation in the `Fraction.ToDouble` method, they can exceed the precision limit of the `double` type, resulting in a loss of precision.

```csharp
var value = Fraction.FromDouble(double.Epsilon);
Console.WriteLine(value.ToDouble() == double.Epsilon); // Outputs "False"
```

For more detailed information about the behavior of the `Fraction.FromDouble` method and the limitations of the `double` type, please refer to the XML documentation comments in the source code.

### Creation from `double` with maximum number of significant digits

The `Fraction.FromDoubleRounded(double, int)` method allows you to specify the maximum number of significant digits when converting a `double` to a `Fraction`. This can help to avoid large numerators and denominators, and can make the `Fraction` suitable for comparison operations. 

```csharp
var value = Fraction.FromDoubleRounded(0.1, 15); // Returns a fraction with a maximum of 15 significant digits
Console.WriteLine(value); // Outputs "1/10"
```

If you care only about minimizing the size of the numerator/denominator, and do not expect to use the fraction in any strict comparison operations, then [creating an approximated fraction](#creation-from-double-with-rounding-to-a-close-approximation) using the `Fraction.FromDoubleRounded(double)` overload should offer the best performance.

### Creation from `double` with rounding to a close approximation

You can use the `Fraction.FromDoubleRounded(double)` method to avoid big numbers in numerator and denominator. Example:

```csharp
var value = Fraction.FromDoubleRounded(0.1);
Console.WriteLine(value); // Outputs "1/10"
```

However, please note that while rounding to an approximate value would mostly produce the expected result, it shouldn't be relied on for any strict comparison operations. Consider this example:

```csharp
var doubleValue = 1055.05585262;
var roundedValue = Fraction.FromDoubleRounded(doubleValue);      // returns {4085925351/3872710} which is 1055.0558526199999483565771772222
var literalValue = Fraction.FromDoubleRounded(doubleValue, 15);  // returns {52752792631/50000000} which is 1055.05585262 exactly
Console.WriteLine(roundedValue.CompareTo(literalValue); // Outputs "-1" which stands for "smaller than"
Console.WriteLine(roundedValue.ToDouble() == doubleValue); // Outputs "true" as the actual difference is smaller than the precision of the doubles
```

## Creation from `string`

The following string patterns can be parsed:

- `[+/-]n` where _n_ is an integer. Examples: _`+5`_, _`-6`_, _`1234`_, _`0`_
- `[+/-]n.m` where _n_ and _m_ are integers. The decimal point symbol depends on the system's culture settings. Examples: _`-4.3`_, _`0.45`_
- `[+/-]n/[+/-]m` where _n_ and _m_ are integers. Examples: _`1/2`_, _`-4/5`_, _`+4/-3`_, _`32/100`_

Example:

```csharp
var value = Fraction.FromString("1,5", CultureInfo.GetCultureInfo("de-DE"))
// Returns 3/2 which is 1.5
Console.WriteLine(value);
```
> [!TIP]
> You should consider the `TryParse` methods when reading numbers as text from user input. **Furthermore it is best practice to always supply a culture information (e.g. `CultureInfo.InvariantCulture`).** Otherwise you will sooner or later parse wrong numbers because of different decimal point symbols or included Thousands character.

Here is a table presenting the different overloads, and the default parameters that are assumed for each of them:

| Method | `NumberStyles` | `IFormatProvider` | Normalize |
| --- | --- | --- | --- |
| `Fraction.FromString(string)` | `Number` | `null` | `✔️` |
| `Fraction.FromString(string, boolean)` | `Number` | `null` | `❓` |
| `Fraction.FromString(string, IFormatProvider)` | `Number` | `❓` | `✔️` |
| `Fraction.FromString(string, NumberStyles, IFormatProvider)` | `❓` | `❓` | `✔️` |
| `Fraction.FromString(string, NumberStyles, IFormatProvider, bool)` | `❓` | `❓` | `❓` |
| `Fraction.TryParse(string, out Fraction)` | `Number` | `null` | `✔️` |
| `Fraction.TryParse(string, NumberStyles, IFormatProvider, out Fraction)` | `❓` | `❓` | `✔️` |
| `Fraction.TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider, bool, out Fraction)` | `❓` | `❓` | `❓` |

## Conversion

You can convert a `Fraction` to any supported data type by calling:

- `.ToInt32()`
- `.ToUInt32()`
- `.ToInt64()`
- `.ToUInt64()`
- `.ToBigInteger()`
- `.ToDecimal()`
- `.ToDouble()`
- `.ToString()` (using current culture)
- `.ToString(string)` (using format string and the system's current culture)
- `.ToString(string,IFormatProvider)`

If the target's data type boundary values are exceeded the system will throw an `OverflowException`.

Example:

```csharp
var rationalNumber = new Fraction(1, 3);
var value = rationalNumber.ToDecimal();
// result is 0.33333
Console.WriteLine(Math.Round(value, 5));
```

## String format

| Specifier | Description |
| ----------| ----------- |
| G | General format: &lt;numerator&gt;/&lt;denominator&gt; e.g. _1/3_ |
| n | Numerator |
| d | Denominator |
| z | The fraction as integer |
| r | The positive remainder of all digits after the decimal point using the format: &lt;numerator&gt;/&lt;denominator&gt; or `string.Empty` if the fraction is a valid integer without digits after the decimal point. |
| m | The fraction as mixed number e.g. _2 1/3_ instead of _7/3_ |

**Note:** The special characters _#_, and _0_ like in _#.###_ are not supported. Consider converting the `Fraction` to `decimal`/`double` if you want to support the [custom formats](https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-numeric-format-strings).

Example:

```csharp
var value = new Fraction(3, 2);
// returns 1 1/2
Console.WriteLine(value.ToString("m", CultureInfo.GetCultureInfo("de-DE")));
```

## Decimal Notation Formatter

The `DecimalNotationFormatter` class allows for formatting `Fraction` objects using the standard decimal notation, and the specified format and culture-specific format information. 
Unlike standard numeric types such as `double` and `decimal`, there is no limit to the represented range or precision when using `DecimalNotationFormatter`.

### Usage

Here is a general example of how to use the `DecimalNotationFormatter`:

```csharp
Fraction value = Fraction.FromString("123456789987654321.123456789987654321");
string formattedValue = DecimalNotationFormatter.Instance.Format("G36", value, CultureInfo.InvariantCulture);
Console.WriteLine(formattedValue); // Outputs "123456789987654321.123456789987654321"
```

In this example, the `Format` method is used to format the value of a `Fraction` object into a string using the 'G' (General) format with a precision specifier of 36, which formats the fraction with up to 36 significant digits.

### Supported Formats

The `Format` method supports the following format strings. For more information about these formats, see the [official .NET documentation](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings).

| Specifier | Format Name | Fraction | Format | Output |
|-----------|-------------|----------|--------|--------|
| 'G' or 'g' | [General format](https://docs.microsoft.com/dotnet/standard/base-types/standard-numeric-format-strings#the-general-g-format-specifier) | `400/3` | 'G2' | `1.3E+02` |
| 'F' or 'f' | [Fixed-point format](https://docs.microsoft.com/dotnet/standard/base-types/standard-numeric-format-strings#the-fixed-point-f-format-specifier) | `12345/10` | 'F2' | `1234.50` |
| 'N' or 'n' | [Standard Numeric format](https://docs.microsoft.com/dotnet/standard/base-types/standard-numeric-format-strings#the-number-n-format-specifier) | `1234567/1000` | 'N2' | `1,234.57` |
| 'E' or 'e' | [Scientific format](https://docs.microsoft.com/dotnet/standard/base-types/standard-numeric-format-strings#the-exponential-e-format-specifier) | `1234567/1000` | 'E2' | `1.23E+003` |
| 'P' or 'p' | [Percent format](https://docs.microsoft.com/dotnet/standard/base-types/standard-numeric-format-strings#the-percent-p-format-specifier) | `2/3` | 'P2' | `66.67 %` |
| 'C' or 'c' | [Currency format](https://docs.microsoft.com/dotnet/standard/base-types/standard-numeric-format-strings#the-currency-c-format-specifier) | `1234567/1000` | 'C2' | `$1,234.57` |
| 'R' or 'r' | [Round-trip format](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#RFormatString) | `1234567/1000` | 'R' | `1234.567` |
| 'S' or 's' | [Significant Digits After Radix format](#significant-digits-after-radix-format) | `400/3` | 'S2' | `133.33` |

Please note that the 'R' format and the [custom formats](https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-numeric-format-strings) are handled by casting the `Fraction` to `double`, which may result in a loss of precision.

### Significant Digits After Radix Format

The 'S' format is a non-standard format that formats the fraction with significant digits after the radix and dynamically switches between decimal and scientific notation depending on the value of the fraction. 

For fractions where the absolute value is greater than or equal to 0.001 and less than 10000, the 'S' format uses decimal notation. For all other values, it switches to scientific notation.

Here are a few examples:

```csharp
Fraction value = new Fraction(1, 3);
Console.WriteLine(value.ToString("S")); // Outputs "0.33"

value = newFraction(1, 1000);
Console.WriteLine(value.ToString("S")); // Outputs "0.001"

value = new Fraction(1, 100000);
Console.WriteLine(value.ToString("S")); // Outputs "1E-05"
```

## Mathematic operators

The following mathematic operations are supported:

- `.Reduce()` returns a normalized fraction (e.g. _2/4_ -> _1/2_)
- `.Add(Fraction)` returns the sum of `(a + b)`
- `.Subtract(Fraction)` returns the difference of `(a - b)`
- `.Multiply(Fraction)` returns the product of `(a * b)`
- `.Divide(Fraction)` returns the quotient of `(a / b)`
- `.Remainder(Fraction)` returns the remainder (or left over) of `(a % b)`
- `.Negate()` returns a negated fraction (same operation as `(a * -1)`)
- `.Abs()` returns the absolute value `|a|`
- `Fraction.Pow(Fraction, int)` returns a base raised to a power `(a ^ exponent)` (e.g. _1/10_^(-1) -> _10/1_)
- `Fraction.Round(Fraction, int, MidpointRounding)` returns the fraction, which is rounded to the specified precision
- `Fraction.RoundToBigInteger(Fraction, MidpointRounding)` returns the fraction as rounded BigInteger

As extension method:

- `FractionExt.Sqrt(this Fraction, int)` returns the square root, specifying the precision after the decimal point.

Example:

```csharp
 var a = new Fraction(1, 3);
 var b = new Fraction(2, 3);
 var result = a * b;
 // returns 2/9 which is 0,2222...
 Console.WriteLine(result);
```

### Working with non-normalized fractions

> [!IMPORTANT]
> For performance reasons, as of version 8.0.0, mathematical operations such as addition and multiplication no longer reduce the result to it's lowest terms, unless both operands are already simplified. 
> This change in behavior may introduce unexpected results when, for example, calling `ToString` on a `Fraction` that is the result of an expression, having one or more of the values de-serialized using the default `JsonFractionConverter` settings (i.e. without explicit reduction).


| Symbol | Description                                                                                          |
| ------ | ---------------------------------------------------------------------------------------------------- |
| $NF$   | Non-normalized (possibly reducible) fraction, created with `normalize: false`                         |
| $F$    | Fraction created with `normalize: true` (irreducible)                                                              |
| $⊙$   | Mathematical operation having two operands ($+$, $-$, $*$, $/$, $mod$).                              |

The following rules apply:

$F ⊙ F = F$  
$F ⊙ NF = NF$  
$NF ⊙ F = NF$  
$NF ⊙ NF = NF$  

That said, the following applies for normalized fractions:

```csharp
var a = new Fraction(4, 4, normalize: true); // a is 1/1
var b = new Fraction(2);    // b is 2/1 (automatically normalized)
var result = a / b;         // result is 1/2
```

$\frac{1}{1}/\frac{2}{1}=\frac{1}{2}$

However, for non-normalized fractions the following applies:

```csharp
var a = new Fraction(4, 4, normalize: false);
var b = new Fraction(2);    // b is 2/1 (automatically normalized)
var result = a / b;         // result is 4/8
```

$\frac{4}{4}/\frac{2}{1}=\frac{4}{8}$

### Working with `NaN` and `Infinity`

Starting from version 8.0.0, it is now possible for a `Fraction` to be constructed with `0` in the denominator. 
This is typically the result of a division by zero which, depending on the sign of the dividend, now returns `Fraction.PositiveInfinity`, `Fraction.NegativeInfinity` or `Fraction.NaN`.

Subsequent mathematical operations with these values follow the same rules, as implemented by the `double` type.
For example, any number multiplied by infinity results in infinity (or negative infinity, depending on the sign), and any number divided by infinity is zero. 

You can check if a `Fraction` represents one of the special values using the properties:

- `Fraction.IsPositiveInfinity`
- `Fraction.IsNegativeInfinity`
- `Fraction.IsNaN`

> [!TIP]
> You could also check if a `Fraction` represents either `NaN` or `Infinity` by testing whether it's `Denomintor.IsZero`.

## Equality operators

`Fraction` implements the following interfaces:

- `IEquatable<Fraction>`,
- `IComparable`,
- `IComparable<Fraction>`

> [!IMPORTANT]
> Please note that the default behavior of the `.Equals(Fraction)` method has changed with version 8.0.0. `Equals` now compares the calculated value from the $numerator/denominator$ ($Equals(\frac{1}{2}, \frac{2}{4}) = true$).

In case you want to compare the numerator and denominator exactly (i.e. $Equals(\frac{1}{2}, \frac{2}{4}) = false$) then you can use the new `FractionComparer.StrictEquality` comparer.

That said:

```csharp
var a = new Fraction(1, 2, normalize: true);
var b = new Fraction(1, 2, normalize: false);
var c = new Fraction(2, 4, normalize: false); // the fraction is not reduced

// result1 is true
var result1 = a == a;

// result2 is true
var result2 = a == b;

// result3 is true
var result3 = a == c;

// Special case:
// same behavior as with double, see https://learn.microsoft.com/en-us/dotnet/api/system.double.op_equality#remarks
// result4 is false 
var result4 = Fraction.NaN == Fraction.NaN;
```

## Under the hood

The data type stores the numerator and denominator as `BigInteger`. Per default it will reduce fractions to its normalized form during creation. The result of each mathematical operation will be reduced as well. There is a special constructor to create a non-normalized fraction. Be aware that `Equals` relies on normalized values when comparing two different instances.

## Performance considerations

We have a suite of benchmarks that test the performance of various operations in the Fractions library. These benchmarks provide valuable insights into the relative performance of different test cases.
For more detailed information about these benchmarks and how to interpret them, please refer to the [Fractions Benchmarks Readme](./benchmarks/Readme.md) in the benchmarks subfolder.

## Build from source

Just run `dotnet build -c Release`.

### Required software frameworks

- .Net 8.0 SDK
