using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Feature.HeroInformation
{
    public class HeroLevel: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private TextMeshProUGUI atkText;
        [SerializeField] private TextMeshProUGUI defText;
        [SerializeField] private TextMeshProUGUI rangeText;
        
        [SerializeField] private Image experienceBarImage;

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
            experienceBarImage.fillAmount = experienceNormalized;
        }

        private void SetLevelNumber(int levelNumber)
        {
            levelText.text = (levelNumber).ToString();
        }
        
        private void SetHealthPointNumber(int hpNumber)
        {
            hpText.text = (hpNumber).ToString();
        }
        
        private void SetAttackNumber(int atkNumber)
        {
            atkText.text = (atkNumber).ToString();
        }
        
        private void SetDefenseNumber(int defNumber)
        {
            defText.text = (defNumber).ToString();
        }
        
        private void SetRangeNumber(double rangeNumber)
        {
            rangeText.text = (rangeNumber).ToString("F2");
        }
    }
}