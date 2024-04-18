using GamePlay.Scripts.Menu.UnitInformationPanel;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip
{

    public class PreviewTooltipViewModel : MonoBehaviour
    {
        [SerializeField] private PreviewTooltipView _previewTooltipView;
        [SerializeField] private List<ItemUnitStatView> _unitStatViews;
        public void Setup(PreviewTooltipComposite previewTooltipComposite)
        {
            _previewTooltipView.Setup(previewTooltipComposite);
            SetupStatsView(previewTooltipComposite.StatComposites);
        }
        private void SetupStatsView(List<ItemStatComposite> statComposites)
        {
            if (statComposites == null)
                return;
            
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
