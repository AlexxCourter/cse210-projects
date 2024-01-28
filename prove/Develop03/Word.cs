public class Word
{
    private string _text;
    private bool _isHidden;
    public Word(string text)
    {
        _text = text;
    }
    //setter functions
    public void Show()
    {
        _isHidden = false;
    }
    public void Hide()
    {
        _isHidden = true;
    }
    //getter functions
    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        string result = _text;
        if (_isHidden == true)
        {
            result = "";
            // make the hidden string the same size
            for (int i = 0; i < _text.Length; i++)
            {
                result += "_";
            }
        }
        return result;
    }
}