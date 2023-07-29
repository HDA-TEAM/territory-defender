using UnityEngine;
using UnityEngine.UI;
namespace Feature.HeroInformation
{
    public class ButtonChain : MonoBehaviour
    {
        [SerializeField] private Button button;

        [SerializeField] private ChangeButtonController characterController;

        public void OnButtonClick()
        {
            characterController.ChangeButtonImages(button);
        }
    }

}

