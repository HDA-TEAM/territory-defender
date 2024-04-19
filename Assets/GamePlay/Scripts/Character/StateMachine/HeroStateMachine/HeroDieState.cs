using Common.Scripts;
using GamePlay.Scripts.Character.StateMachine;
using SuperMaxim.Messaging;

public class HeroDieState : CharacterDieState
{

    public HeroDieState(BaseHeroStateMachine currentContext) : base(currentContext)
    {
        
    }
    public override void ExitState()
    {
        Messenger.Default.Publish(new AudioPlayOneShotPayload
        {
            AudioClip = Context.AudioClipDeath,
        });
        
        Context.AnimationController.StopAllClip();
        Context.gameObject.SetActive(false);
        Messenger.Default.Publish(new UnitRevivePayload
        {
            UnitBase = Context.UnitBaseParent()
        });
    }
}
