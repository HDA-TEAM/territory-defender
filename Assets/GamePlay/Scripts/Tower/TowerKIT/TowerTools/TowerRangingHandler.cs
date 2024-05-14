using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Tower.TowerKIT.TowerTools
{
    public class TowerRangingHandler : MonoBehaviour
    {
        [SerializeField] private Image _imgRangeCamping;
        [SerializeField] private Image _imgRangeTower;
        public void SetUp(float range)
        {
            //  scale of base transform = 0.01, so need to convert real world space = 1
            range *= 100;
            
            SetRangeOfTower(range);
        }
        private void SetRangeOfTower(float range)
        {
            _imgRangeCamping.rectTransform.sizeDelta = new Vector2(range * 2, range * 2);
            _imgRangeTower.rectTransform.sizeDelta = _imgRangeCamping.rectTransform.sizeDelta;
        }
        public void SetShowRanging(bool isShow)
        {
            _imgRangeCamping.enabled = isShow;
        }
    }
}
