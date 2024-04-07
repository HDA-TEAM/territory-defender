using UnityEngine;

namespace GamePlay.Scripts.GamePlay
{
    public abstract class GamePlaySingletonBase<T> : SingletonBase<T> where T : MonoBehaviour
    {
        public abstract void SetUpNewGame();
        public abstract void ResetGame();
    }
}
