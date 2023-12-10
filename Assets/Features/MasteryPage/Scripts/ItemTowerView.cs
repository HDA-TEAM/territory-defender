using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTowerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtName;
    [SerializeField] private Button _btn;
    //[SerializeField] private Image _imageBg;
    //[SerializeField] private Sprite _spriteSelectedBg;

    public TowerComposite TowerComposite;
    // Internal
    private Sprite _sprite;
    private Action<ItemTowerView> _onSelected;
    
    private readonly string _hexSelectedColor = "#F3EF94";
    private readonly string _hexDeselectedColor = "#323232";
    private void Awake()
    {
        _btn.onClick.AddListener(OnSelectedTower);
    }
    
    public void Setup(TowerComposite towerComposite,Action<ItemTowerView> onSelected)
    {
        TowerComposite = towerComposite;
        
        _onSelected = onSelected;

        SetName(towerComposite.Name);
    }
    
    public void OnSelectedTower()
    {
        Debug.Log("select tower...");
        _onSelected?.Invoke(this);
    }

    public void RemoveSelected()
    {
        Debug.Log("remove select...");
    }

    private void SetName(string name)
    {
        _txtName.text = name;
    }
}
