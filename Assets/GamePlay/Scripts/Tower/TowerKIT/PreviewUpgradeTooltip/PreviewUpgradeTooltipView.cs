using TMPro;
using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.PreviewUpgradeTooltip
{
    public class PreviewUpgradeTooltipView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _txtTitle;
        [SerializeField] private TextMeshProUGUI _txtDesc;
        public void Setup(PreviewUpgradeTooltipComposite previewUpgradeTooltipComposite)
        {
            _txtTitle.text = previewUpgradeTooltipComposite.Title;
            _txtDesc.text = previewUpgradeTooltipComposite.Desc;
        }
    }
}
