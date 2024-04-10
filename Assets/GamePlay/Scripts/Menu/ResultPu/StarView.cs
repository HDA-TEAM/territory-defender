using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StarView : MonoBehaviour
{
    [SerializeField] private Image _iconStar;
    [SerializeField] private Transform _startAnimPos;
    [SerializeField] private float _originRotation;
    [SerializeField] private float _startScale;
    [SerializeField] private float _durationAnimAppearing = 0.5f;
    [SerializeField] private AnimationCurve _animationCurveRotate;
    [SerializeField] private AnimationCurve _animationCurveScale;
    [SerializeField] private ParticleSystem _hitEffect;
    
    //internal
    private Sequence _starAppearingFlow;
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
        _starAppearingFlow = DOTween.Sequence().Pause();

        _hitEffect.Stop();
        _iconStar.transform.position = _startAnimPos.position;
        _iconStar.transform.localScale = Vector3.one * _startScale;

        Tween scaleStarTween = _iconStar.transform.DOScale(Vector3.one, _durationAnimAppearing).SetEase(_animationCurveScale);
        _starAppearingFlow.Append(scaleStarTween);

        Tween movingStarTween = _iconStar.transform.DOMove(transform.position, _durationAnimAppearing);
        _starAppearingFlow.Join(movingStarTween);

        Tween rotateStarTween = DOVirtual.Float(0f, 1f, _durationAnimAppearing, (val) =>
        {
            Quaternion quaternion = Quaternion.Euler(0f, 0, -val * 360f);
            _iconStar.transform.rotation = quaternion;
        }).SetEase(_animationCurveRotate).OnComplete(() =>
        {
            _iconStar.transform.rotation = Quaternion.Euler(0f, 0, _originRotation);
            _hitEffect.Play();
        }).SetUpdate(true);

        _starAppearingFlow.Join(rotateStarTween);

        _starAppearingFlow.Play().SetUpdate(true);
    }
}
