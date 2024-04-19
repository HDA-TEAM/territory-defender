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
        [Button("CheckingStageSuccess")]
        [Button("CheckingEndGame", usePropertyAsParameter: true)]
        [SerializeField] private int _lifeTest;
#endif

        [Header("Data"), Space(12)] [SerializeField]
        private InGameInventoryRuntimeData _inventoryRuntimeData;
        [SerializeField] private GameResultHandler _resultsController;
        [SerializeField] private StageDataConfig _stageDataConfig;
        [SerializeField] private StageEnemySpawningFactory _enemySpawningFactory;

        public bool IsFinishSpawn;

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
            IsFinishSpawn = false;
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
        // private bool IsStageSuccess()
        // {
        //     return IsFinishSpawn;
        // }
        public void CheckingStageSuccess()
        {
            Debug.Log("End StageSuccess");
            IsGamePlaying = false;
            _resultsController.ShowStageSuccessPu();
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
            _enemySpawningFactory.StartSpawning(_startStageComposite.StageId, OnFinishedSpawning);
        }
        private void OnFinishedSpawning()
        {
            IsFinishSpawn = true;
        }
    }
}
