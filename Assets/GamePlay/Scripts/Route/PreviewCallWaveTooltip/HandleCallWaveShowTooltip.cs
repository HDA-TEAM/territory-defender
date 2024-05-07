using UnityEngine;

namespace GamePlay.Scripts.Route.PreviewCallWaveTooltip
{
    public class HandleSingleCallWaveShowTooltip : MonoBehaviour
    {
        [SerializeField] private CallWavePreviewTooltipViewModel _callWavePreviewTooltipLeft;
        [SerializeField] private CallWavePreviewTooltipViewModel _callWavePreviewTooltipRight;
        [SerializeField] private CallWavePreviewTooltipViewModel _callWavePreviewTooltipTop;
        [SerializeField] private CallWavePreviewTooltipViewModel _callWavePreviewTooltipDown;

        public ECallWaveUnitPreviewDirectionType ECallWaveUnitPreviewDirectionType;
        public void ShowTooltip(CallWavePreviewUnitComposite callWavePreviewUnitComposite)
        {
            HideAll();

            if (callWavePreviewUnitComposite.UnitPreviewComposites == null)
                return;

            switch (callWavePreviewUnitComposite.DirectionType)
            {
                case ECallWaveUnitPreviewDirectionType.TopSide:
                    {
                        _callWavePreviewTooltipTop.gameObject.SetActive(true);
                        _callWavePreviewTooltipTop.Setup(callWavePreviewUnitComposite);
                        break;
                    }
                case ECallWaveUnitPreviewDirectionType.DownSide:
                    {
                        _callWavePreviewTooltipDown.gameObject.SetActive(true);
                        _callWavePreviewTooltipDown.Setup(callWavePreviewUnitComposite);
                        break;
                    }
                case ECallWaveUnitPreviewDirectionType.LeftSide:
                    {
                        _callWavePreviewTooltipLeft.gameObject.SetActive(true);
                        _callWavePreviewTooltipLeft.Setup(callWavePreviewUnitComposite);
                        break;
                    }
                case ECallWaveUnitPreviewDirectionType.RightSide:
                    {
                        _callWavePreviewTooltipRight.gameObject.SetActive(true);
                        _callWavePreviewTooltipRight.Setup(callWavePreviewUnitComposite);
                        break;
                    }
            }
           
        }
        private void HideAll()
        {
            _callWavePreviewTooltipLeft.gameObject.SetActive(false);
            _callWavePreviewTooltipRight.gameObject.SetActive(false);
            _callWavePreviewTooltipTop.gameObject.SetActive(false);
            _callWavePreviewTooltipDown.gameObject.SetActive(false);
        }
    }
}
