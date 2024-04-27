using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.MasteryPage.Scripts.RuneDetailView
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
            RuneComposite = runeComposite;
            _onSelected = onSelected;
        }
        private void OnSelectedResetRune()
        {
            Debug.Log("OnSelectedResetRune");
            _onSelected?.Invoke(this);
        }
    }
}
