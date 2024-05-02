using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Route.PreviewCallWaveTooltip
{
    public class SingleUnitCallWavePreviewTooltipView : MonoBehaviour
    {
        [SerializeField] private Image _imgAvatar;
        [SerializeField] private TextMeshProUGUI _txtAmount;
        private static readonly string AmountPattern = "x {0}";
        
        public void Setup(Sprite unitAvatar, int amount)
        {
            _imgAvatar.sprite = unitAvatar;
            _txtAmount.text = String.Format(AmountPattern,amount);
        }
    }
}
