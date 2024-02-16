using System;
using System.Collections.Generic;

public static class GameEvents
{
    public static event Action<IComposite> OnCompositeSelected;
    public static event Action<List<IComposite>> OnListCompositeSelected;
    public static void SelectComposite<T>(T composite) where T : IComposite
    {
        OnCompositeSelected?.Invoke(composite);
    }
    
    public static void UpdateListCompositeData(List<IComposite> compositeList)
    {
        OnListCompositeSelected?.Invoke(compositeList);
    }
}

public interface IComposite
{
    // Common properties or methods for composites
}

