using System;

class Program
{
    static void Main(string[] args)
    {
        BreathingActivity breathe = new("breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.", 30);
        ReflectingActivity reflect = new("reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.", 30);
        ListingActivity listing = new("listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.", 30);
        
        //creative addition: stat tabulation class keeps track of how long you do activities for in the session.
        Stats statistics = new();

        bool running = true;

        while(running == true)
        {
            string choice = Menu();

            switch(choice)
            {
                case "1":
                    breathe.Run();
                    statistics.RecordBreathing(breathe.GetDuration());
                    break;
                case "2":
                    reflect.Run();
                    statistics.RecordReflection(reflect.GetDuration());
                    break;
                case "3":
                    listing.Run();
                    statistics.RecordListing(listing.GetDuration());
                    break;
                case "4":
                    statistics.DisplayStats();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("\nInvalid input: Please enter the number of the choice you would like to make. i.e., 4 for Quit.\n");
                    break;
            }
        }
    }

    static string Menu()
    {
        Console.WriteLine("Menu Options:");
        Console.WriteLine("  1. Start breathing activity");
        Console.WriteLine("  2. Start reflecting activity");
        Console.WriteLine("  3. Start listing activity");
        Console.WriteLine("  4. See stats");
        Console.WriteLine("  5. Quit");
        Console.Write("Select a choice from the menu: ");
        string choice = Console.ReadLine();
        return choice;
    }
}