﻿using System;

WorkWithIntegers();
OrderPrecedence();
TestLimits();
WorkWithDoubles();
WorkWithDecimals();
AreaOfCircle();

/// <summary>
/// Method to demonstrate simple maths equations
/// </summary>
void WorkWithIntegers()
{
    Console.WriteLine("---WorkWithIntegers---");
    int a = 18;
    int b = 6;

    // addition
    int c = a + b;
    Console.WriteLine(c);
    // subtraction
    c = a - b;
    Console.WriteLine(c);

    // multiplication
    c = a * b;
    Console.WriteLine(c);

    // division
    c = a / b;
    Console.WriteLine(c);
}

/// <summary>
/// Method to demonstrate the order precedence for different mathematical operations
/// </summary>
void OrderPrecedence()
{
    Console.WriteLine("---OrderPrecedence---");
    int a = 5;
    int b = 4;
    int c = 2;
    int d = a + b * c;
    Console.WriteLine(d);

    d = (a + b) * c;
    Console.WriteLine(d);

    d = (a + b) - 6 * c + (12 * 4) / 3 + 12;
    Console.WriteLine(d);

    int e = 7;
    int f = 4;
    int g = 3;
    int h = (e + f) / g;
    Console.WriteLine(h);
}

/// <summary>
/// Method to demonstrate integer precision and limits
/// </summary>
void TestLimits()
{
    Console.WriteLine("---TestLimits---");
    int a = 7;
    int b = 4;
    int c = 3;
    int d = (a + b) / c;
    int e = (a + b) % c;
    Console.WriteLine($"quotient: {d}");
    Console.WriteLine($"remainder: {e}");

    int max = int.MaxValue;
    int min = int.MinValue;
    Console.WriteLine($"The range of integers is {min} to {max}");

    int what = max + 3;
    Console.WriteLine($"An example of overflow: {what}");
}

/// <summary>
/// Method to demonstrate double type precision and limits
/// </summary>
void WorkWithDoubles()
{
    Console.WriteLine("---WorkWithDoubles---");
    double a = 5;
    double b = 4;
    double c = 2;
    double d = (a + b) / c;
    Console.WriteLine(d);

    double e = 19;
    double f = 23;
    double g = 8;
    double h = (e + f) / g;
    Console.WriteLine(h);

    double max = double.MaxValue;
    double min = double.MinValue;
    Console.WriteLine($"The range of double is {min} to {max}");

    double third = 1.0 / 3.0;
    Console.WriteLine(third);
}

/// <summary>
/// Method to demonstrate decimal types precision and limits
/// </summary>
void WorkWithDecimals()
{
    Console.WriteLine("---WorkWithDecimals---");
    decimal min = decimal.MinValue;
    decimal max = decimal.MaxValue;
    Console.WriteLine($"The range of the decimal type is {min} to {max}");

    double a = 1.0;
    double b = 3.0;
    Console.WriteLine(a / b);

    decimal c = 1.0M;
    decimal d = 3.0M;
    Console.WriteLine(c / d);
}

/// <summary>
/// Method to demonstrate the use of Math.PI constant
/// </summary>
void AreaOfCircle()
{
    Console.WriteLine("---AreaOfCircle---");
    double r = 2.50;
    double a = (r * r) * Math.PI;
    Console.WriteLine(a);
}