using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    }
    
    
    
    #endregion
}
