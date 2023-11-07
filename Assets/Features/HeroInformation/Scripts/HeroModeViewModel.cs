using System.Collections.Generic;
using UnityEngine;

namespace Features.HeroInformation
{
    // Enum for the different character menu modes
    public enum MenuCharacterModes
    {
        Skill,
        Skin,
        History
    }

    public class HeroModeViewModel : MonoBehaviour
    {
        [SerializeField] private List<HeroModeView> _buttonViewModels;
        [SerializeField] private List<GameObject> _objContentList;

        [SerializeField] private Sprite _positiveImage;
        [SerializeField] private Sprite _absoluteImage;

        private Color _positiveColor;
        private Color _absoluteColor;

        private readonly string _hexPositiveColor = "#F3EF94";
        private readonly string _hexAbsoluteColor = "#323232";

        // Mapping of menu type to button view model and content object
        private Dictionary<MenuCharacterModes, (HeroModeView viewModel, GameObject contentObject)> _menuMapping;

        private void Start()
        {
            if (ColorUtility.TryParseHtmlString(_hexPositiveColor, out _positiveColor) &&
                ColorUtility.TryParseHtmlString(_hexAbsoluteColor, out _absoluteColor))
            {
                InitializeButtonViewModels();
                CreateMenuMapping();
                SetInitialContentState();
            }
            else
            {
                Debug.LogError("Invalid color strings.");
            }
        }

        private void InitializeButtonViewModels()
        {
            foreach (var viewModel in _buttonViewModels)
            {
                viewModel.Initialize(HandleButtonSelected);
            }
        }

        private void CreateMenuMapping()
        {
            _menuMapping = new Dictionary<MenuCharacterModes, (HeroModeView, GameObject)>();

            for (int i = 0; i < _objContentList.Count; i++)
            {
                var menuType = _buttonViewModels[i].MenuType; // Assuming ButtonModeViewModel has a MenuType property of type MenuCharacterModes
                _menuMapping[menuType] = (_buttonViewModels[i], _objContentList[i]);
            }
        }

        private void SetInitialContentState()
        {
            foreach (var content in _objContentList)
            {
                content.SetActive(false);
            }
            if (_objContentList.Count > 0)
            {
                _objContentList[0].SetActive(true);
            }
        }

        private void HandleButtonSelected(MenuCharacterModes menuType)
        {
            UpdateButtonVisualStates(menuType);
            SwapContent(menuType);
        }

        private void UpdateButtonVisualStates(MenuCharacterModes activeMenuType)
        {
            foreach (var pair in _menuMapping)
            {
                var viewModel = pair.Value.viewModel;
                var targetSprite = pair.Key == activeMenuType ? _positiveImage : _absoluteImage;
                var targetColor = pair.Key == activeMenuType ? _positiveColor : _absoluteColor;
                viewModel.UpdateVisualState(targetSprite, targetColor);
            }
        }

        private void SwapContent(MenuCharacterModes menuType)
        {
            foreach (var pair in _menuMapping)
            {
                pair.Value.contentObject.SetActive(pair.Key == menuType);
            }
        }
    }
}
