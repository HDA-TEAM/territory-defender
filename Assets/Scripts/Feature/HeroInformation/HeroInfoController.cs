using SO.HeroInfos;
using UnityEngine;

namespace Feature.HeroInformation
{
    public class HeroInfoController : MonoBehaviour
    {
        public TrungTracHeroInfoSO _trungTracHeroInfo;

        public void SetHeroInfo(TrungTracHeroInfoSO info)
        {
            _trungTracHeroInfo = info;
        }
    }
}