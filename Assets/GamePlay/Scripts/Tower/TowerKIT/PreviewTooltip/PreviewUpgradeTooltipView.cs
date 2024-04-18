using TMPro;
using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip
{
    public class PreviewUpgradeTooltipView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _txtTitle;
        [SerializeField] private TextMeshProUGUI _txtDesc;
        public void Setup(PreviewTooltipComposite previewUpgradeTooltipComposite)
        {
            _txtTitle.text = previewUpgradeTooltipComposite.Title;
            _txtDesc.text = previewUpgradeTooltipComposite.Desc;
        }
    }
}
