using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroDetailView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtHeroName;
    [SerializeField] private TextMeshProUGUI _txtLevel;
    [SerializeField] private Image _imgHero;
    
    #region Core
    public void Setup(HeroComposite heroComposite)
    {
        _txtHeroName.text = heroComposite.Name;
        _txtLevel.text = heroComposite.Level;
        _imgHero.sprite = heroComposite.Avatar;
    }
    #endregion
    
}
