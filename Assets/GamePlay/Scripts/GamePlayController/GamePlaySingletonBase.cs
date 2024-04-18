using Common.Loading.Scripts;
using UnityEngine;

namespace GamePlay.Scripts.GamePlay
{
    public abstract class GamePlaySingletonBase<T> : SingletonBase<T> where T : MonoBehaviour
    {
        public abstract void SetUpNewGame(StartStageComposite startStageComposite);
        public abstract void ResetGame();
    }
}
