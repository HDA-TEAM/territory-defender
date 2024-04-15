using UnityEngine;
using UnityEngine.Serialization;

public partial class InGameStateController 
{
    [SerializeField] private StageDataAsset _stageDataAsset;
    [SerializeField] private StageEnemySpawningFactory _enemySpawningFactory;

    private bool _isFinishSpawn;
    private int _totalEnemySpawning;
    public void Start()
    {
        _totalEnemySpawning = 0;
        _isFinishSpawn = false;
        IsGamePlaying = true;
    }
    public void StartGame()
    {
        var spawningConfig = _enemySpawningFactory.SpawningConfig.FindSpawningConfig(StageId.Chap1Stage1);
        _totalEnemySpawning = spawningConfig.GetTotalUnitsSpawning();
        _enemySpawningFactory.StartSpawning(StageId.Chap1Stage1,OnFinishedSpawning);
    }
    private void OnFinishedSpawning()
    {
        _isFinishSpawn = true;
    }
    private bool IsStageSuccess()
    {
        return _isFinishSpawn && _totalEnemySpawning <= 0;
    }
    
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
