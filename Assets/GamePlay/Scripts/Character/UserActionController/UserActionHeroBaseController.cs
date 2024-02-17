using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserActionHeroBaseController : UserActionController
{
    public UserMovingHero UserMovingHero;
    public UserUsingHeroSkill UserUsingHeroSkill;

    private void OnEnable()
    {
        Messenger.Default.Subscribe<UsingSkillPayload>(OnUsingSkill);
    }
    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<UsingSkillPayload>(OnUsingSkill); 
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Select Unit");
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
        UnitManager.Instance.ResetTarget(_unitBaseParent);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        UserMovingHero = new UserMovingHero(mousePos);
    }
    private void OnUsingSkill(UsingSkillPayload usingSkillPayload)
    {
        _eUserAction = EUserAction.UsingSkill;
        Debug.Log("Execute Skill");
        SkillDataSO skillConfig = _skillsDataAsset.GetSkillDataById(ESkillId.SummonElephant);
        UserUsingHeroSkill = new UserUsingHeroSkill(ESkillId.SummonElephant, skillConfig);
    }
}
