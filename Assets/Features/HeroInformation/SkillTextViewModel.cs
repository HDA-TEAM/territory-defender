using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Features.HeroInformation
{
    public class SkillTextViewModel : MonoBehaviour
    {
        public List<SkillTextView> _buttonSkillDescribesList;

        private bool _status;
        public void ClickedSkill(Button skillClicked)
        {
            foreach (SkillTextView button in _buttonSkillDescribesList)
            {
                _status = button.SkillDescribeButton() == skillClicked ? true : false;
                button.DescribeSkillImage().gameObject.SetActive(_status);
            }
        }
    }
}

