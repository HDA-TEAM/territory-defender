using Common.Scripts;
using GamePlay.Scripts.GamePlay;
using GamePlay.Scripts.GamePlayController;
using SuperMaxim.Messaging;
using UnityEngine;

namespace GamePlay.Scripts.Menu.InGameStageScreen
{
    public class SpawningHeroAtStart : GamePlayMainFlowBase
    {
        private UnitId.Hero _heroId;
        protected override void OnSetupNewGame(SetUpNewGamePayload setUpNewGamePayload)
        {
            _heroId = setUpNewGamePayload.StartStageComposite.HeroId;
            Messenger.Default.Publish(new OnSpawnObjectPayload
            {
                ActiveAtSpawning = false,
                ObjectType = _heroId.ToString(),
                InitPosition = Vector3.zero,
                OnSpawned = OnHeroSpawned,
            });
        }
        private void OnHeroSpawned(GameObject objectSpawned)
        {
            UnitBase unitBase = objectSpawned.GetComponent<UnitBase>();
            unitBase.OnUpdateStats?.Invoke();
            
            objectSpawned.SetActive(true);
            
            Messenger.Default.Publish(new HeroSpawnedPayload
            {
                UnitBase = unitBase,
                HeroId = _heroId,
            });
        }
        protected override void OnResetGame(ResetGamePayload resetGamePayload)
        {
        }
    }
}
