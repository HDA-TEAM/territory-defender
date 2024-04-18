using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    public abstract class DataConfigBase<Key,Val> : ScriptableObject
    {
        [SerializeField] [SerializedDictionary("Key","Val")]
        private SerializedDictionary<Key,Val> _unitsData;
        
        public Val GetUnitConfigById(Key unitId)
        {
            _unitsData.TryGetValue(unitId, out Val unitDataComposite);
            return unitDataComposite;
        }
        public bool IsExistUnit(Key unitId)
        {
            return _unitsData.ContainsKey(unitId);
        }
    }
}
