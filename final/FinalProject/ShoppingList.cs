public class ShoppingList : DataModel
{
    private string _dateStamp;
    public ShoppingList() : base()
    {
        string stamp = DateTime.Now.ToString();
        _dateStamp = stamp;
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
        string result = $"{_dateStamp}::";
        result += base.GetSaveData();
        return result;
    }
}