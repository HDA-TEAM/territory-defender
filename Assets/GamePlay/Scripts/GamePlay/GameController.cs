using GamePlay.Scripts.GamePlay;
using System;
using UnityEngine;

public class GameController : GamePlaySingletonBase<GameController>
{
    [SerializeField] private StageDataAsset _stageDataAsset;
    [SerializeField] private StageEnemySpawningFactory _enemySpawningFactory;
    public bool IsInGameScene;

    public void Start()
    {
        IsInGameScene = true;
    }
    public void StartGame()
    {
        var spawningConfig = _enemySpawningFactory.FindSpawningConfig(StageId.Chap1Stage1);
        _enemySpawningFactory.StartSpawning(spawningConfig);
    }
    public override void SetUpNewGame()
    {
        IsInGameScene = true;
        RouteSetController.Instance.SetUpNewGame();
        TowerKitSetController.Instance.SetUpNewGame();
        PoolingController.Instance.SetUpNewGame();
    }
    public override void ResetGame()
    {
        IsInGameScene = false;
        // Stop update game first
        UnitManager.Instance.ResetGame();
        // remove all units
        _enemySpawningFactory.CancelSpawning();
        PoolingController.Instance.ResetGame();
        RouteSetController.Instance.ResetGame();
        TowerKitSetController.Instance.ResetGame();
    }
}
