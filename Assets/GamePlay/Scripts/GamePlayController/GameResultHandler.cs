using UnityEngine;

namespace GamePlay.Scripts.GamePlay
{
    public class GameResultHandler : MonoBehaviour
    {
        [SerializeField] private StageSuccessPu _stageSuccessPu;
        [SerializeField] private StageFailedPu _stageFailedPu;
        public StageSuccessPu StageSuccessPu() => _stageSuccessPu;
        public StageFailedPu StageFailedPu() => _stageFailedPu;
    }
}
