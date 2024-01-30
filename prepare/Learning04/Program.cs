using System;

class Program
{
    static void Main(string[] args)
    {
        Assignment assign = new("Alex", "Math");
        Console.WriteLine(assign.GetSummary());

        Console.WriteLine();

        MathAssignment math = new("Alexander", "Math", "101", "8-10");
        Console.WriteLine(math.GetSummary());
        Console.WriteLine(math.GetHomeworkList());

        Console.WriteLine();

        WritingAssignment write = new("Alexx", "English", "Analysis of Shakespeare");
        Console.WriteLine(write.GetSummary());
        Console.WriteLine(write.GetWritingInformation());

    }
}