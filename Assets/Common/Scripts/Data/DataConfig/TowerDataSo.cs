using Common.Scripts.Data.DataAsset;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Data.DataConfig
{
    [CreateAssetMenu(fileName = "TowerDataSo", menuName = "ScriptableObject/Config/TowerDataSo")]
    public class TowerDataSo : ScriptableObject
    {
        public UnitId.Tower _towerId;
        public List<RuneData> _runeLevels;
        public void InitializeRune(List<RuneData> runeLevels)
        {
            _runeLevels = new List<RuneData>(runeLevels);
        }
        public UnitId.Tower GetTowerId()
        {
            return _towerId;
        }
        #region MasteryPage access

        public List<RuneData> GetAllRuneDataLevels()
        {
            return _runeLevels;
        }
        #endregion
    }
}



