using UnityEngine;

public class CameraHorizontalScroll : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _rink;
    [SerializeField] private float _magnitudeUnit;
    private Camera _camera;
    private float _acceptY;
    private Vector3 _prevMousePos;
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnMouseDown()
    {
        _acceptY = _rink.bounds.size.y / 2 - _camera.orthographicSize;
        Vector3 curPos = Input.mousePosition;
        _prevMousePos = curPos;
    }

    private void OnMouseDrag()
    {
        Vector3 curPos = Input.mousePosition;

        if (curPos == _prevMousePos)
            return;

        Vector3 scrollMagnitudes = CalculateScrollMagnitudes(_prevMousePos, curPos);
        if (IsInAcceptBound(yPlus: scrollMagnitudes.y))
            _camera.transform.Translate(scrollMagnitudes);
        
        _prevMousePos = curPos;
    }
    
    private bool IsInAcceptBound(float yPlus)
    {
        float yVal = _camera.transform.position.y + yPlus;
        return yVal <= _acceptY - _magnitudeUnit && yVal >= -_acceptY + _magnitudeUnit;
    }

    private Vector3 CalculateScrollMagnitudes(Vector3 prevPos, Vector3 curPos)
    {
        if (prevPos == curPos)
            return new Vector3(0, 0, 0);
        float magnitude = curPos.y > prevPos.y ? _magnitudeUnit : -_magnitudeUnit;
        return new Vector3(0, magnitude, 0);
    }
}
