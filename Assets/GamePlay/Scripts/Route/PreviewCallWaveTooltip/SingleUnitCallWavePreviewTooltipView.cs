using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Route.PreviewCallWaveTooltip
{
    public class SingleUnitCallWavePreviewTooltipView : MonoBehaviour
    {
        [SerializeField] private Image _imgAvatar;
        [SerializeField] private TextMeshProUGUI _txtAmount;

        private static readonly string AmountPattern = $"x {0}";
        
        public void Setup(SingleUnitPreviewComposite statPreviewUpgradeTooltipComposite)
        {
            _imgAvatar.sprite = statPreviewUpgradeTooltipComposite.SpriteAvatar;
            _txtAmount.text = string.Format(AmountPattern,statPreviewUpgradeTooltipComposite.Amount);
        }
    }
}
