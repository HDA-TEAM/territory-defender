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
            
            int availableShowItem = _unitStatViews.Count < statInformationComposite.StatComposites.Count ? statInformationComposite.StatComposites.Count : _unitStatViews.Count;
            for (int i = 0; i < availableShowItem; i++)
            {
                if (i < _unitStatViews.Count)
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
