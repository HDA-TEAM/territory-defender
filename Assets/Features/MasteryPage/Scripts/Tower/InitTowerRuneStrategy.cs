using Features.Home.Scripts.HomeScreen.Common;
using UnityEngine;

namespace Features.MasteryPage.Scripts.Tower
{
    public interface ITowerRune
    {
        void Execute(TowerRuneDataController controller);
    }
    
    public sealed class InitTowerRuneStrategy : ITowerRune
    {
        public void Execute(TowerRuneDataController controller)
        {
            controller.InitializeTowerRuneData();
        }
    }
}
