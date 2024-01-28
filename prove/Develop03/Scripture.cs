
public class Scripture
{
    private Reference _reference;
    private List<Word> _words = new List<Word>();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;

        string[] wordList = text.Split(" ");
        foreach (string word in wordList)
        {
            Word newWord = new(word);
            _words.Add(newWord);
        }
    }

    public bool IsAllHidden()
    {
        foreach (Word word in _words)
        {
            if (word.IsHidden() == false)
            {
                //if we find a word not hidden, immediately return false as all is not hidden.
                return false;
            }
        }
        return true;
    }

    public void HideRandomWords(int numberToHide)
    {
        //for a specific number of words, selects random words to hide.
        //ignores words already hidden.

        // Need to check if all words are hidden, or else infinite loop will occur.
        if (IsAllHidden() == false)
        {
            Random rand = new();
            for (int i = 0; i < numberToHide; i++)
            {
                bool successCondition = false;
                while(successCondition == false)
                {
                    //this likely causes a lot of overhead and would be bad for very long passages.
                    //a better system would involve marking what indexes are hidden and keeping that
                    //logged so that random numbers are only selected from ranges that haven't been
                    //hidden yet.
                    
                    int selector = rand.Next(0, _words.Count);
                    if (_words[selector].IsHidden() == false)
                    {
                        _words[selector].Hide();
                        successCondition = true;
                    }
                    //need to check for if the text is all hidden in the middle of this loop.
                    //breaks loop if the scripture becomes all hidden.
                    if (IsAllHidden() == true)
                    {
                        successCondition = true;
                    }
                }
            }
        }
    }

    public string GetDisplayText()
    {
        string result = $"{_reference.GetDisplayText()} ";
        //add current words
        foreach (Word word in _words)
        {
            result += word.GetDisplayText() + " ";

        }
        return result;
    }
}