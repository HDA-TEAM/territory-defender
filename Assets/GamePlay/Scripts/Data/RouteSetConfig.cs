using AYellowpaper.SerializedCollections;
using GamePlay.Scripts.Route.PreviewCallWaveTooltip;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Scripts.Data
{
    [CreateAssetMenu(fileName = "RouteSetConfig", menuName = "ScriptableObject/Database/Stage/RouteSetConfig")]
    public class RouteSetConfig : ScriptableObject
    {
        [SerializeField][SerializedDictionary("StageId", "RouteLineSet")]
        private SerializedDictionary<StageId, RouteSet> _routeLineSets = new SerializedDictionary<StageId, RouteSet>();

        /// Equal a set of main lines in a stage 
        [Serializable]
        public struct RouteSet
        {
            public ECallWaveUnitPreviewDirectionType ECallWaveUnitPreviewDirectionType;
            public List<RouteLine> MainRouteLines;
        }
        /// Equal a main line in map
        [Serializable]
        public struct RouteLine
        {
            public Vector3 CallwaveButtonPos;
            public List<SubRouteLine> SubRouteLines;
        }
        /// Equal a sub line contain by main line
        [Serializable]
        public struct SubRouteLine
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
}
