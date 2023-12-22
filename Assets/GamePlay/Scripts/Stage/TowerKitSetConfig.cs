using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TowerKitSetConfig_", menuName = "ScriptableObject/Database/Stage/TowerKitSetConfig")]
public class TowerKitSetConfig : ScriptableObject
{
    [SerializeField] [SerializedDictionary("StageId", "StageConfig")]
    private SerializedDictionary<StageId, List<Vector3>> _towerKitLocations = new SerializedDictionary<StageId, List<Vector3>>();
    public void SaveToConfig(List<Vector3> towerKitPlaces, StageId stageId)
    {
        if (!_towerKitLocations.ContainsKey(stageId))
        {
            Debug.LogError($"No config found for key {stageId} on {this.name}");
            return;
        }
        
        _towerKitLocations[stageId] = towerKitPlaces;
    }
    public List<Vector3> LoadFromConfig( StageId stageId)
    {
        List<Vector3> places = new List<Vector3>();
        if (!_towerKitLocations.TryGetValue(stageId, out places))
        {
            Debug.LogError($"No config found for key {stageId} on {this.name}");
            return new List<Vector3>();
        }
        return places;
    }
}
