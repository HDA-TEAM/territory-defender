using System;
using UnityEngine;

public class UnitDirection : UnitBaseComponent
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Vector3 _prePos;
    private Vector3 _curPos;

    private bool _isFocusTarget;
    private UnitBase _target;
    
    #region Core
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
            isTurnOnFlipX = !CheckLeftToRightDirection(_curPos,_target.transform.position);
        }
        else
        {
            isTurnOnFlipX = !CheckLeftToRightDirection(_prePos,_curPos);
        }
        _spriteRenderer.flipX = isTurnOnFlipX;
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
