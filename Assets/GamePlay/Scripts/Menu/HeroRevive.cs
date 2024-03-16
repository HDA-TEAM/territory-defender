using Cysharp.Threading.Tasks;
using DG.Tweening;
using SuperMaxim.Messaging;
using System;
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
    [SerializeField] private Image _imgHeroAvatarCooldown;
    [SerializeField] private Button _btnSelectHero;
    private bool _isCooldown;
    private void Awake()
    {
        Messenger.Default.Subscribe<UnitRevivePayload>(OnReviveHero);
        _btnSelectHero.onClick.AddListener(SelectHero);
    }
    private void OnDestroy()
    {
        Messenger.Default.Unsubscribe<UnitRevivePayload>(OnReviveHero);
    }
    private void SelectHero()
    {
        if (_isCooldown == true)
            return;
        
        Messenger.Default.Publish(new SelectHeroPayload
        {
            UnitBase = _hero
        });
    }
    private void OnReviveHero(UnitRevivePayload unitRevivePayload)
    {
        if (unitRevivePayload.UnitBase == _hero)
        {
            _imgHeroAvatarCooldown.fillAmount = 1f;
            _imgHeroAvatarCooldown.raycastTarget = false;
            _isCooldown = true;
            _imgHeroAvatarCooldown.DOFillAmount(0f,_cooldownRevive).OnComplete(
                () =>
                {
                    _isCooldown = false;
                    _hero.OnUpdateStats?.Invoke();
                    _hero.gameObject.SetActive(true);
                    _imgHeroAvatarCooldown.raycastTarget = true;
                });
        }
    }
}
