public class Recipe : DataModel
{
    private string _name;
    private List<string> _directions;

    public Recipe(string name) : base()
    {
        _directions = new();

        _name = name;
    }

    public List<string> GetDirections()
    {
        return _directions;
    }

    public void AddDirection(string direction)
    {
        _directions.Add(direction);
    }

    public string GetName()
    {
        return _name;
    }

    public override string GetSaveData()
    {
         string result = $"[+]{_name}::";
         result += base.GetSaveData();
         result += "[-]"; //delimiter separates recipe ingredients from directions specifically.
         foreach (string direction in _directions)
         {
            result += $"{direction}[=]";
         }
         return result;
    }
}