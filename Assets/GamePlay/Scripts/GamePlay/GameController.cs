using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private StageDataAsset _stageDataAsset;
    [SerializeField] private StageEnemySpawningFactory _enemySpawningFactory;
    public void StartGame()
    {
        var spawningConfig = _enemySpawningFactory.FindSpawningConfig(StageId.CHAP_1_STAGE_1);
        _enemySpawningFactory.StartSpawning(spawningConfig);
    }
}
