using DG.Tweening;
using System;
using Common.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Menu
{
    [Serializable]
    public struct HeroItemViewComposite
    {
        public Sprite IconHeroSelected;
        public Sprite IconHeroUnSelected;
        public Sprite SpriteHeroCooldown;
    }

    public class HeroItemView : MonoBehaviour
    {
        [SerializeField] private Image _imgHeroSelected;
        [SerializeField] private Image _imgHeroAvatarCooldown;
        [SerializeField] private Button _btnSelectHero;
        [SerializeField] private GameObject _gameObjectSelected;
        [SerializeField] private Image _imgHeroUnSelected;
        [SerializeField] private Image _imgHeroAvatar;

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
            _imgHeroSelected.sprite = heroItemViewComposite.IconHeroSelected;
            _imgHeroUnSelected.sprite = heroItemViewComposite.IconHeroUnSelected;
            _imgHeroAvatar.sprite = heroItemViewComposite.SpriteHeroCooldown;
            
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
