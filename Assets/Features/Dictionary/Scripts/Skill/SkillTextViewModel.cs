using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTextViewModel : MonoBehaviour
{
    public List<SkillTextView> _itemSkillTextViews;

    private bool _status;

    private void Awake()
    {
        UpdateData();
    }

    private void UpdateData()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        for (int i = 0; i < _itemSkillTextViews.Count; i++)
        {
            
        }
    }

    public void ClickedSkill(SkillTextView skillClicked)
    {
        foreach (SkillTextView button in _itemSkillTextViews)
        {
            _status = button.SkillDescribeButton() == skillClicked ? true : false;
            button.DescribeSkillImage().gameObject.SetActive(_status);
        }
    }

    private void OnSkillTextViewSelected(SkillTextView itemSkillTextView)
    {
        ClickedSkill(itemSkillTextView);
    }
}


