using UnityEngine;

namespace SO.HeroInfos
{
    [CreateAssetMenu(fileName = "Hero", menuName = "Hero", order = 2)]
    public class HeroSO : ScriptableObject
    {
        public string _heroName;
        public int _heroLevel;
        //public Skill heroSkill;
        public Sprite _heroImage;
    }
}