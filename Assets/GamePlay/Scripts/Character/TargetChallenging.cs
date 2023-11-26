using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetChallenging : UnitBaseComponent
{
    private const int MAX_CHALLENGER = 1;
    private int _challengerCount;
    private bool _isChallenging;

    private List<UnitBase> _challengers;
    private UnitBase _challenger;
    public void CanChallenging(UnitBase unit)
    {
        
        _unitBaseParent.OnTargetChanging?.Invoke(unit);
    }
    public void SetChallenger(UnitBase unit)
    {
        _challenger = unit;
    }
    
    private void Update()
    {
        
        if (_challenger != null && _challenger.gameObject.activeSelf)
        {
            _unitBaseParent.OnTargetChanging?.Invoke(_challenger);
        }
        else
        {
            _unitBaseParent.OnTargetChanging?.Invoke(null);
        }
    }
}
