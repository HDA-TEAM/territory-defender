using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemHeroChooseView : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Image _imgHero;
    private Action<ItemHeroChooseView> _onSelected;

    public HeroComposite HeroComposite;
    private void Awake()
    {
        _btn.onClick.AddListener(OnSelectedHeroChoose);
    }
    
    public void Setup(HeroComposite heroComposite,Action<ItemHeroChooseView> onAction)
    {
        _onSelected = onAction;
        HeroComposite = heroComposite;
        _imgHero.sprite = heroComposite.HeroOwned;
    }

    private void OnSelectedHeroChoose()
    {
        _imgHero.sprite = HeroComposite.HeroChoose;
        _onSelected?.Invoke(this);
    }

    public void RemoveSelected()
    {
        _imgHero.sprite = HeroComposite.HeroOwned;
    }
    
}

