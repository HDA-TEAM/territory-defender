using System;
using TMPro;
using UnityEngine;

public class RuneDetailView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtRuneName;
    [SerializeField] private TextMeshProUGUI _txtRuneDescribe;
    [SerializeField] private TextMeshProUGUI _txtRuneStacks;
    
    #region Core
    public void Setup(RuneComposite runeComposite)
    {
        _txtRuneName.text = runeComposite.TypeName;
        _txtRuneDescribe.text = runeComposite.Operate + " (+" + runeComposite.AdditionalValue + ")";
        _txtRuneStacks.text = runeComposite.CurrentStacks + " / " + runeComposite.Stacks;
        
       // _onDataUpdated?.Invoke();
    }

    public void UpdateCurrentStackView(RuneDataSO runeDataSo)
    {
        _txtRuneStacks.text = runeDataSo._currentStacks + " / " + runeDataSo._stacks;
    }

    #endregion
}
