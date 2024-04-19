using CustomInspector;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using GamePlay.Scripts.Menu.ResultPu;
using UnityEngine;

namespace GamePlay.Scripts.GamePlayController
{
    public partial class InGameStateController : GamePlaySingletonBase<InGameStateController>
    {
#if UNITY_EDITOR
        [Button("CheckingStageSuccess", usePropertyAsParameter: true)]
        [SerializeField] private int _enemyDieTest;
        [Button("CheckingEndGame", usePropertyAsParameter: true)]
        [SerializeField] private int _lifeTest;
#endif

        [Header("Data"), Space(12)] [SerializeField]
        private InGameInventoryRuntimeData _inventoryRuntimeData;
        [SerializeField] private GameResultHandler _resultsController;
        [SerializeField] private StageDataConfig _stageDataConfig;
        [SerializeField] private StageEnemySpawningFactory _enemySpawningFactory;

        private bool _isFinishSpawn;
        private int _totalEnemySpawning;

        // Access
        public StageId CurStageId { get; private set; }
        public bool IsGamePlaying { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            CurStageId = StageId.Chap1Stage0;
            _inventoryRuntimeData.InitData(_stageDataConfig.GeConfigByKey(CurStageId));
            _inventoryRuntimeData.RegisterLifeChange(OnLifeChange);
        }
        public void Start()
        {
            _totalEnemySpawning = 0;
            _isFinishSpawn = false;
            IsGamePlaying = true;
        }
        protected override void OnDestroy()
        {
            _inventoryRuntimeData.UnRegisterLifeChange(OnLifeChange);
        }
        private void OnLifeChange(int life)
        {
            CheckingEndGame(life);
        }
        private bool IsStageSuccess()
        {
            return _isFinishSpawn && _totalEnemySpawning <= 0;
        }
        public void CheckingStageSuccess(int enemyDie = 1)
        {
            _totalEnemySpawning -= enemyDie;
            if (IsStageSuccess())
            {
                Debug.Log("End StageSuccess");
                IsGamePlaying = false;
                _resultsController.ShowStageSuccessPu();
            }
        }
        private void CheckingEndGame(int life)
        {
            if (life <= 0)
            {
                //todo
                // notify game ended 
                // show results
                IsGamePlaying = false;
                Debug.Log("Stage Failed");
                _resultsController.ShowStageFailedPu();
            }
        }
        public void StartSpawning()
        {
            var spawningConfig = _enemySpawningFactory.SpawningConfig.FindSpawningConfig(_startStageComposite.StageId);
            _totalEnemySpawning = spawningConfig.GetTotalUnitsSpawning();
            _enemySpawningFactory.StartSpawning(_startStageComposite.StageId, OnFinishedSpawning);
        }
        private void OnFinishedSpawning()
        {
            _isFinishSpawn = true;
        }
    }
}
