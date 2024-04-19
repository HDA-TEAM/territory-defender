using Common.Scripts;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [CreateAssetMenu(fileName = "AllyTroopsDataConfig", menuName = "ScriptableObject/Common/Configs/AllyTroopsDataConfig")]
    public class AllyTroopsDataConfigBase: DataConfigBase<UnitId.Ally,UnitDataComposite>
    {
    }
}
