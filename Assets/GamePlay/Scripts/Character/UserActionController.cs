
using SuperMaxim.Messaging;
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
    [SerializeField] private EUserAction _eUserAction;
    public EUserAction CurUserAction { get { return _eUserAction; } }
    public UserMovingHero UserMovingHero;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        // UserActionHandle.Instance.OnCompleteAction(SetMovingPosition);
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
        Debug.Log("SetMovingPosition"  + mousePos);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        UserMovingHero = new UserMovingHero(mousePos);
    }
    public void SetFinishedUserAction()
    {
        _eUserAction = EUserAction.None;
    }
    public bool IsInAction()
    {
        return _eUserAction != EUserAction.None;
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