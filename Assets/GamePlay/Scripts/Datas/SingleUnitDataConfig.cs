using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace GamePlay.Scripts.Datas
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
    }
}
