using DG.Tweening;
using GamePlay.Scripts.Menu;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.UI;

public struct UnitRevivePayload
{
    public UnitBase UnitBase;
}

public struct SelectHeroPayload
{
    public UnitBase UnitBase;
}

public class HeroRevive : MonoBehaviour
{
    [SerializeField] private UnitBase _hero;
    [SerializeField] private float _cooldownRevive;
    [SerializeField] private HeroItemView _heroItemView;
    private bool _isCooldown;
    private void Start()
    {
        Messenger.Default.Subscribe<UnitRevivePayload>(OnReviveHero);
        Messenger.Default.Subscribe<ShowUnitInformationPayload>(OnCheckSelectingHero);
        Messenger.Default.Subscribe<HideUnitInformationPayload>(OnRemoveHeroSelected);
        _heroItemView.Setup(new HeroItemViewComposite
        {
            HeroId = EHeroId.TrungTrac
        }, SelectHero);
    }
    private void OnDestroy()
    {
        Messenger.Default.Unsubscribe<UnitRevivePayload>(OnReviveHero);
        Messenger.Default.Unsubscribe<ShowUnitInformationPayload>(OnCheckSelectingHero);
        Messenger.Default.Unsubscribe<HideUnitInformationPayload>(OnRemoveHeroSelected);
    }
    private void OnRemoveHeroSelected(HideUnitInformationPayload payload)
    {
        _heroItemView.SetHeroSelected(false);
    }
    private void OnCheckSelectingHero(ShowUnitInformationPayload payload)
    {
        if (payload.UnitBase != _hero)
            _heroItemView.SetHeroSelected(false);
    }
    private void SelectHero()
    {
        if (_isCooldown)
            return;

        _heroItemView.SetHeroSelected(true);

        Messenger.Default.Publish(new SelectHeroPayload
        {
            UnitBase = _hero
        });
    }
    private void OnReviveHero(UnitRevivePayload unitRevivePayload)
    {
        if (unitRevivePayload.UnitBase == _hero)
        {
            _isCooldown = true;
            _heroItemView.SetHeroSelected(false);
            _heroItemView.SetCooldownProcessing(_cooldownRevive, OnEndOfRevive);
        }
    }
    private void OnEndOfRevive()
    {
        _isCooldown = false;
        _hero.gameObject.SetActive(true);
        _hero.OnUpdateStats?.Invoke();
    }
}
