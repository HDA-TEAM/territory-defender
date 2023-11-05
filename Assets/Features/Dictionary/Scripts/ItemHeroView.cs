using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemHeroView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtName;
    [SerializeField] private Button _btn;
    [SerializeField] private Image _imageBg;
    [SerializeField] private Sprite _spriteSelectedBg;

    public HeroComposite HeroComposite;
    
    private Sprite _sprite;
    private Action<ItemHeroView> _onSelected;
    #region Core
    private void Awake()
    {
        _sprite = _imageBg.sprite;
        _btn.onClick.AddListener(OnSelectedHero);
    }
    public void Setup(HeroComposite heroComposite,Action<ItemHeroView> onSelected)
    {
        HeroComposite = heroComposite;
        
        _onSelected = onSelected;
        
        SetName(heroComposite.Name);
    }
    #endregion
    private void OnSelectedHero()
    {
        _imageBg.sprite = _spriteSelectedBg;
        _onSelected?.Invoke(this);
    }
    public void RemoveSelected()
    {
        _imageBg.sprite = _sprite;
    }
    private void SetName(string name)
    {
        _txtName.text = name;
    }
    
}
