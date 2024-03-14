using SuperMaxim.Messaging;
using UnityEngine;

public class HeroDieState : CharacterDieState
{

    public HeroDieState(BaseHeroStateMachine currentContext) : base(currentContext)
    {
        
    }
    public override void ExitState()
    {
        Context.CharacterAnimator.SetBool("IsDie",false);
        Context.gameObject.SetActive(false);
        Messenger.Default.Publish(new UnitRevivePayload
        {
            UnitBase = Context.UnitBaseParent()
        });
    }
}
