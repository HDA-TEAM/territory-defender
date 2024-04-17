using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Menu.UnitInformationPanel
{
    public class UnitShowInformationViewModel : MonoBehaviour
    {
        [SerializeField] private List<ItemUnitStatView> _unitStatViews;
        [SerializeField] private UnitAvatarView _avatarView;
        public void Setup(ShowStatsInformationComposite statInformationComposite)
        {
            _avatarView.Setup(statInformationComposite);
            SetupStatsView(statInformationComposite);
        }
        private void SetupStatsView(ShowStatsInformationComposite statInformationComposite)
        {
            int availableShowItem = statInformationComposite.StatComposites.Count;
            for (int i = 0; i < _unitStatViews.Count; i++)
            {
                if (i < availableShowItem)
                {
                    _unitStatViews[i].gameObject.SetActive(true);
                    _unitStatViews[i].Setup(statInformationComposite.StatComposites[i]);
                }
                else
                    _unitStatViews[i].gameObject.SetActive(false);
            }
        }
    }
}
