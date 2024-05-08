using Common.Scripts.Navigator;
using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using UnityEngine;

namespace GamePlay.Scripts.Menu.ResultPu
{
    public class GameResultHandler : MonoBehaviour
    {
        private readonly CalculateStageSuccessRewarding _calculateStageSuccess = new CalculateStageSuccessRewarding();

        [SerializeField] private InGameInventoryRuntimeData _inventoryRuntimeData;
        [SerializeField] private StageDataConfig _stageDataConfig;

        private int _curClaimStarsCount;
        public void ShowStageSuccessPu()
        {
            StageId stageId = GamePlayController.InGameStateController.Instance.CurStageId;
            int maxLife = _stageDataConfig.GeConfigByKey(stageId).MaxHealth;
            int curLife = _inventoryRuntimeData.GetLifeValue();

            _curClaimStarsCount = _calculateStageSuccess.GetStarsRewarding(maxLife, curLife);
            
            
            NavigatorController.MainModalContainer.Push<StageSuccessPu>(
                ResourceKey.InGame.StageSuccessPu, 
                playAnimation: true,
                onLoad: OnLoad);
        }
        private void OnLoad((string modalId, StageSuccessPu modal) obj)
        {
            obj.modal.SetupData(_curClaimStarsCount);
        }
        public void ShowStageFailedPu()
        {
            NavigatorController.MainModalContainer.Push<StageFailedPu>(ResourceKey.InGame.StageFailPu, playAnimation: true);
        }
    }
}
