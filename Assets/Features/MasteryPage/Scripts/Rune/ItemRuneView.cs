
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRuneView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtName;
    [SerializeField] private Button _btn;
    [SerializeField] private Image _imageBg;
    //[SerializeField] private Sprite _spriteSelectedBg;
    
    public RuneComposite RuneComposite;
    // Internal
    private Sprite _sprite;
    private Action<ItemRuneView> _onSelected;

    private void Awake()
    {
        _sprite = _imageBg.sprite;
        _btn.onClick.AddListener(OnSelectedRune);
    }
    
    public void Setup(RuneComposite runeComposite,Action<ItemRuneView> onSelected)
    {
        RuneComposite = runeComposite;
        _onSelected = onSelected;
        
    }

    public void OnSelectedRune()
    {
        Debug.Log("select rune...");
        _onSelected?.Invoke(this);
    }

    public void SetAvatarRune(Sprite spriteImg)
    {
        _imageBg.sprite = spriteImg;
    }
    
}
