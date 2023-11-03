using UnityEngine;

namespace SO.HeroInfos
{
    [CreateAssetMenu(fileName = "TrungNhiHeroInfo", menuName = "HeroInfo/TrungNhiHeroInfo", order = 3)]
    public class TrungNhiHeroInfoSO : ScriptableObject
    {
        public string _heroName;
        public int _heroLevel;
        //public Skill heroSkill;
        public Sprite _heroImage;
    }
}