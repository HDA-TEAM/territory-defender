using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;

public enum CharacterState
{
    Idle = 0,
    Moving = 1,
    Attacking = 2,
    Guarding = 3,
    TakingDame = 4,
    Die = 5,
    Stun = 6,
    Slow = 7
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
        _states[CharacterState.Die] = new EnemyDieState(_context, this);
        _states[CharacterState.Moving] = new EnemyMovingState(_context, this);
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
        _states[CharacterState.Die] = new HeroIdleState(_context, this);
        _states[CharacterState.Attacking] = new HeroAttackState(_context, this);
        _states[CharacterState.Idle] = new HeroIdleState(_context, this);
        _states[CharacterState.Moving] = new HeroDieState(_context, this);
    }
    
    public CharacterBaseState GetState(CharacterState characterState)
    {
        return _states[characterState];
    }
    
}

    