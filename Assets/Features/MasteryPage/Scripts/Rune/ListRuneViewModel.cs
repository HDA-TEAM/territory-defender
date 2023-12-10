
using System;
using System.Collections.Generic;
using UnityEngine;

public struct RuneComposite
{
    public string TypeName;
    public string AdditionalValue;
    public string Operate;
    public string CurrentStacks;
    public string Stacks;
    public Sprite AvatarSelected;
    public Sprite AvatarStarted;
}
public class ListRuneViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemRuneView> _itemRuneViews;

    [Header("Data"), Space(12)] 
    [SerializeField] private RuneDataAsset _runeDataAsset;
    
    
    private List<RuneComposite> _runeComposites;
    private ItemRuneView _preSelectedItem;
    private int _memeValue;
    private void Awake()
    {
        _runeComposites = new List<RuneComposite>();

        UpdateData();
    }

    private void UpdateData()
    {
        List<RuneDataSO> listRuneDataSo = _runeDataAsset.GetAllRuneData();

        foreach (var runeDataSo in listRuneDataSo)
        {
            _runeComposites.Add(
                new RuneComposite
                {
                    TypeName = runeDataSo._name,
                    AdditionalValue =  runeDataSo._additionalValue,
                    Operate = runeDataSo._operate,
                    CurrentStacks = runeDataSo._currentStacks,
                    Stacks = runeDataSo._stacks,
                    AvatarSelected = runeDataSo._avatarSelected,
                    AvatarStarted = runeDataSo._avatarStarted
                }
            );
        }
        UpdateView();
    }

    private void UpdateView()
    {
        
        for (int i = 0; i < _itemRuneViews.Count; i++)
        {
            int.TryParse(_runeComposites[i].CurrentStacks, out _memeValue);
            if (_memeValue > 0)
            {
                _itemRuneViews[i].SetAvatarRune(_runeComposites[i].AvatarSelected);
                _itemRuneViews[i].Setup(_runeComposites[i], OnSelectedItem);
            } else {
                _itemRuneViews[i].SetAvatarRune(_runeComposites[i].AvatarStarted);
            }
        }
    }

    private void OnSelectedItem(ItemRuneView itemRuneView)
    {
        if (_preSelectedItem != null)
        {
            
        }
    }
}
