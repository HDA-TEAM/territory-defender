
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
    private Sprite _sprite;
    private Action<ItemRuneView> _onSelected;

    private void Awake()
    {
        _sprite = _imageBg.sprite;
        _btn.onClick.AddListener(OnSelectedRuneItem);
    }
    
    public void Setup(RuneComposite runeComposite, Action<ItemRuneView> onSelected)
    {
        Debug.Log("Base stack: " + _valueCastStringToInt);
        _onSelected = onSelected;
        RuneComposite = runeComposite;
    }

    private void OnSelectedRuneItem()
    {
        Debug.Log("select rune...");
        _onSelected?.Invoke(this);
    }

    public void SetRuneStacks(RuneComposite runeComposite)
    {
        _txtRuneStacks.text = runeComposite.CurrentStacks + " / " + runeComposite.Stacks;
    }

    public void SetAvatarRune(Sprite spriteImg)
    {
        _imageBg.sprite = spriteImg;
    }
    
}
