using GamePlay.Scripts.Menu.InGameStageScreen.UnitInformationPanel;
using GamePlay.Scripts.Menu.UnitInformationPanel;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip
{

    public class TowerPreviewTooltipViewModel : MonoBehaviour
    {
        [SerializeField] private TowerPreviewTooltipView _towerPreviewTooltipView;
        [SerializeField] private List<ItemUnitStatView> _unitStatViews;
        public void Setup(StatPreviewTooltipComposite statPreviewTooltipComposite)
        {
            _towerPreviewTooltipView.Setup(statPreviewTooltipComposite);
            SetupStatsView(statPreviewTooltipComposite.StatComposites);
        }
        private void SetupStatsView(List<ItemStatComposite> statComposites)
        {
            if (statComposites == null)
            {
                foreach (var view in _unitStatViews)
                    view.gameObject.SetActive(false);
                return;
            }
            
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
