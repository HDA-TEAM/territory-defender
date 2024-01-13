using Debug = UnityEngine.Debug;
using System.Diagnostics;

public static class CommonLog 
{
    [Conditional("ALL_LOG")]
    public static void LogError(string mess)
    {
        Debug.LogError(mess);
    }
}
