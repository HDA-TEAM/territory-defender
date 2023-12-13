
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

        if (_runeDataDict.TryGetValue(runeDataSo._runeId, out RuneDataSO existingRuneData))
        {
            Debug.Log("Cur stacks: " + existingRuneData._currentStacks);
        } else {
            Debug.LogError("Rune ID not found in dictionary: " + runeDataSo._runeId);
        }
    }
}
