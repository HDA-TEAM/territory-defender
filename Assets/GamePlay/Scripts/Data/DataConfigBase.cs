using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    public abstract class DataConfigBase<TKey,TVal> : ScriptableObject
    {
        [SerializeField] [SerializedDictionary("TKey","TVal")]
        protected SerializedDictionary<TKey,TVal> _data;
        
        public TVal GetUnitConfigById(TKey unitId)
        {
            _data.TryGetValue(unitId, out TVal unitDataComposite);
            return unitDataComposite;
        }
        public bool IsExistUnit(TKey unitId)
        {
            return _data.ContainsKey(unitId);
        }
    }
}
