using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RouteSetController : SingletonBase<RouteSetController>
{
    [SerializeField] private List<LineRenderer> _currentRouteLineRenders = new List<LineRenderer>();
    [SerializeField] private StageDataAsset _stageDataAsset;

    private StageConfig _stageConfig;
    public List<LineRenderer> CurrentRouteLineRenderers
    {
        get
        {
            return _currentRouteLineRenders;
        } 
        set
        {
            _currentRouteLineRenders = value;
        }
    }
    private void Start()
    {
        _stageConfig = _stageDataAsset.GetStageConfig();
        // _currentRouteLineRenders = _stageConfig.RouteSetConfig.LoadFromConfig(_currentRouteLineRenders);
    }
    
    [SerializeField] private StageId _currentStageId;
    [SerializeField] private RouteSetConfig _routeSetConfig;
    [ContextMenu("SaveToConfig")]
    public void SaveToConfig()
    {
        // List<List<Vector3>> lineRouteSet = new List<List<Vector3>>();
        // for (int i = 0; i < _currentRouteLineRenders.Count; i++)
        // {
        //     // Check if lineRender want to save
        //     if (!_currentRouteLineRenders[i].gameObject.activeSelf)
        //         continue;
        //     
        //     lineRouteSet.Add(new List<Vector3>());
        //         
        //     for (int j = 0; j < _currentRouteLineRenders[i].positionCount; j++)
        //         lineRouteSet[i].Add(_currentRouteLineRenders[i].GetPosition(j));
        // }
        //
        // _routeSetConfig.SaveToConfig(lineRouteSet,_currentStageId);
    }
    
    [ContextMenu("LoadFromConfig")]
    public void LoadFromConfig()
    {
        // List<List<Vector3>> lineRouteSet = _routeSetConfig.LoadFromConfig(_currentStageId);
        //
        // for (int i = 0; i < lineRouteSet.Count; i++)
        // {
        //     // If route set config < total active routeSet on map
        //     if (i >= lineRouteSet.Count)
        //     {
        //         _currentRouteLineRenders[i].gameObject.SetActive(false);
        //         continue;
        //     }
        //     
        //     // Check if this lineRender available to save
        //     _currentRouteLineRenders[i].gameObject.SetActive(true);
        //     
        //     // Init lineRender max space
        //     _currentRouteLineRenders[i].positionCount = lineRouteSet[i].Count;
        //     
        //     // Set position at each point in lineRender
        //     // z always zero
        //     for (int j = 0; j < lineRouteSet[i].Count; j++)
        //         _currentRouteLineRenders[i].SetPosition(j,
        //             new Vector3(
        //                 lineRouteSet[i][j].x,
        //                 lineRouteSet[i][j].y,
        //                 0));
        // }
    }
    public Vector3 GetNearestPosFromRoute(Vector3 posA)
    {
        float nearestDis = float.MaxValue;
        Vector3 res = Vector3.zero;
        foreach (var routeLineRender in _currentRouteLineRenders)
        {
            for (int i = 0; i < routeLineRender.positionCount; i++)
            {
                var curDis = VectorUtility.Distance2dOfTwoPos(posA, routeLineRender.GetPosition(i));
                if (nearestDis > curDis)
                {
                    nearestDis = curDis;
                    res = routeLineRender.GetPosition(i);
                }
                
            }
        }
        return res;
    }
}
