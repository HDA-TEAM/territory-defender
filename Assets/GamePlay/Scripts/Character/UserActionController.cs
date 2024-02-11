
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
        UserActionHandle.Instance.OnCompleteAction(SetMovingPosition);
    }
    private void SetMovingPosition()
    {
        _userAction = UserAction.SetMovingPoint;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }
    public bool IsInAction()
    {
        return _userAction != UserAction.None;
    }
}
