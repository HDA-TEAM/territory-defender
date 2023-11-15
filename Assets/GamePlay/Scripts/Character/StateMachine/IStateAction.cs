namespace GamePlay.Scripts.Character.StateMachine
{
    public interface IStateAction
    {
        public void OnEnter();
        public void OnProcessing();
        public bool OnCompleted();
        public void OnExit();
    }
}
