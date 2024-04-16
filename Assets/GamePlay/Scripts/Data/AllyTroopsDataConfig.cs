using GamePlay.Scripts.Datas;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [CreateAssetMenu(fileName = "AllyTroopsDataConfig", menuName = "ScriptableObject/Common/Configs/AllyTroopsDataConfig")]
    public class AllyTroopsDataConfig: SingleUnitDataConfig<UnitId.Ally>
    {
    }
}
