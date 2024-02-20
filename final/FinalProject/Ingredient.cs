public class Ingredient
{
    private string _name;
    private int _quantity;
    private string _measurement;
    private bool _checked;

    public Ingredient(string name, int quantity, bool check = false)
    {
        _name = name;
        _quantity = quantity;
        _measurement = "";

        _checked = check;
    }

    public Ingredient(string name, int quantity, string measurement, bool check = false)
    {
        _name = name;
        _quantity = quantity;
        _measurement = measurement;

        _checked = check;
    }

    public string GetDisplayString()
    {
        return $"{_quantity} {_measurement} {_name}";
    }

    public string GetChecklistString()
    {
        //this part corrects any strange numberings like "1 flour" which would
        //have read "1 cup flour" with the measurement. When purchasing, it is easier to
        //read just "flour" as it is unlikely to be sold by the cup as a commodity.
        string quantityCorrection;
        if (_measurement.Length > 0)
        {
            quantityCorrection = "";
        }
        else
        {
            quantityCorrection = $"{_quantity}x ";
        }

        if (_checked)
        {
            return $"[X] {quantityCorrection}{_name}";
        }
        else
        {
            return $"[ ] {quantityCorrection}{_name}";
        }
        
    }

    public string GetSaveable()
    {
        string measure = _measurement;
        if (measure == "")
        {
            measure = "NONE"; //corrects for proper save file formatting.
        }
        return $"{_name}||{_quantity}||{measure}||{_checked}";
    }

    public string GetName()
    {
        return _name;
    }

    public void Checkmark()
    {
        _checked = true;
    }
}