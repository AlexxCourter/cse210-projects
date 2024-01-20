using System.IO;
using System.Runtime.CompilerServices;

public class Journal
{
    public List<Entry> _entries = new();
    
    string _delimiter = "|||";

    public void DisplayEntries()
    {
        foreach (Entry entry in _entries)
        {
            entry.DisplayEntry();
            //insert whitespace for user readability
            Console.WriteLine("");
        }
    }

    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine($"Date: {entry._date}, Prompt: {entry._prompt}");
                outputFile.WriteLine($"{entry._userEntry}");
                outputFile.WriteLine($"{_delimiter}");
            }
        }
        Console.WriteLine("Journal saved.");
        Console.WriteLine("");
    }

    public void LoadFromFile(string filename)
    {
        string[] lines = System.IO.File.ReadAllLines(filename);
        //is there a better way than concat back into one string to separate myself?
        string fullText = String.Join("~~", lines);
        fullText = fullText.Trim(new Char[]{'|'}); //remove delimiter at end of doc to prevent empty lines
        string[] entries = fullText.Split("|||");

        foreach (string item in entries)
        {
            string entry = item.Trim(new Char[]{'~'}); //remove delimiter at ends to prevent empty lines
            string[] parts = entry.Split("~~");
            //get date
            parts[0] = parts[0].Replace("Date: ", String.Empty);
            string date = parts[0].Substring(0,10);
            //get prompt
            parts[0] = parts[0].Replace("Prompt: ", String.Empty);
            string prompt = parts[0].Remove(0,12); //remove the date, comma, and space from start
            // string prompt = parts[0].Trim(new Char[]{',', ' '});
            //user response should at this point just be parts[1]
            Entry newEntry = new();
            newEntry._date = date;
            newEntry._prompt = prompt;
            newEntry._userEntry = parts[1];

            AddEntry(newEntry);
        }
        Console.WriteLine($"Journal loaded from {filename}.");
        Console.WriteLine("");
    }
}