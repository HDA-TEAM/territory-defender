using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip
{
    public class HandleShowTooltip : MonoBehaviour
    {
        [SerializeField] private PreviewUpgradeTooltipViewModel _previewTooltipLeft;
        [SerializeField] private PreviewUpgradeTooltipViewModel _previewTooltipRight;
        public void ShowTooltip(PreviewTooltipBase previewTooltipBase)
        {
            PreviewTooltipComposite previewTooltipComposite = previewTooltipBase.GetPreviewTooltipComposite();
            bool isLeftOfMap = VectorUtility.CheckLeftToRightDirection(Vector3.zero, transform.position);
            if (isLeftOfMap)
            {
                _previewTooltipLeft.Setup(previewTooltipComposite);
                _previewTooltipLeft.gameObject.SetActive(true);
            }
            else
            {
                _previewTooltipRight.Setup(previewTooltipComposite);
                _previewTooltipRight.gameObject.SetActive(true);
            }
        }
        public void HideAll()
        {
            _previewTooltipLeft.gameObject.SetActive(false);
            _previewTooltipRight.gameObject.SetActive(false);
        }
    }
}
