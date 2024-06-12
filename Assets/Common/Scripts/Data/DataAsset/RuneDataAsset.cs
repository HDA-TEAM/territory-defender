using System;
using AYellowpaper.SerializedCollections;
using Common.Scripts.Data.DataConfig;
using System.Collections.Generic;
using System.Linq;
using Common.Scripts.Data.DataAsset;
using UnityEngine;

namespace Common.Scripts.Data.DataAsset
{
    public enum RuneId
    {
        Red1 = 1,
        Red2 = 2,
        Red3 = 3,
    
        Yellow1 = 10,
        Yellow2 = 11,
        Yellow3 = 12,
    
        Green1 = 20,
        Green2 = 21,
        Green3 = 22
    }

    [CreateAssetMenu(fileName = "RuneDataAsset", menuName = "ScriptableObject/DataAsset/RuneDataAsset")]
    public class RuneDataAsset : LocalDataAsset<RuneDataModel>
    {
        [SerializedDictionary("RuneId", "RuneDataSO")] 
        public SerializedDictionary<RuneId, RuneDataSo> _masteryPageDataDict = new SerializedDictionary<RuneId, RuneDataSo>();

        public RuneDataSo GetRune(RuneId runeId)
        {
            if (_masteryPageDataDict.TryGetValue(runeId, out RuneDataSo runeDataSo))
                return runeDataSo;
            
            Debug.LogError($"No rune value found for key {runeId} on ");
            return null;
        }
        public List<RuneDataSo> GetAllRuneData()
        {
            return _masteryPageDataDict.Values.ToList();
        }
        
        public List<RuneData> GetAllRuneDataAsRuneData()
        {
            return _masteryPageDataDict.Values.Select(runeDataSo => new RuneData
            {
                RuneId = runeDataSo.GetRuneId(),
                Level = 0 // Assuming you want to set default level to 0
            }).ToList();
        }

        public void RuneUpdate(RuneDataSo runeDataSo)
        {
        }

    }
}

[Serializable]
public struct RuneDataModel : IDefaultDataModel
{
    public List<RuneData> ListRuneDatas;

    public bool IsEmpty()
    {
        return (ListRuneDatas == null || ListRuneDatas.Count == 0);
    }

    public void SetDefault()
    {
        
    }
}

[Serializable]
public struct RuneData
{
    public RuneId RuneId;
    public int Level;

    public RuneData(RuneId runeId, int level)
    {
        RuneId = runeId;
        Level = level;
    }
}
