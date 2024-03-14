using Cysharp.Threading.Tasks;
using SuperMaxim.Messaging;
using System;
using UnityEngine;

public struct UnitRevivePayload
{
    public UnitBase UnitBase;
}
public class HeroRevive : MonoBehaviour
{
    [SerializeField] private UnitBase _hero;
    [SerializeField] private float _cooldownRevive;
    // private float _curCooldown;
    private void Awake()
    {
        Messenger.Default.Subscribe<UnitRevivePayload>(OnReviveHero);
    }
    private void OnDestroy()
    {
        Messenger.Default.Unsubscribe<UnitRevivePayload>(OnReviveHero);
    }
    private async void OnReviveHero(UnitRevivePayload unitRevivePayload)
    {
        if (unitRevivePayload.UnitBase == _hero)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_cooldownRevive));
            _hero.OnUpdateStats?.Invoke();
            _hero.gameObject.SetActive(true);
        }
    }
}
