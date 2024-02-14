using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RouteSetConfig", menuName = "ScriptableObject/Database/Stage/RouteSetConfig")]
public class RouteSetConfig : ScriptableObject
{
    
    // [SerializeField] [SerializedDictionary("StageId","SingleLineIdx")]
    // private SerializedDictionary<StageId, List<int>> _routeSets = new SerializedDictionary<StageId, List<int>>();
    //
    // [SerializeField] [SerializedDictionary("StageId","SingleLine")]
    // private SerializedDictionary<int, SingleRouteLine> _singleRouteLines = new SerializedDictionary<int, SingleRouteLine>();
    //
    // public struct SingleRouteLineKey
    // {
    //     
    // }
    // [Serializable]
    // public struct SingleRouteLine
    // {
    //     public List<Vector3> PosSets;
    // }
    // public void SaveToConfig(List<List<Vector3>> lineRouteSet, StageId stageId)
    // {
    //     Debug.LogError(_routeSets.Keys.Count);
    //     if (!_routeSets.ContainsKey(stageId))
    //     {
    //         Debug.LogError($"No config found for key {stageId} on {name}");
    //         List<int> singeRouteIdx = new List<int>();
    //         foreach (var singleLine in lineRouteSet)
    //         {
    //             _singleRouteLines.Add(new SingleRouteLine
    //             {
    //                 PosSets = singleLine
    //             });    
    //             singeRouteIdx.Add(_singleRouteLines.Count);
    //         }
    //         _routeSets.Add(stageId, singeRouteIdx);
    //         
    //         return;
    //     }
    //     foreach (var singleLineIdx in _routeSets[stageId])
    //     {
    //         if (singleLineIdx > _singleRouteLines.Count)
    //         {
    //             
    //         }
    //     }
    // }
    // public List<List<Vector3>> LoadFromConfig(StageId stageId)
    // {
    //     List<List<Vector3>> lineSet = new List<List<Vector3>>();
    //     if (!_routeSets.TryGetValue(stageId, out lineSet))
    //     {
    //         Debug.LogError($"No config found for key {stageId} on {name}");
    //         return new List<List<Vector3>>();
    //     }
    //     return lineSet;
    // }
}