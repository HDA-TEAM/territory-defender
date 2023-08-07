using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.HeroInformation
{
    public class SkillTextManage : MonoBehaviour
    {
        public List<ButtonSkillDescribe> buttonSkillDescribesList;

        private bool _status;
        public void ClickedSkill(Button skillClicked)
        {
            foreach (ButtonSkillDescribe button in buttonSkillDescribesList)
            {
                _status = button.SkillDescribeButton() == skillClicked ? true : false;
                button.DescribeSkillImage().gameObject.SetActive(_status);
            }
        }
    }
}

