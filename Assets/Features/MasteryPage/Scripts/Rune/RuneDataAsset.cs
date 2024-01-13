
using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum RuneId
{
    Attack = 1,
    Defense = 10,
    // Hp = 20,
    // Control = 30
}

[CreateAssetMenu(fileName = "RuneDataAsset", menuName = "ScriptableObject/DataAsset/RuneDataAsset")]
public class RuneDataAsset : ScriptableObject
{
    [FormerlySerializedAs("_runeDataDict")] [SerializedDictionary("RuneId", "RuneDataSO")] [SerializeField]
    private SerializedDictionary<RuneId, RuneSO> _masteryPageDataDict = new SerializedDictionary<RuneId, RuneSO>();

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
        // if (runeSo == null)
        // {
        //     Debug.LogError("runeDataSo is null");
        //     return;
        // }
        //
        // RuneId runeId = runeSo.GetRuneId(); // Using GetRuneId() instead of directly accessing the field.
        //
        // if (_masteryPageDataDict.TryGetValue(runeId, out RuneSO existingRuneData))
        // {
        //     //existingRuneData.SaveRuneData();
        //     Debug.Log(runeId + " rune current stacks: " + existingRuneData.GetCurrentStacks()); // Assuming GetCurrentStacks() is a method.
        // }
        // else
        // {
        //     Debug.LogError("Rune ID not found in dictionary: " + runeId);
        // }
        //
    }

}
