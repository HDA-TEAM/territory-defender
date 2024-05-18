using Common.Loading.Scripts;
using SuperMaxim.Messaging;
using UnityEngine;

namespace GamePlay.Scripts.GamePlay
{
    public abstract class GamePlaySingletonBase<T> : SingletonBase<T> where T : MonoBehaviour
    {
        public abstract void SetUpNewGame(StartStageComposite startStageComposite);
        public abstract void ResetGame();
    }

    public struct SetUpNewGamePayload
    {
        public StartStageComposite StartStageComposite;
    }

    public struct ResetGamePayload
    {
    }

    public abstract class GamePlayMainFlowBase : MonoBehaviour
    {
        protected virtual void Awake()
        {
            Messenger.Default.Subscribe<SetUpNewGamePayload>(OnSetupNewGame);
            Messenger.Default.Subscribe<ResetGamePayload>(OnResetGame);
        }
        protected virtual void OnDestroy()
        {
            Messenger.Default.Unsubscribe<SetUpNewGamePayload>(OnSetupNewGame);
            Messenger.Default.Unsubscribe<ResetGamePayload>(OnResetGame);
        }
        protected abstract void OnSetupNewGame(SetUpNewGamePayload setUpNewGamePayload);
        protected abstract void OnResetGame(ResetGamePayload resetGamePayload);
    }
}
