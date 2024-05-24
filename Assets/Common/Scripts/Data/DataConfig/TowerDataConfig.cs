using Common.Scripts.Data.DataAsset;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Data.DataConfig
{
    [CreateAssetMenu(fileName = "TowerDataConfig", menuName = "ScriptableObject/DataAsset/TowerDataConfig")]
    public class TowerDataConfig : ScriptableObject
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
}
