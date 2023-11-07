using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private StageDataAsset _stageDataAsset;
    public void StartGame()
    {
        var config = _stageDataAsset.GetStageConfig();
        config.StageSpawningConfig.StartSpawning();
    }
}
