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

    public class CharacterModesButton : MonoBehaviour
    {
        [SerializeField] private List<ButtonChain> _btnClickedList;

        public GameObject _skillObject;
        public GameObject _skinObject;
        public GameObject _historyObject;

        private MenuCharacterModes _currentMenuType;
        private Dictionary<MenuCharacterModes, GameObject> _buttonToObjectDictionary = new Dictionary<MenuCharacterModes, GameObject>();

        private void Start()
        {
            // Map buttons to corresponding objects
            _buttonToObjectDictionary[MenuCharacterModes.Skill] = _skillObject;
            _buttonToObjectDictionary[MenuCharacterModes.Skin] = _skinObject;
            _buttonToObjectDictionary[MenuCharacterModes.History] = _historyObject;

            // Set initial states
            _skillObject.SetActive(true);
            _skinObject.SetActive(false);
            _historyObject.SetActive(false);

            foreach (var buttonChain in _btnClickedList)
            {
                buttonChain.ChangeContent(SwapContent);
            }
        }

        private void SwapContent(MenuCharacterModes menuType)
        {
            if (_buttonToObjectDictionary.ContainsKey(menuType))
            {
                GameObject correspondingObject = _buttonToObjectDictionary[menuType];

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
