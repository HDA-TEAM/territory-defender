using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private StageDataAsset _stageDataAsset;
    [SerializeField] private StageEnemySpawningFactory _enemySpawningFactory;
    public void StartGame()
    {
        var spawningConfig = _enemySpawningFactory.FindSpawningConfig(StageId.Chap1Stage1);
        _enemySpawningFactory.StartSpawning(spawningConfig);
    }
}
