using SuperMaxim.Messaging;
using UnityEngine.EventSystems;

public class UnitShowingInformation : UnitBaseComponent, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ShowUnitInformation();
    }
    private void OnEnable()
    {
        Messenger.Default.Subscribe<SelectHeroPayload>(OnSelectHero);
    }
    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<SelectHeroPayload>(OnSelectHero);
    }
    private void OnSelectHero(SelectHeroPayload payload)
    {
        if (payload.UnitBase != _unitBaseParent)
            return;
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
