using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Feature.HeroInformation
{
    public class ChangeButtonController : MonoBehaviour
    {
        [SerializeField] private List<ButtonChain> _buttonList;
        
        [SerializeField] private Sprite _positiveImage;
        [SerializeField] private Sprite _absoluteImage;

        private readonly string _hexPositiveColor = "#F3EF94"; // Replace this with your desired hexadecimal color
        private  Color _positiveColor;
        
        private readonly string _hexAbsoluteColor = "#323232"; // Replace this with your desired hexadecimal color
        private  Color _absoluteColor;

        private void Start()
        {
            foreach (var buttonChain in _buttonList)
            {
                buttonChain.SetUp(ChangeButtonImagesAndColorText);
            }
        }

        private void ChangeButtonImagesAndColorText(Button clickedButton)
        {        
            if (ColorUtility.TryParseHtmlString(_hexPositiveColor, out _positiveColor) &&
                ColorUtility.TryParseHtmlString(_hexAbsoluteColor, out _absoluteColor))
            {
                foreach (ButtonChain buttonChain in _buttonList)
                {
                    buttonChain.Button().image.sprite = buttonChain.Button() == clickedButton ? _positiveImage : _absoluteImage;

                    if (buttonChain.Text() != null)
                    {
                        buttonChain.Text().color = buttonChain.Button() == clickedButton ? _positiveColor : _absoluteColor;
                    }
                }
            }
        }
    }
}