using UnityEngine;

public class UnitDirection : UnitBaseComponent
{
    [SerializeField] private Transform _transform;
    [SerializeField] private bool _isLeftToRightDirection;
    private Vector3 _prePos;
    private Vector3 _curPos;
    private bool _isFocusTarget;
    private UnitBase _target;
    private float _localScaleX;


    #region Core
    private void Start()
    {
        _localScaleX = _transform.localScale.x;
    }
    #endregion
    #region Logic direction
    private void Update()
    {
        _target = _unitBaseParent.CurrentTarget;
        _curPos = gameObject.transform.position;

        // If a target exists, it will face them directly
        bool isLeftToRightDirection = _target
            ? VectorUtility.CheckLeftToRightDirection(_curPos, _target.transform.position)
            : VectorUtility.CheckLeftToRightDirection(_prePos, _curPos);

        _prePos = _curPos;

        // Nothing happen
        if (isLeftToRightDirection == _isLeftToRightDirection)
            return;

        // Set the new direction
        var localScale = _transform.localScale;
        _transform.localScale = isLeftToRightDirection ? new Vector3(_localScaleX, localScale.y, localScale.z) : new Vector3(-_localScaleX, localScale.y, localScale.z);
        _isLeftToRightDirection = isLeftToRightDirection;
    }
    private void OnTargetChanging(UnitBase target) => _target = target;
    #endregion
}
