using System;
using UnityEngine;

public class UnitDirection : UnitBaseComponent
{
    [SerializeField] private Transform _transform;
    private Vector3 _prePos;
    private Vector3 _curPos;

    private bool _isFocusTarget;
    private UnitBase _target;
    private float _localScaleX;
    
    #region Core
    private void Start() => _localScaleX = _transform.localScale.x;
    private void OnEnable()
    {
        _unitBaseParent.OnTargetChanging += OnTargetChanging;
    }
    private void OnDisable()
    {
        _unitBaseParent.OnTargetChanging -= OnTargetChanging;
    }
    #endregion

    #region Data update
    private void Update()
    {
        bool isTurnOnFlipX;
        _curPos = this.gameObject.transform.position;
        if (_target)
        {
            isTurnOnFlipX = CheckLeftToRightDirection(_curPos,_target.transform.position);
        }
        else
        {
            isTurnOnFlipX = CheckLeftToRightDirection(_prePos,_curPos);
        }
        var localScale = _transform.localScale;
        _transform.localScale = isTurnOnFlipX ? new Vector3(_localScaleX, localScale.y, localScale.z) : new Vector3(-_localScaleX, localScale.y, localScale.z);
        _prePos = _curPos;  
    }
    private void OnTargetChanging(UnitBase target) => _target = target;
    #endregion
    #region Logic direction
    private bool CheckLeftToRightDirection(Vector3 posA, Vector3 posB)
    {
        Vector3 direction = (posB - posA).normalized;
        return direction.x > 0;
    }
    #endregion
    
}
