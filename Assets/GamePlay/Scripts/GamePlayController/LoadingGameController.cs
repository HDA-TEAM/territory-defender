using Common.Loading.Scripts;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlayController;
using UnityEngine;

public partial class InGameStateController 
{
    [SerializeField] private StageDataAsset _stageDataAsset;

    public override void SetUpNewGame(StartStageComposite startStageComposite)
    {
        IsGamePlaying = true;
        RouteSetController.Instance.SetUpNewGame(startStageComposite);
        TowerKitSetController.Instance.SetUpNewGame(startStageComposite);
        PoolingController.Instance.SetUpNewGame(startStageComposite);
    }
    public override void ResetGame()
    {
        IsGamePlaying = false;
        _enemySpawningFactory.CancelSpawning();
        // Stop update game first
        UnitManager.Instance.ResetGame();
        // remove all units
        PoolingController.Instance.ResetGame();
        RouteSetController.Instance.ResetGame();
        TowerKitSetController.Instance.ResetGame();
    }
}
