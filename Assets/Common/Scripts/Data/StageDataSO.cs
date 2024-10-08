using GamePlay.Scripts.Data;
using UnityEngine;

namespace Common.Scripts.Data
{
    [CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObject/Data/Stage")]
    public class StageDataSO : ScriptableObject
    {
        public StageId _stageId;
    }
}
