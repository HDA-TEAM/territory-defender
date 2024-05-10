using CustomInspector;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using GamePlay.Scripts.Menu.ResultPu;
using GamePlay.Scripts.Stage;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Scripts.GamePlayController
{
    public partial class InGameStateController : GamePlaySingletonBase<InGameStateController>
    {
#if UNITY_EDITOR
        [Button("CheckingStageSuccess")]
        [Button("CheckingEndGame", usePropertyAsParameter: true)]
        [SerializeField] private int _lifeTest;
        [SerializeField] private bool _isFastSetupStageForTest;
#endif

        [FormerlySerializedAs("_inventoryRuntimeData")]
        [Header("Data"), Space(12)] [SerializeField]
        private InGameResourceRuntimeData _resourceRuntimeData;
        [SerializeField] private GameResultHandler _resultsController;
        [SerializeField] private StageDataConfig _stageDataConfig;
        [SerializeField] private StageEnemySpawningFactory _enemySpawningFactory;
        // Access
        public StageId CurStageId { get; private set; }
        public bool IsGamePlaying { get; private set; }

        private bool IsFinishSpawn;

        protected override void Awake()
        {
            base.Awake();
            _resourceRuntimeData.RegisterLifeChange(OnLifeChange);
        }
        public void Start()
        {
            IsFinishSpawn = false;
            IsGamePlaying = true;

#if UNITY_EDITOR
            if (_isFastSetupStageForTest)
                SetUpTestNewGame(_startStageComposite);
#endif

            _resourceRuntimeData.InitData(_stageDataConfig.GeConfigByKey(_startStageComposite.StageId));
        }
        private void Update()
        {
            if (UnitManager.Instance.IsEmptyActiveEnemy
                && IsGamePlaying
                && IsFinishSpawn)
                CheckingStageSuccess();
        }
        protected override void OnDestroy()
        {
            _resourceRuntimeData.UnRegisterLifeChange(OnLifeChange);
        }
        private void OnLifeChange(int life)
        {
            CheckingEndGame(life);
        }
        // private bool IsStageSuccess()
        // {
        //     return IsFinishSpawn;
        // }
        private void CheckingStageSuccess()
        {
            Debug.Log("End StageSuccess");
            IsGamePlaying = false;
            _resultsController.ShowStageSuccessPu();
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
        public void StartSpawning()
        {
            _enemySpawningFactory.StartSpawning(OnFinishedSpawning);
        }
        private void OnFinishedSpawning()
        {
            IsFinishSpawn = true;
        }
    }
}
