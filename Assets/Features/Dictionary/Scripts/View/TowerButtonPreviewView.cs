using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Dictionary.Scripts.View
{
    public class TowerButtonPreviewView : MonoBehaviour
    {
        [SerializeField] private Button _btn;

        private Action _onClick;
        // Internal
        private Sprite _sprite;
        
        #region Core
        private void Awake()
        {
            _btn.onClick.AddListener(OnSelected);
        }
        public void Setup(Action onClick)
        {
            _onClick = onClick;
        }
        #endregion
        protected virtual void OnSelected()
        {
            _onClick?.Invoke();
        }
    }
}
