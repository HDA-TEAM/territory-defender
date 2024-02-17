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
    [SerializeField] protected EUserAction _eUserAction;
    [SerializeField] protected SkillsDataAsset _skillsDataAsset;
    public EUserAction CurUserAction { get { return _eUserAction; } }
    
    public virtual void OnPointerClick(PointerEventData eventData)
    {
    }
    protected virtual void SetMovingPosition()
    {
        _eUserAction = EUserAction.SetMovingPoint;
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