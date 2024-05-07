using Common.Scripts.Datas;
using CustomInspector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace GamePlay.Scripts.Data
{
    public enum StageId
    {
        Chap1Stage0 = 100,
        Chap1Stage1 = 101,
        Chap1Stage2 = 102,
    }
    
    [Serializable,Preserve]
    public struct StageConfig
    {
        public StageId StageId;
        public int StartCoin;
        public int MaxHealth;
    }
    [CreateAssetMenu(fileName = "StageDataConfig", menuName = "ScriptableObject/Configs/StageDataConfig")]
    public class StageDataConfig : DataConfigBase<StageId,StageConfig>
    {
        #if UNITY_EDITOR
        [Button("ParseToJson")]
        [Button("ReadJsonData")]
        [SerializeField] private string _dataString;
        public void ParseToJson()
        {
            _dataString = JsonConvert.SerializeObject(_data.Values);
            Debug.Log("ParseToJson " + _data);
        }
        public void ReadJsonData()
        {
            List<StageConfig> stageParseData = JsonConvert.DeserializeObject<List<StageConfig>>(_dataString);
            _data.Clear();
            foreach (var stage in stageParseData)
            {
                _data.Add(stage.StageId,stage);
            }
        }
#endif
    }
}
