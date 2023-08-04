using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Feature.HeroInformation
{
    public class ChangeButtonController : MonoBehaviour
    {
        public List<Button> buttonList;
        
        [SerializeField] private Sprite positiveImage;
        [SerializeField] private Sprite absoluteImage;

        private readonly string _hexPositiveColor = "#F3EF94"; // Replace this with your desired hexadecimal color
        private  Color _positiveColor;
        
        private readonly string _hexAbsoluteColor = "#323232"; // Replace this with your desired hexadecimal color
        private  Color _absoluteColor;
        
        public void ChangeButtonImagesAndColorText(Button clickedButton)
        {
            if (ColorUtility.TryParseHtmlString(_hexPositiveColor, out _positiveColor) &&
                ColorUtility.TryParseHtmlString(_hexAbsoluteColor, out _absoluteColor))
            {
                foreach (Button button in buttonList)
                {
                    button.image.sprite = button == clickedButton ? positiveImage : absoluteImage;
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                    
                    if (buttonText != null)
                    {
                        buttonText.color = button == clickedButton ? _positiveColor : _absoluteColor;
                    }
                }
            }
        }
    }
}