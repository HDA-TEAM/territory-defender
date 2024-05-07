using Common.Scripts;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [CreateAssetMenu(fileName = "AllyTroopsDataConfig", menuName = "ScriptableObject/Common/Configs/AllyTroopsDataConfig")]
    public class AllyTroopsDataConfigBase: UnitDataConfigBase<UnitId.Ally,UnitDataComposite>
    {
    }
}
