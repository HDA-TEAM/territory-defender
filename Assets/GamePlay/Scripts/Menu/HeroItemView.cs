using DG.Tweening;
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
        [SerializeField] private Image _imgHeroAvatarCooldown;
        [SerializeField] private Button _btnSelectHero;
        [SerializeField] private GameObject _gameObjectSelected;

        private HeroItemViewComposite _heroItemViewComposite;
        private Action _onSelectHero;

        private void Awake()
        {
            _btnSelectHero.onClick.AddListener(OnSelectHero);
        }
        private void OnSelectHero() => _onSelectHero?.Invoke();
        public void Setup(HeroItemViewComposite heroItemViewComposite, Action onSelecting)
        {
            SetHeroSelected(false);
            //Set up avatar
            _heroItemViewComposite = heroItemViewComposite;
            _onSelectHero = onSelecting;
        }
        public void SetCooldownProcessing(float cooldownRevive, Action endOfCooldown)
        {
            _imgHeroAvatarCooldown.fillAmount = 1f;
            _imgHeroAvatarCooldown.raycastTarget = false;
            _imgHeroAvatarCooldown.DOFillAmount(0f, cooldownRevive).OnComplete(
                () =>
                {
                    endOfCooldown?.Invoke();
                    _imgHeroAvatarCooldown.raycastTarget = true;
                });
        }
        public void SetHeroSelected(bool isSelected)
        {
            _gameObjectSelected.SetActive(isSelected);
        }
    }
}
