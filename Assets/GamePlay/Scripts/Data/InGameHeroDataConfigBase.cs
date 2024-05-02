using Common.Scripts;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [CreateAssetMenu(fileName = "InGameHeroDataConfig", menuName = "ScriptableObject/Common/Configs/InGameHeroDataConfig")]
    public class InGameHeroDataConfigBase : UnitDataConfigBase<UnitId.Hero,UnitDataComposite>
    {
        
    }
}
