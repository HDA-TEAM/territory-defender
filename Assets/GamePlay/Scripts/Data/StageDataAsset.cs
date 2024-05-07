using Common.Scripts.Datas;
using UnityEngine;

namespace GamePlay.Scripts.Data
{

    public struct StageDataModel : IDefaultDataModel
    {
        public bool IsEmpty()
        {
            return false;
        }
        public void SetDefault()
        {
        }
    }
    
    [CreateAssetMenu(fileName = "StageDataAsset", menuName = "ScriptableObject/Database/Stage/StageDataAsset")]
    public class StageDataAsset : ScriptableObject
    {
    }
}