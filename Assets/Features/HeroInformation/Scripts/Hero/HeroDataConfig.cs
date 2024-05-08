using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using Common.Scripts;
using CustomInspector;
using GamePlay.Scripts.Data;
using Newtonsoft.Json;
using UnityEngine;

// public enum EHeroId
// {
//     TrungTrac = 1,
//     TrungNhi = 2,
//     ToDinh = 3,
//     NgoQuyen = 4,
//     QuangTrung = 5,
//     MaVien = 6,
//     TrungTrac2 = 7
// }

// Du lieu tho
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
