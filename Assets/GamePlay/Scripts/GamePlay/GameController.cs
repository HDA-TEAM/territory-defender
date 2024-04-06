using GamePlay.Scripts.GamePlay;
using UnityEngine;

public class GameController : GamePlaySingletonBase<GameController>
{
    [SerializeField] private StageDataAsset _stageDataAsset;
    [SerializeField] private StageEnemySpawningFactory _enemySpawningFactory;
    public void StartGame()
    {
        var spawningConfig = _enemySpawningFactory.FindSpawningConfig(StageId.Chap1Stage1);
        _enemySpawningFactory.StartSpawning(spawningConfig);
    }
    public override void SetUpNewGame()
    {
        _enemySpawningFactory.CancelSpawning();
        RouteSetController.Instance.SetUpNewGame();
        TowerKitSetController.Instance.SetUpNewGame();
        PoolingController.Instance.SetUpNewGame();
    }
    public override void ResetGame()
    {
        // Stop update game first
        UnitManager.Instance.ResetGame();
        // remove all units
        PoolingController.Instance.ResetGame();
        RouteSetController.Instance.ResetGame();
        TowerKitSetController.Instance.ResetGame();
    }
}
