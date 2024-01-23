using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction defaultFrac = new Fraction();
        Fraction wholeFrac = new Fraction(6);
        Fraction frac = new Fraction(6,7);

        Console.WriteLine("**constructors**");
        Console.WriteLine(defaultFrac.GetFractionString());
        Console.WriteLine(wholeFrac.GetFractionString());
        Console.WriteLine(frac.GetFractionString());

        Console.WriteLine(""); //spacer
        Console.WriteLine("**getters and setters**");
        Console.WriteLine(defaultFrac.GetTop());
        Console.WriteLine(defaultFrac.GetBottom());
        Console.WriteLine("Setting top to 5 and bottom to 4");
        defaultFrac.SetTop(5);
        defaultFrac.SetBottom(4);
        Console.WriteLine(defaultFrac.GetFractionString());

        Fraction frac1 = new Fraction();
        Fraction frac2 = new Fraction(5);
        Fraction frac3 = new Fraction(3,4);
        Fraction frac4 = new Fraction(1,3);

        Console.WriteLine(frac1.GetFractionString());
        Console.WriteLine(frac1.GetDecimalValue());
        Console.WriteLine(frac2.GetFractionString());
        Console.WriteLine(frac2.GetDecimalValue());
        Console.WriteLine(frac3.GetFractionString());
        Console.WriteLine(frac3.GetDecimalValue());
        Console.WriteLine(frac4.GetFractionString());
        Console.WriteLine(frac4.GetDecimalValue());
    }
}