using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Features.MasteryPage.Scripts.RuneDetailView
{
    public class ItemResetRuneView : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        [SerializeField] private TextMeshProUGUI _txtName;
        
        public RuneComposite RuneComposite;
        private Action<ItemResetRuneView> _onSelected;

        private void Awake()
        {
            _btn.onClick.AddListener(OnSelectedResetRune);
        }
        public void Setup(RuneComposite runeComposite, Action<ItemResetRuneView> onSelected)
        {
            RuneComposite = runeComposite;
            _onSelected = onSelected;

            string str = "Reset";
            SetName(str.ToUpper());
        }
        private void OnSelectedResetRune()
        {
            Debug.Log("OnSelectedResetRune");
            _onSelected?.Invoke(this);
        }
        private void SetName(string btnName)
        {
            _txtName.text = btnName;
        }
    }
}
