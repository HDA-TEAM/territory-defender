using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "RouteSetConfig", menuName = "ScriptableObject/Database/Stage/RouteSetConfig")]
public class RouteSetConfig : ScriptableObject
{
    [SerializeField][SerializedDictionary("StageId", "RouteLineSet")]
    private SerializedDictionary<StageId, RouteSet> _routeLineSets = new SerializedDictionary<StageId, RouteSet>();

    [Serializable]
    public struct RouteSet
    {
        public List<RouteLine> RouteLines;
    }
    [Serializable]
    public struct RouteLine
    {
        public List<Vector3> PointSet;
    }
    public void SaveToConfig(RouteSet inputRouteSet, StageId stageId)
    {

        if (!_routeLineSets.ContainsKey(stageId))
        {
            Debug.LogError($"No config found for key {stageId} on {name}");
            _routeLineSets.Add(stageId, inputRouteSet);
        }
        else
        {
            _routeLineSets[stageId] = inputRouteSet;
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
    public RouteSet LoadFromConfig(StageId stageId)
    {
        if (_routeLineSets.TryGetValue(stageId, out RouteSet lineSet))
            return lineSet;
        Debug.LogError($"No config found for key {stageId} on {name}");
        return new RouteSet();
    }
}
