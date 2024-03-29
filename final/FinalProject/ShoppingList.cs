public class ShoppingList : DataModel
{
    private string _dateStamp;
    public ShoppingList() : base()
    {
        string stamp = DateTime.Now.ToString();
        _dateStamp = stamp;
    }

    public ShoppingList(string timeStamp) : base()
    {
        _dateStamp = timeStamp;
    }

    public string GetTimeStamp()
    {
        return _dateStamp;
    }

    public void MarkItem(int index)
    {
        _ingredients[index].Checkmark();
    }

    public override string GetSaveData()
    {
        string result = $"[s]::{_dateStamp}::";
        result += base.GetSaveData();
        return result;
    }

    public List<string> GetIngredientChecklist()
    {
        List<string> result = new();

        foreach(Ingredient item in _ingredients)
        {
            result.Add(item.GetChecklistString());
        }

        return result;
    }
}