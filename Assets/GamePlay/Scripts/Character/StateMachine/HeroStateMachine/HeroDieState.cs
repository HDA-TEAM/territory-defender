using UnityEngine;

public class HeroDieState : CharacterDieState
{

    public HeroDieState(BaseHeroStateMachine currentContext, CharacterStateFactory characterStateFactory) : base(currentContext, characterStateFactory)
    {
    }
}
