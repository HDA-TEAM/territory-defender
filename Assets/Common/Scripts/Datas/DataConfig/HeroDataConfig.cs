using Common.Scripts;
using Common.Scripts.Datas;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroDataConfig", menuName = "ScriptableObject/Config/HeroDataConfig")]
public class HeroDataConfig : DataConfigBase<UnitId.Hero,HeroDataConfig>
{
    
// #if UNITY_EDITOR
//     [Button("ParseToJson")]
//     [Button("ReadJsonData")]
//     [SerializeField] private string _dataString;
//     public void ParseToJson()
//     {
//         _dataString = JsonConvert.SerializeObject(_data.Values);
//         Debug.Log("ParseToJson " + _data);
//     }
//     //TODO
//     // public void ReadJsonData()
//     // {
//     //     List<StageConfig> stageParseData = JsonConvert.DeserializeObject<List<StageConfig>>(_dataString);
//     //     _data.Clear();
//     //     foreach (var stage in stageParseData)
//     //     {
//     //         _data.Add(stage.StageId,stage);
//     //     }
//     // }
// #endif
}
