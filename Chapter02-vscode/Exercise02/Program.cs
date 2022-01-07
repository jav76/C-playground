using static System.Console;
using System.Collections.Generic;
using System.Globalization;


Console.WriteLine(String.Concat(Enumerable.Repeat("-", 100)));

Console.WriteLine(
    "{0, -12}{1, -20}{2, 34}{3, 34}",
    "Type", "Byte(s) of memory", "Min", "Max");

Console.WriteLine(
    "{0, -12}{1, -20}{2, 34}{3, 34}",
    "sbyte", sizeof(sbyte), sbyte.MinValue, sbyte.MaxValue);
Console.WriteLine(
    "{0, -12}{1, -20}{2, 34}{3, 34}",
    "byte", sizeof(byte), byte.MinValue, byte.MaxValue);
Console.WriteLine(
    "{0, -12}{1, -20}{2, 34}{3, 34}",
    "short", sizeof(short), short.MinValue, short.MaxValue);
Console.WriteLine(
    "{0, -12}{1, -20}{2, 34}{3, 34}",
    "ushort", sizeof(ushort), ushort.MinValue, ushort.MaxValue);
Console.WriteLine(
    "{0, -12}{1, -20}{2, 34}{3, 34}",
    "int", sizeof(int), int.MinValue, int.MaxValue);
Console.WriteLine(
    "{0, -12}{1, -20}{2, 34}{3, 34}",
    "uint", sizeof(uint), uint.MinValue, uint.MaxValue);
Console.WriteLine(
    "{0, -12}{1, -20}{2, 34}{3, 34}",
    "long", sizeof(long), long.MinValue, long.MaxValue);
Console.WriteLine(
    "{0, -12}{1, -20}{2, 34}{3, 34}",
    "ulong", sizeof(ulong), ulong.MinValue, ulong.MaxValue);
Console.WriteLine(
    "{0, -12}{1, -20}{2, 34}{3, 34}",
    "float", sizeof(float), float.MinValue, float.MaxValue);
Console.WriteLine(
    "{0, -12}{1, -20}{2, 34}{3, 34}",
    "double", sizeof(double), double.MinValue, double.MaxValue);
Console.WriteLine(
    "{0, -12}{1, -20}{2, 34}{3, 34}",
    "decimal", sizeof(decimal), decimal.MinValue, decimal.MaxValue);

