using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EUserAction
{
    None = 0,
    SetMovingPoint = 1,
    UsingSkill = 2,
}
public class UserActionController : UnitBaseComponent, IPointerClickHandler
{
    [SerializeField] private EUserAction _eUserAction;
    public EUserAction CurUserAction { get { return _eUserAction; } }
    public UserMovingHero UserMovingHero;
    public UserUsingHeroSkill UserUsingHeroSkill;
    [SerializeField] private SkillsDataAsset _skillsDataAsset;

    private void OnEnable()
    {
        Messenger.Default.Subscribe<UsingSkillPayload>(OnUsingSkill);
    }
    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<UsingSkillPayload>(OnUsingSkill); 
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Select hero");
        Messenger.Default.Publish(new HandleCancelRaycastPayload
        {
            IsOn = true,
            callback = SetMovingPosition,
        });
    }
    private void SetMovingPosition()
    {
        _eUserAction = EUserAction.SetMovingPoint;
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
    public void SetFinishedUserAction()
    {
        _eUserAction = EUserAction.None;
    }
    public bool IsInAction()
    {
        return _eUserAction != EUserAction.None;
    }
    public bool IsUserActionBlocked()
    {
        return _eUserAction == EUserAction.SetMovingPoint;
    }
}

public class UserAction
{
    
}
public class UserMovingHero : UserAction
{
    public Vector3 DesPos;
    public UserMovingHero(Vector3 des)
    {
        DesPos = des;
    }
}

public class UserUsingHeroSkill : UserAction
{
    public ESkillId SkillId;
    public SkillDataSO SkillConfig;
    public UserUsingHeroSkill(ESkillId eSkillId, SkillDataSO inGameSkillBase)
    {
        SkillId = eSkillId;
        SkillConfig = inGameSkillBase;
    }
}