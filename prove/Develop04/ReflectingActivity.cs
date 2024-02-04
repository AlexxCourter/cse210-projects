using System.Transactions;

public class ReflectingActivity : Activity
{
    List<string> _prompts;
    List<string> _questions;
    public ReflectingActivity(string name, string description, int duration): base(name,description,duration)
    {
        _prompts = new(){
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questions = new(){
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };
    }

    public void Run()
    {
        DisplayStartingMessage();

        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(5);
        Console.WriteLine();
        DisplayPrompt();
        Console.WriteLine("\nNow ponder each of the following questions as they relate to this experience.");
        Console.Write("You may begin in: ");
        ShowCountDown(3);
        DisplayQuestions();

        DisplayEndingMessage();
    }

    private string GetRandomPrompt()
    {
        Random rand = new();
        int i = rand.Next(0, _prompts.Count);
        return _prompts[i];
    }

    private string GetRandomQuestion()
    {
        Random rand = new();
        int i = rand.Next(0, _questions.Count);
        return _questions[i];
    }

    private void DisplayPrompt()
    {
        Console.WriteLine("Consider the following prompt:");

        string prompt = GetRandomPrompt();
        Console.WriteLine($"\n--- {prompt} ---\n");
        Console.Write("When you have something in mind, press enter to continue.");
        Console.ReadLine(); //does nothing except pause the program until the user presses enter.
    }

    private void DisplayQuestions()
    {
        Console.Clear();
        
        DateTime current = DateTime.Now;
        DateTime end = current.AddSeconds(GetDuration());
        while(current < end)
        {
            current = DateTime.Now;

            Console.Write($"\n> {GetRandomQuestion()}");
            ShowSpinner(5);
        }
    }
}