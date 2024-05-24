using Common.Scripts;
using CustomInspector;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using GamePlay.Scripts.Menu.ResultPu;
using GamePlay.Scripts.Stage;
using SuperMaxim.Messaging;
using UnityEngine;

namespace GamePlay.Scripts.GamePlayController
{
    public partial class InGameStateController : GamePlayMainFlowBase
    {
#if UNITY_EDITOR
        [Button("CheckingStageSuccess")]
        [Button("CheckingEndGame", usePropertyAsParameter: true)]
        [SerializeField] private int _lifeTest;
        [SerializeField] private bool _isFastSetupStageForTest;
#endif

        [Header("Data"), Space(12)] [SerializeField]
        private InGameResourceRuntimeData _resourceRuntimeData;
        [SerializeField] private GameResultHandler _resultsController;
        [SerializeField] private StageDataConfig _stageDataConfig;
        [SerializeField] private StageEnemySpawningFactory _enemySpawningFactory;
        [SerializeField] private UnitManager _unitManager;
        // Access
        public bool IsGamePlaying { get; private set; }
        private bool IsFinishSpawn;

        protected override void Awake()
        {
            base.Awake();
            _resourceRuntimeData.RegisterLifeChange(OnLifeChange);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _resourceRuntimeData.UnRegisterLifeChange(OnLifeChange);
        }
       
        private void Init()
        {
            IsFinishSpawn = false;
            IsGamePlaying = true;

#if UNITY_EDITOR
            if (_isFastSetupStageForTest)
                SetUpTestNewGame(_startStageComposite);
#endif
            StageConfig stageConfig = _stageDataConfig.GeConfigByKey(_startStageComposite.StageId);
            _resourceRuntimeData.InitData(stageConfig);

            Messenger.Default.Publish(new StageStartPayload
            {
                StageId = _startStageComposite.StageId,
            });
        }
        private void Update()
        {
            if (_unitManager.IsEmptyActiveEnemy
                && IsGamePlaying
                && IsFinishSpawn)
                CheckingStageSuccess();
        }
        private void OnLifeChange(int life)
        {
            CheckingEndGame(life);
        }
        private void CheckingStageSuccess()
        {
            Debug.Log("End StageSuccess");
            IsGamePlaying = false;
            _resultsController.ShowStageSuccessPu(_startStageComposite);
        }
        private void CheckingEndGame(int life)
        {
            if (life <= 0 && IsGamePlaying)
            {
                //todo
                // notify game ended 
                // show results
                IsGamePlaying = false;
                Debug.Log("Stage Failed");
                _resultsController.ShowStageFailedPu();
            }
        }
        private void StartSpawning()
        {
            _enemySpawningFactory.StartSpawning(OnFinishedSpawning);
        }
        private void OnFinishedSpawning()
        {
            IsFinishSpawn = true;
        }
    }
}
