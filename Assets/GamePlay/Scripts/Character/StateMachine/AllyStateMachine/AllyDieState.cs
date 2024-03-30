using GamePlay.Scripts.Character.StateMachine;

public class AllyDieState : CharacterDieState
{
    private readonly BaseAllyStateMachine _context;
    public AllyDieState(BaseAllyStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
}
