using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlayController;
using UnityEngine;
using UnityEngine.Serialization;

public partial class InGameStateController 
{
    [SerializeField] private StageDataAsset _stageDataAsset;

    public override void SetUpNewGame()
    {
        IsGamePlaying = true;
        RouteSetController.Instance.SetUpNewGame();
        TowerKitSetController.Instance.SetUpNewGame();
        PoolingController.Instance.SetUpNewGame();
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
