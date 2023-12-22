
using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

public enum RuneId
{
    Attack = 1,
    Defense = 10,
    Hp = 20,
    Control = 30
}

[CreateAssetMenu(fileName = "RuneDataAsset", menuName = "ScriptableObject/DataAsset/RuneDataAsset")]
public class MasteryPageDataAsset : ScriptableObject
{
    [FormerlySerializedAs("_runeDataDict")] [SerializedDictionary("RuneId", "RuneDataSO")] [SerializeField]
    private SerializedDictionary<RuneId, RuneDataSO> _masteryPageDataDict = new SerializedDictionary<RuneId, RuneDataSO>();
    
    public Action _onDataUpdated;

    public RuneDataSO GetRune(RuneId runeId)
    {
        if (_masteryPageDataDict.TryGetValue(runeId, out RuneDataSO runeDataSo))
            return runeDataSo;
            
        Debug.LogError($"No rune value found for key {runeId} on ");
        return null;
    }
    public List<RuneDataSO> GetAllRuneData()
    {
        return _masteryPageDataDict.Values.ToList();
    }

    public void RuneUpdate(RuneDataSO runeDataSo)
    {
        if (runeDataSo == null)
        {
            Debug.LogError("runeDataSo is null");
            return;
        }

        RuneId runeId = runeDataSo.GetRuneId(); // Using GetRuneId() instead of directly accessing the field.

        if (_masteryPageDataDict.TryGetValue(runeId, out RuneDataSO existingRuneData))
        {
            Debug.Log(runeId + " rune current stacks: " + existingRuneData.GetCurrentStacks()); // Assuming GetCurrentStacks() is a method.
        }
        else
        {
            Debug.LogError("Rune ID not found in dictionary: " + runeId);
        }
        _onDataUpdated?.Invoke();
    }

}
