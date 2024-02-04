public class BreathingActivity : Activity
{
    public BreathingActivity(string name, string description, int duration) : base(name, description, duration)
    {

    }

    public void Run()
    {
        DisplayStartingMessage();
        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(5);

        DateTime current = DateTime.Now;
        DateTime end = current.AddSeconds(GetDuration());

        while(current < end)
        {
            current = DateTime.Now; //update the timer
            Console.Write("Breathe in...");
            ShowCountDown(4);
            Console.Write("\nBreathe out...");
            ShowCountDown(6);
            Console.WriteLine("\n");
        }
        DisplayEndingMessage();
    }
}