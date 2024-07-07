using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Common.Scripts.Data.DataConfig
{
    public abstract class DataConfigBase<TKey, TVal> : ScriptableObject
    {
        [SerializeField] [SerializedDictionary("TKey", "TVal")]
        protected SerializedDictionary<TKey, TVal> _data;

        public TVal GetConfigByKey(TKey keyId)
        {
            _data.TryGetValue(keyId, out TVal unitDataComposite);
            return unitDataComposite;
        }
        public bool IsExist(TKey keyId)
        {
            return _data.ContainsKey(keyId);
        }
        public List<TVal> GetListItem()
        {
            return _data.Values.ToList();
        }
    }
}
