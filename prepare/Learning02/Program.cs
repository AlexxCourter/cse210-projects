using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new();

        job1._jobTitle = "Software Engineer";
        job1._company = "Microsoft";
        job1._startYear = 2021;
        job1._endYear = 2023;

        // Console.WriteLine(job1._company);

        Job job2 = new();

        job2._jobTitle = "Senior Software Engineer";
        job2._company = "Apple";
        job2._startYear = 2023;
        job2._endYear = 2024;

        // Console.WriteLine(job2._company);

        Resume resume1 = new();

        resume1._name = "John Doe";
        resume1._jobs.Add(job1);
        resume1._jobs.Add(job2);

        // Console.WriteLine(resume1._jobs[0]._jobTitle);
        resume1.Display();
    }
}