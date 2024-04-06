using UnityEngine;

public class ClickEffectHandle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _clickingEffect;
    private bool _isOnDown;
    private Camera _camera;
    private void Update()
    {
        switch (Input.anyKeyDown)
        {
            case true when _isOnDown == false:
                _isOnDown = true;
                OnPlayClickEffect();
                break;
            case false: _isOnDown = false;
                break;
        }
    }
    private void OnPlayClickEffect()
    {
        if (!GameController.Instance.IsInGameScene) return;
        var screenPos = Input.mousePosition;
        if (_camera == null)
            _camera = Camera.main;
        var worldPos = _camera.ScreenToWorldPoint(screenPos);
        _clickingEffect.transform.position = worldPos;
        _clickingEffect.Play();
    }
}
