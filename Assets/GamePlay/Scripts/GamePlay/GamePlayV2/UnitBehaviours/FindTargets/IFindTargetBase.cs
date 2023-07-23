using GamePlay.Scripts.Unit;
using System.Collections.Generic;
using System.Numerics;

public interface IFindTargetBase
{
    public UnitBaseOld ApplyFindTarget(UnitBaseOld finder, List<UnitBaseOld> unitBases);
}

public class FindNearestTargetInDetectRange : IFindTargetBase
{
    public UnitBaseOld ApplyFindTarget(UnitBaseOld finder, List<UnitBaseOld> unitBases)
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
    Vector2 ApplyMoving(UnitBaseOld unitBaseOld);
}

public class MovingDestinationGate : IMovingBase
{
    public Vector2 ApplyMoving(UnitBaseOld unitBaseOld)
    { 
        // todo 
        return new Vector2();
    }
}

public class MovingGuardPlace: IMovingBase
{
    public Vector2 ApplyMoving(UnitBaseOld unitBaseOld)
    { 
        // todo 
        return new Vector2();
    }
}

