using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.HeroInformation
{
    public enum MenuCharacterModes
    {
        Skill,
        Skin,
        History
    }

    public class ButtonView : MonoBehaviour
    {
        // List button
        [SerializeField] private List<ButtonChain> _btnClickedList;

        // Modes object
        [SerializeField] private List<GameObject> _objContentList;
        // public GameObject _skillObject;
        // public GameObject _skinObject;
        // public GameObject _historyObject;

        private MenuCharacterModes _currentMenuType;
        private Dictionary<MenuCharacterModes, GameObject> _buttonToObjectDictionary = new Dictionary<MenuCharacterModes, GameObject>();

        // Button type
        [SerializeField] private Sprite _positiveImage;
        [SerializeField] private Sprite _absoluteImage;
        
        private readonly string _hexPositiveColor = "#F3EF94"; // Replace this with your desired hexadecimal color
        private  Color _positiveColor;
        
        private readonly string _hexAbsoluteColor = "#323232"; // Replace this with your desired hexadecimal color
        private  Color _absoluteColor;
        
        private void Start()
        {
            // Ensure the count of objects matches the count of enum values
            if (_objContentList.Count != Enum.GetNames(typeof(MenuCharacterModes)).Length)
            {
                Debug.LogError("Object list count does not match the number of enum values.");
                return;
            }

            // Map buttons to corresponding objects
            for (int i = 0; i < _btnClickedList.Count; i++)
            {
                var menuType = (MenuCharacterModes)i;
                _buttonToObjectDictionary[menuType] = _objContentList[i];
            }

            // Set initial states
            foreach (var obj in _objContentList)
            {
                obj.SetActive(false);
            }
            _objContentList[0].SetActive(true); // Activate the first object by default

            foreach (var buttonChain in _btnClickedList)
            {
                buttonChain.SetUp(ChangeButtonImagesAndColorText);
                buttonChain.ChangeContent(SwapContent);
            }
        }
        
        private void ChangeButtonImagesAndColorText(Button clickedButton)
        {        
            if (ColorUtility.TryParseHtmlString(_hexPositiveColor, out _positiveColor) &&
                ColorUtility.TryParseHtmlString(_hexAbsoluteColor, out _absoluteColor))
            {
                foreach (ButtonChain buttonChain in _btnClickedList)
                {
                    buttonChain.Button().image.sprite = buttonChain.Button() == clickedButton ? _positiveImage : _absoluteImage;

                    if (buttonChain.Text() != null)
                    {
                        buttonChain.Text().color = buttonChain.Button() == clickedButton ? _positiveColor : _absoluteColor;
                    }
                }
            }
        }

        private void SwapContent(MenuCharacterModes menuType)
        {
            if (_buttonToObjectDictionary.TryGetValue(menuType, out GameObject correspondingObject))
            {
                // Toggle the active state of the corresponding object
                correspondingObject.SetActive(true); // Always set to true when changing modes

                // Deactivate other objects
                foreach (var obj in _buttonToObjectDictionary.Values)
                {
                    if (obj != correspondingObject)
                    {
                        obj.SetActive(false);
                    }
                }
            }
        }
    }
}
