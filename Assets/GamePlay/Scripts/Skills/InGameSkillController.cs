using Cysharp.Threading.Tasks;
using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.Serialization;

public struct UsingSkillPayload
{
    public ESkillId ESkillId;
}
public class InGameSkillController : MonoBehaviour
{
    [SerializeField] private InGameActiveSkillView _firstSkillView;
    [FormerlySerializedAs("_skillsDataConfig")]
    [FormerlySerializedAs("_skillsDataAsset")]
    [SerializeField] private SkillDataConfig _skillDataConfig;
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
        _curSkillConfig = _skillDataConfig.GetSkillDataById(ESkillId.SummonElephant);
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
