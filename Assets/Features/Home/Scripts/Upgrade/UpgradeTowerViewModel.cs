using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTowerViewModel : MonoBehaviour
{
    [SerializeField] private GameObject _objUpgradeTower;

    private ItemUpgradeTowerView _preUpgradeTowerItem;
    private void Awake()
    {
        throw new NotImplementedException();
    }

    private void OnUpgradeTowerSelected(ItemUpgradeTowerView itemUpgradeTowerView)
    {
        _preUpgradeTowerItem = itemUpgradeTowerView;
        //_objUpgradeTower.gameObject.SetActive();
    }
}

