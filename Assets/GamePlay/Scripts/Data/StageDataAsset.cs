using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    public enum StageId
    {
        Chap1Stage0 = 100,
        Chap1Stage1 = 101,
        Chap1Stage2 = 102,
    }
    
    [CreateAssetMenu(fileName = "StageDataAsset", menuName = "ScriptableObject/Database/Stage/StageDataAsset")]
    public class StageDataAsset : ScriptableObject
    {
    }
}