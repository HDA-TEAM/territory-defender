using Cysharp.Threading.Tasks;
using SuperMaxim.Messaging;
using System;
using UnityEngine;

public struct UsingSkillPayload
{
    public ESkillId ESkillId;
}
public class InGameSkillController : MonoBehaviour
{
    [SerializeField] private InGameActiveSkillView _firstSkillView;
    [SerializeField] private SkillsDataAsset _skillsDataAsset;
    private SkillDataSO _curSkillConfig;
    private bool _isCooldown = false;
    private void Awake()
    {
        SetUpSkill();
    }
    private void OnDestroy()
    {
    }
    private void SetUpSkill()
    {
        _firstSkillView.SetUpSkill(ESkillId.SummonElephant,ExecuteSkill);
        _curSkillConfig = _skillsDataAsset.GetSkillDataById(ESkillId.SummonElephant);
    }
    private async void ExecuteSkill(ESkillId eSkillId)
    {
        if (_isCooldown)
            return;
        
        _isCooldown = true;
        
        Messenger.Default.Publish(new UsingSkillPayload
        {
            ESkillId = eSkillId,
        }); 
        _firstSkillView.SetSkillCooldown(true);

        //todo 
        // Get skill cooldown config
        // _curSkillConfig
        await UniTask.Delay(TimeSpan.FromSeconds(3f));

        _isCooldown = false;
        
        _firstSkillView.SetSkillCooldown(false);
    }
}
