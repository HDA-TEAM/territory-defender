namespace GamePlay.Scripts.Tower.TowerKIT.TowerTools
{
    public class TowerEmptyTool : TowerToolBase
    {
        protected void OnEnable()
        {
            _towerToolStatusHandle.SetUp(TowerTooltatus.Block);
        }
    }
}
