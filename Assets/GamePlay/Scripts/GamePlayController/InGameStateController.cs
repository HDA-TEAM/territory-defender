using CustomInspector;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using UnityEngine;

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
    [SerializeField] private StageInventoryConfig _stageInventoryConfig;
    
    // Access
    public StageId CurStageId { get; private set; }
    public bool IsGamePlaying { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        CurStageId = StageId.Chap1Stage0;
        _inventoryRuntimeData.InitData(_stageInventoryConfig.GetStageInventory(CurStageId));
        _inventoryRuntimeData.RegisterLifeChange(OnLifeChange);
    }
    protected override void OnDestroy()
    {
        _inventoryRuntimeData.UnRegisterLifeChange(OnLifeChange);
    }
    private void OnLifeChange(int life)
    {
        CheckingEndGame(life);
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

}
