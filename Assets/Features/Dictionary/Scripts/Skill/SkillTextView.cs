using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillTextView : MonoBehaviour
{
    [SerializeField] private Button _buttonSkillDescribe;
    [SerializeField] private Image _describeSkillImage;
    [SerializeField] private Button _skillTextViewModel;
    
    public Button SkillDescribeButton() => _buttonSkillDescribe;
    public Image DescribeSkillImage() => _describeSkillImage;

    // Internal
    private Action<SkillTextView> _onSelected;

    private void Start()
    {
        _skillTextViewModel.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
       _onSelected?.Invoke(this);
    }
}


