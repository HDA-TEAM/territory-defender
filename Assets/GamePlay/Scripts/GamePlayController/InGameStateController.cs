using GamePlay.Scripts.GamePlay;
using UnityEngine;

public partial class InGameStateController : GamePlaySingletonBase<InGameStateController>
{
    [Header("Data"), Space(12)] [SerializeField]
    private InGameInventoryRuntimeData _inventoryRuntimeData;
    [SerializeField] private GameResultHandler _resultsController;
    [SerializeField] private StageInventoryConfig _stageInventoryConfig;
    protected override void Awake()
    {
        base.Awake();
        _inventoryRuntimeData.InitData(_stageInventoryConfig.GetStageInventory(StageId.Chap1Stage0));
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
    private void CheckingEndGame(int life)
    {
        if (life <= 0)
        {
            //todo
            // notify game ended 
            // show results
            Debug.Log("End game");
            _resultsController.StageFailedPu().gameObject.SetActive(true);
        }
    }
    public void CheckingStageSuccess(int enemyDie = 1)
    {
        _totalEnemySpawning -= enemyDie;
        if (IsStageSuccess())
        {
            Debug.Log("End StageSuccess");
            _resultsController.StageSuccessPu().gameObject.SetActive(true);
        }
    }
    
}
