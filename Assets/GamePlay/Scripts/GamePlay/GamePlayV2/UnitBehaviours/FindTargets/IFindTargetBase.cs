using GamePlay.Scripts.Unit;
using System.Collections.Generic;
using System.Numerics;

public interface IFindTargetBase
{
    public UnitBase ApplyFindTarget(UnitBase finder, List<UnitBase> unitBases);
}

public class FindNearestTargetInDetectRange : IFindTargetBase
{
    public UnitBase ApplyFindTarget(UnitBase finder, List<UnitBase> unitBases)
    {
        //todo
        //need to define
        return unitBases[0];
    }
}

public interface IAttackBase
{
    void ApplyAttack();
}

public interface IMovingBase
{
    Vector2 ApplyMoving(UnitBase unitBase);
}

public class MovingDestinationGate : IMovingBase
{
    public Vector2 ApplyMoving(UnitBase unitBase)
    { 
        // todo 
        return new Vector2();
    }
}

public class MovingGuardPlace: IMovingBase
{
    public Vector2 ApplyMoving(UnitBase unitBase)
    { 
        // todo 
        return new Vector2();
    }
}

