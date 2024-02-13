
using UnityEngine;
using UnityEngine.EventSystems;

public enum UserAction
{
    None = 0,
    SetMovingPoint = 1,
    UsingSkill = 2,
}
public class UserActionController : UnitBaseComponent, IPointerClickHandler
{
    [SerializeField] private UserAction _userAction;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        UserActionHandle.Instance.OnCompleteAction(SetMovingPosition);
    }
    private void SetMovingPosition()
    {
        _userAction = UserAction.SetMovingPoint;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("SetMovingPosition"  +"mousePos");
    }
    private void SetFinishedUserAction()
    {
        _userAction = UserAction.None;

    }
    public bool IsInAction()
    {
        return _userAction != UserAction.None;
    }
}
