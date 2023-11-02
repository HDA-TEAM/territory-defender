using System.Collections;
using System.Collections.Generic;
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
