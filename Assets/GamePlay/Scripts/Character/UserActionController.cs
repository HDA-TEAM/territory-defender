
using UnityEngine;
using UnityEngine.EventSystems;

public enum UserAction
{
    SetMovingPoint = 1,
    UsingSkill = 2,
}
public class UserActionController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UserActionHandle.Instance.OnCompleteAction(SetMovingPosition);
    }
    private void SetMovingPosition()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
