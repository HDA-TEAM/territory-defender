using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RouteSetController : MonoBehaviour
{
    [FormerlySerializedAs("currentRouteLineRenders")]
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
        _currentRouteLineRenders = _stageConfig.RouteSetConfig.LoadFromConfig(_currentRouteLineRenders);
    }
}
