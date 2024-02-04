public class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public Activity(string name, string description, int duration)
    {
        _name = name;
        _description = description;
        _duration = duration;
    }

    public void DisplayStartingMessage()
    {
        Console.WriteLine($"Welcome to the {_name} activity.\n");
        Console.WriteLine($"{_description}\n");
        //ask the user to set a custom duration.
        PromptCustomDuration();
    }

    public void PromptCustomDuration()
    {
        //will create a loop that breaks once the user has correctly entered a valid number of seconds or chosen the default duration.
        //asks the user to set the duration of the activity.
        Console.Write($"How long in seconds would you like for your session? (press enter for default {_duration} seconds): ");
        //create loop which will exit when a correct input is made
        bool success = false;
        while(success == false)
        {
            string response = Console.ReadLine();
            if (response.Length > 0 && int.TryParse(response, out int seconds))
            {
                seconds = int.Parse(response);
                _duration = seconds;
                success = true;
            }
            else if (response.Length == 0)
            {
                //allows duration to remain default set at declaration of the activity.
                success = true;
            }
            else
            {
                Console.WriteLine("Woops! There may have been a space or an incorrect character. Please enter a number of seconds, such as 30: ");
            }
        }
    }

    public int GetDuration()
    {
        return _duration;
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine("\nWell done!!\n");
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name} activity.\n");
    }

    public void ShowSpinner(int seconds)
    {
        List<string> animations = new(){
            "|",
            "/",
            "-",
            "\\"
        };

        DateTime start = DateTime.Now;
        DateTime end = start.AddSeconds(seconds);
        DateTime current = DateTime.Now;

        int i = 0;
        while(current < end)
        {
            current = DateTime.Now;
            Console.Write(animations[i]);
            Thread.Sleep(500);
            Console.Write("\b \b");
            i++;
            if(i >= animations.Count)
            {
                i = 0;
            }
        }
    }

    public void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i}");
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

}