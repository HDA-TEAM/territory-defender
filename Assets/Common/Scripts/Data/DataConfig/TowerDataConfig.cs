using Common.Scripts.Data.DataAsset;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Data.DataConfig
{
    [CreateAssetMenu(fileName = "TowerDataConfig", menuName = "ScriptableObject/Config/TowerDataConfig")]
    public class TowerDataConfig : ScriptableObject
    {
        public UnitId.Tower _towerId;
        public List<RuneLevelData> _runeLevels;
        public void InitializeRune(List<RuneLevelData> runeLevels)
        {
            _runeLevels = new List<RuneLevelData>(runeLevels);
        }
        public UnitId.Tower GetTowerId()
        {
            return _towerId;
        }
        #region MasteryPage access

        public List<RuneLevelData> GetAllRuneDataLevels()
        {
            return _runeLevels;
        }
        #endregion
    }
}



