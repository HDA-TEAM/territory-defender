using SO.HeroInfos;
using UnityEngine;

namespace Feature.HeroInformation
{
    public class HeroInfoController : MonoBehaviour
    {
        public HeroSO _heroInfo;

        public void SetHeroInfo(HeroSO info)
        {
            _heroInfo = info;
        }
    }
}