using GamePlay.Scripts.Menu.UnitInformationPanel;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip
{

    public class PreviewUpgradeTooltipViewModel : MonoBehaviour
    {
        [SerializeField] private PreviewUpgradeTooltipView _previewUpgradeTooltipView;
        [SerializeField] private List<ItemUnitStatView> _unitStatViews;
        public void Setup(PreviewTooltipComposite previewTooltipComposite)
        {
            _previewUpgradeTooltipView.Setup(previewTooltipComposite);
            SetupStatsView(previewTooltipComposite.StatComposites);
        }
        private void SetupStatsView(List<ItemStatComposite> statComposites)
        {
            int availableShowItem = statComposites.Count;
            for (int i = 0; i < _unitStatViews.Count; i++)
            {
                if (i < availableShowItem)
                {
                    _unitStatViews[i].gameObject.SetActive(true);
                    _unitStatViews[i].Setup(statComposites[i]);
                }
                else
                    _unitStatViews[i].gameObject.SetActive(false);
            }
        }
    }
}
