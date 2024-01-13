using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTowerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtName;
    [SerializeField] private Button _btn;
    [SerializeField] private Image _imageBg;
    [SerializeField] private Sprite _spriteSelectedBg;
    
    public TowerComposite TowerComposite;
    
    // Internal
    private Sprite _sprite;
    private Action<ItemTowerView> _onSelected;
    private readonly string _hexSelectedColor = "#F3EF94";
    private readonly string _hexDeselectedColor = "#323232";
    
    private void Awake()
    {
        _sprite = _imageBg.sprite;
        _btn.onClick.AddListener(OnSelectedTower);
    }
    
    public void Setup(TowerComposite towerComposite,Action<ItemTowerView> onSelected)
    {
        TowerComposite = towerComposite;
        
        _onSelected = onSelected;

        SetName(towerComposite.TowerId.ToString());
    }
    
    public void OnSelectedTower()
    {
        _imageBg.sprite = _spriteSelectedBg;
        if (ColorUtility.TryParseHtmlString(_hexSelectedColor, out Color selectedColor))
        {
            _txtName.color = selectedColor; // Set to the color represented by the hex string
        }
        
        //Debug.Log("select tower... " + TowerComposite.Name);
        _onSelected?.Invoke(this);
    }

    public void RemoveSelected()
    {
        _imageBg.sprite = _sprite;
        if (ColorUtility.TryParseHtmlString(_hexDeselectedColor, out Color unSelectedColor))
        {
            _txtName.color = unSelectedColor;
        }
        //Debug.Log("remove select... " + TowerComposite.Name);
    }

    private void SetName(string name)
    {
        _txtName.text = name;
    }
}
