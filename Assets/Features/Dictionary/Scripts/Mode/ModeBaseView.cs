using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModeBaseView : MonoBehaviour
{
    [SerializeField] private Button _heroModeBtn;
    [SerializeField] private TextMeshProUGUI _txtName;
    [SerializeField] private Image _imageBg;
    [SerializeField] private Sprite _selectedSpriteBg;
    [SerializeField] private Sprite _unselectedSpriteBg;
    [SerializeField] private EHeroViewMode _modeType;
    
    private readonly string _hexSelectedColor = "#F3EF94";
    private readonly string _hexUnselectedColor = "#323232";
    
    protected Action<EHeroViewMode> _onSelectedButton;
    protected HeroComposite HeroComposite;
    
    private void Start()
    {
        _heroModeBtn.onClick.AddListener(OnClick);
    }
    private void SetSpriteButton(bool isSelected)
    {
        _imageBg.sprite = isSelected ? _selectedSpriteBg : _unselectedSpriteBg;
    }

    private void SetNameButton(bool isSelected)
    {
        string hexColor = isSelected ? _hexSelectedColor : _hexUnselectedColor;

        if (ColorUtility.TryParseHtmlString(hexColor, out Color selectedColor))
        {
            _txtName.color = selectedColor;
        }
    }
    
    public void SetSelectedState(bool isSelected)
    {
        SetSpriteButton(isSelected);
        SetNameButton(isSelected);
    }
    
    private void OnClick()
    {
        _onSelectedButton?.Invoke( _modeType);
        Debug.Log("SelectButton invoked for HeroSkinView with mode: " + _modeType);
    }
}