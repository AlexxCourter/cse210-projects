public class PromptGenerator
{
    public List<string> _prompts = new List<string> {
        "What experience taught you something new today?",
        "Did you go somewhere special today? and if not, why?",
        "What are 3 things you want to get done this month?",
        "What did you do better today than you did yesterday?",
        "write about a time you did something nice for someone else.",
        "What was your favorite thing that happened today?"
    };

    public string RandomPrompt()
    {
        Random rand = new();
        string prompt = _prompts[rand.Next(0,_prompts.Count)];
        return prompt;
    }

    // beyond core requirements:
    // CUSTOM PROMPTS
    // an additional menu option lets the user write a journal entry to a custom prompt they come up with.
    // saves the prompt and response normally as an Entry.
    public string CustomPrompt()
    {
        Console.WriteLine("Enter the custom prompt you'd like to respond to:");
        Console.Write("> ");
        string prompt = Console.ReadLine();
        return prompt;
    }

}