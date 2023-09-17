using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
namespace Feature.HeroInformation
{
    public class ButtonSkillDescribe : MonoBehaviour
    {
        [SerializeField] private Button _buttonSkillDescribe;
        [SerializeField] private Image _describeSkillImage;
        [SerializeField] private SkillTextManage _skillTextManage;
        
        public Button SkillDescribeButton() => _buttonSkillDescribe;
        public Image DescribeSkillImage() => _describeSkillImage;

        public void OnButtonClick()
        {
            _skillTextManage.ClickedSkill(_buttonSkillDescribe);
        }
    }
}

