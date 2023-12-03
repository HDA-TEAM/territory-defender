using System;
using UnityEngine;
using UnityEngine.UI;
public class ItemUpgradeTowerView : MonoBehaviour
{
    [SerializeField] private Button _btnUpgradeTower;
    
    private void Start()
    {
       _btnUpgradeTower.onClick.AddListener(OnSelectUpgradeTower);
    }

    private Action<ItemUpgradeTowerView> _onSelected;
    
    public void OnSelectUpgradeTower()
    {
        _onSelected?.Invoke(this);
    }
}


