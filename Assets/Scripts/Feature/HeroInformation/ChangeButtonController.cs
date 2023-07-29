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

        public void ChangeButtonImages(Button clickedButton)
        {
            foreach (Button button in buttonList)
            {
                button.image.sprite = button == clickedButton ? positiveImage : absoluteImage;
            }
        }
    }
}