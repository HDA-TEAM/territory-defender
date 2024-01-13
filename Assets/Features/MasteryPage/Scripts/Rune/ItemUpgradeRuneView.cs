using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemUpgradeRuneView : MonoBehaviour
{
    [SerializeField] private Button _btnUpgradeRune;

    private Action<ItemUpgradeRuneView> _onSelected;
    public RuneComposite RuneComposite;
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
