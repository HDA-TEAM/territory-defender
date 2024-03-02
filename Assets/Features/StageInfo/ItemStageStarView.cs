
using UnityEngine;
using UnityEngine.UI;

public class ItemStageStarView : MonoBehaviour
{
    [SerializeField] private Image _imgStar;
    [SerializeField] private Sprite _spriteYellowStar;
    [SerializeField] private Sprite _spriteGrownStar;
    
    public void SetupYellowStar()
    {
        _imgStar.sprite = _spriteYellowStar;
    }
    
    public void SetupGrownStar()
    {
        _imgStar.sprite = _spriteGrownStar;
    }
}

