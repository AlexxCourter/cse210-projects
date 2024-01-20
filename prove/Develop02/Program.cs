using System;
using System.Formats.Asn1;
using System.IO.Enumeration;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to your Journal.");
        //create instances of the Journal and menu
        Journal activeJournal = new();
        PromptGenerator pg = new();
        string answer = "";
        string filename;

        do
        {
            answer = ShowMenu();

            switch(answer)
            {
                case "1":
                    // write a new entry
                    NewEntry(activeJournal, pg);
                    Console.WriteLine(""); //whitespace for readability
                    break;
                case "2":
                    // display the journal
                    activeJournal.DisplayEntries();
                    break;
                case "3":
                    // load journal from file
                    Console.Write("Enter the filename: ");
                    filename = Console.ReadLine();
                    activeJournal.LoadFromFile(filename);
                    break;
                case "4":
                    // save journal to file
                    Console.Write("Enter the filename: ");
                    filename = Console.ReadLine();
                    activeJournal.SaveToFile(filename);
                    break;
                case "5":
                    // Beyond Core Requriements: CUSTOM PROMPTS
                    NewEntry(activeJournal, pg, true);
                    Console.WriteLine(""); //whitespace for readability
                    break;
                case "6":
                    // quits the program.
                    break;
                default:
                    // executed if choice doesn't match any available option
                    Console.WriteLine("Invalid input: Please type only the number of the option you would like.");
                    break;
            }
        }
        while (answer != "6");
    }
    // Beyond core requirements: Added custom prompt where user specifies the prompt they want to write in response to.
    static string ShowMenu()
    {
        Console.WriteLine("Please select one of the following choices by typing the appropriate number.");
        Console.WriteLine("1. Write new entry");
        Console.WriteLine("2. Display journal");
        Console.WriteLine("3. Load");
        Console.WriteLine("4. Save");
        Console.WriteLine("5. Write new entry with custom prompt");
        Console.WriteLine("6. Quit");

        Console.Write("> ");
        string answer = Console.ReadLine();
        //insert whitespace for user readability
        Console.WriteLine("");

        return answer;
    }

    static void NewEntry(Journal journal, PromptGenerator pg, bool custom = false)
    {
        DateTime dateTime = DateTime.UtcNow.Date;

        Entry userResponse = new();
        if (custom == true)
        {
            userResponse._prompt = pg.CustomPrompt();
        }
        else
        {
             userResponse._prompt = pg.RandomPrompt();
        }

        userResponse._date = dateTime.ToString("MM/dd/yyy");

        Console.WriteLine($"{userResponse._prompt}");
        userResponse._userEntry = Console.ReadLine();

        journal.AddEntry(userResponse);
    }
}