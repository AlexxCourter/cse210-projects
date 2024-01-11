using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int> {};
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        int number;
        do
        {
            //
            Console.Write("Enter a number: ");
            number = int.Parse(Console.ReadLine());
            if (number != 0)
            {
                numbers.Add(number);
            }
        }
        while (number != 0);

        float sum = SumList(numbers);
        float average = AverageList(numbers, sum);
        int high = Highest(numbers);
        int low = Lowest(numbers);
        numbers.Sort();
        
        //present calculations for the list.
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {high}");
        Console.WriteLine($"The smallest positive number is: {low}");
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine($"{num}");
        }
    }

    static float SumList(List<int> list)
    {
        float total = 0;
        foreach (int num in list)
        {
            total += num;
        }
        return total;
    }

    static float AverageList(List<int> list, float sum)
    {
        // to avoid redundancy and also dependency within this function body,
        // the sum must be calculated beforehand and input as a parameter.
        int length = list.Count;
        float result = sum / length;
        return result;
    }

    static int Highest(List<int> list)
    {
        int highest = 0;
        foreach (int num in list)
        {
            if (num > highest)
            {
                highest = num;
            }
        }
        return highest;
    }

    static int Lowest(List<int> list)
    {
        int lowest = 0;
        foreach (int num in list)
        {
            if (num > 0 && num < lowest)
            {
                lowest = num;
            }
            //set default case
            else if (lowest == 0)
            {
                lowest = num;
            }
        }
        return lowest;
    }
}