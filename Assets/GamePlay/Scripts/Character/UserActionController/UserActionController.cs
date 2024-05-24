using Common.Scripts.Data;
using Common.Scripts.Data.DataConfig;
using GamePlay.Scripts.GamePlayController;
using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public enum EUserAction
{
    None = 0,
    SetMovingPoint = 1,
    UsingSkill = 2,
}
public class UserActionController : UnitBaseComponent, IPointerClickHandler
{
    [SerializeField] protected EUserAction _eUserAction;
    [FormerlySerializedAs("_skillsDataConfig")]
    [FormerlySerializedAs("_skillsDataAsset")]
    [SerializeField] protected SkillDataConfig _skillDataConfig;
    public EUserAction CurUserAction { get { return _eUserAction; } }
    public UserMoveUnitToCampingPlace UserMoveUnitToCampingPlace;
    
    public virtual void OnPointerClick(PointerEventData eventData)
    {
    }
    protected virtual void SetMovingPosition()
    {
        _eUserAction = EUserAction.SetMovingPoint;
    }
    public virtual void SetMovingPosition(Vector3 des)
    {
        _eUserAction = EUserAction.SetMovingPoint;
        Messenger.Default.Publish(new OnUnitResetTargetPayload()
        {
            Unit = _unitBaseParent,
        });
        UserMoveUnitToCampingPlace = new UserMoveUnitToCampingPlace(des);
    }
    
    public virtual void SetFinishedUserAction()
    {
        _eUserAction = EUserAction.None;
    }
    public virtual bool IsInAction()
    {
        return _eUserAction != EUserAction.None;
    }
    public virtual bool IsUserActionBlocked()
    {
        return _eUserAction == EUserAction.SetMovingPoint;
    }
}

public class UserAction
{
    
}
public class UserMoveUnitToCampingPlace : UserAction
{
    public Vector3 DesPos;
    public UserMoveUnitToCampingPlace(Vector3 des)
    {
        DesPos = des;
    }
}

public class UserUsingHeroSkill : UserAction
{
    public ESkillId SkillId;
    public readonly SkillDataSO SkillConfig;
    public UserUsingHeroSkill(ESkillId eSkillId, SkillDataSO inGameSkillBase)
    {
        SkillId = eSkillId;
        SkillConfig = inGameSkillBase;
    }
}
