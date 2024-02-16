using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;

public enum CharacterState
{
    Idle = 0,
    Moving = 1,
    Approaching = 2,
    Attacking = 3,
    Guarding = 4,
    TakingDame = 5,
    Die = 6,
    Stun = 7,
    Slow = 8,
    UsingSkill = 9,
}

public class CharacterStateFactory
{
    private CharacterStateMachine _context;
    private Dictionary<CharacterState, CharacterBaseState> _states = new Dictionary<CharacterState, CharacterBaseState>();

    // public CharacterStateFactory(CharacterStateMachine currentContext)
    // { 
    //     _context = currentContext;
    //     _states[CharacterState.Idle] = new CharacterIdleState(_context, this);
    //     _states[CharacterState.Moving] = new CharacterIdleState(_context, this);
    // }
    //
    // public CharacterBaseState GetState(CharacterState characterState)
    // {
    //     return _states[characterState];
    // }

}

public class EnemyStateFactory : CharacterStateFactory
{
    private BaseEnemyStateMachine _context;
    private readonly Dictionary<CharacterState, CharacterBaseState> _states = new Dictionary<CharacterState, CharacterBaseState>();

    public EnemyStateFactory(BaseEnemyStateMachine currentContext)
    {
        _context = currentContext;
        _states[CharacterState.Idle] = new EnemyIdleState(_context);
        _states[CharacterState.Die] = new EnemyDieState(_context);
        _states[CharacterState.Attacking] = new EnemyAttackState(_context);
        _states[CharacterState.Moving] = new EnemyMovingState(_context);
    }

    public CharacterBaseState GetState(CharacterState characterState)
    {
        return _states[characterState];
    }

}

public class HeroStateFactory : CharacterStateFactory
{
    private BaseHeroStateMachine _context;
    private readonly Dictionary<CharacterState, CharacterBaseState> _states = new Dictionary<CharacterState, CharacterBaseState>();

    public HeroStateFactory(BaseHeroStateMachine currentContext)
    {
        _context = currentContext;
        _states[CharacterState.Die] = new HeroDieState(_context);
        _states[CharacterState.Attacking] = new HeroAttackState(_context);
        _states[CharacterState.Idle] = new HeroIdleState(_context);
        _states[CharacterState.Moving] = new HeroMovingState(_context);
        _states[CharacterState.Approaching] = new HeroApproachingState(_context);
        _states[CharacterState.UsingSkill] = new HeroExecuteActiveSkillState(_context);
    }

    public CharacterBaseState GetState(CharacterState characterState)
    {
        return _states[characterState];
    }
}
public class AllyStateFactory : CharacterStateFactory
{
    private BaseAllyStateMachine _context;
    private readonly Dictionary<CharacterState, CharacterBaseState> _states = new Dictionary<CharacterState, CharacterBaseState>();

    public AllyStateFactory(BaseAllyStateMachine currentContext)
    {
        _context = currentContext;
        _states[CharacterState.Die] = new AllyDieState(_context);
        _states[CharacterState.Attacking] = new AllyAttackState(_context);
        _states[CharacterState.Idle] = new AllyIdleState(_context);
        _states[CharacterState.Moving] = new AllyApproachingState(_context);
    }

    public CharacterBaseState GetState(CharacterState characterState)
    {
        return _states[characterState];
    }
}
public class TowerStateFactory : CharacterStateFactory
{
    private BaseTowerStateMachine _context;
    private readonly Dictionary<CharacterState, CharacterBaseState> _states = new Dictionary<CharacterState, CharacterBaseState>();

    public TowerStateFactory(BaseTowerStateMachine currentContext)
    {
        _context = currentContext;
        // _states[CharacterState.Die] = new HeroDieState(_context);
        _states[CharacterState.Attacking] = new TowerAttackState(_context);
        _states[CharacterState.Idle] = new TowerIdleState(_context);
        // _states[CharacterState.Moving] = new HeroMovingState(_context);
    }

    public CharacterBaseState GetState(CharacterState characterState)
    {
        return _states[characterState];
    }
}