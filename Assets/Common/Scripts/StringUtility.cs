using System.Text.RegularExpressions;


public class StringUtility
{
    public static string PrepareConvertIntValue(string str)
    {
        return Regex.Replace(str, @"[^a-zA-Z0-9 ]", "");
    }
}

