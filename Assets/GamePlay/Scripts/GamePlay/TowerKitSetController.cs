using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerKitSetController : SingletonBase<TowerKitSetController>
{
    [SerializeField] private List<TowerKit> _currentTowerKits = new List<TowerKit>();
    [SerializeField] private StageDataAsset _stageDataAsset;
    [SerializeField] private TowerDataAsset _towerDataAsset;
    public TowerKit CurrentSelectedKit;
    private StageConfig _stageConfig;
    private Action _onSelected;
    
    // public List<TowerKit> CurrentTowerKits
    // {
    //     get
    //     {
    //         return _currentTowerKits;
    //     } 
    //     set
    //     {
    //         _currentTowerKits = value;
    //     }
    // }
    private void Reset()
    {
        _currentTowerKits = GetComponentsInChildren<TowerKit>().ToList();
    }
    public override void Awake()
    {
        base.Awake();
        SetUpData();
    }
    private void SetUpData()
    {
        // Loading position and place for each kit
        _stageConfig = _stageDataAsset.GetStageConfig();
        // _stageConfig.TowerKitSetConfig.LoadTowerKitsPositionFromConfig(_currentTowerKits);
        
        // Setup id for each tower kit
        foreach (TowerKit kit in _currentTowerKits)
        {
            kit.Setup(SetCurrentSelectedKit);
        }
        // Setup towerKit runtime data dictionary
        // _towerDataAsset.LoadRuntimeData(ref _currentTowerKits);
    }
    private void SetCurrentSelectedKit(TowerKit towerKit) => CurrentSelectedKit = towerKit;
}
