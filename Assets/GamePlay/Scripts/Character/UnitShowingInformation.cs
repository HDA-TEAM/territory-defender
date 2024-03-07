using SuperMaxim.Messaging;
using UnityEngine.EventSystems;

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
            StatsData = _unitBaseParent.UnitStatsHandlerComp().GetBaseStats(),
        });
    }
}
