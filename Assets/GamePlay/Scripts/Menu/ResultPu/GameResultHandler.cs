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

        public async void ShowStageSuccessPu()
        {
            StageId stageId = GamePlayController.InGameStateController.Instance.CurStageId;
            int maxLife = _stageDataConfig.GeConfigByKey(stageId).MaxHealth;
            int curLife = _inventoryRuntimeData.GetLifeValue();

            int curClaimStarsCount = _calculateStageSuccess.GetStarsRewarding(maxLife, curLife);

            StageSuccessPu stageSuccessPu = null;
            await NavigatorController.MainModalContainer.Push<StageSuccessPu>(
                ResourceKey.InGame.StageSuccessPu, 
                playAnimation: true,
                onLoad: x =>
                {
                    stageSuccessPu = x.modal;
                });
            
            if (stageSuccessPu)
                stageSuccessPu.SetupData(curClaimStarsCount);
        }
        public void ShowStageFailedPu()
        {
            NavigatorController.MainModalContainer.Push<StageFailedPu>(ResourceKey.InGame.StageFailPu, playAnimation: true);
        }
    }
}
