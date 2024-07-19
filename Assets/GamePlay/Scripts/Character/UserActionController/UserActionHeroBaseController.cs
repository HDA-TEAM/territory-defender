using Common.Scripts.Data;
using Common.Scripts.Data.DataConfig;
using GamePlay.Scripts.GamePlayController;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserActionHeroBaseController : UserActionController
{
    public UserUsingHeroSkill UserUsingHeroSkill;
    public ESkillId ActiveSkillId;
    
    private void OnEnable()
    {
        Messenger.Default.Subscribe<UsingSkillPayload>(OnUsingSkill);
        Messenger.Default.Subscribe<SelectHeroPayload>(OnSelectHero);
    }
    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<UsingSkillPayload>(OnUsingSkill); 
        Messenger.Default.Unsubscribe<SelectHeroPayload>(OnSelectHero);
    }
    private void OnSelectHero(SelectHeroPayload payload)
    {
        if (payload.UnitBase != _unitBaseParent)
            return;
        
        Messenger.Default.Publish(new HandleCancelRaycastPayload
        {
            UnitSelectionShowType = EUnitSelectionShowType.ShowInformationPanelAndBlockRaycast,
            IsOn = true,
            callback = SetMovingPosition,
        });
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        Messenger.Default.Publish(new HandleCancelRaycastPayload
        {
            IsOn = true,
            callback = SetMovingPosition,
        });
    }
    protected override void SetMovingPosition()
    {
        base.SetMovingPosition();
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Messenger.Default.Publish(new OnUnitResetTargetPayload()
        {
            Unit = _unitBaseParent,
        });
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        UserMoveUnitToCampingPlace = new UserMoveUnitToCampingPlace(mousePos);
    }
    private void OnUsingSkill(UsingSkillPayload usingSkillPayload)
    {
        _eUserAction = EUserAction.UsingSkill;
        SkillDataSO skillConfig = _skillDataConfig.GetSkillDataById(ActiveSkillId);
        UserUsingHeroSkill = new UserUsingHeroSkill(ActiveSkillId, skillConfig);
    }
}
