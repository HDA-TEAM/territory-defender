using SO.HeroInfos;
using UnityEngine;

namespace Feature.HeroInformation
{
    public class HeroInfoListInitializer : MonoBehaviour
    {
        public ListHeroInfoSO _heroList;

        public HeroInfoController _trungTracHeroController;
        public HeroInfoController _trungNhiHeroController;

        void Start()
        {
            if (_heroList._trungTracHeroInfo != null)
            {
                _trungTracHeroController.SetHeroInfo(_heroList._trungTracHeroInfo);
            }
            else
            {
                Debug.LogWarning("Trung Trac Hero info is missing.");
            }

            if (_heroList._trungTracHeroInfo != null)
            {
                _trungNhiHeroController.SetHeroInfo(_heroList._trungTracHeroInfo);
            }
            else
            {
                Debug.LogWarning("Trung Nhi Hero info is missing.");
            }
        }
    }
}