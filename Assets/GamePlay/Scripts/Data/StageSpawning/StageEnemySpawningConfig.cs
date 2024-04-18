using CustomInspector;
using GamePlay.Scripts.Datas.StageSpawning;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace GamePlay.Scripts.Data.StageSpawning
{
    [CreateAssetMenu(fileName = "StageEnemySpawningConfig", menuName = "ScriptableObject/Database/Stage/SpawningConfigData")]
    [Serializable, Preserve]
    public class StageEnemySpawningConfig : ScriptableObject
    {
        [SerializeField] private List<SingleStageSpawningConfig> _stageEnemySpawningConfigs;
        public SingleStageSpawningConfig FindSpawningConfig(StageId stageId) => _stageEnemySpawningConfigs.Find(stage => stage.StageId == stageId);

        public int GetNumberOfUnitSpawningWithStageId(StageId stageId) => FindSpawningConfig(stageId).GetTotalUnitsSpawning();

#if UNITY_EDITOR
        [Button("ParseToJson")]
        [Button("ReadJsonData")]
        [SerializeField] private string _data;
        public void ParseToJson()
        {
            List<StageParseData> parseData = StageParseDataAdapter.ParseDataToJson(_stageEnemySpawningConfigs);
        
            _data = JsonConvert.SerializeObject(parseData);
        
            Debug.Log("ParseToJson " + _data);
        }
        public void ReadJsonData()
        {
            List<StageParseData> stageParseData = JsonConvert.DeserializeObject<List<StageParseData>>(_data);
            _stageEnemySpawningConfigs = StageParseDataAdapter.ParseJsonToData(stageParseData);
        }
#endif
    }
}