using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
namespace Feature.HeroInformation
{
    public class ButtonSkillDescribe : MonoBehaviour
    {
        [SerializeField] private Button buttonSkillDescribe;
        [SerializeField] private Image describeSkillImage;
        [SerializeField] private SkillTextManage skillTextManage;
        
        public Button SkillDescribeButton() => buttonSkillDescribe;
        public Image DescribeSkillImage() => describeSkillImage;

        public void OnButtonClick()
        {
            skillTextManage.ClickedSkill(buttonSkillDescribe);
        }
    }
}

