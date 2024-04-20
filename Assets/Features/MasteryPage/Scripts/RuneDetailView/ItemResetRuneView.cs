using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.MasteryPage.Scripts.Rune
{
    public class ItemResetRuneView : MonoBehaviour
    {
        [SerializeField] private Button _btnResetRune;

        public RuneComposite RuneComposite;
        private Action<ItemResetRuneView> _onSelected;

        private void Awake()
        {
            _btnResetRune.onClick.AddListener(OnSelectedResetRune);
        }

        public void Setup(RuneComposite runeComposite, Action<ItemResetRuneView> onSelected)
        {
            
        }

        private void OnSelectedResetRune()
        {
            _onSelected?.Invoke(this);
        }
    }
}
