using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string grade = Console.ReadLine();
        int grade_parsed = int.Parse(grade);
        
        //env does not like when the variable is unassigned outside of if blocks.
        //need to add error handling in case all if-else branches fail
        string letter = "";
        string sign = "";

        if (grade_parsed >= 90)
        {
            letter = "A";
            // Console.WriteLine("Your letter grade is A.");
        }
        else if (grade_parsed >= 80 && grade_parsed < 90)
        {
            letter = "B";
            // Console.WriteLine("Your letter grade is B.");
        }
        else if (grade_parsed >= 70 && grade_parsed < 80)
        {
            letter = "C";
            // Console.WriteLine("Your letter grade is C.");
        }
        else if (grade_parsed >= 60 && grade_parsed < 70)
        {
            letter = "D";
            // Console.WriteLine("Your letter grade is D.");
        }
        else if (grade_parsed < 60)
        {
            letter = "F";
            // Console.WriteLine("Your letter grade is F.");
        }

        // stretch challenge 1
        if (grade_parsed % 10 >= 7)
        {
            // stretch challenge 2
            if (grade_parsed >= 90 || grade_parsed < 60)
            {
                sign = "";
            }
            else
            {
                sign = "+";
            }
        }
        else if (grade_parsed % 10 < 3)
        {
            //upper limit is 93 since 92 or less will be A-
            if (grade_parsed >= 93 || grade_parsed < 60)
            {
                sign = "";
            }
            else
            {
                sign = "-";
            }
        }
        //print letter grade
        Console.WriteLine($"Your letter grade is {letter}{sign}.");

        if (grade_parsed >= 70)
        {
            Console.WriteLine("Congratulations, you passed the course.");
        }
        else
        {
            Console.WriteLine("Sorry, you did not pass the course. However, you are much more prepared for next time!");
        }
    }
}