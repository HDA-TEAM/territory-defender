using AYellowpaper.SerializedCollections;
using Common.Scripts.Data.DataConfig;
using System.Collections.Generic;
using System.Linq;
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
    public class RuneDataAsset : ScriptableObject
    {
        [SerializedDictionary("RuneId", "RuneDataSO")] 
        [SerializeField] private SerializedDictionary<RuneId, RuneDataSo> _masteryPageDataDict = new SerializedDictionary<RuneId, RuneDataSo>();

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

        public void RuneUpdate(RuneDataSo runeDataSo)
        {
        }

    }
}