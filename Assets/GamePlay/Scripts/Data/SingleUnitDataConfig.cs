using AYellowpaper.SerializedCollections;
using GamePlay.Scripts.Datas;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    public abstract class SingleUnitDataConfig<T> : ScriptableObject
    {
        [SerializeField] [SerializedDictionary("UnitId","UnitDataComposite")]
        private SerializedDictionary<T,UnitDataComposite> _unitsData;
        
        public UnitDataComposite GetUnitConfigById(T unitId)
        {
            _unitsData.TryGetValue(unitId, out UnitDataComposite unitDataComposite);
            return unitDataComposite;
        }
        public bool IsExistUnit(T unitId)
        {
            return _unitsData.ContainsKey(unitId);
        }
    }
}
