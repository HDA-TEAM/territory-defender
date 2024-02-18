using SuperMaxim.Messaging;
using UnityEngine;

public struct UsingSkillPayload
{
    public ESkillId ESkillId;
}
public class InGameSkillController : MonoBehaviour
{
    [SerializeField] private InGameActiveSkill _firstSkill;
    private void Awake()
    {
        SetUpSkill();
    }
    private void SetUpSkill()
    {
        _firstSkill.SetUpSkill(ESkillId.SummonElephant,ExecuteSkill);
    }
    private void ExecuteSkill(ESkillId eSkillId)
    {
        Messenger.Default.Publish(new UsingSkillPayload
        {
            ESkillId = eSkillId
        }); 
    }
}
