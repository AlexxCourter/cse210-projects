using System;

class Program
{
    static void Main(string[] args)
    {
        // *** CREATIVE ADDITION ***
        // *** user chooses from a list of 24 doctrinal mastery scriptures ***
        //initialize the scripture dictionary
        Scriptionary scriptionary = new();
        //prompt the user to select a scripture from the list as the scripture
        //that will be used in the memorizer program
        Scripture scrip = StartUp(scriptionary);

        bool continueGuessing = true;

        do
        {
            Console.Clear();
            //display the scripture
            Console.WriteLine(scrip.GetDisplayText());
            Console.WriteLine(); //spacer
            //determines if program continues or quits
            continueGuessing = Menu();
            //check if scripture is all hidden. If true, next enter press will quit.
            if (scrip.IsAllHidden() == true)
            {
                continueGuessing = false;
            }
            else
            {
                //when the scripture isn't all hidden, hides random words
                scrip.HideRandomWords(3);
            }
        }
        while (continueGuessing == true);
    }

    static Scripture StartUp(Scriptionary scriptionary)
    {
        Console.Clear();
        Console.WriteLine(scriptionary.GetDictionaryReferences());
        Console.WriteLine("Welcome to the Scripture memorizer app. Enter the number from the list above for the scripture you want to memorize.");
        Console.Write("> ");
        int scriptureNumber = int.Parse(Console.ReadLine());
        //get the scripture related to the user's selection
        return scriptionary.GetScriptureByNumber(scriptureNumber);
    }

    static bool Menu()
    {
        //calls the user input to either continue or quit
        bool continueGuessing;
        Console.WriteLine("Press enter to continue or type 'quit' to finish:");
        string response = Console.ReadLine();

        if (response.ToLower() == "quit")
        {
            continueGuessing = false;
        }
        else
        {
            continueGuessing = true;
        }
        return continueGuessing;
    }
}