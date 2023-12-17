using System.Collections.Generic;
using UnityEngine;

public class SingleTargetController
{
    private static readonly List<TargetDetecting> _targetDetectings = new List<TargetDetecting>();
    // private static List<List<CampingTargetDetecting>> _campingRoomGroups = new List<List<CampingTargetDetecting>>();
    public void OnRegister(TargetDetecting targetDetecting)
    {
        if (!_targetDetectings.Contains(targetDetecting))
            _targetDetectings.Add(targetDetecting);

    }
    public void OnUnRegister(TargetDetecting targetDetecting)
    {
        if (_targetDetectings.Contains(targetDetecting))
            _targetDetectings.Remove(targetDetecting);
    }

    public void FindTarget()
    {
        foreach (var campingRoom in _targetDetectings)
        {
            campingRoom.FindTarget();
        }
    }
}
