using CustomInspector;
using GamePlay.Scripts.Data;
using UnityEngine;

namespace GamePlay.Scripts.GamePlay
{
    public class GameResultHandler : MonoBehaviour
    {
        private readonly CalculateStageSuccessRewarding _calculateStageSuccess = new CalculateStageSuccessRewarding();

        [SerializeField] private StageSuccessPu _stageSuccessPu;
        [SerializeField] private StageFailedPu _stageFailedPu;
        [SerializeField] private InGameInventoryRuntimeData _inventoryRuntimeData;
        [SerializeField] private StageInventoryConfig _stageInventoryConfig;

        public void ShowStageSuccessPu()
        {
            StageId stageId = InGameStateController.Instance.CurStageId;
            int maxLife = _stageInventoryConfig.GetStageInventory(stageId).MaxLife;
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
