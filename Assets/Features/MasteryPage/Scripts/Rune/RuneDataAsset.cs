
using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum RuneId
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
    private SerializedDictionary<RuneId, RuneDataSO> _runeDataDict = new SerializedDictionary<RuneId, RuneDataSO>();
    
    public Action _onDataUpdated;

    public RuneDataSO GetRune(RuneId runeId)
    {
        if (_runeDataDict.TryGetValue(runeId, out RuneDataSO runeDataSo))
            return runeDataSo;
            
        Debug.LogError($"No rune value found for key {runeId} on ");
        return null;
    }
    public List<RuneDataSO> GetAllRuneData()
    {
        return _runeDataDict.Values.ToList();
    }

    public void RuneUpdate(RuneDataSO runeDataSo)
    {
        if (runeDataSo == null)
        {
            Debug.LogError("runeDataSo is null");
            return;
        }

        RuneId runeId = runeDataSo.GetRuneId(); // Using GetRuneId() instead of directly accessing the field.

        if (_runeDataDict.TryGetValue(runeId, out RuneDataSO existingRuneData))
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
