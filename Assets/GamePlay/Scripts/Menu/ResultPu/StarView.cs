using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StarView : MonoBehaviour
{
    [SerializeField] private Image _iconStar;
    [SerializeField] private Transform _startAnimPos;
    [SerializeField] private float _originRotation;
    [SerializeField] private float _startScale;
    [SerializeField] private float _durationRotate;
    [SerializeField] private AnimationCurve _animationCurveRotate;
    [SerializeField] private AnimationCurve _animationCurveScale;
    [SerializeField] private ParticleSystem _hitEffect;
    public async void SetIconStar(bool isActive, float delay)
    {
        if (!isActive)
        {
            _iconStar.gameObject.SetActive(isActive);
            return;
        }
        
        await UniTask.Delay(TimeSpan.FromSeconds(delay));
        _iconStar.gameObject.SetActive(isActive);
        PlayAnimation();
    }
    private void PlayAnimation()
    {
        Sequence starAppearingFlow = DOTween.Sequence();
        _hitEffect.Stop();
        _iconStar.transform.position = _startAnimPos.position;
        _iconStar.transform.localScale = Vector3.one * _startScale;
        
        _iconStar.transform.DOScale(Vector3.one, _durationRotate).SetEase(_animationCurveScale);
        _iconStar.transform.DOMove(transform.position, _durationRotate);
        DOVirtual.Float(0f, 1f, _durationRotate, (val) =>
        {
            Quaternion quaternion = Quaternion.Euler(0f, 0, -val * 360f);
            _iconStar.transform.rotation = quaternion;
        }).SetEase(_animationCurveRotate).OnComplete(() =>
        {
            _iconStar.transform.rotation = Quaternion.Euler(0f, 0, _originRotation);
            _hitEffect.Play();
        });
    }
}
