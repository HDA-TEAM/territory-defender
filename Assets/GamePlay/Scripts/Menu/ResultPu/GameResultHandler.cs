using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using UnityEngine;

namespace GamePlay.Scripts.Menu.ResultPu
{
    public class GameResultHandler : MonoBehaviour
    {
        private readonly CalculateStageSuccessRewarding _calculateStageSuccess = new CalculateStageSuccessRewarding();

        [SerializeField] private StageSuccessPu _stageSuccessPu;
        [SerializeField] private StageFailedPu _stageFailedPu;
        [SerializeField] private InGameInventoryRuntimeData _inventoryRuntimeData;
        [SerializeField] private StageDataConfig _stageDataConfig;

        public void ShowStageSuccessPu()
        {
            StageId stageId = GamePlayController.InGameStateController.Instance.CurStageId;
            int maxLife = _stageDataConfig.GeConfigByKey(stageId).MaxHealth;
            int curLife = _inventoryRuntimeData.GetLifeValue();
            _stageSuccessPu.gameObject.SetActive(true);

            int claimStarsCount = _calculateStageSuccess.GetStarsRewarding(maxLife, curLife);
            _stageSuccessPu.SetupData(claimStarsCount);
        }
        public void ShowStageFailedPu()
        { 
            _stageFailedPu.gameObject.SetActive(true);
        }
    }
}
