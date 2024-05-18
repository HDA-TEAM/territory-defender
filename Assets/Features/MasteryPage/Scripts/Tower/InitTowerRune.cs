using Features.Home.Scripts.HomeScreen.Common;
using UnityEngine;

namespace Features.MasteryPage.Scripts.Tower
{
    public sealed class InitTowerRuneState : ITowerRune
    {
        public void TowerRuneExecute()
        {
            Debug.Log("Executing InitTowerRuneState");
            // Implementation for initializing tower rune data
            
        }
    }
    // public sealed class UpgradeTowerRuneState : ITowerRune
    // {
    //     public void TowerRuneExecute(TowerRuneDataController controller)
    //     {
    //         // Implementation for upgrading tower rune data
    //         controller.UpgradeTowerRuneData();
    //     }
    // }
    //
    // public sealed class ResetTowerRuneState : ITowerRune
    // {
    //     public void TowerRuneExecute(TowerRuneDataController controller)
    //     {
    //         // Implementation for resetting tower rune data
    //         controller.ResetTowerRuneData();
    //     }
    // }
}
