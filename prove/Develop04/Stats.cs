public class Stats
{
    private int _breatheTime = 0;
    private int _reflectTime = 0;
    private int _listTime = 0;

    public void DisplayStats()
    {
        Console.WriteLine("\nStats for this session:");
        Console.WriteLine($"Total seconds spent on breathing activity: {_breatheTime}");
        Console.WriteLine($"Total seconds spent on reflection activty: {_reflectTime}");
        Console.WriteLine($"Total seconds spent on listing activty: {_listTime}");
        int total = (_breatheTime + _reflectTime + _listTime)/60;
        int remainder = (_breatheTime + _reflectTime + _listTime)%60;
        Console.WriteLine($"\nTotal time doing activities this session: {total} minutes {remainder} seconds.\n");
    }

    public void RecordBreathing(int duration)
    {
        _breatheTime += duration;
    }

    public void RecordReflection(int duration)
    {
        _reflectTime += duration;
    }

    public void RecordListing(int duration)
    {
        _listTime += duration;
    }
}