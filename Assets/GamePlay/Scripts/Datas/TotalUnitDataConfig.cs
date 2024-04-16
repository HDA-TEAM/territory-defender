using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

[Serializable]
public struct UnitDataComposite
{
    public UnitBase UnitBase;
} 

[CreateAssetMenu(fileName = "UnitDataConfig", menuName = "ScriptableObject/Config/UnitDataConfig")]
public class TotalUnitDataConfig : ScriptableObject
{
    [SerializeField] [SerializedDictionary("UnitId.Ally","UnitDataComposite")]
    private SerializedDictionary<UnitId.Ally,UnitDataComposite> _allisData;

    
    
    public UnitDataComposite GetUnitConfigByAllyId(UnitId.Ally unitId)
    {
        _allisData.TryGetValue(unitId, out UnitDataComposite unitDataComposite);
        return unitDataComposite;
    }
}
