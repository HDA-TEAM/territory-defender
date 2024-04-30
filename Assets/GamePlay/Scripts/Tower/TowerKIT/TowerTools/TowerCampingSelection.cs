using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Tower.TowerKIT.TowerTools
{
    public class TowerCampingSelection : MonoBehaviour
    {
        [SerializeField] private Image _imgRangeCamping;
        [SerializeField] private Button _clickButton;
        [SerializeField] private float _durationFlaggingAnim;
        [SerializeField] private Image _imgFlag;
        private Action _onSelectCampingPlace;

        private void Awake()
        {
            _clickButton.onClick.AddListener(OnClickSelectCampingPlace);
        }
        public void SetUp(float range, Action onSelectCampingPlace)
        {
            _imgRangeCamping.enabled = true;
            _imgFlag.gameObject.SetActive(false);
            _onSelectCampingPlace = onSelectCampingPlace;
            
            // todo get real range
            range *= 50;
            SetRangeOfTower(range);
        }
        
        private void SetRangeOfTower(float range)
        {
            _clickButton.image.rectTransform.sizeDelta = new Vector2(range * 2, range * 2);
        }
        private void OnClickSelectCampingPlace()
        {
            _onSelectCampingPlace?.Invoke();
            _onSelectCampingPlace = null;

            PlaySetCampingFlagAnim();
        }
        public void SetFlagCampingPos(Vector3 campingPos)
        {
            _imgFlag.transform.position = campingPos;
        }
        private async void PlaySetCampingFlagAnim()
        {
            _imgRangeCamping.enabled = false;
            _imgFlag.gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_durationFlaggingAnim));
            _imgFlag.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
