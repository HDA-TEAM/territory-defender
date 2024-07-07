using Features.Dictionary.Scripts.View;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Dictionary.Scripts.ViewModel
{
    public class ListButtonDictionaryViewModel : MonoBehaviour
    {
        [SerializeField] private List<UnitButtonDictionaryView> _unitDictionaryViews;

        private UnitButtonDictionaryView _curUnitButtonDictionaryView;
        private Action<string> _onSelectedButton;
        public void SetUpButton(List<ButtonDictionaryComposite> buttonDictionaryComposites, Action<string> onSelectedButton)
        {
            SetUpViews(buttonDictionaryComposites);
            _onSelectedButton = onSelectedButton;
        }
        private void SetUpViews(IReadOnlyList<ButtonDictionaryComposite> buttonDictionaryComposites)
        {
            for (int index = 0; index < _unitDictionaryViews.Count; index++)
            {
                bool isShow = index < buttonDictionaryComposites.Count;
                if (isShow)
                {
                    UnitButtonDictionaryView unitDictionaryView = _unitDictionaryViews[index];
                    unitDictionaryView.SetUp(OnSelected, buttonDictionaryComposites[index]);
                }
                _unitDictionaryViews[index].gameObject.SetActive(isShow);

            }
        }
        private void OnSelected(UnitButtonDictionaryView buttonDictionaryView)
        {
            if (_curUnitButtonDictionaryView == buttonDictionaryView)
                return;
            
            if (_curUnitButtonDictionaryView)
                _curUnitButtonDictionaryView.RemoveSelected();
            
            _curUnitButtonDictionaryView = buttonDictionaryView;
            
            _onSelectedButton?.Invoke(buttonDictionaryView.DictionaryComposite.UnitId);
        }
    }
}
