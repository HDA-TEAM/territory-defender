using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkillView : MonoBehaviour
{
    [SerializeField] private Button _buttonSkill;
    [SerializeField] private Image _describeSkillImage;
    public ItemSkillView SkillDescribeButton() => this;
    public Image DescribeSkillImage() => _describeSkillImage;

    // Internal
    private Action<ItemSkillView> _onSelected;

    private void Awake()
    {
        _buttonSkill.onClick.AddListener(OnButtonClick);
    }
    
    public void Setup(Action<ItemSkillView> onSelected)
    {
        _onSelected = onSelected;
    }

    public void OnButtonClick()
    {
       _onSelected?.Invoke(this);
    }

    public void ResetSkillViews()
    {
        _describeSkillImage.gameObject.SetActive(false);
    }
}


