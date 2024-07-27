using Common.Scripts;
using Common.Scripts.Data;
using Common.Scripts.Data.DataConfig;
using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using SuperMaxim.Messaging;
using System;
using UnityEngine;

public struct UsingSkillPayload
{
    public ESkillId ESkillId;
}
public class InGameSkillController : GamePlayMainFlowBase
{
    [SerializeField] private InGameActiveSkillView _firstSkillView;
    [SerializeField] private SkillDataConfig _skillDataConfig;
    [SerializeField] private InGameHeroDataConfigBase _heroDataConfig;
    private SkillDataSO _curSkillConfig;
    private bool _isCooldown = false;
    protected override void OnSetupNewGame(SetUpNewGamePayload setUpNewGamePayload)
    {
        UnitId.Hero heroId = setUpNewGamePayload.StartStageComposite.HeroId;
        UnitBase unitBase = _heroDataConfig.GetConfigByKey(heroId).UnitBase;
        UserActionHeroBaseController userActionController = unitBase.UserActionController() as UserActionHeroBaseController;
        SetUpSkill(userActionController.ActiveSkillId);
    }
    protected override void OnResetGame(ResetGamePayload resetGamePayload)
    {
    }
    private void SetUpSkill(ESkillId skillId)
    {
        _firstSkillView.SetUpSkill(skillId,ExecuteSkill);
        _curSkillConfig = _skillDataConfig.GetSkillDataById(skillId);
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
