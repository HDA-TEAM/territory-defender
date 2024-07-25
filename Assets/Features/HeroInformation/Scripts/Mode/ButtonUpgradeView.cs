using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.HeroInformation.Scripts.Mode
{
    public class ButtonUpgradeView : MonoBehaviour
    {
        [SerializeField] private Button _upgradeBtn;
        [SerializeField] private TextMeshProUGUI _txtPrice;
        [SerializeField] private Image _imageBg;
        [SerializeField] private Sprite _availableSpriteBg;
        [SerializeField] private Sprite _unavailableSpriteBg;
        [SerializeField] private Color _availableTextColor;
        [SerializeField] private Color _unavailableTextColor;

        private Action _onClick;
        #region Core
        private void Awake()
        {
            _upgradeBtn.onClick.AddListener(OnClick);
        }
        #endregion
        private void OnClick()
        {
            _onClick?.Invoke();
        }
        private void SetUpgradable(string amount, bool isUpgradable)
        {
            _txtPrice.text = amount;
            _txtPrice.color = isUpgradable ? _availableTextColor : _unavailableTextColor;
            _imageBg.sprite = isUpgradable ? _availableSpriteBg : _unavailableSpriteBg;
        }

        public void Setup(int amount, bool isUpgradable, Action onClick)
        {
            SetUpgradable(amount.ToString(), isUpgradable);
            _onClick = onClick;
        }
    }
}
