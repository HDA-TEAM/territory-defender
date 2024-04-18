using Common.Loading.Scripts;

namespace GamePlay.Scripts.GamePlayController
{
    public partial class InGameStateController 
    {
        private StartStageComposite _startStageComposite;
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
