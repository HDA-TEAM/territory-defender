using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroDetailView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtHeroName;
    [SerializeField] private TextMeshProUGUI _txtLevel;
    [SerializeField] private TextMeshProUGUI _txtHp;
    [SerializeField] private TextMeshProUGUI _txtAtk;
    [SerializeField] private TextMeshProUGUI _txtDef;
    [SerializeField] private TextMeshProUGUI _txtRange;
    [SerializeField] private Image _imgHero;
    
    #region Core
    public void Setup(HeroComposite heroComposite)
    {
        _txtHeroName.text = heroComposite.Name;
        _txtLevel.text = heroComposite.Level;
        _txtHp.text = heroComposite.Hp;
        _txtAtk.text = heroComposite.Atk;
        _txtDef.text = heroComposite.Def;
        _txtRange.text = heroComposite.Range;
        _imgHero.sprite = heroComposite.Avatar;
    }
    #endregion
    
}
