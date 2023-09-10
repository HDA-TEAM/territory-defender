using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Feature.HeroInformation
{
    public class HeroLevel: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image experienceBarImage;

        private void Awake()
        {
            SetExperienceBarSize(.5f);
            SetLevelNumber(7);
        }

        private void SetExperienceBarSize(float experienceNormalized)
        {
            experienceBarImage.fillAmount = experienceNormalized;
        }

        private void SetLevelNumber(int levelNumber)
        {
            levelText.text = "Level: " + (levelNumber + 1);
        }
    }
}