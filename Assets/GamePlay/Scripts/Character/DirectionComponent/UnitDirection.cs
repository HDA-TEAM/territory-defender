using UnityEngine;

public class UnitDirection : UnitBaseComponent
{
    [SerializeField] private Transform _transform;
    private Vector3 _prePos;
    private Vector3 _curPos;
    private bool _isFocusTarget;
    private UnitBase _target;
    private float _localScaleX;

    private bool _isLeftToRightDirection;
    
    #region Core
    private void Start()
    {
        _localScaleX = _transform.localScale.x;
    } 
    // private void OnEnable()
    // {
    //     _unitBaseParent.OnTargetChanging += OnTargetChanging;
    // }
    // private void OnDisable()
    // {
    //     _unitBaseParent.OnTargetChanging -= OnTargetChanging;
    // }
    #endregion
    #region Update data
    private void Update()
    {
        bool isLeftToRightDirection;
        _curPos = gameObject.transform.position;
        if (_target)
        {
            isLeftToRightDirection = CheckLeftToRightDirection(_curPos,_target.transform.position);
        }
        else
        {
            isLeftToRightDirection = CheckLeftToRightDirection(_prePos,_curPos);
        }
        
        _prePos = _curPos;
        
        if (isLeftToRightDirection == _isLeftToRightDirection )
            return;
        var localScale = _transform.localScale;
        _transform.localScale = isLeftToRightDirection ? new Vector3(_localScaleX, localScale.y, localScale.z) : new Vector3(-_localScaleX, localScale.y, localScale.z);
        _isLeftToRightDirection = isLeftToRightDirection;
    }
    private void OnTargetChanging(UnitBase target) => _target = target;
    #endregion
    #region Logic direction
    private bool CheckLeftToRightDirection(Vector3 posA, Vector3 posB)
    {
        Vector3 direction = (posB - posA).normalized;
        return direction.x >= 0;
    }
    #endregion
}
