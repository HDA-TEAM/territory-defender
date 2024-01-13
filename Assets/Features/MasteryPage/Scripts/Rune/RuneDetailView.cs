using System;
using TMPro;
using UnityEngine;

public class RuneDetailView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtRuneName;
    [SerializeField] private TextMeshProUGUI _txtRuneDescribe;
    [SerializeField] private TextMeshProUGUI _txtRuneStacks;

    public Action _onDataUpdated;
    #region Core
    public void Setup(RuneComposite runeComposite)
    {
        _txtRuneName.text = runeComposite.TypeName;
        _txtRuneDescribe.text = runeComposite.Operate + " (+" + runeComposite.AdditionalValue + ")";
        _txtRuneStacks.text = runeComposite.CurrentStacks + " / " + runeComposite.Stacks;
        
       // _onDataUpdated?.Invoke();
    }
    #endregion
}
