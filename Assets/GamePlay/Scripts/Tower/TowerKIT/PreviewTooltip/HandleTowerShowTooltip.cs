using Common.Scripts.Utilities;
using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip
{
    public class HandleTowerShowTooltip : MonoBehaviour
    {
        [SerializeField] private TowerPreviewTooltipViewModel _towerPreviewTooltipLeft;
        [SerializeField] private TowerPreviewTooltipViewModel _towerPreviewTooltipRight;
        public void ShowTooltip(TowerPreviewToolTipBase towerPreviewToolTipBase)
        {
            StatPreviewTooltipComposite statPreviewTooltipComposite = towerPreviewToolTipBase.GetPreviewTooltipComposite();
            bool isLeftOfMap = VectorUtility.CheckLeftToRightDirection(Vector3.zero, transform.position);
            if (isLeftOfMap)
            {
                _towerPreviewTooltipLeft.Setup(statPreviewTooltipComposite);
                _towerPreviewTooltipLeft.gameObject.SetActive(true);
            }
            else
            {
                _towerPreviewTooltipRight.Setup(statPreviewTooltipComposite);
                _towerPreviewTooltipRight.gameObject.SetActive(true);
            }
        }
        public void HideAll()
        {
            _towerPreviewTooltipLeft.gameObject.SetActive(false);
            _towerPreviewTooltipRight.gameObject.SetActive(false);
        }
    }
}
