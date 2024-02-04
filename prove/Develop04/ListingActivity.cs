public class ListingActivity : Activity
{
    private int _count = 0;
    private List<string> _prompts;

    public ListingActivity(string name, string description, int duration) : base(name,description,duration)
    {
        //initialize the list of prompts.
        _prompts = new(){
            "Who are people that you appreciate?",
            "What are your personal strengths?",
            "Who are people that you went out of your way to help this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your heroes?"
        };
    }

    public void Run()
    {
        DisplayStartingMessage();

        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(5);
        Console.WriteLine();
        Console.WriteLine("\nList as many responses as you can to the following prompt:");
        Console.WriteLine($"--- {GetRandomPrompt()} ---");
        Console.Write("\nYou may begin in: ");
        ShowCountDown(3);
        Console.WriteLine();
        List<string> items = GetListFromUser();
        _count = items.Count;

        Console.WriteLine($"You listed {_count} items!\n");

        DisplayEndingMessage();
    }

    private string GetRandomPrompt()
    {
        Random rand = new();
        int i = rand.Next(0, _prompts.Count);
        return _prompts[i];
    }

    private List<string> GetListFromUser()
    {
        List<string> userList = new();

        DateTime current = DateTime.Now;
        DateTime end = current.AddSeconds(GetDuration());

        while(current < end)
        {
            current = DateTime.Now;
            Console.Write("> ");
            string item = Console.ReadLine();
            userList.Add(item);
        }

        return userList;
    }
}