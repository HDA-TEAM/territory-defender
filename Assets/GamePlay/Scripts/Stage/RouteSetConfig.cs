using CustomInspector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    }
    public List<List<Vector3>> LoadFromConfig(StageId stageId)
    {
        List<List<Vector3>> lineSet = new List<List<Vector3>>();
        if (_routeSets.TryGetValue(stageId, out lineSet))
            return lineSet;
        Debug.LogError($"No config found for key {stageId} on {name}");
        return new List<List<Vector3>>();
    }
}
