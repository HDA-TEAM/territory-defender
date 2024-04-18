using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [CreateAssetMenu(fileName = "EnemyDataConfig", menuName = "ScriptableObject/Common/Configs/EnemyDataConfig")]
    public class EnemyDataConfigBase: DataConfigBase<UnitId.Enemy,UnitDataComposite>
    {
    }
}
