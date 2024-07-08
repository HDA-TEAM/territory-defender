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
            // reset data because 2 view model using this list button
            if (_curUnitButtonDictionaryView)
            {
                _curUnitButtonDictionaryView.RemoveSelected();
                _curUnitButtonDictionaryView = null;
            }
            _onSelectedButton = onSelectedButton;
            SetUpViews(buttonDictionaryComposites);
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
                    if (index == 0)
                    {
                        // To show default unit when open pu
                        unitDictionaryView.OnDefaultShow();
                    }
                }
                _unitDictionaryViews[index].gameObject.SetActive(isShow);

            }
        }
        private void OnSelected(UnitButtonDictionaryView buttonDictionaryView)
        {
            // preventing don't change button
            if (_curUnitButtonDictionaryView == buttonDictionaryView)
                return;
            
            // RemoveSelected for old view
            if (_curUnitButtonDictionaryView)
                _curUnitButtonDictionaryView.RemoveSelected();
            
            _curUnitButtonDictionaryView = buttonDictionaryView;
            
            _onSelectedButton?.Invoke(buttonDictionaryView.DictionaryComposite.UnitId);
        }
    }
}
