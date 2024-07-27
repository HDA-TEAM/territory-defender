using Common.Scripts;
using Common.Scripts.Data.DataConfig;
using UnityEngine;

namespace GamePlay.Scripts.Menu.InGameStageScreen
{
    [CreateAssetMenu(fileName = "InGameHeroImageConfig", menuName = "ScriptableObject/Common/Configs/InGameHeroImageConfig")]
    public class InGameHeroImageConfig : DataConfigBase<UnitId.Hero, HeroItemViewComposite>
    {

    }
}
