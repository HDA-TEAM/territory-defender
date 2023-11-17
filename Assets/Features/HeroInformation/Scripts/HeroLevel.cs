using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Features.HeroInformation
{
    public class HeroLevel: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _hpText;
        [SerializeField] private TextMeshProUGUI _atkText;
        [SerializeField] private TextMeshProUGUI _defText;
        [SerializeField] private TextMeshProUGUI _rangeText;
        [SerializeField] private Image _experienceBarImage;

        private void Awake()
        {
            SetExperienceBarSize(.9f);
            SetLevelNumber(7);
            SetHealthPointNumber(1000);
            SetAttackNumber(200);
            SetDefenseNumber(80);
            SetRangeNumber(2.32);
        }

        private void SetExperienceBarSize(float experienceNormalized)
        {
            _experienceBarImage.fillAmount = experienceNormalized;
        }

        private void SetLevelNumber(int levelNumber)
        {
            _levelText.text = (levelNumber).ToString();
        }
        
        private void SetHealthPointNumber(int hpNumber)
        {
            _hpText.text = (hpNumber).ToString();
        }
        
        private void SetAttackNumber(int atkNumber)
        {
            _atkText.text = (atkNumber).ToString();
        }
        
        private void SetDefenseNumber(int defNumber)
        {
            _defText.text = (defNumber).ToString();
        }
        
        private void SetRangeNumber(double rangeNumber)
        {
            _rangeText.text = (rangeNumber).ToString("F2");
        }
    }
}