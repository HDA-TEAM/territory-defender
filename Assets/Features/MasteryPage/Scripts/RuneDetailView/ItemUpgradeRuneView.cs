using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemUpgradeRuneView : MonoBehaviour
{
    [SerializeField] private Button _btnUpgradeRune;
    
    public RuneComposite RuneComposite;
    private Action<ItemUpgradeRuneView> _onSelected;
    private void Awake()
    {
        _btnUpgradeRune.onClick.AddListener(OnSelectedUpgradeRune);
    }

    public void Setup(RuneComposite runeComposite, Action<ItemUpgradeRuneView> onSelected)
    {
        RuneComposite = runeComposite;
        _onSelected = onSelected;
    }
    
    private void OnSelectedUpgradeRune()
    {
        _onSelected?.Invoke(this);
    }
}
