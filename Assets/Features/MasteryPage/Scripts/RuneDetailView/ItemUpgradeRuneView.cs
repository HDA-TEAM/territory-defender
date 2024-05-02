using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemUpgradeRuneView : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private TextMeshProUGUI _txtName;
    
    public RuneComposite RuneComposite;
    private Action<ItemUpgradeRuneView> _onSelected;
    private void Awake()
    {
        _btn.onClick.AddListener(OnSelectedUpgradeRune);
    }

    public void Setup(RuneComposite runeComposite, Action<ItemUpgradeRuneView> onSelected)
    {
        RuneComposite = runeComposite;
        _onSelected = onSelected;

        string str = runeComposite.Level == 0 ? "Unlock" : "Upgrade";
        
        SetName(str.ToUpper());
    }
    
    private void OnSelectedUpgradeRune()
    {
        _onSelected?.Invoke(this);
    }
    
    private void SetName(string btnName)
    {
        _txtName.text = btnName;
    }
}
