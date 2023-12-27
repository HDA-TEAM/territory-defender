
using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "CommonTowerMasteryPageDataAsset", menuName = "ScriptableObject/DataAsset/CommonTowerMasteryPageDataAsset")]
public class CommonTowerMasteryPageDataAsset : ScriptableObject
{
    [SerializedDictionary("TowerId", "RuneData")] [SerializeField]
    private SerializedDictionary<TowerId, MasteryPageDataAsset> 
        _towerMasteryPageDict = new SerializedDictionary<TowerId, MasteryPageDataAsset>();
    
    public Action _onDataUpdatedAction;
    private TowerId _towerId;

    public TowerId GetTowerId(MasteryPageDataAsset masteryPageDataAsset)
    {
        foreach (var kvp in _towerMasteryPageDict)
        {
            if (kvp.Value == masteryPageDataAsset)
            {
                _towerId = kvp.Key;
                return _towerId;
            }
        }
        return _towerId;
    }
    #region MasteryPage access

    public List<MasteryPageDataAsset> GetAllRuneData()
    {
        return _towerMasteryPageDict.Values.ToList();
    }

    public MasteryPageDataAsset GetMasteryPageDataAsset(TowerId towerId)
    {
        _towerMasteryPageDict.TryGetValue(towerId, out MasteryPageDataAsset towerMasteryPageDataAsset);
        return towerMasteryPageDataAsset;
    }

    public void UpdateMasteryPage(MasteryPageDataAsset masteryPageDataAsset, RuneDataSO runeDataSo)
    {
        foreach (var kvp in _towerMasteryPageDict)
        {
            if (kvp.Value == masteryPageDataAsset)
            {
                _towerMasteryPageDict[kvp.Key].RuneUpdate(runeDataSo);
                #if UNITY_EDITOR
                    // Mark the ScriptableObject as dirty and save the changes
                    EditorUtility.SetDirty(this);
                    AssetDatabase.SaveAssets();
                    Debug.Log("Save asset dict......");
                #endif
            }
        }
        _onDataUpdatedAction?.Invoke();
    }

    
    #endregion
}

