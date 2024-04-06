using CustomInspector;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

[CreateAssetMenu(fileName = "RouteSetConfig", menuName = "ScriptableObject/Database/Stage/RouteSetConfig")]
public class RouteSetConfig : ScriptableObject
{
    [Button("ParseToJson")]
    [SerializeField] private string _data;

    private readonly Dictionary<StageId, List<List<Vector3>>> _routeSets = new Dictionary<StageId, List<List<Vector3>>>();

    private void ParseToJson()
    {
        _data = "";
        foreach (var route in _routeSets)
        {
            _data += $"Id : {route.Key}  Count: {route.Value.Count}";
        }
    }
    public void SaveToConfig(List<List<Vector3>> inputRouteSet, StageId stageId)
    {

        if (!_routeSets.ContainsKey(stageId))
        {
            Debug.LogError($"No config found for key {stageId} on {name}");
            _routeSets.Add(stageId, inputRouteSet);
        }
        else
        {
            _routeSets[stageId] = inputRouteSet;
        }
        EditorUtility.SetDirty(this);
    }
    public List<List<Vector3>> LoadFromConfig(StageId stageId)
    {
        if (_routeSets.TryGetValue(stageId, out List<List<Vector3>> lineSet))
            return lineSet;
        Debug.LogError($"No config found for key {stageId} on {name}");
        return new List<List<Vector3>>();
    }
}
