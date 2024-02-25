using System;
using TMPro;
using UnityEngine;

public class RuneDetailView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtRuneName;
    [SerializeField] private TextMeshProUGUI _txtRuneDescribe;
    [SerializeField] private TextMeshProUGUI _txtRuneStacks;

    private int _additionAttribute;
    
    #region Core
    public void Setup(RuneComposite runeComposite)
    {
        _txtRuneName.text = runeComposite.Name;

        _additionAttribute = runeComposite.Level * 10; 
        _txtRuneDescribe.text = runeComposite.RuneId + " (+" + _additionAttribute + ")";

        _txtRuneStacks.text = "Level " + runeComposite.Level;
    }

    public void UpdateCurrentRuneData(RuneComposite runeComposite)
    {
        _additionAttribute = runeComposite.Level * 10; 
        _txtRuneDescribe.text = runeComposite.RuneId + " (+" + _additionAttribute + ")";

        _txtRuneStacks.text = "Level " + runeComposite.Level;
    }

    #endregion
}
