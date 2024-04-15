using AYellowpaper.SerializedCollections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "TowerKitSetConfig", menuName = "ScriptableObject/Database/Stage/TowerKitSetConfig")]
public class TowerKitSetConfig : ScriptableObject
{
    [SerializeField] [SerializedDictionary("StageId", "TowerKitLocation")]
    private SerializedDictionary<StageId, List<Vector3>> _towerKitLocations = new SerializedDictionary<StageId, List<Vector3>>();
    public void SaveToConfig(List<Vector3> towerKitPlaces, StageId stageId)
    {
        if (_towerKitLocations.ContainsKey(stageId))
        {
            _towerKitLocations[stageId] = towerKitPlaces;
        }
        else
        {
            Debug.LogError($"No config found for key {stageId} on {name}");
            _towerKitLocations.TryAdd(stageId, towerKitPlaces);   
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
    public List<Vector3> LoadFromConfig(StageId stageId)
    {
        if (_towerKitLocations.TryGetValue(stageId, out List<Vector3> places))
            return places;
        Debug.LogError($"No config found for key {stageId} on {name}");
        return new List<Vector3>();
    }
}
