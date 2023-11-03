using UnityEngine;

namespace SO.HeroInfos
{
    [CreateAssetMenu(fileName = "TrungTracHeroInfo", menuName = "HeroInfo/TrungTracHeroInfo", order = 2)]
    public class TrungTracHeroInfoSO : ScriptableObject
    {
        public string _heroName;
        public int _heroLevel;
        //public Skill heroSkill;
        public Sprite _heroImage;
    }
}