using UnityEngine;
using UnityEngine.Serialization;

public class StageSpawningMachine : MonoBehaviour
{
    [FormerlySerializedAs("stageSpawningInformation")]
    [SerializeField] private StageSpawningConfig stageSpawningConfig;
    private void StartSpawning()
    {
        stageSpawningConfig.StartSpawning();
    }
}
