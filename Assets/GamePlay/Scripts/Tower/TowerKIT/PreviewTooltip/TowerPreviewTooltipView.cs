using TMPro;
using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip
{
    public class TowerPreviewTooltipView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _txtTitle;
        [SerializeField] private TextMeshProUGUI _txtDesc;
        public void Setup(StatPreviewTooltipComposite statPreviewUpgradeTooltipComposite)
        {
            _txtTitle.text = statPreviewUpgradeTooltipComposite.Title;
            _txtDesc.text = statPreviewUpgradeTooltipComposite.Desc;
        }
    }
}
