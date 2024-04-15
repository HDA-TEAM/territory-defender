using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum RuneId
{
    Red1 = 1,
    Red2 = 2,
    Red3 = 3,
    
    Yellow1 = 10,
    Yellow2 = 11,
    Yellow3 = 12,
    
    Purple1 = 20,
    Purple2 = 21,
    Purple3 = 22
}

[CreateAssetMenu(fileName = "RuneDataAsset", menuName = "ScriptableObject/DataAsset/RuneDataAsset")]
public class RuneDataAsset : ScriptableObject
{
   [SerializedDictionary("RuneId", "RuneDataSO")] 
   [SerializeField] private SerializedDictionary<RuneId, RuneSO> _masteryPageDataDict = new SerializedDictionary<RuneId, RuneSO>();

    public RuneSO GetRune(RuneId runeId)
    {
        if (_masteryPageDataDict.TryGetValue(runeId, out RuneSO runeDataSo))
            return runeDataSo;
            
        Debug.LogError($"No rune value found for key {runeId} on ");
        return null;
    }
    public List<RuneSO> GetAllRuneData()
    {
        return _masteryPageDataDict.Values.ToList();
    }

    public void RuneUpdate(RuneSO runeSo)
    {
    }

}
