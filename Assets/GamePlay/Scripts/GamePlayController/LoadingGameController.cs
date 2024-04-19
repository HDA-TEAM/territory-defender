using Common.Loading.Scripts;
using CustomInspector;
using UnityEngine;

namespace GamePlay.Scripts.GamePlayController
{
    public partial class InGameStateController 
    {
#if UNITY_EDITOR
        public void SetUpTestNewGame(StartStageComposite startStageComposite)
        {
            _startStageComposite = startStageComposite;
            IsGamePlaying = true;
            RouteSetController.Instance.SetUpNewGame(startStageComposite);
            TowerKitSetController.Instance.SetUpNewGame(startStageComposite);
            PoolingController.Instance.SetUpNewGame(startStageComposite);
            _enemySpawningFactory.SetUpNewGame(startStageComposite);
        }
        [Button("SetUpTestNewGame",usePropertyAsParameter: true)]
#endif
       
        [SerializeField] private StartStageComposite _startStageComposite;
        
        public StartStageComposite StartStageComposite
        {
            get
            {
                return _startStageComposite;
            }
        }


        public override void SetUpNewGame(StartStageComposite startStageComposite)
        {
            _startStageComposite = startStageComposite;
            IsGamePlaying = true;
            RouteSetController.Instance.SetUpNewGame(startStageComposite);
            TowerKitSetController.Instance.SetUpNewGame(startStageComposite);
            PoolingController.Instance.SetUpNewGame(startStageComposite);
        }
        public override void ResetGame()
        {
            IsGamePlaying = false;
            _enemySpawningFactory.CancelSpawning();
            // Stop update game first
            UnitManager.Instance.ResetGame();
            // remove all units
            PoolingController.Instance.ResetGame();
            RouteSetController.Instance.ResetGame();
            TowerKitSetController.Instance.ResetGame();
        }
    }
}
