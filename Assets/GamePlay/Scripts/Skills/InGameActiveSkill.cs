using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameActiveSkill : MonoBehaviour
{
    [SerializeField] private Button _btnUsingKill;
    private Action<ESkillId> _onClick;
    private ESkillId _skillId;
    private void Awake()
    {
        _btnUsingKill.onClick.AddListener(OnClickUsingSkill);
    }
    public void SetUpSkill(ESkillId eSkillId,Action<ESkillId> onUsingSkill)
    {
        _skillId = eSkillId;
        _onClick = onUsingSkill;
    }
    private void OnClickUsingSkill()
    {
        _onClick?.Invoke(_skillId);
    }
}
