using System;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        int magic = Randomize(1,101);

        // Console.Write("What is the magic number? ");
        // string magic = Console.ReadLine();
        // int magic_parse = int.Parse(magic);

        string game_condition = "yes";
        int round_count = 0;

        do
        {
            int guess = PlayGame();

            round_count++;

            if(guess == magic)
            {
                game_condition = "win";
            }
            else if (guess < magic)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magic)
            {
                Console.WriteLine("Lower");
            }

            if (game_condition == "win")
            {
                Console.WriteLine("You guessed it!");
                game_condition = GameCheck();
                if (game_condition == "yes")
                {
                    magic = Randomize(1,101);
                }
                else if (game_condition == "no")
                {
                    Console.WriteLine("Try again next time.");
                }
            }
            
            
        }
        while (game_condition == "yes");

    }

    // stretch challenge
    static string GameCheck()
    {
        // asks the user to provide a yes/no response
        // if they want to continue the game.
        string game_condition;
        do
        {
            Console.Write("Continue guessing? [yes / no]: ");
            game_condition = Console.ReadLine();
            //set to all lowercase for proper comparison
            game_condition = game_condition.ToLower();
            if (game_condition != "yes" && game_condition != "no")
            {
                Console.WriteLine("Please respond only with yes or no");
            }
        }
        while (game_condition != "yes" && game_condition != "no");

        return game_condition;
    }

    static int PlayGame()
    {
        /// <summary>
        /// PlayGame asks the user for a integer guess.
        /// the guess is parsed into type <c>int</c>
        /// and returned.
        /// </summary>
        Console.Write("What is your guess? ");
        string guess = Console.ReadLine();
        int guess_parse = int.Parse(guess);
        //could add error handling and input validation here
        return guess_parse;
    }

    static int Randomize(int start, int end)
    {
        /// <summary>
        /// Randomize accepts a start and end integer, and uses
        /// the Random class to generate a random number in that range.
        /// Also prints a game starting message explaining the rules to the user.
        /// </summary>
        Random randomGenerator = new Random();
        int magic = randomGenerator.Next(start,end);
        Console.WriteLine("A new magic number has been generated. Please try to guess the number, between 1-100!");
        return magic;
    }
}