using Common.Scripts.Datas;
using CustomInspector;
using UnityEditor;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    public abstract class UnitDataConfigBase<TKey,TVal> : DataConfigBase<TKey,TVal>
    {
#if UNITY_EDITOR
        [Button("SetUnitId")]
        public string Test;
        public void SetUnitId()
        {
            foreach (var unit in _data)
            {
                UnitDataComposite unitDataComp = unit.Value is UnitDataComposite value ? value : default;
                UnitBase unitBase = unitDataComp.UnitBase;
                unitBase.UnitId = unit.Key.ToString();
                EditorUtility.SetDirty(unitBase);
            }
        }
#endif
    }
}
