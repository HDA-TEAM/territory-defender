using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.PreviewUpgradeTooltip
{
    public class HandleShowUpgradeTooltip : MonoBehaviour
    {
        [SerializeField] private PreviewUpgradeTooltipViewModel _previewUpgradeTooltipLeft;
        [SerializeField] private PreviewUpgradeTooltipViewModel _previewUpgradeTooltipRight;
        public void ShowTooltip(PreviewUpgradeTooltipComposite previewUpgradeTooltip)
        {
            HideAll();
            bool isLeftOfMap = VectorUtility.CheckLeftToRightDirection(Vector3.zero, transform.position);
            if (isLeftOfMap)
            {
                // _previewUpgradeTooltipLeft.Setup(previewUpgradeTooltip);
                _previewUpgradeTooltipLeft.gameObject.SetActive(true);
            }
            else
            {
                // _previewUpgradeTooltipRight.Setup(previewUpgradeTooltip);
                _previewUpgradeTooltipRight.gameObject.SetActive(true);
            }
        }
        private void HideAll()
        {
            _previewUpgradeTooltipLeft.gameObject.SetActive(false);
            _previewUpgradeTooltipRight.gameObject.SetActive(false);
        }
    }
}
