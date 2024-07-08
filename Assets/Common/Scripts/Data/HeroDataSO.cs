using Common.Scripts.Data.DataConfig;
using GamePlay.Scripts.Character.Stats;
using UnityEngine;

namespace Common.Scripts.Data
{
    [CreateAssetMenu(fileName = "HeroSO", menuName = "ScriptableObject/Data/Hero")]
    public class HeroDataSO : ScriptableObject
    {
        public UnitId.Hero _heroId;
        public Sprite _imageHero;
        public Sprite _imageHeroChoose;
        public Sprite _imageHeroOwned;
    
        public ESkillId ActiveSkillId;
        public ESkillId PassiveSkillId;
        public Stats _stats;
    }
}
