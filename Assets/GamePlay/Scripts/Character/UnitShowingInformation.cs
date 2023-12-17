using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UnitShowingInformation : UnitBaseComponent, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ShowUnitInformation();
    }
    public void ShowUnitInformation()
    {
        Messenger.Default.Publish(new ShowUnitInformationPayload
        {
            StatsData = _unitBaseParent.UnitStatsComp()
        });
    }
}
