using Common.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CampingRoomController
{
    
    private static readonly List<CampingTargetDetecting> _campingRooms = new List<CampingTargetDetecting>();
    // private static List<List<CampingTargetDetecting>> _campingRoomGroups = new List<List<CampingTargetDetecting>>();
    public void OnRegister(CampingTargetDetecting campingTargetDetecting)
    {
        if (!_campingRooms.Contains(campingTargetDetecting))
            _campingRooms.Add(campingTargetDetecting);
    }
    public void OnUnRegister(CampingTargetDetecting campingTargetDetecting)
    {
        if (_campingRooms.Contains(campingTargetDetecting))
            _campingRooms.Remove(campingTargetDetecting);
    }

    public void FindTarget()
    {
        foreach (var campingRoom in _campingRooms)
        {
            campingRoom.FindTarget();
        }
    }

}

// public class Room
// {
//     
//     private Dictionary<int,UnitBase> _unitAllies = new Dictionary<int, UnitBase>();
//     private Dictionary<int,UnitBase> _unitEnemies = new Dictionary<int, UnitBase>();
//
//     public Room(UnitBase a, UnitBase b)
//     {
//         _unitAllies.Add(a.GetInstanceID(), a);
//         _unitEnemies.Add(b.GetInstanceID(), b);
//     }
//     public bool IsEmpty() => _unitAllies.Count <= 0 && _unitEnemies.Count <= 0;
//     public bool CheckInRoom(UnitBase unit) => _unitAllies.ContainsKey(unit.GetInstanceID()) || _unitEnemies.ContainsKey(unit.GetInstanceID());
//     
//     private bool CheckRoomAvailable() => _unitAllies.Count > 0 && _unitEnemies.Count > 0;
//     public void JoinRoom(UnitBase unitBase)
//     {
//         if (unitBase.tag == UnitConstant.AllyTag)
//             _unitAllies.Add(unitBase.GetInstanceID(), unitBase);
//         else if (unitBase.tag == UnitConstant.EnemyTag)
//             _unitEnemies.Add(unitBase.GetInstanceID(), unitBase);
//         unitBase.OnOutOfHeal += OnOutOfHeal;
//     }
//     
//     private void OutRoom(UnitBase unitBase)
//     {
//         _unitAllies.Remove(unitBase.GetInstanceID());
//         _unitEnemies.Remove(unitBase.GetInstanceID());
//         
//         unitBase.OnOutOfHeal -= OnOutOfHeal;
//         
//         FindTarget();
//     }
//     private void OnOutOfHeal(UnitBase unitWilOut)
//     {
//         foreach (var unitA in _unitAllies)
//             if (unitA.Value.CurrentTarget == unitWilOut)
//                 unitA.Value.CurrentTarget = null;
//         OutRoom(unitWilOut);
//     }
//
//     private void FindTarget()
//     {
//         if (_unitAllies.Count < _unitEnemies.Count)
//         {
//             foreach (var unitA in _unitAllies)
//             {
//             
//             }
//         }
//     }
// }
//
// public interface IRoomListener
// {
//     public void Update();
// }
//
// public class OutRoomListener : IRoomListener
// {
//     public void Update()
//     {
//         
//     }
// }
