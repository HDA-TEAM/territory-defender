using System.Collections.Generic;
using Features.Home.Scripts.HomeScreen.Common;
using Features.MasteryPage.Scripts.Rune;
using UnityEngine;

namespace Features.MasteryPage.Scripts.Tower
{
    public interface ITowerRune
    {
        public void TowerRuneExecute();
    }

    public abstract class TowerRuneBase : ITowerRune
    {
        public TowerRuneBase TowerRuneExecuteBase(RuneDataAsset runeDataAsset, TowerRuneDataAsset towerRuneDataAsset)
        {
            return this;
        }
            
        public abstract void TowerRuneExecute();
    }
}
