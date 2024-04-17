using GamePlay.Scripts.Datas;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [CreateAssetMenu(fileName = "EnemyDataConfig", menuName = "ScriptableObject/Common/Configs/EnemyDataConfig")]
    public class EnemyDataConfig: SingleUnitDataConfig<UnitId.Enemy>
    {
    }
}
