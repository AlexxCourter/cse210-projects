public class Entry
{
    public string _date;
    public string _prompt;
    public string _userEntry;

    public void DisplayEntry()
    {
        Console.WriteLine($"Date: {_date}, Prompt: {_prompt}");
        Console.WriteLine($"{_userEntry}");
    }
}