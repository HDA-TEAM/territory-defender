using UnityEngine;

namespace SO.HeroInfos
{
    [CreateAssetMenu(fileName = "HeroList", menuName = "HeroInfo/ListHeroInfo", order = 1)]
    public class ListHeroInfoSO : ScriptableObject
    {
        public TrungTracHeroInfoSO _trungTracHeroInfo;
        public TrungNhiHeroInfoSO _trungNhiHeroInfo;
    }
}