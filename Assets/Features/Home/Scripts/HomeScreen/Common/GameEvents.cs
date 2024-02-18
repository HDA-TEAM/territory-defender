using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameEvents // SO
{
    public static event Action<IComposite> OnCompositeSelected;
    public static event Action<List<IComposite>> OnListCompositeSelected;

    public static List<HeroComposite> CurHeroComposite;
    public static void SelectComposite<T>(T composite) where T : IComposite
    {
        OnCompositeSelected?.Invoke(composite);
    }
    
    public static void UpdateListCompositeData(List<IComposite> compositeList)
    {
        CurHeroComposite = compositeList.OfType<HeroComposite>().ToList();
        OnListCompositeSelected?.Invoke(compositeList);
    }
}

public interface IComposite
{
    // Common properties or methods for composites
}

