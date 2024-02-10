using System;
using System.Collections;
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
    
    // Internal
    private Sprite _sprite;
    private Action<ItemHeroView> _onSelected;
    
    private readonly string _hexSelectedColor = "#F3EF94";
    private readonly string _hexDeselectedColor = "#383838";
    
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
    public void OnSelectedHero()
    {
        _imageBg.sprite = _spriteSelectedBg;
        if (ColorUtility.TryParseHtmlString(_hexSelectedColor, out Color selectedColor))
        {
            _txtName.color = selectedColor; // Set to the color represented by the hex string
        }
        _onSelected?.Invoke(this);
    }
    public void RemoveSelected()
    {
        _imageBg.sprite = _sprite;
        if (ColorUtility.TryParseHtmlString(_hexDeselectedColor, out Color unSelectedColor))
        {
            _txtName.color = unSelectedColor;
        }
    }
    private void SetName(string name)
    {
        _txtName.text = name;
    }

}
