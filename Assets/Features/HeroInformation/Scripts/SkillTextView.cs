using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace Features.HeroInformation
{
    public class SkillTextView : MonoBehaviour
    {
        [SerializeField] private Button _buttonSkillDescribe;
        [SerializeField] private Image _describeSkillImage;
        [FormerlySerializedAs("_skillTextManage")] [SerializeField] private SkillTextViewModel _skillTextViewModel;
        
        public Button SkillDescribeButton() => _buttonSkillDescribe;
        public Image DescribeSkillImage() => _describeSkillImage;

        public void OnButtonClick()
        {
            _skillTextViewModel.ClickedSkill(_buttonSkillDescribe);
        }
    }
}

