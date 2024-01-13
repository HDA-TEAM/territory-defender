
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRuneView : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Image _imageBg;
    [SerializeField] private TextMeshProUGUI _txtRuneStacks;
    
    public RuneComposite RuneComposite;
    
    // Internal
    private int _valueCastStringToInt;
    private Action<ItemRuneView> _onSelected;

    private void Awake()
    {
        _btn.onClick.AddListener(OnSelectedRuneItem);
    }
    
    public void Setup(RuneComposite runeComposite, Action<ItemRuneView> onSelected)
    {
        _onSelected = onSelected;
        RuneComposite = runeComposite;
    }

    private void OnSelectedRuneItem()
    {
        _onSelected?.Invoke(this);
    }

    public void SetRuneStacks(RuneComposite runeComposite)
    {
        _txtRuneStacks.text = "Level " + runeComposite.Level;
    }

    public void SetAvatarRune(Sprite spriteImg)
    {
        _imageBg.sprite = spriteImg;
    }
    
}
