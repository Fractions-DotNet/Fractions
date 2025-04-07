# Breaking changes

## Introduction

This file contains a summary of the most important breaking changes, sorted by version.

## Version

### 9.0.0

- When using the .NET8 assembly, the data type implements the two interfaces `INumber<Fraction>` and `ISignedNumber<Fraction>` with the corresponding support but also possible side effects when using the generic [math functions](https://learn.microsoft.com/en-us/dotnet/standard/generics/math). For more information, see this [Microsoft blog article](https://devblogs.microsoft.com/dotnet/dotnet-7-generic-math/).
- Type conversions to integers or decimal types without support for _NaN_ or _Infinity_ now throw an `OverflowException` instead of a `DivideByZeroException`.

### 8.0.0

- Mathematical operations no longer automatically generate normalized fractions if one of the operands is an non-normalized fraction. This has an impact on your calculations, especially if you have used the `JsonFractionConverter` with default settings. In such cases, deserialized fractions create non-normalized fractions, which can lead to changed behavior when calling `ToString`. Please refer the README section [Working with non-normalized fractions](Readme.md#working-with-non-normalized-fractions)
- The default behavior of the `.Equals(Fraction)` method has changed: `Equals` now compares the calculated value from the numerator/denominator (1/2 = 2/4).
- The standard function `ToString()` now depends on the active culture (`CultureInfo.CurrentCulture`). The reason is that NaN and Infinity should be displayed in the system language or the corresponding symbol should be used.
- Argument name for `Fraction.TryParse(..)` changed from `fractionString` to `value`.
- A fraction of 0/0 no longer has the value 0, but means NaN (not a number). Any fraction in the form x/0 is no longer a valid number. A denominator with the value 0 corresponds to, depending on the numerator, NaN, PositiveInfinity or NegativeInfinity.

### 6.0.0

- `new Fraction(0, 0).ToString()` or `new Fraction(0, 3).ToString()` returns "0" instead of "0/0"