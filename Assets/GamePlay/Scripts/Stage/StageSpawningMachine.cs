using UnityEngine;

public class StageSpawningMachine : MonoBehaviour
{
    [SerializeField] private StageSpawningInformation stageSpawningInformation;
    private void StartSpawning()
    {
        stageSpawningInformation.StartSpawning();
    }
}
