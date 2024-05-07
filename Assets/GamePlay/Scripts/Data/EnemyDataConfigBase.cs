using Common.Scripts;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [CreateAssetMenu(fileName = "EnemyDataConfig", menuName = "ScriptableObject/Common/Configs/EnemyDataConfig")]
    public class EnemyDataConfigBase: UnitDataConfigBase<UnitId.Enemy,UnitDataComposite>
    {
    }
}
