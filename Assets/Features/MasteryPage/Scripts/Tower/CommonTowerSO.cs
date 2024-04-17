using GamePlay.Scripts.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Path = System.IO.Path;

[CreateAssetMenu(fileName = "CommonTowerSO", menuName = "ScriptableObject/DataAsset/CommonTowerSO")]
public class CommonTowerSO : ScriptableObject
{
    public UnitId.Tower _towerId;
    public List<RuneLevel> _runeLevels;
    
    public UnitId.Tower GetTowerId()
    {
        return _towerId;
    }
    #region MasteryPage access

    public List<RuneLevel> GetAllRuneDatLevels()
    {
        return _runeLevels;
    }
    #endregion
}



