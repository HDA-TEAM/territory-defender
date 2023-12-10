
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum ERuneId
{
    Attack = 1,
    Defense = 10,
    Hp = 20,
    Control = 30

}
[CreateAssetMenu(fileName = "RuneDataAsset", menuName = "ScriptableObject/DataAsset/RuneDataAsset")]
public class RuneDataAsset : ScriptableObject
{
    [SerializedDictionary("RuneId", "RuneDataSO")] [SerializeField]
    private SerializedDictionary<ERuneId, RuneDataSO> _runeDataDict = new SerializedDictionary<ERuneId, RuneDataSO>();
    
    public RuneDataSO GetRuneDataWithId(ERuneId runeId)
    {
        _runeDataDict.TryGetValue(runeId, out RuneDataSO runeDataSo);
        return runeDataSo;
    }
    public List<RuneDataSO> GetAllRuneData()
    {
        return _runeDataDict.Values.ToList();
    }
    
}
