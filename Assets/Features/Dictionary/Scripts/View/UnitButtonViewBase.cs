using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Dictionary.Scripts.View
{
    public abstract class UnitButtonViewBase : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _txtName;
        [SerializeField] private Button _btn;
        [SerializeField] private Image _imageBg;
        [SerializeField] private Sprite _spriteSelectedBg;
        [SerializeField] private Color _selectedTextColor;
        [SerializeField] private Color _deselectedTextColor;

        // Internal
        private Sprite _sprite;
        
        #region Core
        private void Awake()
        {
            _sprite = _imageBg.sprite;
            _btn.onClick.AddListener(OnSelected);
        }
        #endregion
        protected virtual void OnSelected()
        {
            _imageBg.sprite = _spriteSelectedBg;
            _txtName.color = _selectedTextColor;
        }
        public virtual void RemoveSelected()
        {
            _imageBg.sprite = _sprite;
            _txtName.color = _deselectedTextColor;
        }
        protected void SetName(string name)
        {
            _txtName.text = name;
        }
    }

}
