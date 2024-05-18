using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Tower.TowerKIT.TowerTools
{
    public class TowerCampingSelection : MonoBehaviour
    {
        [SerializeField] private Button _clickButton;
        [SerializeField] private Image _imgFlag;
        [SerializeField] private float _durationFlaggingAnim;
        private Action _onSelectCampingPlace;

        private void Awake()
        {
            _clickButton.onClick.AddListener(OnClickSelectCampingPlace);
        }
        public void SetUp(Action onSelectCampingPlace)
        {
            _clickButton.image.enabled = true;
            _imgFlag.gameObject.SetActive(false);
            _onSelectCampingPlace = onSelectCampingPlace;
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
            _clickButton.image.enabled = false;
            _imgFlag.gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_durationFlaggingAnim));
            _imgFlag.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
