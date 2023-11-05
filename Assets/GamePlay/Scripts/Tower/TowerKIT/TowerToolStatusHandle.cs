using UnityEngine;
using UnityEngine.UI;

public enum TowerTooltatus
{
    Available = 0,
    UnAvailable = 1,
    Block = 2,
}
public class TowerToolStatusHandle : MonoBehaviour
{
    [SerializeField] private Button _button; 
    [SerializeField] private Image _defaultIcon;
    [SerializeField] private Sprite _spriteBlock;
    [SerializeField] private Material _blockMaterial;

    private Sprite _sprite;
    private Material _material;
    
    #region Core
    private void Awake()
    {
        _sprite = _defaultIcon.sprite;
        _material = _button.image.material;
    }
    public void SetUp(TowerTooltatus towerToolStatus)
    {
        if (_sprite == null || _material == null)
        {
            _sprite = _defaultIcon.sprite;
            _material = _button.image.material;
        }
        // reset to default
        _defaultIcon.sprite = _sprite;
        _button.interactable = true;
        _button.image.material = _material;
        _defaultIcon.material = _material;
        
        switch (towerToolStatus)
        {
            
            case TowerTooltatus.Available:
                {
                    break;
                }
            case TowerTooltatus.UnAvailable:
                {
                    _button.interactable = false;
                    _button.image.material = _blockMaterial;
                    _defaultIcon.material = _blockMaterial;
                    break;
                }
            case TowerTooltatus.Block:
                {
                    _defaultIcon.sprite = _spriteBlock;
                    _button.interactable = false;
                    _button.image.material = _blockMaterial;
                    _defaultIcon.material = _blockMaterial;
                    break;
                }
                
        }
    }
    #endregion
}
