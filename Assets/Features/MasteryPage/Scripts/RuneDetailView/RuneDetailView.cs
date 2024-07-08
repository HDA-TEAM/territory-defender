using System.Collections.Generic;
using Features.MasteryPage.Scripts.Rune;
using TMPro;
using UnityEngine;

public class RuneDetailView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtRuneName;
    [SerializeField] private TextMeshProUGUI _txtRuneDescribe;
    [SerializeField] private TextMeshProUGUI _txtRuneStacks;

    [SerializeField] private RuneRunTimeController _runeRunTimeController;
    private int _additionAttribute;
    
    #region Core
    public void Setup(RuneComposite runeComposite)
    {
        List<int> effects = _runeRunTimeController.GetRuneEffect(runeComposite);
        
        // Post by RuneComposite
        _txtRuneName.text = runeComposite.Name;
        _txtRuneStacks.text = "Level " + runeComposite.Level;
        
        // Post by RuneController
        float level = Mathf.Lerp(0, 2, runeComposite.Level);
        _txtRuneDescribe.text = string.Format(runeComposite.Description, runeComposite.PowerUnits[(int)level]);

    }

    public void UpdateCurrentRuneData(RuneComposite runeComposite)
    {
        List<int> effects = _runeRunTimeController.GetRuneEffect(runeComposite);
        
        // Post by RuneComposite
        _txtRuneName.text = runeComposite.Name;
        _txtRuneStacks.text = "Level " + runeComposite.Level;
        
        // Post by RuneController
        for (int i = 0; i < runeComposite.Effects.Count; i++)
        {
            _txtRuneDescribe.text = runeComposite.Effects[i] + " (+" + effects[i] * runeComposite.Level + "%)";
        }
    }

    #endregion
}
