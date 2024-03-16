using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Menu
{
    public struct HeroItemViewComposite
    {
        public EHeroId HeroId;
    }

    public class HeroItemView : MonoBehaviour
    {
        [SerializeField] private Image _iconHero;
        [SerializeField] private Image _imgCooldownRevival;
        [SerializeField] private Button _btnHeroSelecting;
        
        private HeroItemViewComposite _heroItemViewComposite;
        private Action _onSelectHero;
        public void Setup(HeroItemViewComposite heroItemViewComposite,Action onSelecting)
        {
            _heroItemViewComposite = heroItemViewComposite;
        }
    }
}