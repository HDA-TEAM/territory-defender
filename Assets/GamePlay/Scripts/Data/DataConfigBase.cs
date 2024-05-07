using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    public abstract class DataConfigBase<TKey,TVal> : ScriptableObject
    {
        [SerializeField] [SerializedDictionary("TKey","TVal")]
        protected SerializedDictionary<TKey,TVal> _data;
        
        public TVal GeConfigByKey(TKey keyId)
        {
            _data.TryGetValue(keyId, out TVal unitDataComposite);
            return unitDataComposite;
        }
        public bool IsExist(TKey keyId)
        {
            return _data.ContainsKey(keyId);
        }
    }
}
