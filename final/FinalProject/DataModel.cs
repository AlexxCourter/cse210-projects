public class DataModel
{
    protected List<Ingredient> _ingredients;

    public DataModel()
    {
        _ingredients = new();
    }

    public List<string> GetIngredientNames()
    {
        List<string> result = new();

        foreach(Ingredient item in _ingredients)
        {
            result.Add(item.GetName());
        }

        return result;
    }

    public List<string> GetIngredientDetails()
    {
        List<string> result = new();

        foreach(Ingredient item in _ingredients)
        {
            result.Add(item.GetDisplayString());
        }

        return result;
    }

    public List<Ingredient> GetIngredients()
    {
        return _ingredients;
    }

    public void AddIngredient(Ingredient item)
    {
        _ingredients.Add(item);
    }

    public virtual string GetSaveData()
    {
        //default fits the shopping list model. will be overridden in recipe
        string result = "";

        foreach (Ingredient ingredient in _ingredients)
        {
            result += $"{ingredient.GetSaveable()}[=]";
        }

        return result;
    }

        public void UpdateIngredient(Ingredient ingredient, int index)
    {
        _ingredients[index] = ingredient;
    }
}